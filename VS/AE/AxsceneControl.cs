using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Output;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Animation;
using ESRI.ArcGIS.DataSourcesRaster;
using stdole;
using System.Text.RegularExpressions;
 
 
namespace ESRI项目开发竞赛
{
    public partial class FrcSence : Form
    {
        public FrcSence（）
        {
            InitializeComponent（）;
        }
 
        #region //全局变量
        public Boolean scenePan = false;
        public int clickSceneTime = 0;
        public IPoint scenePanPoints1 = new PointClass（）;
        public IPoint scenePanPoints2 = new PointClass（）;
        public IPoint CameraObs = new PointClass（）;
        public IAnimationTrack Playtrl = new AnimationTrackClass（）;
        public Boolean CreaterKeyFrameSwitch = false;
        public int KeyIndex;
        //public  AxSceneControl paxSceneControl;
        public double startHeight;
        public double endHeight;
        public double nowHeight;
        //private ILegendClass pLegendClass;
        //private ILayer pLayer;
        public ISymbol pSymbol;
        public Image pSymbolImage;
        //private bool p;
        private ILayer TOCRightLayer;
        public ISceneControl mSceneControl;
        //FrmIdentify pFrmIdentify = new FrmIdentify（）;
        public Boolean pIdnetifyIsOrNot;
        //public IScene pScene;
        #endregion
 
        #region //菜单功能
        /// <summary>
        /// 打开Raster文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 打开Raster文件ToolStripMenuItem_Click（object sender， EventArgs e）
        {
            try
            {
                openFileDialog1 = new OpenFileDialog（）;
                openFileDialog1.Title = "添加raster数据";
                openFileDialog1.Filter = "TIFF格式（*.tif）｜*.tif｜Img格式（*.img）｜*.img｜Bmp格式（*.bmp）｜*.bmp｜Jpeg格式（*.jpg）｜*.jpg";
                openFileDialog1.ShowDialog（）;
 
                string sFilePath = openFileDialog1.FileName;
                IRasterLayer pRaster;
                pRaster = new RasterLayerClass（）;
                pRaster.CreateFromFilePath（sFilePath）;
                axSceneControl1.Scene.AddLayer（pRaster， true）;
 
            }
            catch （Exception ex）
            {
                MessageBox.Show（ex.ToString（））;
            }
        }
 
        /// <summary>
        /// 打开Feature文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 打开Feature文件ToolStripMenuItem_Click（object sender， EventArgs e）
        {
            try
            {
                OpenFileDialog pOpenfile = new OpenFileDialog（）;
                pOpenfile.Title = "添加shapefile文件";
                pOpenfile.Filter = "*（.shp）｜*.shp";
                pOpenfile.ShowDialog（）;
 
                ILayerFactoryHelper pLayerFactoryHelper = new LayerFactoryHelperClass（）;
                IFileName filename = new FileNameClass（）;
                filename.Path = pOpenfile.FileName;
                IEnumLayer enumlayer = pLayerFactoryHelper.CreateLayersFromName（filename as IName）;
                ILayer layer;
                enumlayer.Reset（）;
                layer = enumlayer.Next（）;
                while （layer ！= null）
                {
                    axSceneControl1.SceneGraph.Scene.AddLayer（layer， false）;
                    layer = enumlayer.Next（）;
                    axSceneControl1.SceneGraph.RefreshViewers（）;
                }
            }
            catch
            {
                return;
            }
        }
 
        /// <summary>
        /// 打开TIN文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 打开TIN文件ToolStripMenuItem_Click（object sender， EventArgs e）
        {
            //string dirName;
            //ILayer pLayer;
            //FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog（）;
            //if （folderBrowserDialog1.ShowDialog（） == DialogResult.OK）
            //{
 
            //    dirName = folderBrowserDialog1.SelectedPath;
 
            //    pLayer = AddData.openTinLayer（dirName）;
 
            //    if （pLayer ！= null）
            //    {
            //        //axMapControl1.AddLayer（pLayer， 0）;
            //        axSceneControl1.Scene.AddLayer（pLayer， 0）;
 
            //    }
 
            //}
 
 
            try
            {
                 FolderBrowserDialog  openFileDialog1 = new FolderBrowserDialog（）;
                //openFileDialog1.Title = "添加TIN数据";
                //openFileDialog1.Filter = "TIFF格式（*.tif）｜*.tif｜Img格式（*.img）｜*.img｜Bmp格式（*.bmp）｜*.bmp｜Jpeg格式（*.jpg）｜*.jpg";
                openFileDialog1.ShowDialog（）;
 
                string sFilePath;
                sFilePath = openFileDialog1.SelectedPath;
               
                ITin pTIN = new Tin3DPropertiesClass（） as ITin ;
                ITinLayer pTINLyr = new TinLayerClass（）;
                pTINLyr.Dataset = pTIN;
                //pTINLyr.
                axSceneControl1.Scene.AddLayer（pTINLyr）;
                axSceneControl1.Refresh（）;
 
            }
            catch （Exception ex）
            {
                MessageBox.Show（ex.ToString（））;
            }
 
            //ITin pTIN = new pTIN（）;
            //ITinLayer pTINLyr = new TinLayerClass（）;
            //pTINLyr.Dataset = pTIN;
            //axSceneControl1.Scene.AddLayer（pTINLyr）;
 
        }
 
        /// <summary>
        /// 保存场景图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 保存场景图片ToolStripMenuItem_Click（object sender， EventArgs e）
        {
            try
            {
                string sFileName;
                SaveFileDialog pSaveFile = new SaveFileDialog（）;
                pSaveFile.Title = "保存图片";
                pSaveFile.Filter = "BMP图片（*.bmp）｜*.bmp｜JPEG图片（*.jpg）｜*.jpg｜TIF图片（*.tif）｜*.tif";
                pSaveFile.ShowDialog（）;
 
                sFileName = pSaveFile.FileName;
                if （pSaveFile.FilterIndex == 1）
                {
                    axSceneControl1.SceneViewer.GetScreenShot（esri3DOutputImageType.BMP， sFileName）;
                }
                else
                    if （pSaveFile.FilterIndex == 2）
                    {
                        axSceneControl1.SceneViewer.GetScreenShot（esri3DOutputImageType.JPEG， sFileName）;
                    }
                MessageBox.Show（"成功保存图片至:" ＋ sFileName）;
            }
            catch
            {
                MessageBox.Show（"出现错误返回"）;
            }
        }
 
        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 退出ToolStripMenuItem_Click（object sender， EventArgs e）
        {
 
        }
        #endregion
 
        #region //基本操作
        //放大
        private void button1_Click（object sender， EventArgs e）
        {
            axSceneControl1.Camera.Zoom（0.9）;
            axSceneControl1.Refresh（）;
        }
 
        //缩小
        private void button2_Click（object sender， EventArgs e）
        {
            axSceneControl1.Camera.Zoom（1.1）;
            axSceneControl1.Refresh（）;
        }
 
        //漫游
        private void button3_Click（object sender， EventArgs e）
        {
            axSceneControl1.Navigate = true;
        }
 
        //平移
        private void button4_Click（object sender， EventArgs e）
        {
            scenePan = true;
        }
 
        //窗体事件
        private void axSceneControl1_OnMouseDown（object sender， ESRI.ArcGIS.Controls.ISceneControlEvents_OnMouseDownEvent e）
        {
            //鼠标漫游
            if （scenePan）
            {
                //if （CreaterKeyFrameSwitch）
                //{
                //    CreateKeyFrame（axSceneControl1.Scene， Playtrl， KeyIndex）;
 
                //    KeyIndex = KeyIndex ＋ 1;
                //}
 
                if （clickSceneTime == 0）
                {
                    scenePanPoints1.PutCoords（e.x， e.y）;
                    clickSceneTime = 1;
                }
 
                else if （clickSceneTime == 1）
                {
                    scenePanPoints2.PutCoords（e.x， e.y）;
                    axSceneControl1.Camera.Pan（scenePanPoints1， scenePanPoints2）;
                    axSceneControl1.Refresh（）;
                    clickSceneTime = 0;
                    scenePan = false;
                }           
            }
            //鼠标点击查询
            if （pIdnetifyIsOrNot == true）
            {
                IHit3DSet pHit3DSet;
                axSceneControl1.SceneGraph.LocateMultiple（axSceneControl1.SceneGraph.ActiveViewer， e.x， e.y， esriScenePickMode.esriScenePickAll， false， out pHit3DSet）;
                pHit3DSet.OnePerLayer（）;
                if （pHit3DSet.Hits.Count == 0）
                {
                    MessageBox.Show（"当前点未能查找到任何要素"）;
                }
                IHit3D pHit3D = pHit3DSet.Hits.get_Element（0） as IHit3D;
                pIdnetifyIsOrNot = false;
                MessageBox.Show（"X =" ＋ pHit3D.Point.X ＋ "，Y=" ＋ pHit3D.Point.Y ＋ "， Z =" ＋ pHit3D.Point.Z）;
            }
 
            if （e.button == 2）
            {
                contextMenuSence.Show（this.axSceneControl1， e.x， e.y）;
            }
        }
 
        //自动旋转
        private void checkBox1_CheckedChanged（object sender， EventArgs e）
        {
            if （checkBox1.Checked）
            {
                timer1.Enabled = true;
            }
            else
            {
                timer1.Enabled = false;
            }
        }
 
        //自动旋转的速率控制
        private void timer1_Tick（object sender， EventArgs e）
        {
            axSceneControl1.Camera.Rotate（ Convert.ToDouble（comboBox2.Text））;
            axSceneControl1.Refresh（）;
        }
 
        #endregion
 
        #region //方向控制
        //上移
        private void button9_Click（object sender， EventArgs e）
        {
            axSceneControl1.Camera.Move（esriCameraMovementType.esriCameraMoveUp， 0.01）;
            axSceneControl1.Refresh（）;
        }
 
        //左移
        private void button10_Click（object sender， EventArgs e）
        {
            axSceneControl1.Camera.Move（esriCameraMovementType.esriCameraMoveLeft， 0.01）;
            axSceneControl1.Refresh（）;
        }
 
        //右移
        private void button12_Click（object sender， EventArgs e）
        {
            axSceneControl1.Camera.Move（esriCameraMovementType.esriCameraMoveRight， 0.01）;
            axSceneControl1.Refresh（）;
        }
 
        //下移
        private void button11_Click（object sender， EventArgs e）
        {
            axSceneControl1.Camera.Move（esriCameraMovementType.esriCameraMoveDown， 0.01）;
            axSceneControl1.Refresh（）;
        }
 
        //前进
        private void button13_Click（object sender， EventArgs e）
        {
            axSceneControl1.Camera.Move（esriCameraMovementType.esriCameraMoveAway， 0.01）;
            axSceneControl1.Refresh（）;
        }
 
        //后退
        private void button14_Click（object sender， EventArgs e）
        {
            axSceneControl1.Camera.Move（esriCameraMovementType.esriCameraMoveToward， 0.01）;
            axSceneControl1.Refresh（）;
        }
 
        //点击查询    
        //public Boolean pIdnetifyIsOrNot;
        private void button15_Click（object sender， EventArgs e）
        {
            pIdnetifyIsOrNot = true;
        }
 
        #endregion
 
        #region //视屏输出
        //保存视频文件
        private void button16_Click（object sender， EventArgs e）
        {
            try
            {
 
                SaveFileDialog saveVedioFile = new SaveFileDialog（）;
                saveVedioFile.Filter = "视屏文件（*.avi）｜*.avi";
                saveVedioFile.Title = "输出AVI文件";
                saveVedioFile.ShowDialog（）;
 
                ISceneExporter3d p3Dexporter = new AVIExporterClass（）;
                p3Dexporter.ExportFileName = saveVedioFile.FileName;
 
                ISceneVideoExporter pExporter;
                pExporter = p3Dexporter as ISceneVideoExporter;
                pExporter.Viewer = axSceneControl1.Scene.SceneGraph.ActiveViewer;
 
                pExporter.VideoDuration =100 * Convert.ToDouble（comboBox6.Text）;
                pExporter.FrameRate = 10 * trackBar1.Value;
                IAVIExporter pAVIExporter;
                pAVIExporter = p3Dexporter as IAVIExporter;
                pAVIExporter.Quality = 50 * trackBar2.Value;
 
                p3Dexporter.ExportScene（axSceneControl1.Scene）;
                MessageBox.Show（"输出AVI视屏完成"）;
            }
            catch
            {
                return;
            }
        }
 
        #endregion
 
        #region //TOC右键功能
 
        private void axTOCControl1_OnMouseDown（object sender， ITOCControlEvents_OnMouseDownEvent e）
        {
            esriTOCControlItem item = esriTOCControlItem.esriTOCControlItemNone;
            IBasicMap map = null; ILayer layer = null;
            object other = null; object index = null;
            axTOCControl1.HitTest（e.x， e.y， ref item， ref map， ref layer， ref other， ref index）;
            axSceneControl1.CustomProperty = layer;
            if （e.button == 2）
            {
                switch （item）
                {
                    case esriTOCControlItem.esriTOCControlItemLayer:
                        this.TOCRightLayer = layer;
                        //this.contextMenuFeaturLayer.Show（this.axTOCControl1， e.x， e.y）;
                        contextMenuSencelayers.Show（this.axTOCControl1， e.x， e.y）;
                       
 
                        break;
                }
            }
        }
 
        //全屏显示
        private void 全屏ToolStripMenuItem_Click（object sender， EventArgs e）
        {
            //axSceneControl1.Scene.Extent = axSceneControl1.Scene.get_Layer（0）;
        }
       
        //右键删除
        private void 删除ToolStripMenuItem_Click（object sender， EventArgs e）
        {
            this.axSceneControl1.Scene.DeleteLayer（this.TOCRightLayer）;
            this.axSceneControl1.Refresh（）;
        }
 
        #endregion
 
        #region //功能实现
       
        //拉伸显示
        private void button8_Click（object sender， EventArgs e）
        {
            try
            {
                ISceneGraph scenegraph = axSceneControl1.SceneGraph;
                IScene scene = scenegraph.Scene;
                ILayer layer = scene.get_Layer（0）;
                IRasterLayer pLayer = layer as IRasterLayer;
                IRasterSurface pSurface = new RasterSurface（）;
                IRaster raster = （IRaster）pLayer.Raster;
                IRasterBandCollection rasterbands = raster as IRasterBandCollection;
                IRasterBand rasterband = rasterbands.Item（0）;
                pSurface.RasterBand = rasterband;
                ISurface surface = pSurface as ISurface;
                ILayerExtensions layerextensions = layer as ILayerExtensions;
                I3DProperties properties = new Raster3DPropertiesClass（）;
                object p3d;
                for （int i = 0; i < layerextensions.ExtensionCount; i＋＋）
                {
                    p3d = layerextensions.get_Extension（i）;
                    if （p3d ！= null）
                    {
                        properties = （I3DProperties）p3d;
                        break;
                    }
                }
                properties.ZFactor = Convert.ToDouble（comboBox7.Text）;
                properties.BaseOption = esriBaseOption.esriBaseSurface;
                properties.BaseSurface = surface;         
                properties.Apply3DProperties（layer）;      
                axSceneControl1.SceneGraph.RefreshViewers（）;
            }
            catch
            {
                return;
            }
        }
 
        //地形渲染
        private void button7_Click（object sender， EventArgs e）
        {
            try
            {
                IScene scene = axSceneControl1.Scene;
                ILayer layer = scene.get_Layer（0）;
                IRasterLayer pRasterLayer = layer as IRasterLayer;     
                IRaster raster = （IRaster）pRasterLayer.Raster;
                IRasterBandCollection rasterbands = raster as IRasterBandCollection;
                IRasterBand rasterband = rasterbands.Item（0）;           
                IRasterStretchColorRampRenderer pRStretchRender = new RasterStretchColorRampRendererClass（）;
                //创建两个起始颜色
                IRgbColor pFromRgbColor = new RgbColorClass（）;
                pFromRgbColor.Red = 40;
                pFromRgbColor.Green = 30;
                pFromRgbColor.Blue = 250;
                IRgbColor pToRgbColor = new RgbColorClass（）;
                pToRgbColor.Red = 240;
                pToRgbColor.Green = 80;
                pToRgbColor.Blue = 40;
 
                //创建起止颜色带
                IAlgorithmicColorRamp pAlgorithmicColorRamp = new AlgorithmicColorRampClass（）;
                pAlgorithmicColorRamp.Size = 255;
                pAlgorithmicColorRamp.FromColor = pFromRgbColor as IColor;
                pAlgorithmicColorRamp.ToColor = pToRgbColor as IColor;
                bool btrue = true;
                pAlgorithmicColorRamp.CreateRamp（out btrue）;
                //选择拉伸颜色带符号化的波段
                pRStretchRender.BandIndex = 1;
                //pRStretchRender.BandIndex = pRasterLayer.BandCount;
                //设置拉伸颜色带符号化所采用的颜色带
                pRStretchRender.ColorRamp = pAlgorithmicColorRamp as IColorRamp;
                IRasterRenderer pRasterRender = pRStretchRender as IRasterRenderer;
                pRasterLayer = scene.get_Layer（0） as IRasterLayer;
                pRasterRender.Raster = pRasterLayer as IRaster;
                pRasterRender.Update（）;
                //符号化RasterLayer
                pRasterLayer.Renderer = pRasterRender;
                //渲染的刷新
                axSceneControl1.Scene.SceneGraph.Invalidate（pRasterLayer， true， false）;
                axSceneControl1.SceneViewer.Redraw（true）;
                axSceneControl1.Scene.SceneGraph.RefreshViewers（）;
                axTOCControl1.ActiveView.PartialRefresh（esriViewDrawPhase.esriViewForeground， pRasterLayer， axSceneControl1.Scene.Extent）;
                axTOCControl1.Update（）;
            }
            catch （Exception Err）
            {
                MessageBox.Show（Err.Message， "提示"， MessageBoxButtons.OK， MessageBoxIcon.Information）;
 
            }
        }
 
        //水面渲染
        private void button6_Click（object sender， EventArgs e）
        {
            try
            {
 
                ISceneGraph scenegraph = axSceneControl1.SceneGraph;
                IScene scene = scenegraph.Scene;
                ILayer layer = scene.get_Layer（1）;
                IRasterLayer pRasterLayer = layer as IRasterLayer;
                IRaster raster = （IRaster）pRasterLayer.Raster;
                IRasterBandCollection rasterbands = raster as IRasterBandCollection;
                IRasterBand rasterband = rasterbands.Item（1）;
                IRasterStretchColorRampRenderer pRStretchRender = new RasterStretchColorRampRendererClass（）;
                IRasterRenderer pRasterRender = pRStretchRender as IRasterRenderer;         
                pRasterLayer = scene.get_Layer（1） as IRasterLayer;     
                pRasterRender.Raster = pRasterLayer as IRaster;
                pRasterRender.Update（）;
 
                //创建两个起始颜色
                IRgbColor pFromRgbColor = new RgbColorClass（）;
                pFromRgbColor.Red = 80;
                pFromRgbColor.Green = 80;
                pFromRgbColor.Blue = 255;
                IRgbColor pToRgbColor = new RgbColorClass（）;
                pFromRgbColor.Red = 80;
                pFromRgbColor.Green = 80;
                pFromRgbColor.Blue = 255;
 
                //创建起止颜色带
                IAlgorithmicColorRamp pAlgorithmicColorRamp = new AlgorithmicColorRampClass（）;
                pAlgorithmicColorRamp.Size = 255;
                pAlgorithmicColorRamp.FromColor = pFromRgbColor as IColor;
                pAlgorithmicColorRamp.ToColor = pToRgbColor as IColor;
                bool btrue = true;
                pAlgorithmicColorRamp.CreateRamp（out btrue）;
              
                //选择拉伸颜色带符号化的波段
                pRStretchRender.BandIndex = 1;
     
                //设置拉伸颜色带符号化所采用的颜色带
                pRStretchRender.ColorRamp = pAlgorithmicColorRamp as IColorRamp;
                pRasterRender.Update（）;
                //符号化RasterLayer
                pRasterLayer.Renderer = pRasterRender;
                axSceneControl1.SceneGraph.RefreshViewers（）;
                        
                //渲染的刷新
                axSceneControl1.Scene.SceneGraph.Invalidate（pRasterLayer， true， false）;
                axSceneControl1.SceneViewer.Redraw（true）;
                axSceneControl1.Scene.SceneGraph.RefreshViewers（）;
                axTOCControl1.ActiveView.PartialRefresh（esriViewDrawPhase.esriViewForeground， pRasterLayer， axSceneControl1.Scene.Extent）;
                axTOCControl1.Update（）;
 
            }
            catch （Exception Err）
            {
                MessageBox.Show（Err.Message， "提示"， MessageBoxButtons.OK， MessageBoxIcon.Information）;
 
            }
        }
 
        //淹没演示
        private void button5_Click（object sender， EventArgs e）
        {
            try
            {
                IRasterLayer pRasterLayer = new RasterLayerClass（）;
                pRasterLayer = axSceneControl1.Scene.get_Layer（1） as IRasterLayer;
                IRaster pRaster = pRasterLayer.Raster;
 
                IRasterLayer mRasterLayer = new RasterLayerClass（）;
                mRasterLayer = axSceneControl1.Scene.get_Layer（0） as IRasterLayer;
                IRaster mRaster = mRasterLayer.Raster;         
               
                double P = RasterStatistics（mRaster）;
                startHeight = P;
           
                nowHeight = startHeight;
                bool y = Regex.IsMatch（comboBox5.Text，＠"[0－9]*[1－9][0－9]*＄"）;
                if （y）
                {
                    endHeight = Convert.ToDouble（comboBox5.Text） ＋ P;
                 
                    timer2.Enabled = true;
                    
                    P = nowHeight;
                    axSceneControl1.SceneGraph.RefreshViewers（）;
                }
                else
                {
                    MessageBox.Show（"请正确输入！"）;
                }
 
            }
            catch
            {
                return;
            }
        }
 
        //取栅格的最小值
        public double RasterStatistics（IRaster pInRaster）
        {
 
            IRasterBandCollection pRasterBands = pInRaster as IRasterBandCollection;
            IRasterBand pRasterBand = null;
            double dZMin = 0;
            if （pRasterBands.Count > 0）
            {
                pRasterBand = pRasterBands.Item（0）;
                IRasterStatistics pRasterStat = pRasterBand.Statistics;
                //double dZMax = pRasterStat.Maximum;
                dZMin = pRasterStat.Minimum;
                //lblZMax.Text = dZMax.ToString（）;
                return dZMin;
            }
            return dZMin;
        }
 
        //水淹时间控制
        private void timer2_Tick（object sender， EventArgs e）
        {
            try
            {
                IRasterLayer pRasterLayer = axSceneControl1.Scene.get_Layer（1） as IRasterLayer;
                I3DProperties pI3DProperties = new Raster3DPropertiesClass（）;
                ILayerExtensions p = pRasterLayer as ILayerExtensions;
                object pp;
                int i;
                //timer2.Enabled =true;
                for （i = 0; i <= p.ExtensionCount － 1; i＋＋）
                {
                    pp = p.get_Extension（i）;
                    if （pp ！= null）
                    {
                        pI3DProperties = p.get_Extension（i） as I3DProperties;
                    }
                    axSceneControl1.Refresh（）;
                }
 
                pI3DProperties.BaseOption = esriBaseOption.esriBaseExpression;
                nowHeight = nowHeight ＋ 1;
                startHeight = nowHeight;
                pI3DProperties.BaseExpressionString = nowHeight.ToString（）;
                //pI3DProperties.MaxRasterColumns = nowHeight.ToString（）;
                pI3DProperties.Apply3DProperties（pRasterLayer）;
                axSceneControl1.SceneGraph.RefreshViewers（）;
                if （nowHeight >= endHeight）
                {
                    timer2.Enabled = false;
                    //p =bool（false ）;
                    //axSceneControl1.SceneGraph.RefreshViewers（）;
                }
            }
            catch
            {
                return;
            }
        }
 
        #endregion
 
        #region // Scene 右键功能
 
        private void 全屏ToolStripMenuItem1_Click（object sender， EventArgs e）
        {
            //axSceneControl1.Camera = axSceneControl1.Scene.SceneGraph.Extent as ICamera ;
        }
 
        private void 漫游ToolStripMenuItem_Click（object sender， EventArgs e）
        {
            axSceneControl1.Navigate = true;
        }
 
        private void 放大ToolStripMenuItem_Click（object sender， EventArgs e）
        {
            axSceneControl1.Camera.Zoom（0.5）;
            axSceneControl1.Refresh（）;
            //ControlsSceneZoomInTool ScenePan = new ControlsSceneZoomInTool（）;
            //ScenePan.OnCreate（axSceneControl1.Object）;
            //axSceneControl1.CurrentTool = ScenePan as ITool;
        }
 
        private void 缩小ToolStripMenuItem_Click（object sender， EventArgs e）
        {
            axSceneControl1.Camera.Zoom（2.0）;
            axSceneControl1.Refresh（）;
            //ControlsSceneZoomOutTool SP = new ControlsSceneZoomOutTool（）;
            //SP.OnCreate（axSceneControl1.Object）;
            //axSceneControl1.CurrentTool = SP as ITool;
        }
 
        private void 属性ToolStripMenuItem1_Click（object sender， EventArgs e）
        {
            pIdnetifyIsOrNot = true;
        }
 
        private void 旋转ToolStripMenuItem_Click（object sender， EventArgs e）
        {
            axSceneControl1.Camera.Rotate（0）;
        }
 
        private void 刷新ToolStripMenuItem_Click（object sender， EventArgs e）
        {
            axSceneControl1.SceneGraph.RefreshViewers（）;
        }
        #endregion