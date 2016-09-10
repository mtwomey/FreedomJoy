namespace FreedomJoy
{
    class VjoyButton
    {
        private readonly Vcontroller _parentVcontroller;
        public uint ButtonNumber { get; set; }
        private bool _value;
        public bool Value
        {
            get { return _value; }
            set
            {
                if (value)
                {
                    _parentVcontroller.JoystickState.Buttons |= (uint) 1 << (int) (ButtonNumber - 1); // Binary foo to set the correct bit in the 32 bit uint
                }
                else
                {
                    _parentVcontroller.JoystickState.Buttons &= ~(uint)1 << (int)(ButtonNumber - 1);
                }
                _value = value;
            }
        }

        public VjoyButton(Vcontroller parentVcontroller, uint buttonNumber)
        {
            _parentVcontroller = parentVcontroller;
            ButtonNumber = buttonNumber;
        }
    }
}
