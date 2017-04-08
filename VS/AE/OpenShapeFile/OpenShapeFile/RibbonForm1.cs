using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using System.IO;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesFile;

namespace OpenShapeFile
{
    public partial class RibbonForm1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        IWorkspaceFactory m_pWorkspaceFactory;
        IFeatureWorkspace m_pFeatureWorkspace;
        IFeatureLayer m_pFeatureLayer;
        public RibbonForm1()
        {
            InitializeComponent();
        }

        private void ribbon_Click(object sender, EventArgs e)
        {

        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                string path = fbd.SelectedPath;
                String[] files = Directory.GetFiles(path);
                if (files.Length>0)
                {
                    m_pWorkspaceFactory = new ShapefileWorkspaceFactory();
                    m_pFeatureWorkspace = (IFeatureWorkspace)m_pWorkspaceFactory.OpenFromFile(path, 0);
                    List<string> lstFileNames = new List<string>();
                    foreach (var file in files)
                    {
                        m_pFeatureLayer = new FeatureLayerClass();
                        int index= file.LastIndexOf('\\');                        
                        string filename = file.Substring(index+1);
                        if (filename.Substring(filename.Length-3)!="shp")
                        {
                            continue;
                        }
                        m_pFeatureLayer.FeatureClass = m_pFeatureWorkspace.OpenFeatureClass(filename);
                        m_pFeatureLayer.Name = m_pFeatureLayer.FeatureClass.AliasName;
                        axMapControl1.Map.AddLayer(m_pFeatureLayer);
                        axMapControl1.ActiveView.Refresh();
                    }
                }
            }
        }
    }
}