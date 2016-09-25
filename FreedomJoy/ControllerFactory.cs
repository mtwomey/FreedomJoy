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

        public static Controller GetPhysicalController(uint n)
        {
            if (!_physicalControllers.ContainsKey(n))
            {
                _physicalControllers.Add(n, new Controller(n));
            }
            return _physicalControllers[n];
        }

        public static Vcontroller GetvJoyController(uint n)
        {
            if (!_vJoyControllers.ContainsKey(n))
            {
                _vJoyControllers.Add(n, new Vcontroller(n));
            }
            return _vJoyControllers[n];
        }

    }
}
