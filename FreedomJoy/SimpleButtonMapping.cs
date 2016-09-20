namespace FreedomJoy
{
    class SimpleButtonMapping : IMapping
    {
        private readonly Controller _controller;
        private readonly Vcontroller _vcontroller;
        private readonly int[] _controllerPressedButtons;
        private readonly int[] _vJoyPressedButtons;

        public SimpleButtonMapping(int[] controllerPressedButtons, int[] vJoyPressedButtons, Controller controller, Vcontroller vcontroller)
        {
            _controllerPressedButtons = controllerPressedButtons;
            _vJoyPressedButtons = vJoyPressedButtons;
            _controller = controller;
            _vcontroller = vcontroller;
        }

        public void Update()
        {
            bool active = true;
            foreach (int x in _controllerPressedButtons)
            {
                active &= _controller.Buttons[x].State;
            }

            foreach (int x in _vJoyPressedButtons)
            {
                _vcontroller.Buttons[x].State = active;
            }
        }
    }
}
