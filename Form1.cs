using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MouseJockey
{
    public static class BuildingBlocks
    {
        private const int MOUSEEVENTF_MOVE = 0x0001;
        private const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const int MOUSEEVENTF_LEFTUP = 0x0004;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        private const int MOUSEEVENTF_RIGHTUP = 0x0010;
        private const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        private const int MOUSEEVENTF_MIDDLEUP = 0x0040;
        private const int MOUSEEVENTF_ABSOLUTE = 0x8000;

        [DllImport("user32.dll")]
        internal static extern uint SendInput(uint nInputs, [MarshalAs(UnmanagedType.LPArray), In] BuildingBlocks.INPUT[] pInputs, int cbSize);

        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int x, int y);

        // #pragma warning disable 649

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

        // #pragma warning restore 649

        public static async void LeftMouseClick(Point point, int pressLength = 100)
        {

            var oldPos = Cursor.Position;

            SetCursorPos(point.X, point.Y);

            var mouseDown = new INPUT()
            {
                Type = 0,
                Data = new MOUSEKEYBDHARDWAREINPUT()
                {
                    Mouse = new MOUSEINPUT()
                    {
                        Flags = MOUSEEVENTF_LEFTDOWN,
                        X = 0,
                        Y = 0
                    }
                }
            };

            var mouseUp = new INPUT()
            {
                Type = 0,
                Data = new MOUSEKEYBDHARDWAREINPUT()
                {
                    Mouse = new MOUSEINPUT()
                    {
                        Flags = MOUSEEVENTF_LEFTUP,
                        X = 0,
                        Y = 0
                    }
                }
            };

            SendInput(1, new[] {mouseDown}, SizeOfInput());

            await Task.Delay(pressLength);

            SendInput(1, new[] {mouseUp}, SizeOfInput());

            SetCursorPos(oldPos.X, oldPos.Y);

        }

        public static int SizeOfInput()
        {
            return Marshal.SizeOf(typeof(INPUT));
        }

        public static void DragMouse(Point startPoint, Point endPoint)
        {
            var oldPos = Cursor.Position;

            SetCursorPos(startPoint.X, startPoint.Y);

            var mouseDown = new INPUT
            {
                Type = 0,
                Data = new MOUSEKEYBDHARDWAREINPUT()
                {
                    Mouse = new MOUSEINPUT()
                    {
                        Flags = MOUSEEVENTF_LEFTDOWN,
                        X = 0,
                        Y = 0,
                    }
                }
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
                Data = new MOUSEKEYBDHARDWAREINPUT()
                {
                    Mouse = new MOUSEINPUT()
                    {
                        Flags = MOUSEEVENTF_LEFTUP,
                        X = 0,
                        Y = 0,
                    }
                }
            };

            SendInput(1, new[] {mouseDown}, SizeOfInput());

            SendInput(1, new[] {mouseMove}, SizeOfInput());
            SetCursorPos(endPoint.X, endPoint.Y);

            SendInput(1, new[] {mouseUp}, SizeOfInput());

            SetCursorPos(oldPos.X, oldPos.Y);
        }
    }

    public class Controllable
    {
        public String Name { get; set; }
        public int Value { get; set; }
        public ControllableState State { get; set; }
    }

    public class Fader : Controllable
    {
        public Panel Control { get; set; }

        public void MoveFaderTo(Control parent,int value)
        {
            BuildingBlocks.DragMouse(
                parent.PointToScreen(new Point(this.Control.Left, this.Control.Bottom)),
                parent.PointToScreen(new Point(this.Control.Left, this.Control.Top)));

            this.State = ControllableState.Touched;
            this.Value = value;
        }
    }

    public class Button : Controllable
    {
        public RadioButton Control { get; set; }
    }

    public enum ControllableState
    {
        UnTouched,
        Touched,
        Pressed,
        Held
    }

    public partial class Form1 : Form
    {
        private Fader[] Faders;
        private Button[] Buttons;

        private Timer Timer;

        public Form1()
        {
            Faders = new[] {
                new Fader()
                {
                    Name = "GrandMasterFader",
                    Control = grandMasterControl,
                    State = ControllableState.UnTouched,
                    Value = 0
                },
                new Fader()
                {

                }
            };

            this.WindowState = FormWindowState.Maximized;

            InitializeComponent();

            this.ClientSize = new Size()
            {
                Height = 1989,
                Width = 3584
            };

            this.Size = this.BackgroundImage.Size;

            this.Opacity = 0.5;
            this.TopMost = true;

            Timer = new Timer();
            Timer.Tick += new EventHandler(TimerOnTick);
            Timer.Interval = 5000;
            Timer.Start();
        }

        private void TimerOnTick(object? sender, EventArgs e)
        {
            // SetCursorPos(CentrePointOfControl(radioButton1).X, CentrePointOfControl(radioButton1).Y);

            BuildingBlocks.DragMouse(
                this.PointToScreen(new Point(playBack1Control.Left, playBack1Control.Bottom)),
                this.PointToScreen(new Point(playBack1Control.Left, playBack1Control.Top)));

            // DragMouse(
            //     this.PointToScreen(new Point(trackBar1.Left + 10, trackBar1.Bottom - 25)),
            //     this.PointToScreen(new Point(trackBar1.Left + 10, trackBar1.Top + 25)));


            // LeftMouseClick(this.PointToScreen(new Point(radioButton1.Left + (radioButton1.Width / 2), radioButton1.Top + (radioButton1.Height / 2))));

            // LeftMouseClick(this.PointToScreen(CentrePointOfControl(radioButton1)));
        }

        private Point CentrePointOfControl(Control control)
        {
            return new Point(control.Left + (control.Width / 2), control.Top + (control.Height / 2));
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            var style = GetWindowLong(this.Handle, GWL_EXSTYLE);
            SetWindowLong(this.Handle, GWL_EXSTYLE, style | WS_EX_LAYERED | WS_EX_TRANSPARENT);
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        const int GWL_EXSTYLE = -20;
        const int WS_EX_LAYERED = 0x80000;
        const int WS_EX_TRANSPARENT = 0x20;

        private void button1_Click(object sender, EventArgs e)
        {
            // LeftMouseClick(this.PointToScreen(new Point(button2.Left,button2.Top)));

            // LeftMouseClick(this.PointToScreen(new Point(vScrollBar1.Left, vScrollBar1.Bottom - 10)));

            //// LeftMouseClick(
            ////     this.PointToScreen(new Point(trackBar1.Left + 10, trackBar1.Bottom - 25)));

            var fiftyPercent = (trackBar1.Top + 25) + (trackBar1.Height / 2);

            BuildingBlocks.DragMouse(
                this.PointToScreen(new Point(trackBar1.Left + 10, trackBar1.Bottom - 25)),
                this.PointToScreen(new Point(trackBar1.Left + 10, fiftyPercent)));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BuildingBlocks.DragMouse(
                this.PointToScreen(new Point(trackBar1.Left + 10, trackBar1.Top + 25)),
                this.PointToScreen(new Point(trackBar1.Left + 10, trackBar1.Bottom - 25)));
        }
    }
}
