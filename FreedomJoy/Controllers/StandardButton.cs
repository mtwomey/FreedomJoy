namespace FreedomJoy.Controllers
{
    public class StandardButton : Button
    {
        public override bool State
        {
            get
            {
                int value = ParentController.JoystickState.GetButtons()[ButtonNumber - 1] ? 1 : 0;
                return (value == 1);
            }
        }

        public StandardButton(Controller parentController, int buttonNumber) : base(parentController, buttonNumber)
        {

        }
    }
}
