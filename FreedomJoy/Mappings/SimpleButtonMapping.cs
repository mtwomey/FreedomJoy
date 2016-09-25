using FreedomJoy.Controllers;
using FreedomJoy.vJoy;

namespace FreedomJoy.Mappings
{
    class SimpleButtonMapping : IMapping
    {
        private readonly Controller _controller;
        private readonly Vcontroller _vcontroller;
        private readonly string[] _controllerPressedButtons;
        private readonly string[] _controllerNotPressedButtons;
        private readonly string[] _vJoyPressedButtons;

        public SimpleButtonMapping(string[] controllerPressedButtons, string[] controllerNotPressedButtons, string[] vJoyPressedButtons, Controller controller, Vcontroller vcontroller)
        {
            _controllerPressedButtons = controllerPressedButtons;
            _vJoyPressedButtons = vJoyPressedButtons;
            _controller = controller;
            _vcontroller = vcontroller;
        }

        public void Update()
        {
            bool active = true;
            foreach (string s in _controllerPressedButtons)
            {
                active &= _controller.ButtonsByName[s].State;
            }

            foreach (string s in _vJoyPressedButtons)
            {
                _vcontroller.ButtonsByName[s].State = active;
            }
        }
    }
}
