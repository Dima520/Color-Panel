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
    // AC - Alpha Channel
    // L - lattice - #

    /// <summary>
    /// Логика взаимодействия для CopyToHEX.xaml
    /// </summary>
    public partial class CopyToHEX : Window
    {
        Color color;

        public CopyToHEX(bool? topmost, Color color)
        {
            InitializeComponent();
            Topmost.IsChecked = topmost;
            this.color = color;

            CopyToHEXL.Content = CopyToHEXL.Content + ToHEXL(color);
            CopyToACHEXL.Content = CopyToACHEXL.Content + ToACHEXL(color);
            CopyToHEX_.Content = CopyToHEX_.Content + ToHEX(color);
            CopyToACHEX.Content = CopyToACHEX.Content + ToACHEX(color);
        }

        private void BExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private static string ToACHEXL(Color color) => $"#{color.A:X2}{color.R:X2}{color.G:X2}{color.B:X2}";
        private static string ToHEXL(Color color) => $"#{color.R:X2}{color.G:X2}{color.B:X2}";
        private static string ToACHEX(Color color) => $"{color.A:X2}{color.R:X2}{color.G:X2}{color.B:X2}";
        private static string ToHEX(Color color) => $"{color.R:X2}{color.G:X2}{color.B:X2}";

        private void BCopyTo_Click(object sender, RoutedEventArgs e)
        {
            string colorStr = "";
            switch ((sender as Button).Name)
            {
                case "CopyToHEXL": colorStr = ToHEXL(color); break;
                case "CopyToACHEXL": colorStr = ToACHEXL(color); break;
                case "CopyToHEX_": colorStr = ToHEX(color); break;
                case "CopyToACHEX": colorStr = ToACHEX(color); break;
            }

            Clipboard.SetText(colorStr);
            Close();
        }
    }
}
