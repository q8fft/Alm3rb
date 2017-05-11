using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Converter4
{
    public partial class Us : Form
    {
        public Us()
        {
            InitializeComponent();
        }

        bool check = true;

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1.Label();
        }

        private void Us_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1.ap.Stop();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (check)
            {
                Form1.ap.Stop();
                check = false;
            }
            else
            {
                Form1.ap.Stop();
                Form1.ap.Play();
                check = true;
            }
        }

        private void Us_Load(object sender, EventArgs e)
        {
            Form1.ap.Ending += new EventHandler(ap_Ending);
        }

        void ap_Ending(object sender, EventArgs e)
        {
            check = false;
        }
    }
}