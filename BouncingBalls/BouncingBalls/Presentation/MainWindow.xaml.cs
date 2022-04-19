using System.Windows;

namespace BouncingBalls.Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ApplyButton_OnClick(object sender, RoutedEventArgs e)
        {
            StartButton.IsEnabled = true;
        }

        private void StartButton_OnClick(object sender, RoutedEventArgs e)
        {
            ApplyButton.IsEnabled = false;
            StopButton.IsEnabled = true;
            NumberInput.IsEnabled = false;
        }

        private void StopButton_OnClick(object sender, RoutedEventArgs e)
        {
            StopButton.IsEnabled = false;
            ApplyButton.IsEnabled = true;
            StartButton.IsEnabled = false;
            NumberInput.IsEnabled = true;
        }
    }
}