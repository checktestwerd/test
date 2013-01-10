using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;

namespace Visualisator
{
    class AP : Vertex, BoardObjects
    {
        private Medium _MEDIUM = null;
        private MAC _address = new MAC();
        private Int32 _BeaconPeriod = 500;
        private static Random rnadomBeacon = new Random();
        public AP(Medium med)
        {
            this._MEDIUM = med;
            this.VColor = Color.YellowGreen;
            
            _BeaconPeriod = rnadomBeacon.Next(100, 500);
            Thread newThread = new Thread(new ThreadStart(SendBeacon));
            newThread.Start();
        }

        public MAC Address
        {
            get { return _address; }
            set { _address = value; }
        }
        public void SendBeacon()
        {
            while (true)
            {
                Packets.Beacon _beac = new Packets.Beacon();
                Console.WriteLine("[" + _address.getMAC() + "]" + " send beacon. each " + _BeaconPeriod + " ms.");
                _beac.BSSID = _address.getMAC();
                _beac.Source = _address.getMAC();
                _beac.SSID = _address.getMAC();
                _beac.Destination = "FF:FF:FF:FF:FF:FF";
                this.SendPacket(_beac);
                Thread.Sleep(_BeaconPeriod);
            }
        }


        private void SendPacket(Packets.Packet pack)
        {
            if (_MEDIUM.MediumClean)
            {

                _MEDIUM.SendData(pack);
            }
        }
    }
}
