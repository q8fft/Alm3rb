using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Diagnostics;
using Microsoft.DirectX.AudioVideoPlayback;
using System.IO;

namespace Converter4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        OleDbDataAdapter dr;
        DataSet ds;

        bool check = true;
        
        string ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=";
        splash sp;
        int delay = 0;
        public static Audio ap;

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                sp = new splash();
                sp.Opacity = 0;
                timer1.Tick += new EventHandler(timer1_Tick);
                sp.ShowDialog();
                InputLanguage.CurrentInputLanguage = GetArabicInputLanguage();
                ConnectionString += db_path();
                OleDbConnection con = new OleDbConnection(ConnectionString);
                dr = new OleDbDataAdapter("Select Letter,Isolated,Final,Medial,Initial From Table1", con);
                ds = new DataSet();
                con.Open();
                dr.Fill(ds, "db");
                con.Close();
            }
            catch
            {
            }
        }

        void timer1_Tick(object sender, EventArgs e)
        {
            if (sp.Opacity != 1)
                sp.Opacity += 0.1;
            else
            {
                if(delay++==3)
                {
                sp.Close();
                timer1.Stop();
                }
            }
        }

        public string db_path()
        {
            string path = Application.ExecutablePath;

            char[] ar = new char[] { '\\' };
            string[] str = path.Split(ar);

            string db_path = null;
            for (int i = 0; i < str.Length - 1; i++)
                db_path += str[i] + "\\";
            db_path += "db.mdb";

            return db_path;
        }

        private void transform_Click(object sender, EventArgs e)
        {
            try
            {
                if (check)
                {
                    textBox2.Clear();
                    string str = " " + textBox1.Text + " ";
                    string letter = null;
                    int length = str.Length;
                    int rowIndex = 0;

                    for (int i = 1; i < length - 1; i++)
                    {
                        for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                        {
                            letter = ds.Tables[0].Rows[j][0].ToString();

                            if (str[i].ToString() == letter)
                            {
                                rowIndex = j; break;
                            }
                        }

                        if (str[i].ToString() == letter)
                        {
                        }
                        else if (str[i] == ' ')
                        {
                            textBox2.AppendText(" ");
                            continue;
                        }
                        else if (str[i] == '\n')
                        {
                            textBox2.AppendText("\n");
                            continue;
                        }
                        else
                        {
                            textBox2.AppendText(((char)13).ToString());
                            continue;
                        }

                        if ((str[i - 1] == (char)10 || str[i - 1] == ' ' || str[i - 1] == 'Â' || str[i - 1] == 'É' || str[i - 1] == 'Ç' || str[i - 1] == 'Ã' || str[i - 1] == 'Å' || str[i - 1] == 'Ï' || str[i - 1] == 'Ð' || str[i - 1] == 'Ñ' || str[i - 1] == 'Ò' || str[i - 1] == 'æ') && (str[i + 1] == ' ' || str[i + 1] == (char)13 || str[i] == 'Ç' || str[i] == 'Â' || str[i] == 'É' || str[i] == 'Ã' || str[i] == 'Å' || str[i] == 'Ï' || str[i] == 'Ð' || str[i] == 'Ñ' || str[i] == 'Ò' || str[i] == 'æ'))
                        {
                            textBox2.AppendText(ds.Tables[0].Rows[rowIndex][1].ToString());
                        }
                        if (str[i - 1] != ' ' && (str[i + 1] == ' ' || str[i + 1] == (char)13 || str[i] == 'Ç' || str[i] == 'Â' || str[i] == 'É' || str[i] == 'Ã' || str[i] == 'Å' || str[i] == 'Ï' || str[i] == 'Ð' || str[i] == 'Ñ' || str[i] == 'Ò' || str[i] == 'æ') && str[i - 1] != 'Ç' && str[i - 1] != 'Â' && str[i - 1] != 'É' && str[i - 1] != 'Ã' && str[i - 1] != 'Å' && str[i - 1] != 'Ï' && str[i - 1] != 'Ð' && str[i - 1] != 'Ñ' && str[i - 1] != 'Ò' && str[i - 1] != 'æ' && str[i - 1] != (char)10)
                        {
                            textBox2.AppendText(ds.Tables[0].Rows[rowIndex][2].ToString());
                        }
                        if (str[i - 1] != (char)10 && str[i - 1] != ' ' && str[i + 1] != ' ' && str[i + 1] != (char)13 && str[i - 1] != 'Ç' && str[i - 1] != 'Â' && str[i - 1] != 'É' && str[i - 1] != 'Ã' && str[i - 1] != 'Å' && str[i - 1] != 'Ï' && str[i - 1] != 'Ð' && str[i - 1] != 'Ñ' && str[i - 1] != 'Ò' && str[i - 1] != 'æ' && str[i] != 'Ç' && str[i] != 'Â' && str[i] != 'É' && str[i] != 'Ã' && str[i] != 'Å' && str[i] != 'Ï' && str[i] != 'Ð' && str[i] != 'Ñ' && str[i] != 'Ò' && str[i] != 'æ')
                        {
                            textBox2.AppendText(ds.Tables[0].Rows[rowIndex][3].ToString());
                        }
                        if ((str[i - 1] == (char)10 || str[i - 1] == ' ' || str[i - 1] == 'Ç' || str[i - 1] == 'Â' || str[i - 1] == 'É' || str[i - 1] == 'Ã' || str[i - 1] == 'Å' || str[i - 1] == 'Ï' || str[i - 1] == 'Ð' || str[i - 1] == 'Ñ' || str[i - 1] == 'Ò' || str[i - 1] == 'æ') && str[i + 1] != ' ' && str[i + 1] != (char)13 && str[i] != 'Ç' && str[i] != 'Â' && str[i] != 'É' && str[i] != 'Ã' && str[i] != 'Å' && str[i] != 'Ï' && str[i] != 'Ð' && str[i] != 'Ñ' && str[i] != 'Ò' && str[i] != 'æ')
                        {
                            textBox2.AppendText(ds.Tables[0].Rows[rowIndex][4].ToString());
                        }
                    }
                }
                else
                {
                    textBox2.Clear();

                    foreach (char c in textBox1.Text)
                    {
                        if (c == ' ')
                        {
                            textBox2.AppendText(" "); continue;
                        }
                        if (c == '\n')
                        {
                            textBox2.AppendText("\n"); continue;
                        }
                        if (c == (char)13)
                        {
                            textBox2.AppendText(((char)13).ToString());
                        }

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            for (int j = 1; j < ds.Tables[0].Columns.Count; j++)
                            {
                                if (c.ToString() == ds.Tables[0].Rows[i][j].ToString())
                                {
                                    textBox2.AppendText(ds.Tables[0].Rows[i][0].ToString()); break;
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }

        private void Up_Side_Down_Click(object sender, EventArgs e)
        {
            try
            {
                string str = textBox2.Text + (char)10;

                int counter = 0;
                foreach (char c in str)
                    if (c == (char)10)
                        counter++;

                string[] ar = new string[counter];

                int p = 0;

                for (int i = 0; i < str.Length; i++)
                {
                    if (str[i] != (char)10)
                    {
                        ar[p] += str[i];
                    }
                    else
                    {
                        p++;

                        if (p != counter)
                            ar[p] = "";
                    }
                }

                string st = null;
                textBox2.Clear();
                for (int i = 0; i < counter; i++)
                {
                    if (i < counter - 1)
                    {
                        for (int j = ar[i].Length - 2; j > -1; j--)
                        {
                            st += ar[i][j];
                        }
                    }
                    else
                    {
                        for (int j = ar[i].Length - 1; j > -1; j--)
                        {
                            st += ar[i][j];
                        }
                    }
                    textBox2.AppendText(st);
                    st = "";
                    if (i < counter - 1)
                    {
                        textBox2.AppendText(((char)13).ToString());
                        textBox2.AppendText(((char)10).ToString());
                    }
                }
            }
            catch
            {
            }
        }

        private void ar_to_en_Click(object sender, EventArgs e)
        {
            try
            {
                ar_to_en.Enabled = false;
                en_to_ar.Enabled = true;
                textBox1.Focus();
                check = true;  
                textBox1.TextAlign = HorizontalAlignment.Left;
                textBox2.TextAlign = HorizontalAlignment.Left;
                textBox1.RightToLeft = RightToLeft.Yes;
                textBox2.RightToLeft = RightToLeft.No;
                InputLanguage.CurrentInputLanguage = GetArabicInputLanguage();
                textBox2.Clear();
            }
            catch
            {
            }
        }

        private void en_to_ar_Click(object sender, EventArgs e)
        {
            try
            {
                ar_to_en.Enabled = true;
                en_to_ar.Enabled = false;
                textBox1.Focus();
                check = false;
                textBox1.TextAlign = HorizontalAlignment.Left;
                textBox2.TextAlign = HorizontalAlignment.Left;
                textBox1.RightToLeft = RightToLeft.No;
                textBox2.RightToLeft = RightToLeft.Yes;
                InputLanguage.CurrentInputLanguage = GetEnglishInputLanguage();
                textBox2.Clear();
            }
            catch
            {
            }
        }

        private void Us_Click(object sender, EventArgs e)
        {
            try
            {
                ap = Audio.FromFile(Music_path());
                ap.Stop();
                ap.Play();
                Us us = new Us();
                us.ShowDialog();
            }
            catch
            {
            }
        }

        public string Music_path()
        {
            string path = Application.ExecutablePath;

            char[] ar = new char[] { '\\' };
            string[] str = path.Split(ar);
            string Music_path = null;
            for (int i = 0; i < str.Length - 1; i++)
                Music_path += str[i] + "\\";
            Music_path += "Music.mp3";
            return Music_path;
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                textBox1.SelectAll();
            }  
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                textBox2.SelectAll();
            }
        }  

        public static InputLanguage GetArabicInputLanguage()
        {
            foreach (InputLanguage intputLang in InputLanguage.InstalledInputLanguages)
                if (intputLang.Culture.EnglishName.StartsWith("Arabic"))
                    return intputLang;
            return null;
        }

        public static InputLanguage GetEnglishInputLanguage()
        {
            foreach (InputLanguage intputLang in InputLanguage.InstalledInputLanguages)
                if (intputLang.Culture.EnglishName.StartsWith("English"))
                    return intputLang;
            return null;
        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Label();
        }

        public static void Label()
        {
            Process.Start("www.2DQ8.com");
        }
    }
}