using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using FreedomJoy.Annotations;
using FreedomJoy.Controllers;
using FreedomJoy.vJoy;
using SlimDX.DirectInput;

namespace FreedomJoy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        private bool _mainMapperTaskRunning = true;
        private SolidColorBrush _indicatorFill;
        private string _runMainButtonText = "Stop Main";
        public List<DeviceInstance> testlist01 { get; set; } = new List<DeviceInstance>();
        private int _physicalControllerCount = 0;
        private Timer _checkControllersThread;
        private DirectInput _di = new DirectInput();

        public SolidColorBrush IndicatorFill
        {
            get { return _indicatorFill; }
            set
            {
                _indicatorFill = value;
                OnPropertyChanged("IndicatorFill");
            }
        }

        public string RunMainButtonText
        {
            get { return _runMainButtonText; }
            set
            {
                _runMainButtonText = value;
                OnPropertyChanged("RunMainButtonText");
            }
        }

        // Color Brushes
        public SolidColorBrush RedBrush = new SolidColorBrush(Colors.Red);
        public SolidColorBrush GreenBrush = new SolidColorBrush(Colors.Green);

        Map _map;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            IndicatorFill = GreenBrush;
            string s1="";
            BindingOperations.EnableCollectionSynchronization(Logger.Logs, ""); // Todo: figure out why this works and how it really should be done to log to a static class and bind to it as well...
            Logger.Log("Starting up...");
            try
            {
                _map = new Map();
            }
            catch (Exception e)
            {
                Logger.Log(e.Message);
            }


            var _dinput = new DirectInput();
            foreach (var device in _dinput.GetDevices(DeviceClass.GameController, DeviceEnumerationFlags.AttachedOnly))
            {
                testlist01.Add(device);
            };

            _checkControllersThread = new Timer(_checkControllersCallback, null, 0, 5000);

        }


        private void _checkControllersCallback(Object state)
        {
            try
            {
                var newPhysicalControllerCount = _di.GetDevices(DeviceClass.GameController, DeviceEnumerationFlags.AttachedOnly).Count; // I think this call is expensive, if I do it too often, CPU util goes up
                if (newPhysicalControllerCount != _physicalControllerCount)
                {
                    Logger.Log("Controller count changed from " + _physicalControllerCount + " to " + newPhysicalControllerCount);
                    _physicalControllerCount = newPhysicalControllerCount; // This is first so that it's updated ahead of time in case of exception
                    _map.Stop();
                    ControllerFactory.ReleasePhysicalControllers();
                    _map.Reset();
                    _map.Run();
                    
                }
            }
            catch (Exception e)
            {
                Logger.Log(e.Message);
            }
        }

        private void ButtonRunMain_Click(object sender, RoutedEventArgs e)
        {
            var test = new ControllerInfo(); // Todo: rename this
            test.Refresh();


            if (!_mainMapperTaskRunning)
            {
                _map.Run();
                _mainMapperTaskRunning = true;
                IndicatorFill = GreenBrush;
                RunMainButtonText = "Stop Main";
            }
            else
            {
                _mainMapperTaskRunning = false;
                IndicatorFill = RedBrush;
                RunMainButtonText = "Start Main";
                _map.Stop();
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
