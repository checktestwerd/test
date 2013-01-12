using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Visualisator.Packets;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
namespace Visualisator
{
    [Serializable()]
    class RFDevice: ISerializable,IRFDevice
    {
        public  String RF_STATUS = "NONE";
        protected Medium _MEDIUM = null;
        protected Boolean _Enabled = true;
        private StringBuilder   _LOG            =   new StringBuilder();
        private Double          _x;
        private Double          _y;
        private Double          _z;
        private Color           _vColor;
        private Int32           _OperateChannel =   0;
        private String          _OperateBand    =   "";
        private MAC             _address        =   new MAC();
        protected ArrayList _AccessPoint = new ArrayList();
        protected Hashtable _AccessPointTimeCounter = new Hashtable(new ByteArrayComparer());
        protected ArrayList _AssociatedWithAPList = new ArrayList();
        protected ArrayList _PointerToAllRfDevices = null;

        public string DumpAll()
        {
            String ret = "";

            ret = ObjectDumper.Dump(this);
            ret += "_OperateChannel\r\n";
            ret += ObjectDumper.Dump(_OperateChannel);
            ret += "_OperateBand\r\n";
            ret += ObjectDumper.Dump(_OperateBand);
            ret += "_address\r\n";
            ret += ObjectDumper.Dump(_address);

            return (ret);
        }
        public MAC getMAC()
        {
            return _address;
        }
        public String getMACAddress()
        {
            return _address.getMAC();
        }

        public void setMAC(MAC _mac)
        {
            _address = _mac;
        }



        public RFDevice(Double x, Double y, Double z)
        {
            this.SetVertex(x,y,z);
        }
        public RFDevice(RFDevice ver)
        {
            this.SetVertex(ver.x, ver.y, ver.z);
        }
        public RFDevice()
        {
            this.SetVertex(0, 0, 0);
        }

        public SimulatorPacket CreatePacket()
        {
            SimulatorPacket pack = new SimulatorPacket(getOperateChannel(), getOperateBand());
            pack.Source = getMACAddress();
            if (this.GetType() == typeof(AP))
            {
                AP _ap = (AP)this;
                pack.SSID = _ap.SSID;
            }
            pack.X = this.x;
            pack.Y = this.y;

            return(pack);
        }

        public void setOperateChannel(int NewChannel)
        {
            _OperateChannel = NewChannel;
            if (NewChannel > 0 && NewChannel < 15)
            {
                setOperateBand("N");
            }
            else
            {
                setOperateBand("A");
            }
        }

        public int getOperateChannel()
        {
            return (_OperateChannel);
        }

        public void setOperateBand(string NewBand)
        {
            _OperateBand = NewBand;
        }

        public string getOperateBand()
        {
            return (_OperateBand);
        }



        public void Enable()
        {
            throw new NotImplementedException();
        }

        public void SendData(SimulatorPacket PacketToSend)
        {
            throw new NotImplementedException();
        }

        public IPacket ReceiveData(IRFDevice ThisDevice)
        {
            throw new NotImplementedException();
        }

        public void Disable()
        {
            throw new NotImplementedException();
        }

        public void RegisterToMedium(int x, int y, int Channel, string Band, int Radius)
        {
            throw new NotImplementedException();
        }


        public void AddToLog(string newLogEntry)
        {
            _LOG.Append( "[" + this.getMACAddress() + "]" + newLogEntry + "\r\n");
        }

        public double x
        {
            get { return _x; }
            set { _x = value; }
        }
        public double y
        {
            get { return _y; }
            set { _y = value; }
        }
        public double z
        {
            get { return _z; }
            set { _z = value; }
        }

        public Color VColor
        {
            get { return _vColor; }
            set { _vColor = value; }
        }

        public void SetVertex(Double x, Double y, Double z)
        {
            _x = x;
            _y = y;
            _z = z;
        }



        public void ParseReceivedPacket(IPacket pack)
        {
            throw new NotImplementedException();
        }
    }
}
