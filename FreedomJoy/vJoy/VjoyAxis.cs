namespace FreedomJoy.vJoy
{
    class VjoyAxis
    {
        private readonly Vcontroller _parentVcontroller;
        private readonly string _name;
        private int _state;

        public int State
        {
            get { return _state; }
            set
            {
                // This way is much faster than boxing / unboxing the struct to use GetField() and SetValue() (or using __makeref with TypedReference to do it)
                switch (_name)
                {
                    case "x":
                        _parentVcontroller.JoystickState.AxisX = _state = value;
                        break;
                    case "y":
                        _parentVcontroller.JoystickState.AxisY = _state = value;
                        break;
                    case "z":
                        _parentVcontroller.JoystickState.AxisZ = _state = value;
                        break;
                    case "rx":
                        _parentVcontroller.JoystickState.AxisXRot = _state = value;
                        break;
                    case "ry":
                        _parentVcontroller.JoystickState.AxisYRot = _state = value;
                        break;
                    case "rz":
                        _parentVcontroller.JoystickState.AxisZRot = _state = value;
                        break;
                    case "sl0":
                        _parentVcontroller.JoystickState.Slider = _state = value;
                        break;
                    case "sl1":
                        _parentVcontroller.JoystickState.Dial = _state = value;
                        break;

                }
            }
        }

        public VjoyAxis(Vcontroller parentVcontroller, string name)
        {
            _parentVcontroller = parentVcontroller;
            _name = name;
            State = 16000;
        }
    }
}
