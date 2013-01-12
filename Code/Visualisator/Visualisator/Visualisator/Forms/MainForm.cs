using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Visualisator
{
    public partial class MainForm : Form
    {
        PictureBox piB;
        Bitmap bm;
        Graphics gr;
       // private AP[] _vert ;
       //S private STA[] _sta;
        private Int32 STA_SIZE = 15;
        private Int32 APs_SIZE = 5;

        private Int32 SelectedVertex = -1;
        private float SelectedX = 0;
        private float SelectedY = 0;
        private float SelectedZ = 0;

        private ArrayList _objects = new ArrayList();

        private Medium _MEDIUM = new Medium();

        private enum SelectedObjectType
        {
            STA,
            AP,
        };

        private SelectedObjectType _ob;


        public MainForm()
        {
            InitializeComponent();
        }
        private void CreateRandomSimulation()
        {
            ClearObjects();
            Random rand = new Random();
            for (int i = 0; i < APs_SIZE; i++)
            {
                AP _ap = new AP(_MEDIUM);
                _ap.setOperateChannel(rand.Next(1, 14));
                _ap.SetVertex(rand.NextDouble() * 500, rand.NextDouble() * 500, rand.NextDouble() * 500);
                _objects.Add(_ap);
            }
            for (int i = 0; i < STA_SIZE; i++)
            {
                STA _sta = new STA(_MEDIUM);
                _sta.setOperateChannel(0);// (rand.Next(1, 14));       //  TODO delete this line
                _sta.SetVertex(rand.NextDouble() * 500, rand.NextDouble() * 500, rand.NextDouble() * 500);
                _objects.Add(_sta);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
           

            piB = new PictureBox();
            piB.Parent = this;
            piB.Location = new Point(10, 10);
            piB.Size = new Size(500, 500);
            piB.BackColor = Color.Black;

              piB.AllowDrop = true; 
            this.piB.MouseDown += new System.Windows.Forms.MouseEventHandler(this. btnImage_MouseDown);
            this.piB.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnImage_MouseUp);
            this.piB.DragDrop += new System.Windows.Forms.DragEventHandler(this.pictureBox_DragDrop);
            this.piB.DragEnter += new System.Windows.Forms.DragEventHandler(this.pictureBox_DragEnter);
            this.piB.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.BoardDblClick);
          
            
            bm = new Bitmap(piB.Width, piB.Height);
           
            gr = Graphics.FromImage(bm);
        }




        private void BoardDblClick(object sender, MouseEventArgs e)
        {
            txtConsole.Text = "X = " + e.X + "    Y = " + e.Y + "\r\n" + txtConsole.Text;
            int _rad_size = 12;
      
            for (int i = 0; i < _objects.Count; i++)
            {
                if (_objects[i].GetType() == typeof(STA))
                {
                    STA _tsta = (STA)_objects[i];
                    if (_tsta.x >= e.X - _rad_size && _tsta.x <= e.X + _rad_size && _tsta.y >= e.Y - _rad_size && _tsta.y <= e.Y + _rad_size)
                    {
                        txtConsole.Text = "Station selected for view :" + i.ToString() + "\r\n" + txtConsole.Text;
                      //  SelectedVertex = i;
                      //  SelectedX = e.X;
                       // SelectedY = e.Y;
                     //   SelectedZ = e.X + e.Y;
                   //     _ob = SelectedObjectType.STA;
                        StationInfo staForm = new StationInfo(_tsta);
                        
                        staForm.Show();
                        return;
                    }
                }/*
                else if (_objects[i].GetType() == typeof(AP))
                {

                    AP _tap = (AP)_objects[i];
                    if (_tap.x >= e.X - _rad_size && _tap.x <= e.X + _rad_size && _tap.y >= e.Y - _rad_size && _tap.y <= e.Y + _rad_size)
                    {
                        txtConsole.Text = "AP selected for move :" + i.ToString() + "\r\n" + txtConsole.Text;
                        SelectedVertex = i;
                        SelectedX = e.X;
                        SelectedY = e.Y;
                        SelectedZ = e.X + e.Y;
                        _ob = SelectedObjectType.AP;
                        
                        return;
                    }

                }*/
            }
         
        }
        private void btnImage_MouseDown(object sender, MouseEventArgs e)
        {
            txtConsole.Text = "X = " + e.X + "    Y = " + e.Y + "\r\n" + txtConsole.Text;
            int _rad_size = 12;

            for (int i = 0; i < _objects.Count; i++)
            {
                if (_objects[i].GetType() == typeof(STA))
                {
                    STA _tsta = (STA)_objects[i];
                    if (_tsta.x >= e.X - _rad_size && _tsta.x <= e.X + _rad_size && _tsta.y >= e.Y - _rad_size && _tsta.y <= e.Y + _rad_size)
                    {
                        txtConsole.Text = "Station selected for move :" + i.ToString() + "\r\n" + txtConsole.Text;
                        SelectedVertex = i;
                        SelectedX = e.X;
                        SelectedY = e.Y;
                        SelectedZ = e.X + e.Y;
                        _ob = SelectedObjectType.STA;
                        return;
                    }
                }
                else if (_objects[i].GetType() == typeof(AP)){

                    AP _tap = (AP)_objects[i];
                    if (_tap.x >= e.X - _rad_size && _tap.x <= e.X + _rad_size && _tap.y >= e.Y - _rad_size && _tap.y <= e.Y + _rad_size)
                    {
                        txtConsole.Text = "AP selected for move :" + i.ToString() + "\r\n" + txtConsole.Text;
                        SelectedVertex = i;
                        SelectedX = e.X;
                        SelectedY = e.Y;
                        SelectedZ = e.X + e.Y;
                        _ob = SelectedObjectType.AP;
                        return;
                    }

                }
            }
        }

        private void btnImage_MouseUp(object sender, MouseEventArgs e)
        {
            txtConsole.Text = "X = " + e.X + "    Y = " + e.Y + "\r\n" + txtConsole.Text;

            if(SelectedVertex <0)
            {
                ConsolePrint("No object selected for move");
                return;
            }

            if (SelectedX != e.X && SelectedY != e.Y)
            {
                ConsolePrint("Start move object");

                
                if (_ob == SelectedObjectType.AP)
                {
                    AP _tAP = (AP)_objects[SelectedVertex];
                    ConsolePrint("Drawing " + _tAP.getMAC().getMAC());
                      _tAP.x = e.X;
                     _tAP.y = e.Y;
                }
                if ( _ob == SelectedObjectType.STA)
                {
                    STA _tsta = (STA)_objects[SelectedVertex];
                    ConsolePrint("Drawing " + _tsta.getMACAddress());
                    
                    _tsta.x = e.X;
                    _tsta.y = e.Y;
         

                }        
            }
            else
            {
                ConsolePrint("Object not moved: Simple click");
            }

            SelectedVertex = -1;

            refr();
        }
        private void pictureBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void ConsolePrint(String data)
        {
            txtConsole.Text = data + "\r\n" + txtConsole.Text;
        }
        private void pictureBox_DragDrop(object sender, DragEventArgs e)
        {
            ConsolePrint("Start pictureBox_DragDrop");
            try
            {
                PictureBox picbox = (PictureBox)sender;
                Graphics g = picbox.CreateGraphics();
                g.DrawImage((Image)e.Data.GetData(DataFormats.Bitmap), new Point(0, 0));
                ConsolePrint("pictureBox_DragDrop success");
            }
            catch (Exception)
            {
                ConsolePrint("pictureBox_DragDrop error");
                throw;
            }

        }


        private void DrowOnBoard()
        {

            ConsolePrint("Start drawing");
            try
            {
                refr();
                ConsolePrint("Drawing success");
            }
            catch (Exception)
            {
                ConsolePrint("Drawing error");
                throw;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DrowOnBoard();
        }
        
        public void refr()
        {
            gr.Clear(Color.Black);


            for (int i = 0; i < _objects.Count; i++)
            {
                if (_objects[i].GetType() == typeof(STA))
                {
                    STA _tsta = (STA)_objects[i];

                    gr.DrawPie(new Pen(_tsta.VColor), (float)_tsta.x-5, (float)_tsta.y-5, 10, 10, 1, 360);
                    gr.DrawPie(new Pen(System.Drawing.Color.Purple), (float)_tsta.x-25, (float)_tsta.y-25,  50,  50, 1, 360);
                    string drawString = _tsta.getOperateChannel() + " " + _tsta.getMACAddress();
                    System.Drawing.Font drawFont = new System.Drawing.Font(
                        "Arial", 7);
                    System.Drawing.SolidBrush drawBrush = new
                        System.Drawing.SolidBrush(System.Drawing.Color.YellowGreen);

                    gr.DrawString(drawString, drawFont, drawBrush, (int)_tsta.x + 10, (int)_tsta.y + 10);
                    drawFont.Dispose();
                    drawBrush.Dispose();
                }
                else if (_objects[i].GetType() == typeof(AP))
                {
                    AP _tap = (AP)_objects[i];
                    Rectangle myRectangle = new Rectangle((int)_tap.x-5, (int)_tap.y-5, 10, 10);
                    gr.DrawPie(new Pen( System.Drawing.Color.Purple), (float)_tap.x-25, (float)_tap.y-25,  50,  50, 1, 360);
                    gr.DrawRectangle(new Pen(_tap.VColor), myRectangle);


                    string drawString = _tap.getOperateChannel() + " " + _tap.SSID + " " + _tap.getMACAddress();
                    System.Drawing.Font drawFont = new System.Drawing.Font(
                        "Arial", 7);
                    System.Drawing.SolidBrush drawBrush = new
                        System.Drawing.SolidBrush(System.Drawing.Color.White);

                    gr.DrawString(drawString, drawFont, drawBrush, (int)_tap.x+10, (int)_tap.y-10);
                    drawFont.Dispose();
                    drawBrush.Dispose();
              
                }
            }
            
            for (int i = 0; i < APs_SIZE; i++)
            {
            }

            for (int i = 0; i < STA_SIZE; i++)
            {
            }
            gr.DrawPie(new Pen(Color.Yellow), 500 / 2, 500 / 2, 1, 1, 1, 360);
            piB.Image = bm;
        }

        private void btn_AddAP_Click(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void Form1_Leave(object sender, EventArgs e)
        {
            _MEDIUM.StopMedium = true;
        }

        private void BtnStopMediumClick(object sender, EventArgs e)
        {
            _MEDIUM.StopMedium = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var serializer = new BinaryFormatter();
            SimulationContainer _container = new SimulationContainer(_objects, _MEDIUM);
            using (var stream = File.OpenWrite("test.dat"))
            {
                serializer.Serialize(stream, _container);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
            try
            {
                DialogResult dr = this.openDLGOpenSimulationSettings.ShowDialog();

                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    ClearObjects();
                    
                    foreach (String file in openDLGOpenSimulationSettings.FileNames)
                    {
                        try
                        {
                            var serializer = new BinaryFormatter();
                            SimulationContainer _container = new SimulationContainer(null, null);

                            using (var stream = File.OpenRead(file))
                            {
                                _container = (SimulationContainer)serializer.Deserialize(stream);
                                _objects = _container.Objects;
                                _MEDIUM = _container.MEDIUM;
                                _MEDIUM.Enable();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }

                    for (int i = 0; i < _objects.Count; i++)
                    {
                        IRFDevice _dev = (IRFDevice)_objects[i];
                        _dev.Enable();

                    }
                }
            }
            catch (Exception )
            {
                //AddToErrorLog(ex.Message);
            }
            DrowOnBoard();
        }

        private void ClearObjects()
        {
            for (int i = 0; i < _objects.Count; i++)
            {
                IRFDevice _dev = (IRFDevice) _objects[i];
                _dev.Disable();
                _dev = null;

            }
            _MEDIUM.Disable();
            _objects.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CreateRandomSimulation();
            DrowOnBoard();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ClearObjects();
        }

        private void btnShowMediumInfo_Click(object sender, EventArgs e)
        {
            MediumInfo mediumForm = new MediumInfo(_MEDIUM);
            mediumForm.Show();
        }



       
    }
}
