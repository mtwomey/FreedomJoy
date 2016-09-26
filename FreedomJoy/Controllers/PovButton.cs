namespace FreedomJoy.Controllers
{
    public class PovButton : Button
    {
        public enum ButtonType
        {
            Standard,
            Pov
        }

        private readonly int _onValue;
        private readonly Pov _povParent;
        private int _lastValue;
        public override bool State
        {
            get
            {
                int value = _povParent.Value;
                if (_lastValue == 0 & (value == 0 | value == 4500 | value == 31500)) // Add some "stickiness" to a 4-way POV used as buttons to keep the same value if you rock your thumb off to one side by 45 degrees
                {
                    value = 0;
                }
                if (_lastValue == 9000 & (value == 9000 | value == 4500 | value == 13500))
                {
                    value = 9000;
                }
                if (_lastValue == 18000 & (value == 18000 | value == 13500 | value == 22500))
                {
                    value = 18000;
                }
                if (_lastValue == 27000 & (value == 27000 | value == 22500 | value == 31500))
                {
                    value = 27000;
                }

                _lastValue = value;
                return (value == _onValue);
            }
        }

        public PovButton(Controller parentController, string name, int buttonNumber, int onValue, Pov povParent = null) : base(parentController, name, buttonNumber) // Delete this default povParent value ?
        {
            _onValue = onValue;
            _povParent = povParent;
        }
    }
}
