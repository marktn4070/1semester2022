using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Data;
using System.Collections.ObjectModel;
using System.Configuration;

namespace _1Semprojekt2022_Golf
{
    /// <summary>
    /// Interaction logic for Update_route.xaml
    /// </summary>
    public partial class Update_route : Window
    {
        public string R_id_public;
        public string R_starttime_public;
        public Update_route(string R_id_string, string R_name_sting, string R_year_sting, string R_starttime_sting, string R_distance_sting)
        {
            InitializeComponent();

            R_id_public = R_id_string;
            R_starttime_public = R_starttime_sting;

            int R_starttime_int = Int32.Parse(R_starttime_sting);

            //string secs = "1000";
            TimeSpan t = TimeSpan.FromSeconds(R_starttime_int);

            //string answer = string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms",
            //                t.Hours,
            //                t.Minutes,
            //                t.Seconds,
            //                t.Milliseconds);


            R_starttimeHour_txt.Text = t.Hours.ToString();
            R_starttimeMinute_txt.Text = t.Minutes.ToString();





            R_name_txt.Text = R_name_sting;
            R_year_txt.Text = R_year_sting;

            R_distance_txt.Text = R_distance_sting;

            this.Closing += new System.ComponentModel.CancelEventHandler(Main_Window_runner_open);


        }
        SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=Golf; Integrated Security=True");

        void Main_Window_runner_open(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow secondWindow = new MainWindow();
            secondWindow.Show();
        }
        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {

        }




        public void clearData()
        {
            R_name_txt.Clear();
            R_year_txt.Clear();
            R_starttimeHour_txt.Clear();
            R_starttimeMinute_txt.Clear();
            R_distance_txt.Clear();
        }

        public bool IsValid()
        {
            if (R_name_txt.Text == string.Empty || R_year_txt.Text == string.Empty || R_starttimeHour_txt.Text == string.Empty || R_starttimeMinute_txt.Text == string.Empty || R_distance_txt.Text == string.Empty)
            {
                MessageBox.Show("En eller flere felter er ikke udfyldt", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }


        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {


            try
            {

                if (IsValid())
                {
                    con.Open();

                    int R_starttimeHour_int = Int32.Parse(R_starttimeHour_txt.Text);
                    int R_starttimeMinute_int = Int32.Parse(R_starttimeMinute_txt.Text);

                    int R_starttime = R_starttimeHour_int * 60 * 60 + R_starttimeMinute_int * 60;

                    SqlCommand cmd = new SqlCommand("update Route set R_name = '" + R_name_txt.Text + "', R_year = '" + R_year_txt.Text + "', R_starttime = '" + R_starttime + "', R_distance = '" + R_distance_txt.Text + "' WHERE R_id = '" + R_id_public + "' ", con);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("'" + R_name_txt.Text + " er opdateret", "Updated", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        con.Close();
                        clearData();

                        this.Close();
                    }
                }
                else
                {
                    if (R_name_txt.Text == string.Empty)
                    {
                        R_navn_error.Text = "Name skal udfyldes";
                    }
                    else
                    {
                        R_navn_error.Text = "";
                    }

                    if (R_year_txt.Text == string.Empty)
                    {
                        R_year__error.Text = "År skal udfyldes";
                    }
                    else
                    {
                        R_year__error.Text = "";
                    }

                    if (R_starttimeHour_txt.Text == string.Empty || R_starttimeMinute_txt.Text == string.Empty)
                    {
                        R_starttime_error.Text = "Starttid skal udfyldes";
                    }
                    else
                    {
                        R_starttime_error.Text = "";
                    }

                    if (R_distance_txt.Text == string.Empty)
                    {
                        R_distance_error.Text = "Længde skal udfyldes";
                    }
                    else
                    {
                        R_distance_error.Text = "";
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ClearDataBtn_Click(object sender, RoutedEventArgs e)
        {
            clearData();
        }

        private void SearchDataBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
