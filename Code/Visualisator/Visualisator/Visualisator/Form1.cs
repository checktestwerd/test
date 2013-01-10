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
    public partial class Form1 : Form
    {
        PictureBox piB;
        Bitmap bm;
        Graphics gr;
       // private AP[] _vert ;
       //S private STA[] _sta;
        private Int32 STA_SIZE = 3;
        private Int32 VERT_SIZE = 2;

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


        public Form1()
        {
            InitializeComponent();
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


        private void CreateRandomSimulation()
        {
            ClearObjects();
            Random rand = new Random();
            for (int i = 0; i < VERT_SIZE; i++)
            {
                AP _ap = new AP(_MEDIUM);
                _ap.SetVertex(rand.NextDouble() * 500, rand.NextDouble() * 500, rand.NextDouble() * 500);
                _objects.Add(_ap);
            }
            for (int i = 0; i < STA_SIZE; i++)
            {
                STA _sta = new STA(_MEDIUM);
                _sta.SetVertex(rand.NextDouble() * 500, rand.NextDouble() * 500, rand.NextDouble() * 500);
                _objects.Add(_sta);
            }
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
                    ConsolePrint("Drawing " + _tAP.Address.getMAC());
                      _tAP.x = e.X;
                     _tAP.y = e.Y;
                }
                if ( _ob == SelectedObjectType.STA)
                {
                    STA _tsta = (STA)_objects[SelectedVertex];
                    ConsolePrint("Drawing " + _tsta.Address.getMAC());
                    
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

                    gr.DrawPie(new Pen(_tsta.VColor), (float)_tsta.x, (float)_tsta.y, 12, 10, 1, 360);
            
                }
                else if (_objects[i].GetType() == typeof(AP))
                {
                    AP _tap = (AP)_objects[i];
                    Rectangle myRectangle = new Rectangle((int)_tap.x, (int)_tap.y, 10, 10);
                    gr.DrawRectangle(new Pen(_tap.VColor), myRectangle);
            
                }
            }
            
            for (int i = 0; i < VERT_SIZE; i++)
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

        private void btnStopMedium_Click(object sender, EventArgs e)
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
                           // SoapFormatter formatter = new SoapFormatter();
                            using (var stream = File.OpenRead(file))
                            {
                                //System.Runtime.Serialization.Formatters.Binary.BinaryFormatter
                                _container = (SimulationContainer)serializer.Deserialize(stream);
                                _objects = _container.Objects;
                                _MEDIUM = _container.MEDIUM;
                                _MEDIUM.Enable();
                            }
                            //listBox4.Items.Add(file); TODO
                            //ReadParseCreateObjects(file);
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
                //    _settings.setValue("filesFolderPath", Path.GetDirectoryName(openFileDialog2.FileName.ToString()).ToString());
                  //  LoadFilesFromArr(openFileDialog2.FileNames);
                   // openFileDialog2.InitialDirectory = _settings.getValue("filesFolderPath");
                }
            }
            catch (Exception ex)
            {
                //AddToErrorLog(ex.Message);
            }
            DrowOnBoard();
        }

        private void ClearObjects()
        {
            for (int i = 0; i < _objects.Count; i++)
            {
                _objects[i] = null;

            }
            _objects.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CreateRandomSimulation();
            DrowOnBoard();
        }

       
    }
}
