namespace FreedomJoy.Controllers
{
    public abstract class Button
    {
        public int ButtonNumber { get; set; } // I don't htink I need this since name now...
        private readonly string _name;
        public readonly Controller ParentController;
        public abstract bool State { get; }

        protected Button(Controller parentController, string name, int buttonNumber)
        {
            ParentController = parentController;
            _name = name;
            ButtonNumber = buttonNumber;
        }
    }
}
