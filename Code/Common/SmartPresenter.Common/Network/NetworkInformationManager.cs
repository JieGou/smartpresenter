using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmartPresenter.Common.Network
{
    public static class NetworkInformationManager
    {
        #region Methods

        #region Public Methods

        public static long GetUploadSpeed()
        {
            NetworkInterface interfaces = GetConnectedNetwork();            
            long beginValue = interfaces.GetIPv4Statistics().BytesReceived;
            DateTime beginTime = DateTime.Now;

            Thread.Sleep(1000);
            long endValue = interfaces.GetIPv4Statistics().BytesReceived;
            DateTime endTime = DateTime.Now;

            long recievedBytes = endValue - beginValue;
            double totalSeconds = (endTime - beginTime).TotalSeconds;

            long bytesPerSecond = (long)(recievedBytes / totalSeconds);

            return bytesPerSecond;
        }

        public static long GetDownloadSpeed()
        {
            NetworkInterface interfaces = GetConnectedNetwork();
            long beginValue = interfaces.GetIPv4Statistics().BytesSent;
            DateTime beginTime = DateTime.Now;

            Thread.Sleep(1000);
            long endValue = interfaces.GetIPv4Statistics().BytesSent;
            DateTime endTime = DateTime.Now;

            long recievedBytes = endValue - beginValue;
            double totalSeconds = (endTime - beginTime).TotalSeconds;

            long bytesPerSecond = (long)(recievedBytes / totalSeconds);

            return bytesPerSecond;
        }

        #endregion

        #region Private Methods

        private static NetworkInterface GetConnectedNetwork()
        {
            List<NetworkInterface> connectedNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces().Where(networkInterface => networkInterface.OperationalStatus == OperationalStatus.Up).ToList();
            connectedNetworkInterfaces = connectedNetworkInterfaces.Where(networkInterface => networkInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 ||
                                                                                              networkInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet ||
                                                                                              networkInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet3Megabit).ToList();

            return connectedNetworkInterfaces.FirstOrDefault();
        }

        #endregion

        #endregion
    }
}
