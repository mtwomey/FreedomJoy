namespace FreedomJoy
{
    public class PovButton : Button
    {
        public enum ButtonType
        {
            Standard,
            Pov
        }

        private readonly int _onValue;
        private readonly int _offValue;
        private readonly Pov _povParent;
        public override bool State
        {
            get
            {
                int value = _povParent.Value;
                return (value == _onValue);
            }
        }

        public PovButton(Controller parentController, int buttonNumber, int onValue, int offValue, Pov povParent = null)
        {
            ParentController = parentController;
            ButtonNumber = buttonNumber;
            _onValue = onValue;
            _offValue = offValue;
            _povParent = povParent;
        }
    }
}
