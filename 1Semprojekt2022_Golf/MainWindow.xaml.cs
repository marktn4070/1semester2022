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

namespace _1Semprojekt2022_Golf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

            private void Button_Click(object sender, RoutedEventArgs e)
            {
                int index = int.Parse(((Button)e.Source).Uid);

                GridCursor.Margin = new Thickness(10 + (150 * index), 0, 0, 0);

                switch (index)
                {
                    case 0:
                        Test.Text = "1";
                        break;
                    case 1:
                        Test.Text = "2";
                        break;
                    case 2:
                        Test.Text = "3";
                        break;
                    case 3:
                        Test.Text = "4";
                        break;
                    case 4:
                        Test.Text = "5";
                        break;
                    case 5:
                        Test.Text = "6";
                        break;
                    case 6:
                        Test.Text = "7";
                        break;
                }
            }
        }
    }
