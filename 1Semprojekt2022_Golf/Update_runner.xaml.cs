using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Text.RegularExpressions; // Skal bruges for at kunne bruge Regex
using System;

namespace _1Semprojekt2022_Golf
{
    /// <summary>
    /// ///Mark
    /// </summary>
    public partial class Update_runner : Window
    {
        public string P_id_public;
        public Update_runner(string P_id_sting, string P_name_sting, string P_mail_sting, string P_phone_sting, string P_address_sting, string P_zip_sting, string P_city_sting)
        {
            InitializeComponent();
            this.Closing += new System.ComponentModel.CancelEventHandler(Main_Window_runner_open);

            P_id_public = P_id_sting;
            P_name_txt.Text = P_name_sting;
            P_mail_txt.Text = P_mail_sting;
            P_phone_txt.Text = P_phone_sting;
            P_address_txt.Text = P_address_sting;
            P_zip_txt.Text = P_zip_sting;
            P_city_txt.Text = P_city_sting;
        }


        SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=Golf; Integrated Security=True");

        public void clearData()
        {
            P_name_txt.Clear();
            P_mail_txt.Clear();
            P_phone_txt.Clear();
            P_address_txt.Clear();
            P_zip_txt.Clear();
            P_city_txt.Clear();
        }


        private void ClearDataBtn_Click(object sender, RoutedEventArgs e)
        {
            clearData();
        }
        public bool IsValid()
        {

            //Navne feltet
            if (P_name_txt.Text == string.Empty)
            {
                MessageBox.Show("Navn skal udfyldes", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (Regex.IsMatch(P_name_txt.Text, "[^æøåÆØÅa-zA-Z ]"))
            {
                MessageBox.Show("Vær venligst at indtaste bogstaver ved 'Navn'", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            // Email feltet
            if (P_mail_txt.Text == string.Empty)
            {
                MessageBox.Show("E-mail skal udfyldes", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (!Regex.IsMatch(P_mail_txt.Text, @"^[-!#$%&'*+/0-9=?A-Z^_a-z{|}~](\.?[-!#$%&'*+/0-9=?A-Z^_a-z{|}~])*@[a-zA-Z](-?[a-zA-Z0-9])*(\.[a-zA-Z](-?[a-zA-Z0-9])*)+$"))
            {
                MessageBox.Show("Vær venligst at indtaste en korrekt e-mail adresse", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            // Telefonnummer feltet
            if (P_phone_txt.Text == string.Empty)
            {
                MessageBox.Show("Telefon nr. skal udfyldes", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (Regex.IsMatch(P_phone_txt.Text, "[^0-9]"))
            {
                MessageBox.Show("Vær venligst at indtaste tal ved 'Telefon nr'", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                P_phone_txt.Text = P_phone_txt.Text.Remove(P_phone_txt.Text.Length - 1);
                return false;
            }

            // Adresse feltet
            if (P_address_txt.Text == string.Empty)
            {
                MessageBox.Show("Addresse skal udfyldes", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            // Postnummer feltet
            if (P_zip_txt.Text == string.Empty)
            {
                MessageBox.Show("Post nr. skal udfyldes", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (Regex.IsMatch(P_zip_txt.Text, "[^0-9](0-4)"))
            {
                MessageBox.Show("Vær venligst at kun indtaste tal ved 'Post nr'", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            // By feltet
            if (P_city_txt.Text == string.Empty)
            {
                MessageBox.Show("Name is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (Regex.IsMatch(P_city_txt.Text, "[^ÆØÅæøåa-zA-Z]"))
            {
                MessageBox.Show("Vær venligst at indtaste bogstaver ved 'By'", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    SqlCommand cmd = new SqlCommand("update Participant set P_name = '" + P_name_txt.Text + "', P_mail = '" + P_mail_txt.Text + "', P_phone = '" + P_phone_txt.Text + "', P_address = '" + P_address_txt.Text + "', P_zip = '" + P_zip_txt.Text + "', P_city = '" + P_city_txt.Text + "' WHERE P_id = '" + P_id_public + "' ", con);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("'" + P_name_txt.Text + " er opdateret", "Updated", MessageBoxButton.OK, MessageBoxImage.Information);
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
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }





        }

        void Main_Window_runner_open(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow secondWindow = new MainWindow();
            secondWindow.Show();
        }




    }
}