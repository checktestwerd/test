using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Collections;

namespace Visualisator
{
    [Serializable()]
    class STA : Vertex, IBoardObjects,IRFDevice,ISerializable
    {
        private MAC _address = new MAC();
        private Medium _MEDIUM = null;
        private Boolean _Enabled = true;
        private ArrayList _AccessPoint = new ArrayList();

        public STA(Medium med)
        {
            this._MEDIUM = med;
            this.VColor = Color.RoyalBlue;
            _address = new MAC();
            Enable();
        }

        ~STA()
        {
            _Enabled = false;
            Console.WriteLine("[" + _address.getMAC() + "]" + " Destroyed");
        }

        public void Enable()
        {
            _Enabled = true;
            Thread newThread = new Thread(new ThreadStart(Listen));
            newThread.Start();

        }
        public MAC Address
        {
            get { return _address; }
            set { _address = value; }
        }
        public void Disable()
        {
            _Enabled = false;

        }

        public void Listen()
        {
            while (_Enabled)
            {
                if (!_MEDIUM.MediumClean)
                {

                    Packets.IPacket pack = _MEDIUM.ReceiveData();
                    if(pack != null ){

                        if (pack.GetType() == typeof(Packets.Beacon)){
                            Packets.Beacon bec = (Packets.Beacon)pack;
                            if (!_AccessPoint.Contains(bec.SSID))
                                _AccessPoint.Add(bec.SSID);
                        }
                        else{
                             Console.WriteLine("[" + _address.getMAC() + "]" + " listening.");
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


        public void SendData(Packets.IPacket PacketToSend)
        {
            throw new NotImplementedException();
        }

        public Packets.IPacket ReceiveData(IRFDevice ThisDevice)
        {
            throw new NotImplementedException();
        }
    }
}
