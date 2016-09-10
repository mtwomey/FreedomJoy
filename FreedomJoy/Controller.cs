using System;
using System.Collections.Generic;
using SlimDX.DirectInput;
using SlimDX.XAudio2;

namespace FreedomJoy
{
    public class Controller
    {
        private readonly Joystick _joystick;
        private readonly int _standardButtonCount;
        private readonly int _povCount;
        public JoystickState JoystickState { get; set; }
        public List<Button> Buttons { get; } = new List<Button>();
        public List< Pov> Povs { get; }= new List<Pov>();

        public Controller(int controllerNumber)
        {
            DirectInput dinput = new DirectInput();
            DeviceInstance di = dinput.GetDevices(DeviceClass.GameController, DeviceEnumerationFlags.AttachedOnly)[controllerNumber];
            _joystick = new Joystick(dinput, di.InstanceGuid);
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
                Button newButton = new Button (
                    parentController: this,
                    type: Button.ButtonType.Standard,
                    buttonNumber: i + 1,
                    onValue: 1,
                    offValue: 0
                );
                Buttons.Add(newButton);
            }
        }

        private void _initPovs()
        {
            for (int i = 0; i < _povCount; i++)
            {
                Pov newPov = new Pov(
                    parentController: this,
                    type: Pov.PovType.Standard,
                    povNumber: i + 1
                );
                Povs.Add(newPov);
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
        }
    }
}
