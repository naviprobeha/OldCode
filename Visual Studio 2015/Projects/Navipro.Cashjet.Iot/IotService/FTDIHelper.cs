using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.SerialCommunication;

namespace IotService
{
    class FTDIHelper
    {
        public async static Task<DeviceInformation> getFTDISerialPort()
        {
            string AqsFilter = SerialDevice.GetDeviceSelector();
            var dis = await DeviceInformation.FindAllAsync(AqsFilter);

            for (int count = 0; count < dis.Count; count++)
            {
                if (dis[count].Id.Contains("FTDI"))
                {
                    return dis[count];
                }
            }
            return null;
        }


        
    }
}
