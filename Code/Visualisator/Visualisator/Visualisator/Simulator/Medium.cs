using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;
using System.ComponentModel;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Visualisator.Packets;

namespace Visualisator
{
    [Serializable()]
    class Medium :ISerializable
    {
        [Serializable()]
        class Key
        {
            private String _band;
            private Int32 _channel;
            public Key(String band, Int32 Channel)
            {
                _band = band;
                _channel = Channel;
            }
        }
        [Serializable()]
        class PacketKey
        {
            private String _band;
            private Int32 _channel;
            private Type _type;
            public PacketKey(String band, Int32 Channel, Type _t)
            {
                _band = band;
                _channel = Channel;
                _type = _t;
            }
        }       
        private class ByteArrayComparer : IEqualityComparer {
          public int GetHashCode(object obj) {
              byte[] arr = ObjectToByteArray(obj);// as byte[];
            int hash = 0;
            foreach (byte b in arr) hash ^= b;
            return hash;
          }
          public new bool Equals(object x, object y) {
            byte[] arr1 =ObjectToByteArray(x);// as byte[];
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
        private ArrayList _MBands = new ArrayList();
        private ArrayList _Mfrequency = new ArrayList();
        private ArrayList _MChannels = new ArrayList();
        private Boolean _mediumWork = true;

        private Double _Radius = 25;

        private Hashtable _packets = new Hashtable(new ByteArrayComparer());
        private Boolean _mediumClean = true;
        private Hashtable _T = new Hashtable(new ByteArrayComparer()) ;
        private ArrayList _B = new ArrayList();

        public Boolean Registration(String Band, Int32 Channel, Double x, Double y)
        {
            Key Tk = new Key(Band,Channel);

            try
            {
                if (_T.ContainsKey(Tk))
                {
                    ArrayList _temp = (ArrayList)_T[Tk];
                    if (_temp != null)
                    {

                        foreach (var obj in _temp)
                        {
                            RFDevice _tV = (RFDevice)obj;
                            if (getDistance(x, y, _tV.x, _tV.y) < _Radius + _Radius)
                            {
                                return (false);
                            }
                            //Console.WriteLine(y);
                        }

                        RFDevice ver = new RFDevice(x, y, 0);
                        _temp.Add(ver);
                        _T[Tk] = _temp;
                        Thread newThread = new Thread(() => Unregister(Tk, ver));
                        newThread.Start();
                    }
                    else
                    {
                        ArrayList _tempArrL = new ArrayList();
                        RFDevice ver = new RFDevice(x, y, 0);
                        _tempArrL.Add(ver);
                        _T.Add(Tk, _tempArrL);
                        Thread newThread = new Thread(() => Unregister(Tk, ver));
                        newThread.Start();
                    }
                }
                else
                {
                    ArrayList _tempArrL = new ArrayList();
                    RFDevice ver = new RFDevice(x, y, 0);
                    _tempArrL.Add(ver);
                    _T.Add(Tk, _tempArrL);
                    Thread newThread = new Thread(() => Unregister(Tk, ver));
                    newThread.Start();
                }
            }
            catch (Exception) { return false; }
            return (true);
            //_T.Add(Tk,
        }

        private void Unregister(Key Tk,object rem)
        {
           Thread.Sleep(3);
           ArrayList _temp = (ArrayList)_T[Tk];
         //  try
         //  {
               lock (_T)
               {
                   _temp.Remove((RFDevice)rem);
                   //_temp.
                   if (_temp.Count > 0)
                       _T[Tk] = _temp;
                   else
                       _T.Remove(Tk);
               }
          // }

        }
        private Double getDistance(Double x1, Double y1, Double x2, Double y2)
        {
            Double ret = 0;

            ret = Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));

            return (ret);
        }
        public String getMediumInfo()
        {
            String ret = "";

            ret = ObjectDumper.Dump(this);
            ret += "_packets\r\n";
            ret += ObjectDumper.Dump(_packets);
            ret += "_MChannels\r\n";
            ret += ObjectDumper.Dump(_MChannels);
            ret += "_Mfrequency\r\n";
            ret += ObjectDumper.Dump(_Mfrequency);
            ret += "_MBands\r\n";
            ret += ObjectDumper.Dump(_MBands);

            return (ret);
        }
        public Medium(){
            _MBands.Add("G");
            _MBands.Add("N");
            _MBands.Add("A");

            _Mfrequency.Add(2400);
            _Mfrequency.Add(5000);

            for (int i = 1; i < 15; i++)
            {
                _MChannels.Add(i);
            }
            Enable();
        }

        public void Enable()
        {
            _mediumWork = true;
            Thread newThread = new Thread(new ThreadStart(Run));
            newThread.Start();
        }
        public void Disable()
        {
            _mediumWork = false;
        }

        public void Run() 
        {
            while (_mediumWork)
            {                
                //Console.WriteLine("Running in a different thread.");
                Thread.Sleep(3000);
            }
        }

        public void ClearMedium()
        {

        }

        public Boolean MediumHaveAIRWork(RFDevice device)
        {
                Key Pk = new Key(device.getOperateBand(), device.getOperateChannel());
                if (_packets != null && _packets.ContainsKey(Pk))
                    return true;
                return false;
        }
        public void SendData(SimulatorPacket pack)
        {
            MediumClean = false;
            Key _Pk = new Key(pack.PacketBand, pack.PacketChannel);
            try
            {
                if (pack != null)
                {
                    ArrayList LocalPackets = null;
                    if (_packets.ContainsKey(_Pk))
                    {

                        LocalPackets = (ArrayList)_packets[_Pk];
                        if (LocalPackets == null)
                        {
                            LocalPackets = new ArrayList();
                        }
                        LocalPackets.Add(pack);
                        _packets[_Pk] = LocalPackets;
                    }
                    else
                    {
                        LocalPackets = new ArrayList();
                        LocalPackets.Add(pack);
                        _packets.Add(_Pk, LocalPackets);
                    }

                }
                Thread newThread = new Thread(() => ThreadableSendData(_Pk,pack));
                newThread.Start();
            }
            catch (Exception) { }
        }

        private void ThreadableSendData(Key _Pk,object _ref)
        {
            Thread.Sleep(3);

            ArrayList _temp = (ArrayList)_packets[_Pk];

            lock (_T)
            {
                _temp.Remove((SimulatorPacket)_ref);
                //_temp.
                if (_temp.Count > 0)
                    _packets[_Pk] = _temp;
                else
                    _packets.Remove(_Pk);
            }
            MediumClean = true;

        }
        public IPacket ReceiveData(RFDevice device)
        {
            IPacket ret = null;
            try
            {

                Key Pk = new Key(device.getOperateBand(), device.getOperateChannel());
                if (_packets != null)
                {

                    if (_packets.ContainsKey(Pk))
                    {
                        ArrayList LocalPackets = (ArrayList)_packets[Pk];
                        foreach (object pack in LocalPackets)
                        {
                            SimulatorPacket _LocalPack = (SimulatorPacket)pack;
                            if (getDistance(device.x, device.y, _LocalPack.X, _LocalPack.Y) < _Radius + _Radius)
                            {

                                if (pack != null && typeof(Beacon) == _LocalPack.GetType())
                                {
                                    ret = (IPacket)_LocalPack;
                                    return (_LocalPack);
                                }
                            }
                            // loop body
                        }
                    }
                }
            }
            catch (Exception) { }
            return (ret);
        }

        public Boolean MediumClean
        {
            get { return _mediumClean; }
            set { _mediumClean = value; }
        }

        public Boolean StopMedium
        {
            get { return _mediumWork; }
            set { _mediumWork = value; }
        }


        internal string DumpPackets()
        {
            String ret = "";
            //ret += "_packets\r\n";
            ret += ObjectDumper.Dump(_packets);
            return (ret);
        }
    }
}
