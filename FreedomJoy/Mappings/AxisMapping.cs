using FreedomJoy.Controllers;
using FreedomJoy.vJoy;

namespace FreedomJoy.Mappings
{
    class AxisMapping : IMapping
    {
        private readonly Controller _controller;
        private readonly Vcontroller _vcontroller;
        private readonly Axis _physicalAxes;
        private readonly VjoyAxis _vJoyAxis;

        public AxisMapping(string physicalAxis, string vjoyAxis, Controller controller, Vcontroller vcontroller)
        {
            _physicalAxes = controller.AxesByName[physicalAxis];
            _vJoyAxis = vcontroller.AxesByName[vjoyAxis];
            _controller = controller;
            _vcontroller = vcontroller;
        }

        public void Update()
        {
            _vJoyAxis.Value = _physicalAxes.Value; // Range is the same for both becase we set the controller range to 1 - 32768 in the constructor to match vJoy
        }
    }
}
