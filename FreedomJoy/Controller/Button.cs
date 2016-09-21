namespace FreedomJoy
{
    public abstract class Button
    {
        public int ButtonNumber { get; set; }
        public Controller ParentController;
        public abstract bool State { get; }

        protected Button(Controller parentController, int buttonNumber)
        {
            ParentController = parentController;
            ButtonNumber = buttonNumber;
        }
    }
}
