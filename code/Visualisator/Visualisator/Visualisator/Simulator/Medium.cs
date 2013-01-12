﻿using System;
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
       

        private ArrayList _MBands = new ArrayList();
        private ArrayList _Mfrequency = new ArrayList();
        private ArrayList _MChannels = new ArrayList();
        private Boolean _mediumWork = true;

        private Int32 _ConnectCounter = 0;
        private Int32 _ConnectAckCounter = 0;

        private Double _Radius = 50;

        private Hashtable _packets = new Hashtable(new ByteArrayComparer());
        private Hashtable _T = new Hashtable(new ByteArrayComparer()) ;
        private ArrayList _B = new ArrayList();


        //*********************************************************************
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
        }

        //*********************************************************************
        private void Unregister(Key Tk,object rem)
        {
           Thread.Sleep(3);
           ArrayList _temp = (ArrayList)_T[Tk];
           try
           {
               lock (_T)
               {
                   _temp.Remove((RFDevice)rem);
                   if (_temp.Count > 0)
                       _T[Tk] = _temp;
                   else
                       _T.Remove(Tk);
               }
           }
           catch (Exception){ }
        }

        //*********************************************************************
        private Double getDistance(Double x1, Double y1, Double x2, Double y2)
        {
            Double ret = 0;
            Double x = (x1 - x2);
            Double y = (y1 - y2);
            ret = Math.Sqrt(x*x + y*y);

            return (ret);
        }
        //*********************************************************************
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
        //*********************************************************************
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

        //*********************************************************************
        public void Enable()
        {
            _mediumWork = true;
            Thread newThread = new Thread(new ThreadStart(Run));
            newThread.Start();
        }
        //*********************************************************************
        public void Disable()
        {
            _mediumWork = false;
        }
        //*********************************************************************
        public void Run() 
        {
            while (_mediumWork)
            {                
                Thread.Sleep(3000);
            }
        }
        //*********************************************************************
        public Boolean MediumHaveAIRWork(RFDevice device)
        {
                Key Pk = new Key(device.getOperateBand(), device.getOperateChannel());
                if (_packets != null && _packets.ContainsKey(Pk))
                    return true;
                return false;
        }


        public Int32 getConnectCounter()
        {
            return (_ConnectCounter);
        }
        public Int32 getConnectAckCounter()
        {
            return (_ConnectAckCounter);
        }
        //*********************************************************************
        public void SendData(SimulatorPacket pack)
        {
            Key _Pk = new Key(pack.PacketBand, pack.PacketChannel);
            try
            {
                if (pack != null)
                {
                    if (pack.GetType() == typeof(Connect))
                    {
                        _ConnectCounter++;

                        if (_ConnectCounter == 36000)
                            _ConnectCounter = 0;
                    }
                    else if (pack.GetType() == typeof(ConnectionACK))
                    {
                        _ConnectAckCounter++;

                        if (_ConnectAckCounter == 36000)
                            _ConnectAckCounter = 0;
                    }
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
        //*********************************************************************
        private void ThreadableSendData(Key _Pk,object _ref)
        {
            ArrayList _temp = (ArrayList)_packets[_Pk];
            Thread.Sleep(3);
            if (_temp != null)
            {
                lock (_packets)
                {
                   // if (_temp != null)
                   // {
                        if (_temp.Contains(_ref))
                            _temp.Remove((SimulatorPacket)_ref);
  
                        if (_temp.Count > 0)
                            _packets[_Pk] = _temp;
                        else
                            _packets.Remove(_Pk);
                   // }
                }
            }
        }
        //*********************************************************************
        public IPacket ReceiveData(RFDevice device)
        {
            try
            {
                if (_packets != null)
                {
                    Key Pk = new Key(device.getOperateBand(), device.getOperateChannel());
                    if (_packets.ContainsKey(Pk))
                    {
                        ArrayList LocalPackets = (ArrayList)_packets[Pk];
                        foreach (object pack in LocalPackets)
                        {
                            if (pack != null)
                            {
                                SimulatorPacket _LocalPack = (SimulatorPacket)pack;
                                Boolean _BROADCAST = false;
                                if (_LocalPack.Destination.Equals("FF:FF:FF:FF:FF:FF"))
                                {
                                    _BROADCAST = true;
                                }

                                if (_LocalPack.Source != device.getMACAddress() &&
                                  (_LocalPack.Destination.Equals(device.getMACAddress()) || _BROADCAST) &&
                                    getDistance(device.x, device.y, _LocalPack.X, _LocalPack.Y) < _Radius + _Radius)
                                {

                                    if (!_BROADCAST)
                                        LocalPackets.Remove(pack);
                                    return (_LocalPack);
                     
                                }
                            }
                            // loop body
                        }
                    }
                }
            }
            catch (Exception) { }
            return (null);
        }

       // public Boolean MediumClean
       // {
       //     get { return _mediumClean; }
      //      set { _mediumClean = value; }
     //   }

        public Boolean StopMedium
        {
            get { return _mediumWork; }
            set { _mediumWork = value; }
        }


        internal string DumpPackets()
        {
            String ret = "";
            //ret += "_packets\r\n";

            foreach (DictionaryEntry p in _packets)
            {
               // ret += ObjectDumper.Dump(p.Key );
                ret += ObjectDumper.Dump(p.Value);
            }
            
            return (ret);
        }
    }
}
