﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Visualisator.Packets
{
    [Serializable()]
    class Data : SimulatorPacket, IPacket, ISerializable
    {
                        // TODO check if this work corectlly
        public Data(SimulatorPacket pack)
        {
            Type t = typeof(SimulatorPacket);
            PropertyInfo[] properties = t.GetProperties();
            foreach (PropertyInfo pi in properties)
            {
                pi.SetValue(this, pi.GetValue(pack, null), null);
            }
        }
    }
}
