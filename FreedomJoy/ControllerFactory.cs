using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreedomJoy.Controllers;
using FreedomJoy.vJoy;

namespace FreedomJoy
{
    static class ControllerFactory // Controller singletons...
    {
        private static Dictionary<uint, Controller> _physicalControllers = new Dictionary<uint, Controller>();
        private static Dictionary<uint, Vcontroller> _vJoyControllers = new Dictionary<uint, Vcontroller>();

        public static Controller GetPhysicalController(uint x)
        {
            if (!_physicalControllers.ContainsKey(x))
            {
                _physicalControllers.Add(x, new Controller(x));
            }
            return _physicalControllers[x];
        }

        public static Vcontroller GetvJoyController(uint x)
        {
            if (!_vJoyControllers.ContainsKey(x))
            {
                _vJoyControllers.Add(x, new Vcontroller(x));
            }
            return _vJoyControllers[x];
        }

    }
}
