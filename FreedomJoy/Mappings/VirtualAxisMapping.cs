using System.Linq;
using FreedomJoy.Controllers;
using FreedomJoy.vJoy;

namespace FreedomJoy.Mappings
{
    class VirtualAxisMapping : IMapping
    {
        private readonly Controller _controller;
        private readonly Vcontroller _vcontroller;
        private readonly int[] _decreaseButtons;
        private readonly int[] _increaseButtons;
        private readonly int _ratePerSecond;
        private readonly VjoyAxis _virtualAxis;
        private int _accelTicks;
        private int _decelTicks;

        public VirtualAxisMapping(Controller controller, Vcontroller vcontroller, VjoyAxis virtualAxis, int[] increaseButtons, int[] decreaseButtons, int ratePerSecond)
        {
            _controller = controller;
            _vcontroller = vcontroller;
            _virtualAxis = virtualAxis;
            _increaseButtons = increaseButtons;
            _decreaseButtons = decreaseButtons;
            _ratePerSecond = ratePerSecond;

        }

        public void Update()
        {
            // Notes on acceleration
            //
            // _accelTicks*_vcontroller.UpdateRate = the time passed since the button was depressed in ms
            // using a curve to accelerate up to the maximum rate supplied (for increasing or decreasing the axis)
            //

            double divisor;
            int changeRate = _ratePerSecond / (1000 / _vcontroller.UpdateRate); // Rate per second is approximate due to int and also acceleration

            bool active = _increaseButtons.Aggregate(true, (current, x) => current & _controller.Buttons[x].State); // Active if all the buttons are pressed (usually it will be just one button, unless you're using a shift button)
            if (active)
            {
                _accelTicks++;
                divisor = -0.05*(_accelTicks*_vcontroller.UpdateRate) + 14; // Acceleration curve - calc'd this here: http://www.analyzemath.com/parabola/three_points_para_calc.html using (240,2) (160,6) (80,10) as inputs
                if (divisor < 1) { divisor = 1; }

                _virtualAxis.State += changeRate / (int)divisor;
                if (_virtualAxis.State > 32768) { _virtualAxis.State = 32768; }
            } else { _accelTicks = 0; }

            active = _decreaseButtons.Aggregate(true, (current, x) => current & _controller.Buttons[x].State);
            if (active)
            {
                _decelTicks++;
                divisor = -0.05 * (_decelTicks * _vcontroller.UpdateRate) + 14;
                if (divisor < 1) { divisor = 1; }

                _virtualAxis.State -= changeRate / (int)divisor;
                if (_virtualAxis.State < 1) { _virtualAxis.State = 1; }
            } else { _decelTicks = 0; }
        }
    }
}
