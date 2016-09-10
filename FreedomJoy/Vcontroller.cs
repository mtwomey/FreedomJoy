// Note - GetVJDStatus causes occasional crashes. I think it's bugged. I see it when I'm running console app repeatedly...

using System;
using System.Collections.Generic;
using vJoyInterfaceWrap;

namespace FreedomJoy
{
    class Vcontroller
    {
        public vJoy Vjoy;
        private readonly uint _vJoyNumber;
        private readonly int _buttonCount;
        public vJoy.JoystickState JoystickState; // Doesn't work as a property becase it's a struct
        public List<VjoyButton> Buttons { get; } = new List<VjoyButton>();

        public Vcontroller(uint vJoyNumber)
        {
            _vJoyNumber = vJoyNumber;
            Vjoy = new vJoy();
            JoystickState = new vJoy.JoystickState();
            Vjoy.AcquireVJD(_vJoyNumber);
            Vjoy.ResetVJD(_vJoyNumber);
            _buttonCount = Vjoy.GetVJDButtonNumber(_vJoyNumber);
            _initButtons();
            _initJoystickState();
        }

        public void Update()
        {
            Vjoy.UpdateVJD(1, ref JoystickState);
        }

        private void _initJoystickState()
        {
            JoystickState.bDevice = (byte)_vJoyNumber;
            JoystickState.AxisX = 16000; // Range is 0 - 32k (or maybe starts at 1 - see: http://vjoystick.sourceforge.net/joomla256.02/index.php/forum/4-Help/989-unexpected-value-range-on-axes)
            JoystickState.AxisY = 16000;
            JoystickState.AxisZ = 16000;
        }
        public void Close()
        {
            Vjoy.ResetVJD(_vJoyNumber);
            Vjoy.RelinquishVJD(_vJoyNumber);
        }
        private void _initButtons()
        {
            for (int i = 0; i < _buttonCount; i++)
            {
                VjoyButton vjoyButton = new VjoyButton(
                    parentVcontroller: this,
                    buttonNumber: (uint)i + 1 
                );
                Buttons.Add(vjoyButton);
            }
        }

        public void TestButton()
        {
            Vjoy.SetBtn(true, _vJoyNumber, 3);
        }

        public void GetInfo() // Just for testing
        {
            if (Vjoy.vJoyEnabled())
            {
                Console.WriteLine("Vendor: " + Vjoy.GetvJoyManufacturerString());
                Console.WriteLine("Product: " + Vjoy.GetvJoyProductString());
                Console.WriteLine("Serial: " + Vjoy.GetvJoyVersion()); // Why are serial and version backwards??
                Console.WriteLine("Version: " + Vjoy.GetvJoySerialNumberString());
                UInt32 DllVer = 0, DrvVer = 0;
                Console.WriteLine("Driver Match: " + Vjoy.DriverMatch(ref DllVer, ref DrvVer));
                Console.WriteLine("Number of Buttons: " + Vjoy.GetVJDButtonNumber(1));

                uint id = 1; // First device is 1 (not 0) - there is also no way to check for the number of devices (at least that I can find).
                VjdStat status = Vjoy.GetVJDStatus(id);
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
                }
            }
            else
            {
                Console.WriteLine("vJoy driver not enabled: Failed Getting vJoy attributes.");
            }
        }

    }
}
