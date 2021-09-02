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
        private static extern bool SetCursorPos(int x, int y);

        // #pragma warning disable 649

        public struct INPUT
        {
            public UInt32 Type;
            public MOUSEKEYBDHARDWAREINPUT Data;
        }

        [StructLayout(LayoutKind.Explicit)]
        public  struct MOUSEKEYBDHARDWAREINPUT
        {
            [FieldOffset(0)]
            public MOUSEINPUT Mouse;
        }

        public struct MOUSEINPUT
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
}