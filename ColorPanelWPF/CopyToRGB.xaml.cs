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
using System.Windows.Shapes;

namespace ColorPanelWPF
{
    /// <summary>
    /// Логика взаимодействия для CopyToRGB.xaml
    /// </summary>
    public partial class CopyToRGB : Window
    {
        Color color;

        public CopyToRGB(bool? topmost, Color color)
        {
            InitializeComponent();
            Topmost.IsChecked = topmost;
            this.color = color;

            CopyToComma.Content = CopyToComma.Content + ToRGBComma(color);
            CopyToSemicolon.Content = CopyToSemicolon.Content + ToRGBSemicolon(color);
        }

        private void BExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private static string ToRGBComma(Color color) => $"{color.R}, {color.G}, {color.B}";
        private static string ToRGBSemicolon(Color color) => $"{color.R}; {color.G}; {color.B}";

        private void BCopyToComma_Click(object sender, RoutedEventArgs e)
        {
            string colorStr = "";
            switch ((sender as Button).Name)
            {
                case "CopyToComma": colorStr = ToRGBComma(color); break;
                case "CopyToSemicolon": colorStr = ToRGBSemicolon(color); break;
            }

            Clipboard.SetText(colorStr);
            Close();
        }
    }
}
