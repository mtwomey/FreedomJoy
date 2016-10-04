using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using System.Threading;
using System.Threading.Tasks;
using FreedomJoy.Annotations;
using FreedomJoy.Controllers;
using SlimDX.DirectInput;

namespace FreedomJoy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        private CancellationTokenSource _mainMapperTaskTokenSource;
        private bool _mainMapperTaskRunning = false;
        private SolidColorBrush _indicatorFill;
        private string _runMainButtonText = "Start Main";
        public List<DeviceInstance> testlist01 { get; set; } = new List<DeviceInstance>();

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
            IndicatorFill = RedBrush;
            try
            {
                _map = new Map();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "FreedomJoy Error");
                System.Environment.Exit(1);
            }


            var _dinput = new DirectInput();
            foreach (var device in _dinput.GetDevices(DeviceClass.GameController, DeviceEnumerationFlags.AttachedOnly))
            {
                testlist01.Add(device);
            };

        }

        private void ButtonRunMain_Click(object sender, RoutedEventArgs e)
        {
            var test = new ControllerInfo(); // Todo: rename this
            test.Refresh();


            if (!_mainMapperTaskRunning)
            {
                _mainMapperTaskTokenSource = new CancellationTokenSource();
                Task.Factory.StartNew(() => { _map.Run(_mainMapperTaskTokenSource.Token); });

                _mainMapperTaskRunning = true;
                IndicatorFill = GreenBrush;
                RunMainButtonText = "Stop Main";
            }
            else
            {
                _mainMapperTaskTokenSource.Cancel();
                _mainMapperTaskRunning = false;
                IndicatorFill = RedBrush;
                RunMainButtonText = "Start Main";
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
