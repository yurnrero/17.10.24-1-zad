using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.IO;
using System.Windows.Shapes;

namespace _17._10._24_1_zad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            UpdateCloseButtonState();
            SetGradientBackground();
            TextBox1.TextChanged += TextBox_TextChanged;
            TextBox2.TextChanged += TextBox_TextChanged;
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            CloseButton.IsEnabled = string.IsNullOrWhiteSpace(TextBox1.Text) && string.IsNullOrWhiteSpace(TextBox2.Text);
        }
        private void SetGradientBackground()
        {
            var gradientBrush = new LinearGradientBrush();
            gradientBrush.GradientStops.Add(new GradientStop(Colors.LightBlue, 0.0));
            gradientBrush.GradientStops.Add(new GradientStop(Colors.White, 1.0));
            TextBox1.Background = gradientBrush;
            TextBox2.Background = gradientBrush;
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                TextBox1.Text = File.ReadAllText(openFileDialog.FileName);
                TextBox2.Text = File.ReadAllText(openFileDialog.FileName);
            }
            UpdateCloseButtonState();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox1.Clear();
            TextBox2.Clear();
            UpdateCloseButtonState();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void UpdateCloseButtonState()
        {
            CloseButton.IsEnabled = string.IsNullOrEmpty(TextBox1.Text) && string.IsNullOrEmpty(TextBox2.Text);
        }

        private void StyleComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (StyleComboBox.SelectedItem is System.Windows.Controls.ComboBoxItem selectedItem)
            {
                string selectedStyle = selectedItem.Content.ToString();
                ApplyTextStyle(selectedStyle);
            }
        }

        private void ApplyTextStyle(string style)
        {
            var styleParts = style.Split(',');
            if (styleParts.Length == 3)
            {
                TextBox1.FontFamily = new FontFamily(styleParts[0].Trim());
                TextBox2.FontFamily = new FontFamily(styleParts[0].Trim());

                if (double.TryParse(styleParts[1], out double fontSize))
                {
                    TextBox1.FontSize = fontSize;
                    TextBox2.FontSize = fontSize;
                }

                try
                {
                    var color = (Color)ColorConverter.ConvertFromString(styleParts[2].Trim());
                    TextBox1.Foreground = new SolidColorBrush(color);
                    TextBox2.Foreground = new SolidColorBrush(color);
                }
                catch
                {
                    // Игнорируем ошибки преобразования цвета.
                }
            }
        }
    }
}
    