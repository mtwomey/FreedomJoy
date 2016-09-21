namespace FreedomJoy.vJoy
{
    class VjoyButton
    {
        private readonly Vcontroller _parentVcontroller;
        public uint ButtonNumber { get; set; }
        private bool _state;
        public bool State
        {
            get { return _state; }
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
                _state = value;
            }
        }

        public VjoyButton(Vcontroller parentVcontroller, uint buttonNumber)
        {
            _parentVcontroller = parentVcontroller;
            ButtonNumber = buttonNumber;
            State = false;
        }
    }
}
