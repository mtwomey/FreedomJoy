namespace FreedomJoy.vJoy
{
    class VjoyButton
    {
        private readonly Vcontroller _parentVcontroller;
        private readonly string _name;
        private readonly uint _buttonNumber;
        private bool _state;
        public bool State
        {
            get { return _state; }
            set
            {
                if (value)
                {
                    _parentVcontroller.JoystickState.Buttons |= (uint) 1 << (int) (_buttonNumber - 1); // Binary foo to set the correct bit in the 32 bit uint
                }
                else
                {
                    _parentVcontroller.JoystickState.Buttons &= ~(uint)1 << (int)(_buttonNumber - 1);
                }
                _state = value;
            }
        }

        public VjoyButton(Vcontroller parentVcontroller, string name, uint buttonNumber)
        {
            _parentVcontroller = parentVcontroller;
            _name = name;
            _buttonNumber = buttonNumber;
            State = false;
        }
    }
}
