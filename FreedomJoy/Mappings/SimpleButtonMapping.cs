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
            _controllerNotPressedButtons = controllerNotPressedButtons;
            _vJoyPressedButtons = vJoyPressedButtons;
            _controller = controller;
            _vcontroller = vcontroller;
        }

        public void Update()
        {
            bool active = true;
            foreach (string s in _controllerPressedButtons) // Pressed buttons
            {
                active &= _controller.ButtonsByName[s].State;
            }
            foreach (string s in _controllerNotPressedButtons) // Not pressed buttons
            {
                active &= !_controller.ButtonsByName[s].State;
            }

            foreach (string s in _vJoyPressedButtons) // TODO: For future - the way this works now, it's impossible to map two different combos to the same vJoy button, if a given mapping is true, it maps true. If false, it maps fale - BUT there could be another valid combo mapping true. Could fix this with some kind of matrix: "If any mappings targeting this vJOy button are true, then true; if all are false, then false.
            {
                _vcontroller.ButtonsByName[s].State = active;
            }
        }
    }
}
