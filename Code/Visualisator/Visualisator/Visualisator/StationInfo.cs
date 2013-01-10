using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Visualisator
{
    partial class StationInfo : Form
    {
        STA _sta = null;
        
        public StationInfo(STA st)
        {
            _sta = st;
            InitializeComponent();
        }

        private void StationInfo_Load(object sender, EventArgs e)
        {
            lblMac.Text = _sta.Address.getMAC();
            ArrayList _ap = _sta.ScanList();
            foreach (String SSID in _ap)
            {
                cmbAPList.Items.Add(SSID);
            }
        }
    }
}
