using System;
using System.Collections.Generic;
using SlimDX.DirectInput;

namespace FreedomJoy.Controllers
{
    public class Controller : IDisposable
    {
        private readonly Joystick _joystick;
        private readonly int _standardButtonCount;
        private readonly int _povCount;
        private readonly DirectInput _dinput; // Not really sure if I need to hang on to this for closing or if I can dispose of it after I get the joystick...
        public JoystickState JoystickState { get; set; }
        public List<Button> Buttons { get; } = new List<Button>();
        public readonly Dictionary<string, Button> ButtonsByName = new Dictionary<string, Button>();
        public List<Pov> Povs { get; }= new List<Pov>();
        public readonly Dictionary<string, Pov> PovsByName = new Dictionary<string, Pov>();
        private readonly int _axisCount;
        public List<Axis> Axes = new List<Axis>();
        public readonly Dictionary<string, Axis> AxesByName = new Dictionary<string, Axis>();

        public Controller(uint controllerNumber)
        {
             _dinput = new DirectInput();
            DeviceInstance di = _dinput.GetDevices(DeviceClass.GameController, DeviceEnumerationFlags.AttachedOnly)[(int)controllerNumber];
            _joystick = new Joystick(_dinput, di.InstanceGuid);
            _joystick.Properties.SetRange(1, 32768); // Match vJoy
            _standardButtonCount = _joystick.Capabilities.ButtonCount;
            _axisCount = _joystick.Capabilities.AxesCount;
            _povCount = _joystick.Capabilities.PovCount;
            _initButtons();
            _initPovs();
            _initAxes();
            _joystick.SetCooperativeLevel(IntPtr.Zero, CooperativeLevel.Background | CooperativeLevel.Nonexclusive);
            _joystick.Acquire();
            JoystickState = new JoystickState();
        }

        private void _initButtons()
        {
            for (int i = 0; i < _standardButtonCount; i++)
            {
                string name = "b" + (i + 1);
                StandardButton newButton = new StandardButton (
                    parentController: this,
                    name: name,
                    buttonNumber: i + 1
                );
                Buttons.Add(newButton);
                ButtonsByName.Add(name, newButton);
            }
        }

        private void _initPovs()
        {
            for (int i = 0; i < _povCount; i++)
            {
                string name = "pov" + (i + 1);
                Pov newPov = new Pov(
                    parentController: this,
                    type: Pov.PovType.Standard,
                    name: name,
                    povNumber: i + 1
                );
                Povs.Add(newPov);
                PovsByName.Add(name, newPov);
            }
        }

        private void _initAxes()
        {
            for (int i = 0; i < _axisCount; i++)
            {
                string name = "axis" + (i + 1); 
                Axis newAxis = new Axis(
                    parentController: this,
                    name: name,
                    axisNumber: i
                );
                Axes.Add(newAxis);
                AxesByName.Add(name, newAxis);
            }
        }

        public void ConfigurePov(int povNum, Pov.PovType povType)
        {
            Povs[povNum].SetType(povType);
        }

        public void Update()
        {
            _joystick.Poll();
            JoystickState = _joystick.GetCurrentState();
        }

        public void Close()
        {
            _joystick.Dispose();
            _dinput.Dispose();
        }

        public void Dispose()
        {
            _joystick.Dispose();
            _dinput.Dispose();
        }
    }
}
