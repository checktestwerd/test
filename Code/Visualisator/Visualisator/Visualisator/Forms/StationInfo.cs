using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Threading;

namespace Visualisator
{
    partial class StationInfo : Form
    {
        STA _sta = null;
        private Boolean _scanning = false;
        public StationInfo(STA st)
        {
            
            InitializeComponent();
            _sta = st;
        }

        private void StationInfo_Load(object sender, EventArgs e)
        {
            lblMac.Text = _sta.getMACAddress();
            
            PrintAPList();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            Thread newThread = new Thread(new ThreadStart(Scan));
            newThread.Start();
            btnScan.Enabled = false;
            _scanning = true;
            PrintAPList();
        }


        private void Scan()
        {
            _sta.Scan();
            _scanning = false;
           // btnScan.Enabled = true;
        }
        private void PrintAPList()
        {
            cmbAPList.Items.Clear();
            ArrayList _ap = _sta.ScanList();
            if (_ap != null)
            {
                foreach (String SSID in _ap)
                {
                    cmbAPList.Items.Add(SSID);
                }
            }
        }

        private void cmbAPList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbAPList_MouseClick(object sender, MouseEventArgs e)
        {
            PrintAPList();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!_scanning)
            {
                btnScan.Enabled = true;
            }
        }
    }
}
