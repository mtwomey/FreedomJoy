using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlimDX.DirectInput;

namespace FreedomJoy.Controllers
{
    class ControllerInfo
    {

        private readonly DirectInput _dinput = new DirectInput();

        public ControllerInfo()
        {
            Refresh();
        }

        public void Refresh()
        {
            var devices = _dinput.GetDevices(DeviceClass.GameController, DeviceEnumerationFlags.AttachedOnly);
            var x = 10;
        }
    }
}
