using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Visualisator
{
    class STA:Vertex 
    {
        private MAC _address = new MAC();
        public STA()
        {
            this.VColor = Color.RoyalBlue;
            
        }


        public MAC Address
        {
            get { return _address; }
            set { _address = value; }
        }
    }
}
