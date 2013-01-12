using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using Visualisator.Packets;
using System.Collections;

namespace Visualisator
{
    [Serializable()]
    class AP :  RFDevice, IBoardObjects,IRFDevice,ISerializable
    {
 
        
        private Int32 _BeaconPeriod = 500;

        private Int32 _AP_MAX_SEND_PERIOD = 600;
        private Int32 _AP_MIN_SEND_PERIOD = 100;
        private static Random rnadomBeacon = new Random();
        private String _SSID = "";

        public String SSID
        {
            get { return _SSID; }
            set { _SSID = value; }
        }
        private ArrayList _AssociatedDevices = new ArrayList();


        private static Random random = new Random((int)DateTime.Now.Ticks);//thanks to McAden
        private string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }

        public AP(Medium med)
        {
            this._MEDIUM = med;
            this.VColor = Color.YellowGreen;
            this._SSID = RandomString(8);
            _BeaconPeriod = rnadomBeacon.Next(_AP_MIN_SEND_PERIOD, _AP_MAX_SEND_PERIOD);
            
            Enable();
        }
        ~AP()
        {
            _Enabled = false;
        }
        public void Enable()
        {
            RF_STATUS = "NONE";
            _Enabled = true;
            Thread newThread = new Thread(new ThreadStart(SendBeacon));
            newThread.Start();

          Thread newThreadListen = new Thread(new ThreadStart(Listen));
          newThreadListen.Start();

        }

        public void Disable()
        {
            _Enabled = false;

        }

        public void SendBeacon()
        {
            while (_Enabled)
            {
                Beacon _beac = new Beacon(CreatePacket());
                _beac.Destination = "FF:FF:FF:FF:FF:FF";
                this.SendData(_beac);
                Thread.Sleep(_BeaconPeriod);               
            }
        }

        public void SendConnectionACK(String DEST_MAN)
        {
            while (_Enabled)
            {
                ConnectionACK _ack = new ConnectionACK(CreatePacket());
                _ack.Destination = DEST_MAN;
                this.SendData(_ack);
                //Thread.Sleep(_BeaconPeriod);
            }
        }

        public void SendData(SimulatorPacket pack)
        {

            Random ran = new Random((int)DateTime.Now.Ticks);
            while (RF_STATUS != "NONE")
            {
                Thread.Sleep(ran.Next(3,10));
            }
            RF_STATUS = "TX";
            while (!_MEDIUM.Registration(this.getOperateBand(),this.getOperateChannel(),this.x,this.y))
            {
                Thread.Sleep(ran.Next(2, 4));
            }
            _MEDIUM.SendData(pack);
            RF_STATUS = "NONE";
            Thread.Sleep(ran.Next(5, 15));
    
        }



        public Packets.IPacket ReceiveData(IRFDevice ThisDevice)
        {
            throw new NotImplementedException();
        }


        public void Listen()
        {
            while (_Enabled)
            {
                Packets.IPacket pack = null;
                Random ran = new Random((int)DateTime.Now.Ticks);
                while (RF_STATUS != "NONE")
                {
                    Thread.Sleep(ran.Next(1,4));
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
        public void ParseReceivedPacket(IPacket pack)
        {
            if (pack.GetType() == typeof(Packets.Connect))
            {
                Packets.Connect _conn = (Packets.Connect)pack;
                _AssociatedDevices.Add(_conn.Source);
                SendConnectionACK(_conn.Source);

            }
            else
            {

                //Console.WriteLine("[" + getMACAddress() + "]" + " listening.");
            }
        }
        public void RegisterToMedium(int x, int y, int Channel, string Band, int Radius)
        {
            //
        }



    }
}
