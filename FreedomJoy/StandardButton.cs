namespace FreedomJoy
{
    public class StandardButton : IButton
    {
        public int ButtonNumber { get; set; }
        private readonly Controller _parentController;
        public bool State
        {
            get
            {
                int value = _parentController.JoystickState.GetButtons()[ButtonNumber - 1] ? 1 : 0;
                return (value == 1);
            }
        }

        public StandardButton(Controller parentController, int buttonNumber)
        {
            _parentController = parentController;
            ButtonNumber = buttonNumber;
        }
    }
}
