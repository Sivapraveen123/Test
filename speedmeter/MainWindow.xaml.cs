using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace speedmeter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double _currentSpeed = 0;
        private readonly DispatcherTimer _timer;
        public MainWindow()
        {
            InitializeComponent();
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(100); // smooth interval
            _timer.Tick += Timer_Tick;
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (_currentSpeed < 200)
            {
                _currentSpeed += 2; // accelerate gradually
                AnimateNeedle(_currentSpeed);
                SpeedText.Text = $"Speed: {(int)_currentSpeed}";
            }
            else
            {
                _timer.Stop(); // stop when max speed reached
            }
        }

        private void AnimateNeedle(double speed)
        {
            double angle = (speed / 200.0) * 180 - 90;

            var animation = new DoubleAnimation
            {
                To = angle,
                Duration = TimeSpan.FromMilliseconds(200),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };

            NeedleRotate.BeginAnimation(System.Windows.Media.RotateTransform.AngleProperty, animation);
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            _timer.Start();
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
        }
    }
}
