namespace FreedomJoy.vJoy
{
    class VjoyAxis
    {
        private readonly Vcontroller _parentVcontroller;
        private readonly string _name;
        private int _value;

        public int Value
        {
            get { return _value; }
            set
            {
                // This way is much faster than boxing / unboxing the struct to use GetField() and SetValue() (or using __makeref with TypedReference to do it)
                switch (_name)
                {
                    case "x":
                        _parentVcontroller.JoystickState.AxisX = _value = value;
                        break;
                    case "y":
                        _parentVcontroller.JoystickState.AxisY = _value = value;
                        break;
                    case "z":
                        _parentVcontroller.JoystickState.AxisZ = _value = value;
                        break;
                    case "rx":
                        _parentVcontroller.JoystickState.AxisXRot = _value = value;
                        break;
                    case "ry":
                        _parentVcontroller.JoystickState.AxisYRot = _value = value;
                        break;
                    case "rz":
                        _parentVcontroller.JoystickState.AxisZRot = _value = value;
                        break;
                    case "sl0":
                        _parentVcontroller.JoystickState.Slider = _value = value;
                        break;
                    case "sl1":
                        _parentVcontroller.JoystickState.Dial = _value = value;
                        break;

                }
            }
        }

        public VjoyAxis(Vcontroller parentVcontroller, string name)
        {
            _parentVcontroller = parentVcontroller;
            _name = name;
            Value = 16000;
        }
    }
}
