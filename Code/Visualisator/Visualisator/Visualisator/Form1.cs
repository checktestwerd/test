using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Visualisator
{
    public partial class Form1 : Form
    {
        PictureBox piB;
        Bitmap bm;
        Graphics gr;
        private AP[] _vert ;
        private STA[] _sta;
        private Int32 STA_SIZE = 7;
        private Int32 VERT_SIZE = 5;

        private Int32 SelectedVertex = -1;
        private float SelectedX = 0;
        private float SelectedY = 0;
        private float SelectedZ = 0;

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
            
          
            
            bm = new Bitmap(piB.Width, piB.Height);
           
            gr = Graphics.FromImage(bm);
           
            
         
            _vert = new AP[VERT_SIZE];
            _sta = new STA[STA_SIZE];
            Random rand = new Random();
            for (int i = 0; i < VERT_SIZE; i++)
            {
                _vert[i] = new AP();
                _vert[i].SetVertex(rand.NextDouble() * 500, rand.NextDouble() * 500, rand.NextDouble() * 500);
            }
            for (int i = 0; i < STA_SIZE; i++)
            {
                _sta[i] = new STA();
                _sta[i].SetVertex(rand.NextDouble() * 500, rand.NextDouble() * 500, rand.NextDouble() * 500);
            }
       





        }

        private void btnImage_MouseDown(object sender, MouseEventArgs e)
        {
            textBox1.Text = "X = " + e.X + "    Y = " + e.Y + "\r\n" + textBox1.Text;
            int _rad_size = 12;
            for(int i = 0; i< VERT_SIZE;i++)
            {
                if (_vert[i].x >= e.X - _rad_size && _vert[i].x <= e.X + _rad_size && _vert[i].y >= e.Y - _rad_size && _vert[i].y <= e.Y + _rad_size)
                {
                    textBox1.Text = "Vertex :" + i.ToString() + "\r\n" + textBox1.Text;
                    SelectedVertex = i;
                    SelectedX = e.X;
                    SelectedY = e.Y;
                    SelectedZ = e.X + e.Y;
                    _ob = SelectedObjectType.AP;
                    return;
                }
            }


            for (int i = 0; i < STA_SIZE; i++)
            {
                if (_sta[i].x >= e.X - _rad_size && _sta[i].x <= e.X + _rad_size && _sta[i].y >= e.Y - _rad_size && _sta[i].y <= e.Y + _rad_size)
                {
                    textBox1.Text = "Vertex :" + i.ToString() + "\r\n" + textBox1.Text;
                    SelectedVertex = i;
                    SelectedX = e.X;
                    SelectedY = e.Y;
                    SelectedZ = e.X + e.Y;
                    _ob = SelectedObjectType.STA;
                    return;
                }
            }
        }

        private void btnImage_MouseUp(object sender, MouseEventArgs e)
        {
            textBox1.Text = "X = " + e.X + "    Y = " + e.Y + "\r\n" + textBox1.Text;

            if (SelectedVertex >=0 && SelectedX != e.X && SelectedY != e.Y && _ob == SelectedObjectType.AP)
            {
                _vert[SelectedVertex].x = e.X;
                _vert[SelectedVertex].y = e.Y;
            }
            if (SelectedVertex >= 0 && SelectedX != e.X && SelectedY != e.Y && _ob == SelectedObjectType.STA)
            {
                _sta[SelectedVertex].x = e.X;
                _sta[SelectedVertex].y = e.Y;
            }
            SelectedVertex = -1;
          /*  for (int i = 0; i < VERT_SIZE; i++)
            {
                if (_vert[i].x >= e.X - 10 && _vert[i].x <= e.X + 10 && _vert[i].y >= e.Y - 10 && _vert[i].y <= e.Y + 10)
                {
                    textBox1.Text = "- l Vertex :" + i.ToString() + "\r\n" + textBox1.Text;
                }
            }
           * 
           * */
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

        private void pictureBox_DragDrop(object sender, DragEventArgs e)
        {
            PictureBox picbox = (PictureBox)sender;
            Graphics g = picbox.CreateGraphics();
            g.DrawImage((Image)e.Data.GetData(DataFormats.Bitmap), new Point(0, 0));
        }


        private void button1_Click(object sender, EventArgs e)
        {
 
             //   
            refr();
        }
        
        public void refr()
        {
            gr.Clear(Color.Black);
            for (int i = 0; i < VERT_SIZE; i++)
            {
                gr.DrawPie(new Pen(_vert[i].VColor), (float)_vert[i].x, (float)_vert[i].y, 10, 10, 1, 360);
            }

            for (int i = 0; i < STA_SIZE; i++)
            {
                gr.DrawPie(new Pen(_sta[i].VColor), (float)_sta[i].x, (float)_sta[i].y, 12, 10, 1, 360);
            }
            gr.DrawPie(new Pen(Color.Yellow), 500 / 2, 500 / 2, 1, 1, 1, 360);
            piB.Image = bm;
        }

       
    }
}
