using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Shu.FrameWork.Util.Serialize;
using Shu.FrameWork.Test.TestSerialize.Config;
using System.Diagnostics;

namespace Shu.FrameWork.Test.TestSerialize
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

       

        private void buttonEdit1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            ofd.Filter = "配置文件|*.xml";
            ofd.Multiselect = false;
            if (ofd.ShowDialog()==DialogResult.OK)
            {
                buttonEdit1.Text = ofd.FileName;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("dsagd");
            TestConfig tc = TestConfig.Current;            
            tc.AppName = "测大房东试名规划局局的称";
            TestConfig.Read();
        }
    }
}