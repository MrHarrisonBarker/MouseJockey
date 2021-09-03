using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MouseJockey
{
    public class Controllables
    {
        public Dictionary<string, Fader> Faders;
        public List<HotButton> HotButtons;
        private readonly ControlGlass Parent;

        public Controllables(ControlGlass parent)
        {
            Parent = parent;

            Faders = new Dictionary<string, Fader>
            {
                {
                    "grand", new Fader(Parent)
                    {
                        Name = "GrandMasterFader",
                        Control = Parent.grandMasterControl,
                        State = ControllableState.UnTouched,
                        Value = 0
                    }
                }
            };


            // {"",new Fader(Parent)
            //     {
            //         Name = "GrandMasterFader",
            //         Control = Parent.grandMasterControl,
            //         State = ControllableState.UnTouched,
            //         Value = 0
            //     }}
            // };
        }
    }

    public class Controllable
    {
        public Controllable(Control parent)
        {
            Parent = parent;
        }

        public string OscCode { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public ControllableState State { get; set; }
        protected Control Parent { get; set; }
    }

    public class Fader : Controllable
    {
        public Fader(Control parent) : base(parent)
        {
        }

        public Panel Control { get; set; }

        public void MoveTo(int value)
        {
            // BuildingBlocks.DragMouse(SafePointToScreen(this.MapPosition(this.Value)),SafePointToScreen(this.MapPosition(value)));
            BuildingBlocks.DragMouse(
                Parent.PointToScreen(MapPosition(Value)),
                Parent.PointToScreen(MapPosition(value)));

            State = ControllableState.Touched;
            Value = value;
        }

        // Maps a value (0-255) to a point in space based on the length of the fader
        private Point MapPosition(int value)
        {
            var spacePerTick = (float) (Control.Height / 255.00);
            var mappedSpace = spacePerTick * value;

            var p = new Point(Control.Left, Control.Bottom - (int) Math.Floor(mappedSpace));
            return p;
        }
    }

    public class HotButton : Controllable
    {
        public HotButton(Control parent) : base(parent)
        {
        }

        public RadioButton Control { get; set; }

        public void Click()
        {
            BuildingBlocks.LeftMouseClick(Parent.PointToScreen(BuildingBlocks.CentrePointOfControl(Control)));
        }
    }

    public enum ControllableState
    {
        UnTouched,
        Touched,
        Pressed,
        Held
    }
}