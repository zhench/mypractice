using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;

namespace ExtentUpdated
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void axMapControl1_OnViewRefreshed(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnViewRefreshedEvent e)
        {
            double width = axMapControl1.Extent.Envelope.Width;
        }

        private void axMapControl1_OnExtentUpdated(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnExtentUpdatedEvent e)
        {
            var ww = (e.newEnvelope as IEnvelope).Width;
            double width = axMapControl1.Extent.Envelope.Width;
            var w = axMapControl1.ActiveView.Extent.Envelope.Width;
        }
    }
}
