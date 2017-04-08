using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace DelegateTest
{
    public partial class Form1 : Form
    {
        Thread newthread;
        AutoResetEvent are = new AutoResetEvent(false);
        System.Windows.Forms.Timer tim = new System.Windows.Forms.Timer();
        public Form1()
        {
            InitializeComponent();
        }
        delegate void aa(string s);//创建一个代理
        private void Form1_Load(object sender, EventArgs e)
        {
            tim.Interval = 1;
            tim.Tick += new EventHandler(tim_Tick);
            tim.Start();
            newthread = new Thread(new ThreadStart(ttread));
            newthread.Start();
        }
        private void tim_Tick(object sender, EventArgs e)
        {
            are.Set();
        }
        void ttread()
        {
            //pri("77");

            Loadthread();

        }

        private delegate void ProgressBarShow(int i);
        private void pri(string p)
        {
            Loadthread();
        }

        private void Loadthread()
        {
            string s = string.Empty;
            int b = 0;
            for (; b < 60; b++)
            {
                Thread.Sleep(2000);
                this.ShowPro(b);
                s += b.ToString();
            }
            this.ShowPro(b);
            //MessageBox.Show("同一线程内");
            LoadRichebox(s);
        }
        void send()
        {
            Thread.CurrentThread.Abort();
        }
        private void LoadRichebox(string s)
        {
            if (richTextBox1.InvokeRequired)
            {
                 this.Invoke(new aa(LoadRichebox), s);
            }
            else
            {
                richTextBox1.Text = s;
            }

        }
        private void ShowPro(int value)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new ProgressBarShow(ShowPro), value);
                are.WaitOne();
            }
            else
            {
                this.progressBarControl1.Position = value;
                this.label1.Text = value + "% Processing...";

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (newthread.ThreadState == ThreadState.Aborted)
                {
                    newthread = new Thread(new ThreadStart(ttread));
                    newthread.Start();

                }
                else { newthread.Abort(); }

            }
            catch (System.Exception ex)
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                tim.Stop();

            }
            catch (System.Exception ex)
            {

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                tim.Start();
            }
            catch (System.Exception ex)
            {

            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            newthread.Abort();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            are.Set();
        }

    }
}
