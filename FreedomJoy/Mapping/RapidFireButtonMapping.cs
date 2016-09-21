using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreedomJoy
{
    class RapidFireButtonMapping : IMapping
    {
        private readonly Controller _controller;
        private readonly Vcontroller _vcontroller;
        private readonly int[] _controllerPressedButtons;
        private readonly int[] _vJoyPressedButtons;
        private readonly int _rapidFireRate;
        private bool _rapidFireToggle;
        private int _rapidFireTicks;

        public RapidFireButtonMapping(int[] controllerPressedButtons, int[] vJoyPressedButtons, Controller controller, Vcontroller vcontroller, int rapidFireRate = 0)
        {
            _controllerPressedButtons = controllerPressedButtons;
            _vJoyPressedButtons = vJoyPressedButtons;
            _controller = controller;
            _vcontroller = vcontroller;
            _rapidFireRate = rapidFireRate;
            _rapidFireToggle = true;
            _rapidFireTicks = 0;
        }

        public void Update()
        {

            bool active = true;
            foreach (int x in _controllerPressedButtons)
            {
                active &= _controller.Buttons[x].State;
            }

            if (!active) { _rapidFireToggle = true; _rapidFireTicks = 0; }

            if (_rapidFireRate != 0)
            {
                if ((_rapidFireTicks * _vcontroller.UpdateRate) / (_rapidFireRate / 2) >= 1)
                {
                    _rapidFireTicks = 0;
                    _rapidFireToggle = !_rapidFireToggle;
                }
                _rapidFireTicks++;
            }

            foreach (int x in _vJoyPressedButtons)
            {
                _vcontroller.Buttons[x].State = active & _rapidFireToggle;
            }
        }
    }
}
