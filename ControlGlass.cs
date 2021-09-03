using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MouseJockey
{

    public partial class ControlGlass : Form
    {
        private Timer Timer;

        public ControlGlass()
        {
            InitializeComponent();

            this.ClientSize = new Size()
            {
                Height = 1989,
                Width = 3584
            };

            this.Size = this.BackgroundImage.Size;

            this.Opacity = 0.2;
            this.TopMost = true;

            // for (int i = 0; i < 255; i++)
            // {
            //     Faders[0].MoveTo(i);
            //     // Task.Delay(100);
            // }
            //
            // Faders[0].MoveTo(0);

            // Task.Delay(5000);

            // Timer = new Timer();
            // Timer.Tick += new EventHandler(TimerOnTick);
            // Timer.Interval = 1000;
            // Timer.Start();

            // Task.Run(() =>
            // {
            //     for (int i = 0; i < 255; i++)
            //     {
            //         Faders[0].MoveTo(i);
            //         Task.Delay(100);
            //     }
            //
            //     Faders[0].MoveTo(0);
            //
            //     Task.Delay(5000);
            // });
        }

        private void TimerOnTick(object? sender, EventArgs e)
        {
            // SetCursorPos(CentrePointOfControl(radioButton1).X, CentrePointOfControl(radioButton1).Y);

            // if (Faders[0].Value == 255)
            // {
            //     Faders[0].MoveTo(0);
            // }
            // else
            // {
            //     Faders[0].MoveTo(255);
            // }

            // Faders[0].MoveTo(new Random().Next(0,255));

            // Faders[0].MoveTo(125);

            // BuildingBlocks.DragMouse(
            //     this.PointToScreen(new Point(playBack1Control.Left, playBack1Control.Bottom)),
            //     this.PointToScreen(new Point(playBack1Control.Left, playBack1Control.Top)));

            // DragMouse(
            //     this.PointToScreen(new Point(trackBar1.Left + 10, trackBar1.Bottom - 25)),
            //     this.PointToScreen(new Point(trackBar1.Left + 10, trackBar1.Top + 25)));


            // LeftMouseClick(this.PointToScreen(new Point(radioButton1.Left + (radioButton1.Width / 2), radioButton1.Top + (radioButton1.Height / 2))));

            // LeftMouseClick(this.PointToScreen(CentrePointOfControl(radioButton1)));
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            var style = GetWindowLong(this.Handle, GWL_EXSTYLE);
            SetWindowLong(this.Handle, GWL_EXSTYLE, style | WS_EX_TRANSPARENT);
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
