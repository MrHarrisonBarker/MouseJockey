using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MouseJockey
{
    public partial class Form1 : Form
    {
        private Timer Timer;

        public Form1()
        {
            InitializeComponent();

            // this.Opacity = 0.5;
            this.TopMost = true;

            //Timer = new Timer();
            //Timer.Tick += new EventHandler(TimerOnTick);
            //Timer.Interval = 2000;
            //Timer.Start();
        }

        private void TimerOnTick(object? sender, EventArgs e)
        {
            LeftMouseClick(this.PointToScreen(new Point(radioButton1.Left, radioButton1.Top)));
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            var style = GetWindowLong(this.Handle, GWL_EXSTYLE);
            //SetWindowLong(this.Handle, GWL_EXSTYLE, style | WS_EX_LAYERED | WS_EX_TRANSPARENT);
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        const int GWL_EXSTYLE = -20;
        const int WS_EX_LAYERED = 0x80000;
        const int WS_EX_TRANSPARENT = 0x20;

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg,
            IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", EntryPoint = "WindowFromPoint",
            CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr WindowFromPoint(Point point);

        private const int BM_CLICK = 0x00F5;

        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        private const int MOUSEEVENTF_MOVE = 0x0001;
        private const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const int MOUSEEVENTF_LEFTUP = 0x0004;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        private const int MOUSEEVENTF_RIGHTUP = 0x0010;
        private const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        private const int MOUSEEVENTF_MIDDLEUP = 0x0040;
        private const int MOUSEEVENTF_ABSOLUTE = 0x8000;

        [DllImport("user32.dll")]
        internal static extern uint SendInput(uint nInputs, [MarshalAs(UnmanagedType.LPArray), In] INPUT[] pInputs, int cbSize);

#pragma warning disable 649
        internal struct INPUT
        {
            public UInt32 Type;
            public MOUSEKEYBDHARDWAREINPUT Data;
        }

        [StructLayout(LayoutKind.Explicit)]
        internal struct MOUSEKEYBDHARDWAREINPUT
        {
            [FieldOffset(0)]
            public MOUSEINPUT Mouse;
        }

        internal struct MOUSEINPUT
        {
            public Int32 X;
            public Int32 Y;
            public UInt32 MouseData;
            public UInt32 Flags;
            public UInt32 Time;
            public IntPtr ExtraInfo;
        }

#pragma warning restore 649

        //This is a replacement for Cursor.Position in WinForms
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool SetCursorPos(int x, int y);

        public static void MoveTo(int x, int y)
        {
            mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, x, y, 0, 0);
        }

        public static void LeftMouseClick(Point point)
        {

            var oldPos = Cursor.Position;

            SetCursorPos(point.X, point.Y);

            var inputMouseDown = new INPUT();
            inputMouseDown.Type = 0;
            inputMouseDown.Data.Mouse.Flags = 0x0002;

            var inputMouseUp = new INPUT();
            inputMouseUp.Type = 0;
            inputMouseUp.Data.Mouse.Flags = 0x0004;

            var inputs = new INPUT[] { inputMouseDown, inputMouseUp };
            SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));

            SetCursorPos(oldPos.X, oldPos.Y);

        }

        public static void DragMouse(Point startPoint, Point endPoint)
        {
            var oldPos = Cursor.Position;

            SetCursorPos(startPoint.X, startPoint.Y);

            var mouseDown = new INPUT
            {
                Type = 0,
                Data = new MOUSEKEYBDHARDWAREINPUT() { Mouse = new MOUSEINPUT() { Flags = MOUSEEVENTF_LEFTDOWN } }
            };

            var mouseMove = new INPUT()
            {
                Type = 0,
                Data = new MOUSEKEYBDHARDWAREINPUT()
                {
                    Mouse = new MOUSEINPUT()
                    {
                        Flags = MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE,
                        X = endPoint.X,
                        Y = endPoint.Y
                    }
                }
            };

            var mouseUp = new INPUT
            {
                Type = 0,
                Data = new MOUSEKEYBDHARDWAREINPUT() { Mouse = new MOUSEINPUT() { Flags = MOUSEEVENTF_LEFTUP } }
            };

            SendInput(3, new INPUT[] { mouseDown, mouseMove, mouseUp }, Marshal.SizeOf(typeof(INPUT)));

            SetCursorPos(oldPos.X, oldPos.Y);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // var screenPoint = this.PointToScreen(new Point(button2.Left,
            // button2.Top));

            // var handle = WindowFromPoint(screenPoint);

            // SendMessage(handle, BM_CLICK, IntPtr.Zero, IntPtr.Zero);


            // LeftMouseClick(this.PointToScreen(new Point(button2.Left,button2.Top)));

            // LeftMouseClick(this.PointToScreen(new Point(vScrollBar1.Left, vScrollBar1.Bottom - 10)));

            LeftMouseClick(
                this.PointToScreen(new Point(trackBar1.Left + 10, trackBar1.Bottom - 25)));

            DragMouse(
                this.PointToScreen(new Point(trackBar1.Left + 10, trackBar1.Bottom - 25)),
                this.PointToScreen(new Point(trackBar1.Left + 10, trackBar1.Top + 25)));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DragMouse(
                this.PointToScreen(new Point(trackBar1.Left + 15, trackBar1.Top + 25)),
                this.PointToScreen(new Point(trackBar1.Left + 15, trackBar1.Bottom - 25)));
        }
    }
}
