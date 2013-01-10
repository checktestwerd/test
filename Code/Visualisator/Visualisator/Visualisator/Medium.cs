using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;

namespace Visualisator
{
    [Serializable()]
    class Medium :ISerializable
    {

        private Boolean _MEDIUM_WORK = true;


        private ArrayList _packets = new ArrayList();

        private Boolean _MediumClean = true;

        public Boolean MediumClean
        {
            get { return _MediumClean; }
            set { _MediumClean = value; }
        }


        public Boolean StopMedium
        {
            get { return _MEDIUM_WORK; }
            set { _MEDIUM_WORK = value; }
        }
        public Medium()
        {
            Thread newThread = new Thread(new ThreadStart(Run));
            newThread.Start();
        }

        public void Run() 
        {
            while (_MEDIUM_WORK)
            {

                Console.WriteLine("Running in a different thread.");
                Thread.Sleep(15000);
            }
        }

        public void ClearMedium()
        {

        }

        public void SendData(Packets.IPacket pack)
        {
            MediumClean = false;

            if (pack != null)
            {
                _packets.Add(pack);
            }
            Thread newThread = new Thread(new ThreadStart(ThreadableSendData));
            newThread.Start();            
        }

        public void ThreadableSendData()
        {
            Thread.Sleep(10);
            _packets.Clear();
            MediumClean = true;

        }
        public Packets.IPacket ReceiveData()
        {
            Packets.IPacket ret = null;
            ArrayList temp = new ArrayList(_packets);
            if (temp.Count > 0)
            {
                foreach (object pack in temp)
                {
                    if (pack != null && typeof(Packets.Beacon) == pack.GetType())
                    {
                        ret = (Packets.IPacket)pack;
                        return (ret);
                    }
                    // loop body
                }
            }
            return (ret);
        }
    }
}
