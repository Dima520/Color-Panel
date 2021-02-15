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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ColorPanelWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// true - Right, false - Left.
        /// </summary>
        private bool compareColorsSwitch = true;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private static string ToHEX(Color color) => $"#{color.A:X2}{color.R:X2}{color.G:X2}{color.B:X2}";

        private static Brush ToBrush(Color color) => new SolidColorBrush(color);

        private static Color ToColor(Brush brush) => ((SolidColorBrush)brush).Color;

        private bool Checked() => (bool)cbCompareColors.IsChecked;

        /// <summary>
        /// Преобразует число из шестнадцатеричной системы счисления в десятичную.
        /// </summary>
        private static byte _16To10(string value)
        {
            Int64 x = Convert.ToInt64(value, 16);
            string temp = Convert.ToString(x, 10);
            return Convert.ToByte(temp);
        }

        private void SetSlider()
        {
            textBoxR.Text = Convert.ToByte(sliderR.Value).ToString();
            textBoxG.Text = Convert.ToByte(sliderG.Value).ToString();
            textBoxB.Text = Convert.ToByte(sliderB.Value).ToString();
        }

        private void BorderNewColor()
        {
            var color = new Color();
            color.A = 255;
            color.R = (byte)sliderR.Value;
            color.G = (byte)sliderG.Value;
            color.B = (byte)sliderB.Value;

            if (Checked())
            {
                if (compareColorsSwitch)
                    colorBorderRight.Background = ToBrush(color);
                else
                    colorBorderLeft.Background = ToBrush(color);
            }
            else
            {
                colorBorderRight.Background =
                    colorBorderLeft.Background = ToBrush(color);
            }
        }

        private void SR_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SetSlider();
            BorderNewColor();

            Color color;
            if (compareColorsSwitch)
                color = ToColor(colorBorderRight.Background);
            else
                color = ToColor(colorBorderLeft.Background);

            textBoxHEX.Text = ToHEX(color);
        }

        private void InitColorAndChek(out Color color, out bool? check)
        {
            if (compareColorsSwitch)
                color = ToColor(colorBorderRight.Background);
            else
                color = ToColor(colorBorderLeft.Background);

            check = Topmost.IsChecked;
        }

        private void BCopyRGB_Click(object sender, RoutedEventArgs e)
        {
            InitColorAndChek(out Color color, out bool? check);
            var copyToRGB = new CopyToRGB(check, color);
            copyToRGB.ShowDialog();
        }

        private void BCopyHEX_Click(object sender, RoutedEventArgs e)
        {
            InitColorAndChek(out Color color, out bool? check);
            var copyToHEX = new CopyToHEX(check, color);
            copyToHEX.ShowDialog();
        }

        private void TBR_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                switch ((sender as TextBox).Name)
                {
                    case "textBoxR":
                        {
                            var value = Convert.ToByte(textBoxR.Text);
                            sliderR.Value = value;
                        }
                        break;
                    case "textBoxG":
                        {
                            var value = Convert.ToByte(textBoxG.Text);
                            sliderG.Value = value;
                        }
                        break;
                    case "textBoxB":
                        {
                            var value = Convert.ToByte(textBoxB.Text);
                            sliderB.Value = value;
                        }
                        break;
                }
            }
            catch { }
        }

        private void TBHEX_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var color = new Color();
                string temp = textBoxHEX.Text;

                string temp1 = temp.Substring(1, 2);
                color.A = _16To10(temp1);

                string temp2 = temp.Substring(3, 2);
                sliderR.Value = color.R = _16To10(temp2);

                string temp3 = temp.Substring(5, 2);
                sliderG.Value = color.G = _16To10(temp3);

                string temp4 = temp.Substring(7, 2);
                sliderB.Value = color.B = _16To10(temp4);
            }
            catch { }
        }

        private void SetSettingsColor()
        {
            Brush brush = null;
            if (compareColorsSwitch)
                brush = colorBorderRight.Background;
            else
                brush = colorBorderLeft.Background;

            Color color = ToColor(brush);

            textBoxHEX.Text = ToHEX(color);

            textBoxR.Text = color.R.ToString();
            textBoxG.Text = color.G.ToString();
            textBoxB.Text = color.B.ToString();
        }

        private void bSwitch_Click(object sender, RoutedEventArgs e)
        {
            compareColorsSwitch = !compareColorsSwitch;

            // set image
            if (compareColorsSwitch)
            {
                bSwitchImage1.Visibility = Visibility.Visible;
                bSwitchImage2.Visibility = Visibility.Hidden;
            }
            else
            {
                bSwitchImage1.Visibility = Visibility.Hidden;
                bSwitchImage2.Visibility = Visibility.Visible;
            }

            SetSettingsColor();
        }

        private void cbCompareColors_Click(object sender, RoutedEventArgs e)
        {
            bSwitch.IsEnabled = Checked();

            if (Checked() == false)
            {
                if (compareColorsSwitch)
                    colorBorderLeft.Background = colorBorderRight.Background;
                else
                    colorBorderRight.Background = colorBorderLeft.Background;
            }
        }
    }
}
