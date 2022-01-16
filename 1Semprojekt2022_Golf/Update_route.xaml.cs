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

namespace _1Semprojekt2022_Golf
{
    /// <summary>
    /// Interaction logic for Update_route.xaml
    /// </summary>
    public partial class Update_route : Window
    {
        public Update_route(string R_name_sting, string R_year_sting, string R_starttime_sting, string R_distance_sting)
        {
            InitializeComponent();


            R_name_txt.Text = R_name_sting;
            R_year_txt.Text = R_year_sting;
            R_starttime_txt.Text = R_starttime_sting;
            R_distance_txt.Text = R_distance_sting;


        }

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ClearDataBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SearchDataBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
