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
        private DirectInput _dinput; // Not really sure if I need to hang on to this for closing or if I can dispose of it after I get the joystick...
        public JoystickState JoystickState { get; set; }
        public List<Button> Buttons { get; } = new List<Button>();
        public Dictionary<string, Button> ButtonsByName = new Dictionary<string, Button>();
        public List< Pov> Povs { get; }= new List<Pov>();
        public Dictionary<string, Pov> PovsByName = new Dictionary<string, Pov>();

        public Controller(uint controllerNumber)
        {
             _dinput = new DirectInput();
            DeviceInstance di = _dinput.GetDevices(DeviceClass.GameController, DeviceEnumerationFlags.AttachedOnly)[(int)controllerNumber];
            _joystick = new Joystick(_dinput, di.InstanceGuid);
            _standardButtonCount = _joystick.Capabilities.ButtonCount;
            _povCount = _joystick.Capabilities.PovCount;
            _initButtons();
            _initPovs();
            _joystick.SetCooperativeLevel(IntPtr.Zero, CooperativeLevel.Background | CooperativeLevel.Nonexclusive);
            _joystick.Acquire();
            JoystickState = new JoystickState();
        }

        private void _initButtons()
        {
            for (int i = 0; i < _standardButtonCount; i++)
            {
                StandardButton newButton = new StandardButton (
                    parentController: this,
                    name: "b" + (i + 1),
                    buttonNumber: i + 1
                );
                Buttons.Add(newButton);
                ButtonsByName.Add("b" + (i + 1), newButton);
            }
        }

        private void _initPovs()
        {
            for (int i = 0; i < _povCount; i++)
            {
                Pov newPov = new Pov(
                    parentController: this,
                    type: Pov.PovType.Standard,
                    name: "pov" + (i + 1),
                    povNumber: i + 1
                );
                Povs.Add(newPov);
                PovsByName.Add("pov" + (i + 1), newPov);
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
