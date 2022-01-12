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
        Administrator admin = new Administrator();

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
                    Title.Text = "Deltager";
                    page_deltager.Visibility = Visibility.Visible;
                    page_tidstagning.Visibility = Visibility.Hidden;
                    page_løberute.Visibility = Visibility.Hidden;

                    break;

                case 1:
                    Title.Text = "Løberute";
                    page_deltager.Visibility = Visibility.Hidden;
                    page_tidstagning.Visibility = Visibility.Visible;
                    page_løberute.Visibility = Visibility.Hidden;
                    break;
                case 2:
                    Title.Text = "Tidstagning";
                    page_deltager.Visibility = Visibility.Hidden;
                    page_tidstagning.Visibility = Visibility.Hidden;
                    page_løberute.Visibility = Visibility.Visible;
                    break;



            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Create_runner secondWindow = new Create_runner(admin);
            secondWindow.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            update_runner secondWindow = new update_runner();
            secondWindow.Show();
        }
    }
}
