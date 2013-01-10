using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Visualisator.Packets
{
    class Beacon : Packet
    {
        private String _Type = ""; //"Control/managment/Data"
        private String _SubType = ""; //ClearToSend/
        private String _FramControllFlags = ""; // 00000000
        // 1 - Non strich order
        // 2 - Non protective frame
        // 3 - No more data
        // 4 - Power Mangment
        // 5 - This is not retransmision
        // 6 - Last or fragmentet frame
        // 7 - Not and exit from distribution system
        // 8 - not to the distribition system
        private String _Duration = ""; // Microsecond
        private String _Reciver = "00:33:33:22:11:fa";

        private String _Destination = "FF:FF:FF:FF:FF:FF";

        public String Destination
        {
            get { return _Destination; }
            set { _Destination = value; }
        }
        private String _Source = "AA:AA:AA:AA:AA:AA";

        public String Source
        {
            get { return _Source; }
            set { _Source = value; }
        }
        private String _BSSID = "11:22:33:44:55:66";

        public String BSSID
        {
            get { return _BSSID; }
            set { _BSSID = value; }
        }
        private String _SSID = "TRA LA LA";

        public String SSID
        {
            get { return _SSID; }
            set { _SSID = value; }
        }
        private String _MAX_SupportedRate = "54.0 Mbps";
        public Beacon()
        {


        }
    }
}
