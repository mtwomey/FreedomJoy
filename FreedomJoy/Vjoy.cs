using System;
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

                uint id = 1; // First device is 1 (not 0) - there is also no way to check for the number of devices (at least that I can find).
                VjdStat status = _vJoy.GetVJDStatus(id);

                switch (status)
                {
                    case VjdStat.VJD_STAT_OWN:
                        Console.WriteLine("vJoy Device {0} is already owned by this feeder\n", id);
                        break;
                    case VjdStat.VJD_STAT_FREE:
                        Console.WriteLine("vJoy Device {0} is free\n", id);
                        break;
                    case VjdStat.VJD_STAT_BUSY:
                        Console.WriteLine(
                           "vJoy Device {0} is already owned by another feeder\nCannot continue\n", id);
                        return;
                    case VjdStat.VJD_STAT_MISS:
                        Console.WriteLine(
                            "vJoy Device {0} is not installed or disabled\nCannot continue\n", id);
                        return;
                    default:
                        Console.WriteLine("vJoy Device {0} general error\nCannot continue\n", id);
                        return;
                };
            }
            else
            {
                Console.WriteLine("vJoy driver not enabled: Failed Getting vJoy attributes.");
            };
        }

    }
}
