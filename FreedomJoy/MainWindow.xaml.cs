using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using System.Threading;
using FreedomJoy.Annotations;

namespace FreedomJoy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        private Thread _mainMapperThread;
        private bool _mainRunning = false;
        private SolidColorBrush _indicatorFill;
        private string _runMainButtonText = "Start Main";

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

        Map _map = new Map();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            IndicatorFill = RedBrush;
        }

        private void ButtonRunMain_Click(object sender, RoutedEventArgs e)
        {
            if (!_mainRunning)
            {
                _mainMapperThread =
                    new Thread(() => // TODO: Figure out how to do this correctly... need to track it somewhere, ...etc
                    {
                        _map.Run();
                    });
                _mainMapperThread.IsBackground = true;
                _mainMapperThread.Start();
                _mainRunning = true;
                IndicatorFill = GreenBrush;
                RunMainButtonText = "Stop Main";
            }
            else
            {
                _map.Stop();
                IndicatorFill = RedBrush;
                RunMainButtonText = "Start Main";
                _mainRunning = false;
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
