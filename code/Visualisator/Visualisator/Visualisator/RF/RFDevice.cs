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
    class RFDevice: ISerializable
    {


        public  class ByteArrayComparer : IEqualityComparer
        {
            public int GetHashCode(object obj)
            {
                byte[] arr = ObjectToByteArray(obj);// as byte[];
                int hash = 0;
                foreach (byte b in arr) hash ^= b;
                return hash;
            }
            public new bool Equals(object x, object y)
            {
                byte[] arr1 = ObjectToByteArray(x);// as byte[];
                byte[] arr2 = ObjectToByteArray(y);// as byte[];
                if (arr1.Length != arr2.Length) return false;
                for (int ix = 0; ix < arr1.Length; ++ix)
                    if (arr1[ix] != arr2[ix]) return false;
                return true;
            }

            private byte[] ObjectToByteArray(Object obj)
            {
                if (obj == null)
                    return null;
                BinaryFormatter bf = new BinaryFormatter();
                MemoryStream ms = new MemoryStream();
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        private Double _x;
        private Double _y;
        private Double _z;
        private Color _vColor;
        private Int32 _OperateChannel = 0;
        private String _OperateBand = "";
        private MAC _address = new MAC();

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

        
    }
}
