using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Runtime;

namespace Navipro.CleanCashWrapper
{
    class Guids
    {
        public const string coclsguid = "b932a976-1e84-48c3-a6d1-b32edb8933bd";
        public const string intfguid = "d61df921-cc50-424f-b203-45a4a4a1b76a";
        public const string eventguid = "09f5fee7-b4ce-4384-b1dc-a7e21acd5440";

        public static readonly System.Guid idcoclass;
        public static readonly System.Guid idintf;
        public static readonly System.Guid idevent;

        static Guids()
        {
            idcoclass = new System.Guid(coclsguid);
            idintf = new System.Guid(intfguid);
            idevent = new System.Guid(eventguid);
        }
    }

    [Guid(Guids.intfguid), InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface ICleanCashWrapper
    {
        [DispId(1)]
        bool init(string comPort, int baudRate);
        [DispId(2)]
        bool storeReceipt(string receiptNo, string posDeviceId, string userId, string registrationNo, string totalAmount, string returnAmount, string receiptType, string vatText1, string vatText2, string vatText3, string vatText4);
        [DispId(3)]
        string getId();
        [DispId(4)]
        string getSignature();

    }

    [Guid(Guids.coclsguid), ProgId("Navipro.CleanCashWrapper"), ClassInterface(ClassInterfaceType.None)]
    public class CleanCashWrapper : ICleanCashWrapper
    {
        private SerialPort serialPort;
        private string id;
        private string signature;

        public CleanCashWrapper()
        {
        }

        public bool init(string comPort, int baudRate)
        {
            signature = "";

            if (serialPort == null) serialPort = new SerialPort(comPort, baudRate, Parity.None, 8);

            if (!serialPort.IsOpen)
            {
                serialPort.Open();
                serialPort.NewLine = "\r";
                serialPort.ReadTimeout = 10000;
                serialPort.WriteTimeout = 10000;

                if (!serialPort.IsOpen)
                {
                    throw new Exception("Kan inte öppna kommunikationen mot kontrollenheten. Port: "+comPort+", hastighet: "+baudRate.ToString());
                }

            }

            serialPort.WriteLine("#!#00D#IQ#"+lrcCalc("#!#00D#IQ#"));

            string response = serialPort.ReadLine();
            if (!checkNak(response, "Identity Request")) return false;

            id = getField(response, 5, "#");
            return true;

        }

        public bool storeReceipt(string receiptNo, string posDeviceId, string userId, string registrationNo, string totalAmount, string returnAmount, string receiptType, string vatText1, string vatText2, string vatText3, string vatText4)
        {
            signature = "";

            serialPort.WriteLine("#!#00D#ST#"+lrcCalc("#!#00D#ST#"));
            string response = serialPort.ReadLine();

            if (!checkNak(response, "Start Trans.")) return false;

            string command = "#RH#"+DateTime.Now.ToString("yyyyMMddHHmm")+"#"+receiptNo+"#"+posDeviceId+"#"+userId+"# #"+registrationNo.Replace("-", "")+"#"+totalAmount+"#"+returnAmount+"#"+receiptType+"#"+vatText1+"#"+vatText2+"#"+vatText3+"#"+vatText4+"#";
            string commandLength = getHexLength("#!#000"+command+"000");

            serialPort.WriteLine("#!#0"+commandLength+command+lrcCalc("#!#0"+commandLength+command));

            response = serialPort.ReadLine();
            if (!checkNak(response, "Store Receipt")) return false;

            serialPort.WriteLine("#!#00D#SQ#"+lrcCalc("#!#00D#SQ#"));

            response = serialPort.ReadLine();
            if (!checkNak(response, "Signature")) return false;

            getField(response, 5, "#");
            return true;
        }

        public string getId()
        {
            return id;
        }

        public string getSignature()
        {
            return signature;
        }

        private string getField(string buffer, int fieldNo, string separator)
        {
            
            int i = 0;
            while (i < fieldNo-1)
            {
                if (buffer.IndexOf(separator) > -1)
                {
                    buffer = buffer.Substring(buffer.IndexOf(separator)+1);
                }
                else
                {   
                    buffer = "";
                }
                i++;
            }

            if (buffer.IndexOf(separator) > -1)
            {
                return buffer.Substring(0, buffer.IndexOf(separator));
            }
            else
            {
                return buffer;
            }

        }

        private bool checkNak(string response, string method)
        {
            if ((response.IndexOf("NACK") == -1) && (response.IndexOf("NAK") == -1)) return true;

            string errorCode = getField(response, 5, "#");

            serialPort.Close();

            string errorMessage = "";
            if (errorCode == "001") errorMessage = "Fel LRC.";
            if (errorCode == "002") errorMessage = "Okänd meddelandetyp.";
            if (errorCode == "003") errorMessage = "Felaktig parameter.";
            if (errorCode == "004") errorMessage = "Fel anropsordning.";
            if (errorCode == "005") errorMessage = "Fatalt fel.";
            if (errorCode == "006") errorMessage = "Kontrollenheten fungerar inte korrekt.";
            if (errorCode == "007") errorMessage = "Fel kassa-id.";
            if (errorCode == "008") errorMessage = "Internt fel.";
            if (errorCode == "") return true;

            throw new Exception("Felmeddelande från kontrollenheten ("+method+"): "+errorMessage);

            return false;

        }


        private string lrcCalc(string data)
        {
            int lrc = 0;
            foreach (byte b in System.Text.Encoding.Default.GetBytes(data))
            {
                lrc = lrc ^ b;
            }

            string lrcStr = lrc.ToString("X");
            if (lrcStr.Length == 1) lrcStr = "0" + lrcStr;
            return lrcStr;
        }

        private static string getHexLength(string data)
        {
            int length = data.Length;
            return length.ToString("X");
        }
    }
}
