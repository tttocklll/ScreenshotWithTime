using System.Diagnostics;
using System.Drawing.Imaging;

namespace ScreenshotWithTime
{
    public partial class Form1 : Form

    {
        private System.Windows.Forms.Timer timer1;

        public Form1()
        {
            InitializeComponent();
            timer1 = new System.Windows.Forms.Timer();
            timer1.Tick += timer1_Tick;
            Start_Timer();
            Debug.WriteLine("Form1");


        }


        // Import Windows API
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto, CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern short GetKeyState(int nVirtKey);

        private void Start_Timer()
        {
            Debug.WriteLine("start");

            // Start timer1
            timer1.Enabled = true;
            timer1.Interval = 100;
        }

        private void timer1_Tick(object sender, EventArgs e)

        {
            Debug.WriteLine("tick");
            bool LWin_Key_State = IsKeyLocked(Keys.LWin);
            bool Ctrl_Key_State = IsKeyLocked(Keys.ControlKey);
            bool Shift_Key_State = IsKeyLocked(Keys.ShiftKey);
            bool S_Key_State = IsKeyLocked(Keys.S);

            if (LWin_Key_State && Ctrl_Key_State && Shift_Key_State && S_Key_State)
            {
                Debug.WriteLine("shot");
                CaptureScreen();

            }
        }


        public bool IsKeyLocked(System.Windows.Forms.Keys Key_Value)

        {
            bool Key_State = (GetKeyState((int)Key_Value) & 0x80) != 0;
            return Key_State;
        }

        private void CaptureScreen()
        {
            try
            {
                // Capture constant rectangle
                // TODO: drag to select rectangle
                Bitmap caputreBitmap = new Bitmap(1024, 768, PixelFormat.Format32bppArgb);
                Rectangle captureRectangle = Screen.AllScreens[0].Bounds;
                Graphics captureGraphics = Graphics.FromImage(caputreBitmap);
                captureGraphics.CopyFromScreen(captureRectangle.Left, captureRectangle.Top, 0, 0, captureRectangle.Size);
                Clipboard.SetDataObject(caputreBitmap);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }
    }


}