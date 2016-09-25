﻿namespace FreedomJoy.Controllers
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
        public override bool State
        {
            get
            {
                int value = _povParent.Value;
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
