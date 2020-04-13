using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using nuitrack;
using nuitrack.issues;
using System.Runtime.InteropServices;

namespace PPD_gui
{
    public partial class Form1 : Form
    {
        private DirectBitmap _bitmap;
        private bool _visualizeColorImage = false;
        private bool _colorStreamEnabled = false;

        private DepthSensor _depthSensor;
        private ColorSensor _colorSensor;
        private UserTracker _userTracker;
        private SkeletonTracker _skeletonTracker;

        private DepthFrame _depthFrame;
        private SkeletonData _skeletonData;
        private IssuesData _issuesData = null;
        private OutputMode mode;
        private OutputMode colorMode;

        private bool Pause = false;
        private bool isStart = false;
        private bool isAccess = false;
        public Form1()
        {
            try
            {
                Nuitrack.Init("");
            }
            catch (System.Exception exception)
            {
                Console.WriteLine("Cannot initialize Nuitrack.");
                throw exception;
            }

            try
            {
                // Create and setup all required modules
                _depthSensor = DepthSensor.Create();
                _colorSensor = ColorSensor.Create();
                _userTracker = UserTracker.Create();
                _skeletonTracker = SkeletonTracker.Create();
            }
            catch (System.Exception exception)
            {
                Console.WriteLine("Cannot create Nuitrack module.");
                throw exception;
            }

            _depthSensor.SetMirror(false);

            // Add event handlers for all modules
            _depthSensor.OnUpdateEvent += onDepthSensorUpdate;
            _colorSensor.OnUpdateEvent += onColorSensorUpdate;
            _userTracker.OnUpdateEvent += onUserTrackerUpdate;
            _skeletonTracker.OnSkeletonUpdateEvent += onSkeletonUpdate;

            Nuitrack.onIssueUpdateEvent += onIssueDataUpdate;

            mode = _depthSensor.GetOutputMode();
            colorMode = _colorSensor.GetOutputMode();

            if (mode.XRes < colorMode.XRes)
                mode.XRes = colorMode.XRes;
            if (mode.YRes < colorMode.YRes)
                mode.YRes = colorMode.YRes;

            _bitmap = new DirectBitmap(mode.XRes, mode.YRes);
            for (int y = 0; y < mode.YRes; ++y)
            {
                for (int x = 0; x < mode.XRes; ++x)
                {
                    _bitmap.SetPixel(x, y, Color.FromKnownColor(KnownColor.Aqua));
                }
            }

            InitializeComponent();
        }

        private void Start_btn_Click(object sender, EventArgs e)
        {   
            // Set fixed form size
            pictureBox1.MinimumSize = pictureBox1.MaximumSize = new Size(mode.XRes, mode.YRes);
            //this.MinimumSize = this.MaximumSize = new Size(mode.XRes, mode.YRes);

            // Disable unnecessary caption bar buttons
            //this.MinimizeBox = this.MaximizeBox = false;

            // Enable double buffering to prevent flicker
            this.DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            // Run Nuitrack. This starts sensor data processing.
            if(!isStart)
            {
                try
                {
                    Nuitrack.Run();
                    isStart = true;
                    State_txt.Text = "Run";
                }
                catch (System.Exception exception)
                {
                    Console.WriteLine("Cannot start Nuitrack.");
                    throw exception;
                }
            }
            else
            {
                Pause = false;
                State_txt.Text = "Run";
                pictureBox1.Refresh();
            }
            
        }
        private void onDepthSensorUpdate(DepthFrame depthFrame)
        {
            if (Pause) return;
            _depthFrame = depthFrame;
        }
        private void onIssueDataUpdate(IssuesData issuesData)
        {
            if (Pause) return;
            _issuesData = issuesData;
        }


        private void onColorSensorUpdate(ColorFrame colorFrame)
        {
            if (Pause) return;

            if (!_visualizeColorImage) return;
            _colorStreamEnabled = true;
            

            float wStep = (float)_bitmap.Width / colorFrame.Cols;
            float hStep = (float)_bitmap.Height / colorFrame.Rows;

            float nextVerticalBorder = hStep;

            Byte[] data = colorFrame.Data;
            int colorPtr = 0;
            int bitmapPtr = 0;
            const int elemSizeInBytes = 3;

            for (int i = 0; i < _bitmap.Height; ++i)
            {
                if (i == (int)nextVerticalBorder)
                {
                    colorPtr += colorFrame.Cols * elemSizeInBytes;
                    nextVerticalBorder += hStep;
                }

                int offset = 0;
                int argb = data[colorPtr]
                    | (data[colorPtr + 1] << 8)
                    | (data[colorPtr + 2] << 16)
                    | (0xFF << 24);
                float nextHorizontalBorder = wStep;
                for (int j = 0; j < _bitmap.Width; ++j)
                {
                    if (j == (int)nextHorizontalBorder)
                    {
                        offset += elemSizeInBytes;
                        argb = data[colorPtr + offset]
                            | (data[colorPtr + offset + 1] << 8)
                            | (data[colorPtr + offset + 2] << 16)
                            | (0xFF << 24);
                        nextHorizontalBorder += wStep;
                    }

                    _bitmap.Bits[bitmapPtr++] = argb;
                }
            }
        }
        
        private void onUserTrackerUpdate(UserFrame userFrame)
        {
            if (Pause) return;
            int size = userFrame.NumUsers;
            UserNums_txt.Text = size.ToString();

            if (_visualizeColorImage && _colorStreamEnabled) return; 

            if (_depthFrame == null)
                return;

            const int MAX_LABELS = 7;
            bool[] labelIssueState = new bool[MAX_LABELS];
            for (UInt16 label = 0; label < MAX_LABELS; ++label)
            {
                labelIssueState[label] = false;
                if (_issuesData != null)
                {
                    FrameBorderIssue frameBorderIssue = _issuesData.GetUserIssue<FrameBorderIssue>(label);
                    labelIssueState[label] = (frameBorderIssue != null);
                }
            }

            float wStep = (float)_bitmap.Width / _depthFrame.Cols;
            float hStep = (float)_bitmap.Height / _depthFrame.Rows;

            float nextVerticalBorder = hStep;

            Byte[] dataDepth = _depthFrame.Data;
            Byte[] dataUser = userFrame.Data;
            int dataPtr = 0;
            int bitmapPtr = 0;
            const int elemSizeInBytes = 2;
            for (int i = 0; i < _bitmap.Height; ++i)
            {
                if (i == (int)nextVerticalBorder)
                {
                    dataPtr += _depthFrame.Cols * elemSizeInBytes;
                    nextVerticalBorder += hStep;
                }

                int offset = 0;
                int argb = 0;
                int label = dataUser[dataPtr] | dataUser[dataPtr + 1] << 8;
                int depth = Math.Min(255, (dataDepth[dataPtr] | dataDepth[dataPtr + 1] << 8) / 32);
                float nextHorizontalBorder = wStep;
                for (int j = 0; j < _bitmap.Width; ++j)
                {
                    if (j == (int)nextHorizontalBorder)
                    {
                        offset += elemSizeInBytes;
                        label = dataUser[dataPtr + offset] | dataUser[dataPtr + offset + 1] << 8;
                        if (label == 0)
                            depth = Math.Min(255, (dataDepth[dataPtr + offset] | dataDepth[dataPtr + offset + 1] << 8) / 32);
                        nextHorizontalBorder += wStep;
                    }

                    if (label > 0)
                    {
                        int user = label * 40;
                        if (!labelIssueState[label])
                            user += 40;
                        argb = 0 | (user << 8) | (0 << 16) | (0xFF << 24);
                    }
                    else
                    {
                        argb = depth | (depth << 8) | (depth << 16) | (0xFF << 24);
                    }

                    _bitmap.Bits[bitmapPtr++] = argb;
                }
            }
        }
        

        // Event handler for the SkeletonUpdate event
        private void onSkeletonUpdate(SkeletonData skeletonData)
        {
            if (Pause) return;
            _skeletonData = skeletonData;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Release Nuitrack and remove all modules
            try
            {
                Nuitrack.onIssueUpdateEvent -= onIssueDataUpdate;

                _depthSensor.OnUpdateEvent -= onDepthSensorUpdate;
                _colorSensor.OnUpdateEvent -= onColorSensorUpdate;
                _userTracker.OnUpdateEvent -= onUserTrackerUpdate;
                _skeletonTracker.OnSkeletonUpdateEvent -= onSkeletonUpdate;

                Nuitrack.Release();
            }
            catch (System.Exception exception)
            {
                Console.WriteLine("Nuitrack release failed.");
                throw exception;
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (Pause) return;
            base.OnPaint(e);

            // Update Nuitrack data. Data will be synchronized with skeleton time stamps.
            try
            {
                Nuitrack.Update(_skeletonTracker);
            }
            catch (LicenseNotAcquiredException exception)
            {
                Console.WriteLine("LicenseNotAcquired exception. Exception: ", exception);
                throw exception;
            }
            catch (System.Exception exception)
            {
                Console.WriteLine("Nuitrack update failed. Exception: ", exception);
            }

            // Draw a bitmap
            pictureBox1.Image = _bitmap.Bitmap;
            //e.Graphics.DrawImage(_bitmap.Bitmap, new Point(0, 0));

            Graphics g1 = Graphics.FromHwnd(pictureBox1.Handle);

            // Draw skeleton joints
            if (_skeletonData != null)
            {
                const int jointSize = 10;
                foreach (var skeleton in _skeletonData.Skeletons)
                {
                    SolidBrush brush = new SolidBrush(Color.FromArgb(255 - 40 * skeleton.ID, 0, 0));
                    foreach (var joint in skeleton.Joints)
                    {
                        g1.FillEllipse(brush, joint.Proj.X * _bitmap.Width - jointSize / 2,
                                                  joint.Proj.Y * _bitmap.Height - jointSize / 2, jointSize, jointSize);
                        
                    }
                }


            }

            
            // Update Form
            this.Invalidate();
        }

        private void Stop_btn_Click(object sender, EventArgs e)
        {
            Pause = true;
            State_txt.Text = "pause";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            _visualizeColorImage = !_visualizeColorImage;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyValue == 'Z')
            {
                if (isAccess)
                {
                    Access.Text = "Access";
                    Access.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
                }
                else
                {
                    Access.Text = "Denied";
                    Access.ForeColor = System.Drawing.Color.Red;
                }
                isAccess = !isAccess;
            }
            
        }
    }
    public class DirectBitmap : IDisposable
    {
        public Bitmap Bitmap { get; private set; }
        public Int32[] Bits { get; private set; }
        public bool Disposed { get; private set; }
        public int Height { get; private set; }
        public int Width { get; private set; }

        protected GCHandle BitsHandle { get; private set; }

        public DirectBitmap(int width, int height)
        {
            Width = width;
            Height = height;
            Bits = new Int32[width * height];
            BitsHandle = GCHandle.Alloc(Bits, GCHandleType.Pinned);
            Bitmap = new Bitmap(width, height, width * 4, PixelFormat.Format32bppPArgb, BitsHandle.AddrOfPinnedObject());
        }

        public void SetPixel(int x, int y, Color colour)
        {
            int index = x + (y * Width);
            int col = colour.ToArgb();

            Bits[index] = col;
        }

        public Color GetPixel(int x, int y)
        {
            int index = x + (y * Width);
            int col = Bits[index];
            Color result = Color.FromArgb(col);

            return result;
        }

        public void Dispose()
        {
            if (Disposed)
                return;
            Disposed = true;
            Bitmap.Dispose();
            BitsHandle.Free();
        }
    }
}
