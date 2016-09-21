namespace FreedomJoy
{
    public class PovButton : IButton
    {
        public enum ButtonType
        {
            Standard,
            Pov
        }
        public int ButtonNumber { get; set; }
        private readonly int _onValue;
        private readonly int _offValue;
        private readonly Pov _povParent;
        public bool State
        {
            get
            {
                int value = _povParent.Value;
                return (value == _onValue);
            }
        }

        public PovButton(int buttonNumber, int onValue, int offValue, Pov povParent = null)
        {
            ButtonNumber = buttonNumber;
            _onValue = onValue;
            _offValue = offValue;
            _povParent = povParent;
        }
    }
}
