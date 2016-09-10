using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vJoyInterfaceWrap;

namespace FreedomJoy
{
    class Vjoy
    {
        private vJoy _vJoy;

        public Vjoy()
        {
            _vJoy = new vJoy();
        }

        public void GetInfo() // Just for testing
        {
            if (_vJoy.vJoyEnabled())
            {
                Console.WriteLine("Vendor: " + _vJoy.GetvJoyManufacturerString());
                Console.WriteLine("Product: " + _vJoy.GetvJoyProductString());
                Console.WriteLine("Serial: " + _vJoy.GetvJoyVersion()); // Why are serial and version backwards??
                Console.WriteLine("Version: " + _vJoy.GetvJoySerialNumberString());
                UInt32 DllVer = 0, DrvVer = 0;
                Console.WriteLine("Driver Match: " + _vJoy.DriverMatch(ref DllVer, ref DrvVer));
            }
            else
            {
                Console.WriteLine("vJoy driver not enabled: Failed Getting vJoy attributes.");
            };
        }

    }
}
