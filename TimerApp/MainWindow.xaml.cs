using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace TimerApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new TimerViewModel();

        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+"); // Regex to match non-numeric characters
            e.Handled = regex.IsMatch(e.Text);
        }

    }
}