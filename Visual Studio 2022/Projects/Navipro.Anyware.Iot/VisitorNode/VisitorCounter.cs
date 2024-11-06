using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Windows.ApplicationModel.Background;
using Windows.Devices.Bluetooth;
using Windows.Devices.Enumeration;
using Windows.Devices.SerialCommunication;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;

namespace VisitorNode
{
    internal class VisitorCounter
    {

        public bool Start()
        {
            var selector = BluetoothDevice.GetDeviceSelectorFromPairingState(false);

            string[] requestedProperties = { "System.Devices.Aep.DeviceAddress", "System.Devices.Aep.IsConnected", "System.Devices.Aep.IsPresent", "System.Devices.Aep.ContainerId", "System.Devices.Aep.DeviceAddress", "System.Devices.Aep.Manufacturer", "System.Devices.Aep.ModelId", "System.Devices.Aep.ProtocolId", "System.Devices.Aep.SignalStrength" };

            var deviceWatcher = DeviceInformation.CreateWatcher(selector, requestedProperties, DeviceInformationKind.AssociationEndpoint);

            deviceWatcher.Added += DeviceWatcher_Added;
            deviceWatcher.Updated += DeviceWatcher_Updated;
            deviceWatcher.Removed += DeviceWatcher_Removed;

            return true;
        }


        private void DeviceWatcher_Removed(DeviceWatcher sender, DeviceInformationUpdate deviceInfo)
        {
            DoRequest("Device removed: " + deviceInfo.Id);

        }


        private void DeviceWatcher_Updated(DeviceWatcher sender, DeviceInformationUpdate deviceInfo)
        {
            DoRequest("Device updated: " + deviceInfo.Id);

        }

        private void DeviceWatcher_Added(DeviceWatcher sender, DeviceInformation deviceInfo)
        {
            try
            {
                bool isPresent = (bool)deviceInfo.Properties.Single(p => p.Key == "System.Devices.Aep.IsPresent").Value;
                int signalStrenth = 0;
                var rssi = deviceInfo.Properties.Single(d => d.Key == "System.Devices.Aep.SignalStrength").Value;
                if (rssi != null)
                    signalStrenth = int.Parse(rssi.ToString());

                DoRequest("Device added: " + deviceInfo.Id+", "+deviceInfo.Name+", "+isPresent.ToString()+", "+signalStrenth);
            }
            catch(Exception ex)
            {
                DoRequest("Error: " + ex.Message);
            }
        }

        private void DoRequest(string comment)
        {
            var uri = new System.Uri("http://services.workanywhere.se/iot/log.aspx?comment="+comment);
            using (var httpClient = new Windows.Web.Http.HttpClient())
            {
                // Always catch network exceptions for async methods
                try
                {
                    string result = httpClient.GetStringAsync(uri).GetResults();
                }
                catch (Exception ex)
                {
                    // Details in ex.Message and ex.HResult.
                }
            }
        }
    }
}
