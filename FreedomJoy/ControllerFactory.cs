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
        private static Dictionary<string, Controller> _physicalControllersByGuid = new Dictionary<string, Controller>();
        private static Dictionary<uint, Vcontroller> _vJoyControllers = new Dictionary<uint, Vcontroller>();

        public static Controller GetPhysicalController(string guid)
        {
            if (!_physicalControllersByGuid.ContainsKey(guid))
            {
                var controller = new Controller(guid.ToString());
                _physicalControllersByGuid.Add(controller.Guid.ToString(), controller);
            }
            return _physicalControllersByGuid[guid.ToString()];
        }

        public static void ReleasePhysicalControllers()
        {
            foreach (KeyValuePair<string, Controller> entry in _physicalControllersByGuid)
            {
                var controller = entry.Value;
                controller.Dispose();
            }
            _physicalControllersByGuid = new Dictionary<string, Controller>();
        }

        public static Vcontroller GetvJoyController(uint n) // Todo: Change this (everywhere) to use guid instead of id (like I did for physical controllers)
        {
            if (!_vJoyControllers.ContainsKey(n))
            {
                _vJoyControllers.Add(n, new Vcontroller(n));
            }
            return _vJoyControllers[n];
        }

    }
}
