using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Visualisator
{
    [Serializable()]
    class Vertex: ISerializable
    {
        private Double _x;
        private Double _y;
        private Double _z;
        private Color _vColor;

        public double x
        {
            get { return _x; }
            set { _x = value; }
        }
        public double y
        {
            get { return _y; }
            set { _y = value; }
        }
        public double z
        {
            get { return _z; }
            set { _z = value; }
        }

        public Color VColor
        {
            get { return _vColor; }
            set { _vColor = value; }
        }

        public void SetVertex(Double x, Double y, Double z)
        {
            _x = x;
            _y = y;
            _z = z;
        }

        public Vertex(Double x, Double y, Double z)
        {
            this.SetVertex(x,y,z);
        }
        public Vertex(Vertex ver)
        {
            this.SetVertex(ver.x, ver.y, ver.z);
        }
        public Vertex()
        {
            this.SetVertex(0, 0, 0);
        }
 
        
    }
}
