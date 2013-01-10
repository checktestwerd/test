using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Visualisator.Packets;
namespace Visualisator
{
    interface IRFDevice
    {
        void Enable();
        void SendData(IPacket PacketToSend);
        IPacket ReceiveData(IRFDevice ThisDevice);
    }
}
