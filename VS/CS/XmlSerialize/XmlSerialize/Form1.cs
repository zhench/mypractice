using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XmlSerialize
{
    public partial class Form1 : Form
    {
        private const string File_Path = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonEdit1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog()==DialogResult.OK)
            {
            }
            
        }
    }
}
