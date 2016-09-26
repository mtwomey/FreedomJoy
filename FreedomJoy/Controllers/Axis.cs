using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreedomJoy.Controllers
{
    public class Axis
    {

        private Controller _parentController;
        private string _name;
        private int _axisNumber;
        public int Value
        {
            get
            {
                int value = 0;
                switch (_axisNumber)
                {
                    case 0: value = _parentController.JoystickState.X; break;
                    case 1: value = _parentController.JoystickState.Y; break;
                    case 2: value = _parentController.JoystickState.Z; break;
                    case 3: value = _parentController.JoystickState.RotationX; break;
                    case 4: value = _parentController.JoystickState.RotationY; break;
                    case 5: value = _parentController.JoystickState.RotationZ; break;
                }
                return value;
            }
        }
        public Axis(Controller parentController, string name, int axisNumber)
        {
            _parentController = parentController;
            _name = name;
            _axisNumber = axisNumber;
        }
    }
}
