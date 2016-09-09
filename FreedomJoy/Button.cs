namespace FreedomJoy
{
    public class Button
    {
        public enum ButtonType
        {
            Standard,
            Pov
        }

        private ButtonType _type;
        private readonly int _onValue;
        private readonly int _offValue;
        private readonly Controller _parentController;
        private readonly Pov _povParent;
        public int ButtonNumber { get; set; }
        public bool Value
        {
            get
            {
                int value = -1;
                if (_type == ButtonType.Standard)
                {
                    value = _parentController.JoystickState.GetButtons()[ButtonNumber - 1] ? 1 : 0;
                }
                if (_type == ButtonType.Pov)
                {
                    value = _povParent.Value;
                }
                return (value == _onValue);
            }
        }

        public Button(Controller parentController, ButtonType type, int buttonNumber, int onValue, int offValue, Pov povParent = null)
        {
            _parentController = parentController;
            _type = type;
            ButtonNumber = buttonNumber;
            _onValue = onValue;
            _offValue = offValue;
            _povParent = povParent;
        }
    }
}
