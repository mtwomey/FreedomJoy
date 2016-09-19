using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

// Not using this, was just to test if there was a problem in the official wrapper

namespace FreedomJoy
{
    class Wrap
    {
        public enum VjdStat  /* Declares an enumeration data type called BOOLEAN */
        {
            VJD_STAT_OWN,   // The  vJoy Device is owned by this application.
            VJD_STAT_FREE,  // The  vJoy Device is NOT owned by any application (including this one).
            VJD_STAT_BUSY,  // The  vJoy Device is owned by another application. It cannot be acquired by this application.
            VJD_STAT_MISS,  // The  vJoy Device is missing. It either does not exist or the driver is down.
            VJD_STAT_UNKN   // Unknown
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct JoystickState
        {
            public byte bDevice;
            public Int32 Throttle;
            public Int32 Rudder;
            public Int32 Aileron;
            public Int32 AxisX;
            public Int32 AxisY;
            public Int32 AxisZ;
            public Int32 AxisXRot;
            public Int32 AxisYRot;
            public Int32 AxisZRot;
            public Int32 Slider;
            public Int32 Dial;
            public Int32 Wheel;
            public Int32 AxisVX;
            public Int32 AxisVY;
            public Int32 AxisVZ;
            public Int32 AxisVBRX;
            public Int32 AxisVBRY;
            public Int32 AxisVBRZ;
            public UInt32 Buttons;
            public UInt32 bHats;	// Lower 4 bits: HAT switch or 16-bit of continuous HAT switch
            public UInt32 bHatsEx1;	// Lower 4 bits: HAT switch or 16-bit of continuous HAT switch
            public UInt32 bHatsEx2;	// Lower 4 bits: HAT switch or 16-bit of continuous HAT switch
            public UInt32 bHatsEx3;	// Lower 4 bits: HAT switch or 16-bit of continuous HAT switch
            public UInt32 ButtonsEx1;
            public UInt32 ButtonsEx2;
            public UInt32 ButtonsEx3;
        };

        [DllImport("vJoyInterface.dll", EntryPoint = "vJoyEnabled")]
        private static extern bool _vJoyEnabled();
        public bool vJoyEnabled() { return _vJoyEnabled(); }

        [DllImport("vJoyInterface.dll", EntryPoint = "GetvJoyManufacturerString")]
        private static extern IntPtr _GetvJoyManufacturerString();
        public string GetvJoyManufacturerString() { return Marshal.PtrToStringAuto(_GetvJoyManufacturerString()); }

        [DllImport("vJoyInterface.dll", EntryPoint = "GetvJoyProductString")]
        private static extern IntPtr _GetvJoyProductString();
        public string GetvJoyProductString() { return Marshal.PtrToStringAuto(_GetvJoyProductString()); }

        [DllImport("vJoyInterface.dll", EntryPoint = "GetvJoyVersion")]
        private static extern short _GetvJoyVersion();
        public  short GetvJoyVersion() { return _GetvJoyVersion(); }

        [DllImport("vJoyInterface.dll", EntryPoint = "GetvJoySerialNumberString")]
        private static extern IntPtr _GetvJoySerialNumberString();
        public string GetvJoySerialNumberString() { return Marshal.PtrToStringAuto(_GetvJoySerialNumberString()); }

        [DllImport("vJoyInterface.dll", EntryPoint = "DriverMatch")]
        private static extern bool _DriverMatch(ref UInt32 DllVer, ref UInt32 DrvVer);
        public bool DriverMatch(ref UInt32 DllVer, ref UInt32 DrvVer) { return _DriverMatch(ref DllVer, ref DrvVer); }

        [DllImport("vJoyInterface.dll", EntryPoint = "GetVJDButtonNumber")]
        private static extern int _GetVJDButtonNumber(UInt32 rID);
        public int GetVJDButtonNumber(uint rID) { return _GetVJDButtonNumber(rID); }

        [DllImport("vJoyInterface.dll", EntryPoint = "GetVJDStatus")]
        private static extern int _GetVJDStatus(UInt32 rID);
        public VjdStat GetVJDStatus(UInt32 rID) { return (VjdStat)_GetVJDStatus(rID); }

        [DllImport("vJoyInterface.dll", EntryPoint = "AcquireVJD")]
        private static extern bool _AcquireVJD(UInt32 rID);
        public bool AcquireVJD(UInt32 rID) { return _AcquireVJD(rID); }

        [DllImport("vJoyInterface.dll", EntryPoint = "ResetVJD")]
        private static extern bool _ResetVJD(UInt32 rID);
        public bool ResetVJD(UInt32 rID) { return _ResetVJD(rID); }

        [DllImport("vJoyInterface.dll", EntryPoint = "RelinquishVJD")]
        private static extern void _RelinquishVJD(UInt32 rID);
        public void RelinquishVJD(uint rID) { _RelinquishVJD(rID); }

        public void SetBtn(bool b, uint vJoyNumber, int i)
        {
            throw new NotImplementedException();
        }

        public void UpdateVJD(int i, ref JoystickState joystickState)
        {
            throw new NotImplementedException();
        }

    }
}
