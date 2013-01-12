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
    class STA : RFDevice, IBoardObjects, ISerializable,IRFDevice
    {



    
        private Medium _MEDIUM = null;
        private Boolean _Enabled = true;
        private ArrayList _AccessPoint = new ArrayList();
        private Hashtable _AccessPointTimeCounter = new Hashtable( new ByteArrayComparer());
        public STA(Medium med)
        {
            this._MEDIUM = med;
            this.VColor = Color.RoyalBlue;
            Enable();
        }

        ~STA()
        {
            _Enabled = false;
            Console.WriteLine("[" + getMACAddress() + "]" + " Destroyed");
        }

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

        public void Disable()
        {
            _Enabled = false;

        }

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
        public void Scan()
        {
    
            _AccessPoint.Clear();
     

            _AccessPointTimeCounter.Clear();
            
            Int32 perv_channel =this.getOperateChannel();
            for (int i = 1; i < 15; i++)
            {
                setOperateChannel(i);
                Thread.Sleep(20);
            }
            for (int i = 1; i < 15; i++)
            {
                setOperateChannel(i);
                Thread.Sleep(29);
            }
            for (int i = 1; i < 15; i++)
            {
                setOperateChannel(i);
                Thread.Sleep(400);
            }

            setOperateChannel(perv_channel);
        }
        public void Listen()
        {
            while (_Enabled)
            {
                if (_MEDIUM.MediumHaveAIRWork(this))
                {

                    Packets.IPacket pack = _MEDIUM.ReceiveData(this);
                    if(pack != null ){

                        if (pack.GetType() == typeof(Packets.Beacon)){
                            Packets.Beacon bec = (Packets.Beacon)pack;
                                if (!_AccessPoint.Contains(bec.SSID))
                                {
                                    _AccessPoint.Add(bec.SSID);
                                }      

                            /*
                                if (_AccessPointTimeCounter.ContainsKey(bec.SSID))
                                {
                                    int counter = (int)_AccessPointTimeCounter[bec.SSID];
                                    if (counter < 15 && counter>=5)
                                        counter++;
                                    else if (counter < 5)
                                    {
                                        counter = 10;
                                    }
                                    _AccessPointTimeCounter[bec.SSID] = counter;
                                }
                                else
                                {
                                    _AccessPointTimeCounter.Add(bec.SSID, 10);
                                }

                            */
                         

                            Thread.Sleep(2);
                        }
                        else{

                            //Console.WriteLine("[" + getMACAddress() + "]" + " listening.");
                        }
                    }
                }
                Thread.Sleep(1);
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





        public void RegisterToMedium(int x, int y, int Channel, string Band, int Radius)
        {
            throw new NotImplementedException();
        }
    }
}
