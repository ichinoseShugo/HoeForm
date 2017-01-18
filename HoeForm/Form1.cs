using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Kinect;
using WiimoteLib;
using System.IO;

namespace HoeForm
{
    public partial class Form1 : Form
    {
        private KinectSensor kinect = null;
        Wiimote wm = new Wiimote();

        static public DateTime dt = DateTime.Now;//日付
        static string pathDoc = Environment.GetFolderPath(Environment.SpecialFolder.Personal);//マイドキュメントのパス
        static string path = null;
        StreamWriter swk;//kinectファイル書き込み用
        StreamWriter sww;//wiiファイル書き込み用

        int frameKinect = 0;//フレームカウント
        int frameWii = 0;
        private List<Bitmap> bmp = new List<Bitmap>();
        private List<byte[]> color = new List<byte[]>();
        private List<int> frameList = new List<int>();
        byte[] colorData = new byte[640 * 480 * 4];

        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            InitFile();
            InitKinect();
            InitWii();
        }

        private void InitFile()
        {
            //ファイル書き込み用のdirectoryを用意
            string date = dt.Year + digits(dt.Month) + digits(dt.Day) + digits(dt.Hour) + digits(dt.Minute);
            path = pathDoc + "/HoeData/" + date;
            Directory.CreateDirectory(path);
            Directory.CreateDirectory(path + "/bmp");
            //ファイルを用意
            //frameKinect,frameWii,x,y,z
            swk = new StreamWriter(path + "/Kinect.csv", false);
            //frameWii,frameKinect,x,y,z
            sww = new StreamWriter(path + "/Wii.csv", false);
        }

        private void InitKinect()
        {
            try
            {
                if (KinectSensor.KinectSensors.Count == 0)
                    throw new Exception("Kinectが接続されていません");

                // Kinectセンサーの取得
                kinect = KinectSensor.KinectSensors[0];

                //スケルトン平滑化パラメータ
                TransformSmoothParameters parameters = new TransformSmoothParameters();
                parameters.Smoothing = 0.2f;
                parameters.Correction = 0.8f;
                parameters.Prediction = 0.0f;
                parameters.JitterRadius = 0.5f;
                parameters.MaxDeviationRadius = 0.5f;

                //color,skeletonの有効化(引数は解像度とフレームレート)
                kinect.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);
                kinect.SkeletonStream.Enable(parameters);

                //カラーフレームのイベントハンドラの登録
                kinect.ColorFrameReady += ColorImageReady;

                //スケルトンフレームのイベントハンドラの登録
                kinect.SkeletonFrameReady += SkeletonFrameReady;

                //Kinectセンサーからのストリーム取得を開始、以降、Kinectランタイムからフレーム毎に登録したColorFrameReadyメソッドが呼び出される
                kinect.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();
            }
        }

        private void InitWii()
        {
            wm.WiimoteChanged += wm_WiimoteChanged;//イベント登録
            wm.SetReportType(InputReport.ButtonsAccel, true);//イベント呼び出しタイプの設定
            wm.Connect();//Wiiリモコン接続
        }

        private void wm_WiimoteChanged(object sender, WiimoteChangedEventArgs e)
        {
            WiimoteState ws = e.WiimoteState;
            //ファイル書き込み
            if (RecordWii.Checked)
                sww.WriteLine(frameWii + "," + frameKinect + ","+ ws.AccelState.Values.X + "," + ws.AccelState.Values.Y + "," + ws.AccelState.Values.Z);
            //Formを書き換え
            if (RecordWii.Checked)
            {
                ax.Text = "x軸:" + ws.AccelState.Values.X;
                ay.Text = "y軸:" + ws.AccelState.Values.Y;
                az.Text = "z軸:" + ws.AccelState.Values.Z;
            }
            frameWii++;
        }

        private void SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            //スケルトンフレームのデータをskeletonFrame変数に保持
            using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame())
            {
                //スケルトンフレームがnullでないならそのまま
                if (skeletonFrame != null)
                {
                    //人数分のスケルトンデータ変数を配列で作る
                    Skeleton[] skeletonData = new Skeleton[skeletonFrame.SkeletonArrayLength];
                    //配列に各人物のスケルトンフレームのデータをコピー
                    skeletonFrame.CopySkeletonDataTo(skeletonData);

                    //スケルトンを認識した人数の数だけ繰り返す
                    foreach (var skeleton in skeletonData)
                    {
                        //その人物のスケルトンがTracked状態なら続ける
                        if (skeleton.TrackingState == SkeletonTrackingState.Tracked)
                        {
                            jointNum.Text = "JointNum:" + skeleton.Joints.Count;
                            //各ジョイントの数だけ繰り返す
                            foreach (Joint joint in skeleton.Joints)
                            {
                                if (RecordKinect.Checked)
                                    swk.WriteLine(frameKinect + "," + frameWii + ","
                                       + joint.Position.X + ","
                                       + joint.Position.Y + ","
                                       + joint.Position.Z);
                                /*
                                    swc.WriteLine(frameKinect + "," + frameWii + ","
                                       + (skeleton.Position.X - skeleton.Joints[JointType.HipCenter].Position.X) + ","
                                       + (skeleton.Position.Y - skeleton.Joints[JointType.HipCenter].Position.Y) + ","
                                       + (skeleton.Position.Z - skeleton.Joints[JointType.HipCenter].Position.Z));
                                */
                            }
                            if (RecordKinect.Checked)
                                swk.WriteLine(frameKinect + "," + frameWii + ","
                                    + skeleton.Position.X + ","
                                    + skeleton.Position.Y + ","
                                    + skeleton.Position.Z);
                        }
                    }
                }
            }
        }

        private void ColorImageReady(object sender, ColorImageFrameReadyEventArgs e)
        {
            //ColorImageFrameデータを貰う
            using (ColorImageFrame imageFrame = e.OpenColorImageFrame())
            {
                if (imageFrame != null)
                { 
                    imageFrame.CopyPixelDataTo(colorData);
                    unsafe
                    {
                        fixed (byte* ptr = &colorData[0])
                        {
                            Bitmap bmpImage = new Bitmap(
                                        imageFrame.Width,
                                        imageFrame.Height,
                                        imageFrame.BytesPerPixel * imageFrame.Width,
                                        System.Drawing.Imaging.PixelFormat.Format32bppRgb,
                                        (IntPtr)ptr);
                            rgbImage.Image = bmpImage;
                            if (RecordKinect.Checked&&frameKinect%3==0)
                            {
                                //bmp.Add(bmpImage);
                                //color.Add((byte[])colorData.Clone());
                                frameList.Add(frameKinect);
                                bmpImage.Save(path + "/bmp/" + frameKinect + ".bmp", System.Drawing.Imaging.ImageFormat.Bmp);
                            }
                            limit.Text = "ListCount:" + bmp.Count;
                        }
                    }
                }
                frameKinect++;
            }
        }

        private void RecordAll_CheckedChanged(object sender, EventArgs e)
        {
            RecordKinect.CheckState = RecordAll.CheckState;
            RecordWii.CheckState = RecordAll.CheckState;
        }

        public String digits(int date)
        {
            if (date / 10 == 0) return "0" + date;
            else return date.ToString();
        }

        protected override void OnClosed(EventArgs e)
        {
            int cnt = 0;
            if(bmp.Count>=1)
            foreach (var b in bmp)
            {
                b.Save(path + "/bmp/" + frameList[cnt] + ".bmp", System.Drawing.Imaging.ImageFormat.Bmp);
                cnt++;
            }
            
            /*
            foreach (var c in color)
            {
                ImageConverter imgconv = new ImageConverter();
                Image img = (Image)imgconv.ConvertFrom(c);
                img.Save(path + "/bmp/" + frameList[cnt] + ".bmp");
                cnt++;
            }
            */
            swk.Close();
            sww.Close();
            kinect.Stop();
            kinect.Dispose();
        }
    }
}
