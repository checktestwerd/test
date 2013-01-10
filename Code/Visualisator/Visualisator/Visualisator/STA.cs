using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Collections;

namespace Visualisator
{
    class STA : Vertex, BoardObjects
    {
        private MAC _address = new MAC();
        private Medium _MEDIUM = null;

        private ArrayList _AccessPoint = new ArrayList();

        public STA(Medium med)
        {
            this._MEDIUM = med;
            this.VColor = Color.RoyalBlue;
            Thread newThread = new Thread(new ThreadStart(Listen));
            newThread.Start();
            _address = new MAC();
        }


        public MAC Address
        {
            get { return _address; }
            set { _address = value; }
        }


        public void Listen()
        {
            while (true)
            {
                if (!_MEDIUM.MediumClean)
                {

                    Packets.Packet pack = _MEDIUM.ReceiveData();
                    if(pack != null ){

                        if (pack.GetType() == typeof(Packets.Beacon)){
                            Packets.Beacon bec = (Packets.Beacon)pack;
                            if (!_AccessPoint.Contains(bec.SSID))
                                _AccessPoint.Add(bec.SSID);
                        }
                        else{
                            // Console.WriteLine("[" + _address.getMAC() + "]" + " listening.");
                        }
                    }
                }
                Thread.Sleep(2);
            }
        }

        public ArrayList ScanList()
        {
            return (_AccessPoint);
        }

    }
}
