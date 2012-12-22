using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Visualisator
{
    class MAC
    {
        private String _MAC;

        public  MAC()
        {
            Random rand = new Random();
            Int32 _mac1 = rand.Next(0, 255);
            Int32 _mac2 = rand.Next(0, 255);
            Int32 _mac3 = rand.Next(0, 255);
            Int32 _mac4 = rand.Next(0, 255);
            Int32 _mac5 = rand.Next(0, 255);
            Int32 _mac6 = rand.Next(0, 255);

            _MAC = string.Format("{0:x}:{1:x}:{2:x}:{3:x}:{4:x}:{5:x}", _mac1, _mac2, _mac3, _mac4, _mac5, _mac6);
            
        }

        public String getMAC()
        {
            return _MAC;
        }

    }
}
