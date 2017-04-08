using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace ThreadTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void TestFunction()
        {
            Thread.Sleep(3000);
            MessageBox.Show("busy");
        }
        private static int num = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            //int num = 0;

            Thread thd = new Thread(() => {
                num++;
                TestFunction();
                num--;
            });
            thd.Start();
            Application.DoEvents();
            while(num!=0){
                Thread.Sleep(10000);
            }
        }
    }
}
