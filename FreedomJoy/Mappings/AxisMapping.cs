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
            _vJoyAxis.Value = _physicalAxes.Value / 2; // Xbox controller is 0 - 64k, vjoy is 0 - 32k, need to figure out a standard way compatible with everything
        }
    }
}
