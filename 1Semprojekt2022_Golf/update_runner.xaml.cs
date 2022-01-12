using System;
using System.Collections.Generic;
using System.Data;
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

namespace _1Semprojekt2022_Golf
{
    /// <summary>
    /// Interaction logic for update_runner.xaml
    /// </summary>
    public partial class update_runner : Window
    {
        public update_runner()
        {
            InitializeComponent();
            LoadGrid();
        }


        SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=Golf; Integrated Security=True");

        public void clearData()
        {
            P_name_txt.Clear();
            P_mail_txt.Clear();
            P_phone_txt.Clear();
            P_adress_txt.Clear();
            P_zip_txt.Clear();
            P_city_txt.Clear();
        }
        public void LoadGrid()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Participant", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            daragrid.ItemsSource = dt.DefaultView;

        }

        private void ClearDataBtn_Click(object sender, RoutedEventArgs e)
        {
            clearData();
        }
        public bool IsValid()
        {
            //if (P_name_txt.Text == string.Empty)
            //{
            //    MessageBox.Show("Name is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return false;
            //}
            //if (P_mail_txt.Text == string.Empty)
            //{
            //    MessageBox.Show("Name is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return false;
            //}
            //if (P_phone_txt.Text == string.Empty)
            //{
            //    MessageBox.Show("Name is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return false;
            //}
            //if (P_zip_txt.Text == string.Empty)
            //{
            //    MessageBox.Show("Name is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return false;
            //}
            //if (P_city_txt.Text == string.Empty)
            //{
            //    MessageBox.Show("Name is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return false;
            //}
            //if (P_adress_txt.Text == string.Empty)
            //{
            //    MessageBox.Show("Name is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return false;
            //}
            return true;
        }

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (IsValid())
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Participant VALUES (@P_name, @P_mail, @P_phone, @P_adress, @P_zip, @P_city)", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@P_name", P_name_txt.Text);
                    cmd.Parameters.AddWithValue("@P_mail", P_mail_txt.Text);
                    cmd.Parameters.Add("@P_phone", SqlDbType.Int).Value = P_phone_txt.Text;
                    cmd.Parameters.AddWithValue("@P_adress", P_adress_txt.Text);
                    cmd.Parameters.Add("@P_zip", SqlDbType.Int).Value = P_zip_txt.Text;
                    cmd.Parameters.AddWithValue("@P_city", P_city_txt.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    LoadGrid();
                    MessageBox.Show("Successfully registered", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                    clearData();

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

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
