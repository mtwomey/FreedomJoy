﻿using System;
using System.Collections.Generic;

namespace FreedomJoy.vJoy
{
    class Vcontroller
    {

        public vJoyInterfaceWrap.vJoy Vjoy;
        private readonly uint _vJoyNumber;
        private readonly int _buttonCount;
        public vJoyInterfaceWrap.vJoy.JoystickState JoystickState; // Doesn't work as a property becase it's a struct
        public List<VjoyButton> Buttons = new List<VjoyButton>();
        public Dictionary<string, VjoyButton> ButtonsByName = new Dictionary<string, VjoyButton>();
        public List<VjoyAxis> Axes { get; } = new List<VjoyAxis>();
        public Dictionary<string, VjoyAxis> AxesByName = new Dictionary<string, VjoyAxis>();
        public int UpdateRate { get; set; }

        public Vcontroller(uint vJoyNumber, int updateRate = 20)
        {
            _vJoyNumber = vJoyNumber;
            Vjoy = new vJoyInterfaceWrap.vJoy();
            JoystickState = new vJoyInterfaceWrap.vJoy.JoystickState();
            VjdStat status = Vjoy.GetVJDStatus(_vJoyNumber);
            if (status == VjdStat.VJD_STAT_OWN || status == VjdStat.VJD_STAT_FREE)
            {
                Vjoy.AcquireVJD(_vJoyNumber);
            }
            else
            {
                throw (new Exception("Could not acquire Vjoy"));
            }
            Vjoy.ResetVJD(_vJoyNumber);
            _buttonCount = Vjoy.GetVJDButtonNumber(_vJoyNumber);
            _initButtons();
            _initDiscPovs();
            _initAxes();
            _initJoystickState();
            UpdateRate = updateRate;
        }

        public void Update()
        {
            Vjoy.UpdateVJD(1, ref JoystickState);
        }

        private void _initJoystickState()
        {
            JoystickState.bDevice = (byte)_vJoyNumber;
//            JoystickState.AxisX = 16000; // Range is 0 - 32k (or maybe starts at 1 - see: http://vjoystick.sourceforge.net/joomla256.02/index.php/forum/4-Help/989-unexpected-value-range-on-axes)
//            JoystickState.AxisY = 16000;
//            JoystickState.AxisZ = 16000;
        }
        public void Close()
        {
            Vjoy.ResetVJD(_vJoyNumber);
            Vjoy.RelinquishVJD(_vJoyNumber);
        }

        private void _initDiscPovs() // TODO: implement this later
        {
            
        }

        private void _initAxes()
        {
            foreach (string axisName in Enum.GetNames(typeof(HID_USAGES))) // This Enum crap was a PITA
            {
                HID_USAGES axis = (HID_USAGES)Enum.Parse(typeof(HID_USAGES), axisName);
                if (Vjoy.GetVJDAxisExist(_vJoyNumber, axis))
                {
                    VjoyAxis vjoyAxis = new VjoyAxis(
                        parentVcontroller: this,
                        name: axisName.Replace("HID_USAGE_", "").ToLower()
                    );
                    Axes.Add(vjoyAxis);
                    AxesByName.Add(axisName.Replace("HID_USAGE_", "").ToLower(), vjoyAxis);
                }
            }
        }

        private void _initButtons()
        {
            for (int i = 0; i < _buttonCount; i++)
            {
                VjoyButton newVjoyButton = new VjoyButton(
                    parentVcontroller: this,
                    name: "b" + (i + 1),
                    buttonNumber: (uint)i + 1 
                );
                Buttons.Add(newVjoyButton);
                ButtonsByName.Add("b" + (i + 1), newVjoyButton);
            }
        }

        public void TestButton()
        {
            Vjoy.SetBtn(true, _vJoyNumber, 3);
        }

        public Dictionary<string, string> GetInfo() // Just for testing
        {
            Dictionary<string, string> info = new Dictionary<string, string>();
            if (Vjoy.vJoyEnabled())
            {
                info["Vendor"] = Vjoy.GetvJoyManufacturerString();
                info["Product"] = Vjoy.GetvJoyProductString();
                info["Serial"] = Vjoy.GetvJoyVersion().ToString(); // Why are serial and version backwards??
                info["Version"] = Vjoy.GetvJoySerialNumberString();

                UInt32 DllVer = 0, DrvVer = 0;
                info["Driver Match"] = Vjoy.DriverMatch(ref DllVer, ref DrvVer).ToString();
                info["Number of Buttons"] = Vjoy.GetVJDButtonNumber(_vJoyNumber).ToString();

                uint id = 1; // First device is 1 (not 0) - there is also no way to check for the number of devices (at least that I can find).
                VjdStat status = Vjoy.GetVJDStatus(id);
                switch (status)
                {
                    case VjdStat.VJD_STAT_OWN:
                        info["Status"] = "vJoy Device " + _vJoyNumber + " is already owned by this feeder";
                        break;
                    case VjdStat.VJD_STAT_FREE:
                        info["Status"] = "vJoy Device " + _vJoyNumber + " is free";
                        break;
                    case VjdStat.VJD_STAT_BUSY:
                        info["Status"] = "vJoy Device " + _vJoyNumber + " is already owned by another feeder";
                        break;
                    case VjdStat.VJD_STAT_MISS:
                        info["Status"] = "vJoy Device " + _vJoyNumber + "  is not installed or disabled";
                        break;
                    default:
                        info["Status"] = "vJoy Device " + _vJoyNumber + " general error";
                        break;
                }
            }
            else
            {
                info["Status"] = "vJoy driver not enabled: Failed Getting vJoy attributes";
            }
            return info;
        }

    }
}
