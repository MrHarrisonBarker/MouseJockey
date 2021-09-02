using System;
using System.Drawing;
using System.Windows.Forms;

namespace MouseJockey
{
    public class Controllable
    {
        public String Name { get; set; }
        public int Value { get; set; }
        public ControllableState State { get; set; }
    }

    public class Fader : Controllable
    {
        public Panel Control { get; set; }

        public void MoveFaderTo(Control parent, int value)
        {
            BuildingBlocks.DragMouse(
                parent.PointToScreen(new Point(this.Control.Left, this.Control.Bottom)),
                parent.PointToScreen(new Point(this.Control.Left, this.Control.Top)));

            this.State = ControllableState.Touched;
            this.Value = value;
        }
    }

    public class HotButton : Controllable
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
}