using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace ThreadWithNoTimer
{
    public partial class Form1 : Form
    {
        Thread th ;
        public Form1()
        {
            InitializeComponent();
            th = new Thread(YourThread);
            th.Start();  
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length >= 4)
            {
                detailCollectedEvent.Set();                      // 当textBox1的文本超过4位，发一个通知  
            }  
        }
        AutoResetEvent detailCollectedEvent = new AutoResetEvent(false);
        void YourThread()
        {
            MessageBox.Show("input you bank account details into the textbox");
            detailCollectedEvent.WaitOne();                      // 等候通知  
            MessageBox.Show("we will keep the secret.");
        }  
    }
}
