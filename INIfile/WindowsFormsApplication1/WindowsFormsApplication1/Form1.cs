using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        INIfile fini = new INIfile("c:\\test1.ini");
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
       
            textBox1.Text = fini.getValue("order");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            fini.setValue("order", "2000");
            fini.setValue("newSheet", "WORK VERY VELL");
        }

        private void button4_Click(object sender, EventArgs e)
        {
          Hashtable d =   fini.getAll();

             foreach (DictionaryEntry entry in d)
             {
                 Console.WriteLine("{0}={1}", entry.Key, entry.Value);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
