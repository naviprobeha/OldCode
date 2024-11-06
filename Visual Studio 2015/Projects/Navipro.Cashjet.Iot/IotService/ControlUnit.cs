using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.SerialCommunication;
using Windows.Storage.Streams;

namespace IotService
{
    class ControlUnit
    {
        public static async Task<SerialDevice> connect(DeviceInformation deviceInformation)
        {
            SerialDevice serialPort = await SerialDevice.FromIdAsync(deviceInformation.Id);

            //Configure serial settings
            serialPort.WriteTimeout = TimeSpan.FromMilliseconds(1000);    //mS before a time-out occurs when a write operation does not finish (default=InfiniteTimeout).
            serialPort.ReadTimeout = TimeSpan.FromMilliseconds(1000);     //mS before a time-out occurs when a read operation does not finish (default=InfiniteTimeout).
            serialPort.BaudRate = 9600;
            serialPort.Parity = SerialParity.None;
            serialPort.StopBits = SerialStopBitCount.One;
            serialPort.DataBits = 8;

            return serialPort;
        }
        public async static Task<string> checkStatus(DeviceInformation deviceInformation)
        {
            SerialDevice serialDevice = await connect(deviceInformation);
            DataWriter dataWriter = new DataWriter(serialDevice.OutputStream);
            DataReader dataReader = new DataReader(serialDevice.InputStream);

            //writeData(serialDevice, "#!#00E#SIQ#2F"+(char)13+(char)10);
            writeData(dataWriter, "#!#00D#IQ#"+calcLrc("#!#00D#IQ#")+ (char)13 + (char)10);
            string data = await readData(dataReader);

            dataWriter.DetachStream();
            dataReader.DetachStream();

            
            return getField(data, 5, "#");
            
        }

        public async static Task<string> registerReceipt(DeviceInformation deviceInformation, string registeredTransactionNo, string posDeviceId, string userId, string registrationNo, string totalAmount, string returnAmount, string receiptType, string vatText1, string vatText2, string vatText3, string vatText4)
        {
            string receiptTypeText = "normal";
            string modelId = "";
            if (receiptType == "1") receiptTypeText = "kopia";


            SerialDevice serialDevice = await connect(deviceInformation);
            DataWriter dataWriter = new DataWriter(serialDevice.OutputStream);
            DataReader dataReader = new DataReader(serialDevice.InputStream);

            //Check status
            writeData(dataWriter, "#!#00D#IQ#" + calcLrc("#!#00D#IQ#") + (char)13 + (char)10);
            string data = await readData(dataReader);
            modelId = getField(data, 5, "#");

            //Start transaction
            writeData(dataWriter, "#!#00D#ST#" + calcLrc("#!#00D#ST#") + (char)13 + (char)10);

            string response = await readData(dataReader);
            checkNak(response, "Start Trans.");


            //Store receipt
            string command = "#RH#" + DateTime.Now.ToString("yyyyMMddHHmm") + "#" + registeredTransactionNo + "#" + posDeviceId + "#" + userId + "# #" + registrationNo.Replace("-", "") + "#" + totalAmount + "#" + returnAmount + "#" + receiptTypeText + "#" + vatText1 + "#" + vatText2 + "#" + vatText3 + "#" + vatText4 + "#";
            string length = getHexLength("#!#000" + command + "000");

            writeData(dataWriter, "#!#0" + length + command + calcLrc("#!#0" + length + command) + (char)13 + (char)10);


            response = await readData(dataReader);
            checkNak(response, "Store Receipt");

            //Signature
            writeData(dataWriter, "#!#00D#SQ#" + calcLrc("#!#00D#SQ#") + (char)13 + (char)10);

            response = await readData(dataReader);
            checkNak(response, "Signature");
            string signature = getField(response, 5, "#");

            dataWriter.DetachStream();
            dataReader.DetachStream();

            return modelId + ";" + signature;

        }
        public static async void writeData(DataWriter dataWriter, string data)
        {
            
            dataWriter.WriteString(data);

            Task<UInt32> storeAsyncTask = dataWriter.StoreAsync().AsTask();
            await storeAsyncTask;

        }

        public static async Task<string> readData(DataReader dataReader)
        {
            string data = "";

            Task<UInt32> loadAsyncTask = dataReader.LoadAsync(1024).AsTask();

            UInt32 bytesRead = await loadAsyncTask;
            if (bytesRead > 0)
            {
                data = dataReader.ReadString(bytesRead);
            }


            return data;
        }

        public static string calcLrc(string data)
        {
            int lrc = 0;
            foreach (byte b in System.Text.Encoding.ASCII.GetBytes(data))
            {
                lrc = lrc ^ b;
            }

            return lrc.ToString("X");
        }

        public static string getHexLength(string data)
        {
            int length = data.Length;
            return length.ToString("X");
        }

        public static string getField(string data, int fieldNo, string separator)
        {
            int i = 0;
            while (i < fieldNo-1)
            {
                if (data.IndexOf(separator) > -1)
                {
                    data = data.Substring(data.IndexOf(separator) + 1);
                }
                else
                {
                    data = "";
                }
                i++;
            }

            if (data.IndexOf(separator) > -1)
            {
                return data.Substring(0, data.IndexOf(separator));
            }
            return data;
        }

        public static void checkNak(string response, string method)
        {
            if ((response.IndexOf("NACK") == -1) && (response.IndexOf("NAK") == -1)) return;

            string errorCode = getField(response, 5, "#");
            string errorMessage = "";

            if (errorCode == "001") errorMessage = "Fel LRC.";
            if (errorCode == "002") errorMessage = "Okänd meddelandetyp.";
            if (errorCode == "003") errorMessage = "Felaktig parameter.";
            if (errorCode == "004") errorMessage = "Fel anropsordning.";
            if (errorCode == "005") errorMessage = "Fatalt fel.";
            if (errorCode == "006") errorMessage = "Kontrollenheten fungerar inte korrekt.";
            if (errorCode == "007") errorMessage = "Fel kassa-id.";
            if (errorCode == "008") errorMessage = "Internt fel.";
            if (errorCode == "") return;

            throw new Exception("Felmeddelande från kontrollenheten (" + method + ") " + errorMessage);

        }
    }
}
