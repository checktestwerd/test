﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Collections;
using Visualisator.Packets;

namespace Visualisator
{
    [Serializable()]
    class STA : RFDevice, IBoardObjects, ISerializable,IRFDevice
    {

       /* public STA(Medium med)
        {
            this._MEDIUM = med;
            this.VColor = Color.RoyalBlue;
            Enable();
        }*/
        //*********************************************************************
        public STA(Medium med,ArrayList RfObjects)
        {
            this._MEDIUM = med;
            this.VColor = Color.RoyalBlue;
            _PointerToAllRfDevices = RfObjects;
            Enable();
        }
        //*********************************************************************
        ~STA()
        {
            _Enabled = false;
            Console.WriteLine("[" + getMACAddress() + "]" + " Destroyed");
        }

        //*********************************************************************
        public void Enable()
        {
            _Enabled = true;
            Thread newThread = new Thread(new ThreadStart(Listen));
            newThread.Start();
            /*
            Thread newThreadSTACleaner = new Thread(new ThreadStart(STACleaner));
            newThreadSTACleaner.Start();
             * */
        }

        //*********************************************************************
        private void ThreadableConnectToAP(String SSID, Connect _conn, AP _connecttoAP)
        {
            _AssociatedWithAPList.Clear();
            Int32 tRYStOcONNECT = 0;
            _conn.SSID = _connecttoAP.SSID;
            _conn.Destination = _connecttoAP.getMACAddress();
            _conn.PacketChannel = _connecttoAP.getOperateChannel();
            _conn.PacketBand = _connecttoAP.getOperateBand();
            _conn.Reciver = _connecttoAP.getMACAddress();
            this.setOperateChannel(_connecttoAP.getOperateChannel());
            this.setOperateBand(_connecttoAP.getOperateBand());
            while (!_AssociatedWithAPList.Contains(SSID) && tRYStOcONNECT < 10)
            {
                SendData(_conn);
                tRYStOcONNECT++;
                Thread.Sleep(500);
            }
        }

        //*********************************************************************
        public String getAssociatedAP_SSID()
        {
            String ret = "";

            foreach (var ap in _AssociatedWithAPList)
                ret += ap.ToString() + " ";

            return ret;
        }

        //*********************************************************************
        public Boolean ConnectToAP(String SSID)
        {
            if (SSID.Length > 0 && _AccessPoint.Contains(SSID))
            {
                Connect _conn = new Connect(CreatePacket());
                AP _connecttoAP = GetAPBySSID(SSID);
                if (_connecttoAP != null)
                {
                    Thread newThread = new Thread(() => ThreadableConnectToAP(SSID, _conn, _connecttoAP));
                    newThread.Start();
                    return (true);
                }
                else
                {
                    AddToLog("Cannot find AP with SSID:" + SSID);
                    
                }
            }
            return (false);
        }

        //*********************************************************************
        public void Listen()
        {
            Packets.IPacket pack = null;

            while (_Enabled)
            {
                while (RF_STATUS != "NONE")
                {
                    Thread.Sleep(1);
                }
                lock (RF_STATUS)
                {
                    RF_STATUS = "RX";
                    if (_MEDIUM.MediumHaveAIRWork(this))
                        pack = _MEDIUM.ReceiveData(this);
                    
                    RF_STATUS = "NONE";
                }
                if (pack != null)
                    ParseReceivedPacket(pack);

                Thread.Sleep(2);
            }
        }

        //*********************************************************************
        public void ParseReceivedPacket(IPacket pack)
        {
            Type _Pt = pack.GetType();
            if (_Pt  == typeof(Packets.ConnectionACK))
            {
                Packets.ConnectionACK _ack = (Packets.ConnectionACK)pack;
                if (!_AssociatedWithAPList.Contains(_ack.SSID))
                {
                    _AssociatedWithAPList.Add(_ack.SSID);
                    Thread.Sleep(5);
                }
            }
            else if (_Pt == typeof(Packets.Beacon))
            {
                Packets.Beacon bec = (Packets.Beacon)pack;
                if (!_AccessPoint.Contains(bec.SSID))
                {
                    _AccessPoint.Add(bec.SSID);
                }
                Thread.Sleep(2);
            }

            else if (_Pt == typeof(Packets.Data))
            {

                //Thread.Sleep(2);
                _DataReceived++;
            }
            else
            {
                //Console.WriteLine("[" + getMACAddress() + "]" + " listening.");
            }
        }


        //*********************************************************************
        public void SendData(SimulatorPacket PacketToSend)
        {
            Random ran = new Random((int)DateTime.Now.Ticks);
            while(RF_STATUS != "NONE")
                Thread.Sleep(ran.Next(1, 3));
      
            RF_STATUS = "TX";
            while (!_MEDIUM.Registration(this.getOperateBand(), this.getOperateChannel(), this.x, this.y))
            {
                RF_STATUS = "NONE";
                Thread.Sleep(ran.Next(1, 3));
                while (RF_STATUS != "NONE")
                    Thread.Sleep(ran.Next(1, 3));
                RF_STATUS = "TX";
            }
            _MEDIUM.SendData(PacketToSend);
            
            RF_STATUS = "NONE";
            Thread.Sleep(3);
            if (PacketToSend.GetType() == typeof(Data))
            {
                _DataSent++;
            }
        }
                
        public void ResetCounters()
        {
            _DataSent = 0;
            _DataReceived = 0;
        }
        //*********************************************************************
        public RFDevice GetRFDeviceByMAC(String _mac)
        {
            foreach (var obj in _AccessPoint)
            {
                RFDevice _tV = (RFDevice)obj;
                if (_tV.getMACAddress().Equals(_mac))
                    return (_tV);
            }
            return (null);
        }

        public void rfile(String fileName)
        {

            Thread newThread = new Thread(() => ThreadAbleReadFile(fileName));
                newThread.Start();
        }

        public void ThreadAbleReadFile(String fileName)
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\simulator\input.txt");
            foreach (string line in lines)
            {
                // Use a tab to indent each line of the file.

                AP _connecttoAP = GetAPBySSID(_AccessPoint[0].ToString());
                Data dataPack = new Data(CreatePacket());
                dataPack.setData(line);
                dataPack.SSID = _connecttoAP.SSID;
                dataPack.Destination = _connecttoAP.getMACAddress();
                dataPack.PacketChannel = this.getOperateChannel();
                dataPack.PacketBand = this.getOperateBand();
                dataPack.Reciver = fileName;

                SendData(dataPack);
                Console.WriteLine("\t" + line);
            }
        }
        //*********************************************************************
        public AP GetAPBySSID(String _SSID)
        {
            foreach (var obj in _PointerToAllRfDevices)
            {
                if(obj.GetType() == typeof(AP))
                {
                    AP _tV = (AP)obj;
                    if (_tV.SSID.Equals(_SSID))
                        return (_tV);
                }
            }
            return (null);
        }
        //*********************************************************************
        public void Scan()
        {
            Thread newThread = new Thread(new ThreadStart(ThreadableScan));
            newThread.Start();

        }

        public void ThreadableScan()
        {
            _AccessPoint.Clear();
            _AccessPointTimeCounter.Clear();

            Int32 perv_channel = this.getOperateChannel();
            String prev_band = this.getOperateBand();

            setOperateBand("N");
            for (int i = 1; i < 15; i++)
            {
                setOperateChannel(i);
                Thread.Sleep(100);
            }
            for (int i = 1; i < 15; i++)
            {
                setOperateChannel(i);
                Thread.Sleep(400);
            }
            /*
            ArrayList Achannels = _MEDIUM.getBandAChannels();
            setOperateBand("N");
            foreach (int i in Achannels)
            {
                setOperateChannel(i);
                Thread.Sleep(400);
            }*/
            setOperateChannel(perv_channel);
            setOperateBand(prev_band);
        }

        //*********************************************************************
        public ArrayList ScanList()
        {
            return (_AccessPoint);
        }

        //*********************************************************************
        public void Disable()
        {
            _Enabled = false;
        }

        //*********************************************************************
        public Packets.IPacket ReceiveData(IRFDevice ThisDevice)
        {
            throw new NotImplementedException();
        }

        //*********************************************************************
        public void RegisterToMedium(int x, int y, int Channel, string Band, int Radius)
        {
            throw new NotImplementedException();
        }
    }
}



/*
    if (_AccessPointTimeCounter.ContainsKey(bec.SSID)){
        int counter = (int)_AccessPointTimeCounter[bec.SSID];
        if (counter < 15 && counter>=5)
            counter++;
        else if (counter < 5)
            counter = 10;
        _AccessPointTimeCounter[bec.SSID] = counter;
    } else{
        _AccessPointTimeCounter.Add(bec.SSID, 10);
    }      
 * 
 
         /*
        private void STACleaner()
        {
            while (_Enabled)
            {
                List<string> keys = new List<string>();
                foreach (System.Collections.DictionaryEntry de in _AccessPointTimeCounter)
                    keys.Add(de.Key.ToString());

                foreach (string key in keys)
                {

                    int c = (int)_AccessPointTimeCounter[key];
                    c--;
                    if (c < 0)
                    {
                        _AccessPointTimeCounter.Remove(key);
                        _AccessPoint.Remove(key);
                    }
                    else
                        _AccessPointTimeCounter[key] = c;
                }
                Thread.Sleep(300);
            }
        }

        */