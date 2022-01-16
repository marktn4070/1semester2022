using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Text.RegularExpressions; // Skal bruges for at kunne bruge Regex
using System;

namespace _1Semprojekt2022_Golf
{
    /// <summary>
    /// Interaction logic for Update_runner.xaml
    /// </summary>
    public partial class Update_runner : Window
    {
        public string P_id_public;
        public Update_runner(string P_id_sting, string P_name_sting, string P_mail_sting, string P_phone_sting, string P_address_sting, string P_zip_sting, string P_city_sting)
        {
            InitializeComponent();
            LoadGrid_Runner();
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
            //Search_txt.Clear();
            LoadGrid_Runner();
        }
        public void LoadGrid_Runner()
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
            else if (Regex.IsMatch(P_address_txt.Text, "[^ÆØÅæøåa-zA-Z 0-9]"))
            {
                MessageBox.Show("Vær venligst at kun indtaste tal og bogstaver ved 'Adresse'", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (IsValid())
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Participant VALUES (@P_name, @P_mail, @P_phone, @P_address, @P_zip, @P_city)", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@P_name", P_name_txt.Text);
                    cmd.Parameters.AddWithValue("@P_mail", P_mail_txt.Text);
                    cmd.Parameters.Add("@P_phone", SqlDbType.Int).Value = P_phone_txt.Text;
                    cmd.Parameters.AddWithValue("@P_address", P_address_txt.Text);
                    cmd.Parameters.Add("@P_zip", SqlDbType.Int).Value = P_zip_txt.Text;
                    cmd.Parameters.AddWithValue("@P_city", P_city_txt.Text);
                    string P_name = P_name_txt.Text;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    LoadGrid_Runner();
                    MessageBox.Show("'" + P_name + "' er nu oprettet", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
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
            if (Search_txt.Text == "")
            {
                MessageBox.Show("Id'et skal udfyldes før sletningen kan ske", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("delete from Participant where P_id = " + Search_txt.Text + " ", con);
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("'Id " + Search_txt.Text + "' deltageren er slettet", "Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                    con.Close();
                    clearData();
                    LoadGrid_Runner();
                    con.Close();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Deltageren blev ikke slettet: " + ex.Message);
                }
                finally
                {
                    con.Close();
                }

            }

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

        private void SearchDataBtn_Click(object sender, RoutedEventArgs e)
        {
            string runner_id = Search_txt.Text;
            //SqlCommand cmd = new SqlCommand("SELECT * FROM Participant WHERE P_id = '" + runner_id + "'", con);
            SqlCommand cmd = new SqlCommand("SELECT * FROM Participant WHERE P_name like '%" + runner_id + "%' or P_id like '%" + runner_id + "%'", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            daragrid.ItemsSource = dt.DefaultView;



        }






        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {

        }

    }
}
