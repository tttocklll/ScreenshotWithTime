using System.Drawing.Imaging;
using System.Drawing;
using System.Windows.Forms;
using System;
using System.Diagnostics;


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


        // import Windows API
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto, CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern short GetKeyState(int nVirtKey);

        private void Start_Timer()
        {
            Debug.WriteLine("start");

            // start timer
            timer1.Enabled = true;
            timer1.Interval = 100;
        }

        private void timer1_Tick(object sender, EventArgs e)

        {
            Debug.WriteLine("tick");
            bool LWin_Key_State = IsKeyLocked(Keys.LWin);
            bool Alt_Key_State = IsKeyLocked(Keys.Alt);
            bool S_Key_State = IsKeyLocked(Keys.S);

            if (LWin_Key_State || Alt_Key_State || S_Key_State)
            {
                Debug.WriteLine("shot");
                MessageBox.Show("shot");
            }
        }


        public bool IsKeyLocked(System.Windows.Forms.Keys Key_Value)

        {
            // 
            bool Key_State = (GetKeyState((int)Key_Value) & 0x80) != 0;
            return Key_State;
        }
        }
}