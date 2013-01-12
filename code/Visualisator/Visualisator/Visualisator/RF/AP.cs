using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using Visualisator.Packets;

namespace Visualisator
{
    [Serializable()]
    class AP :  RFDevice, IBoardObjects,IRFDevice,ISerializable
    {
        private Medium _MEDIUM = null;
        
        private Int32 _BeaconPeriod = 500;
        private static Random rnadomBeacon = new Random();
        private String _SSID = "";

        public String SSID
        {
            get { return _SSID; }
            set { _SSID = value; }
        }
        private Boolean _Enabled = true;



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
            _BeaconPeriod = rnadomBeacon.Next(100, 500);
            
            Enable();
        }
        ~AP()
        {
            _Enabled = false;
        }
        public void Enable()
        {
            _Enabled = true;
            Thread newThread = new Thread(new ThreadStart(SendBeacon));
            newThread.Start();

        }

        public void Disable()
        {
            _Enabled = false;

        }

        public void SendBeacon()
        {
            while (_Enabled)
            {
                Beacon _beac =  new Beacon(CreatePacket());
              //  Console.WriteLine("[" + _address.getMAC() + "]" + " send beacon. each " + _BeaconPeriod + " ms.");
                _beac.Destination = "FF:FF:FF:FF:FF:FF";
                this.SendPacket(_beac);
                Thread.Sleep(_BeaconPeriod);
            }
        }



        private void SendPacket(SimulatorPacket pack)
        {
            /*if (_MEDIUM.MediumClean)
            {

                _MEDIUM.SendData(pack);
            }
             * */
            if (_MEDIUM.Registration(this.getOperateBand(),this.getOperateChannel(),this.x,this.y))
            {

                _MEDIUM.SendData(pack);
            } 
        }

        public void SendData(Packets.IPacket PacketToSend)
        {
            throw new NotImplementedException();
        }

        public Packets.IPacket ReceiveData(IRFDevice ThisDevice)
        {
            throw new NotImplementedException();
        }




        public void RegisterToMedium(int x, int y, int Channel, string Band, int Radius)
        {
            //
        }
    }
}
