using System;
using System.Text.RegularExpressions;
using System.Windows;

namespace _1Semprojekt2022_Golf
{
    /// <summary>
    /// Interaction logic for Create_runner.xaml
    /// </summary>
    public partial class Create_runner : Window
    {
        public Administrator admin = null;
        public Create_runner(Administrator admn)
        {
            admin = admn;
            InitializeComponent();
            //LoadGrid_Runner();
            this.Closing += new System.ComponentModel.CancelEventHandler(Create_runner_Closing);
        }
        public bool IsValid()
        {



            // Navne feltet
            if (Name.Text == string.Empty)
            {
                MessageBox.Show("Navn skal udfyldes", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (Regex.IsMatch(Name.Text, "[^æøåÆØÅa-zA-Z ]"))
            {
                MessageBox.Show("Vær venligst at indtaste bogstaver ved 'Navn'", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            // Email feltet
            if (Mail.Text == string.Empty)
            {
                MessageBox.Show("E-mail skal udfyldes", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (!Regex.IsMatch(Mail.Text, @"^[-!#$%&'*+/0-9=?A-Z^_a-z{|}~](\.?[-!#$%&'*+/0-9=?A-Z^_a-z{|}~])*@[a-zA-Z](-?[a-zA-Z0-9])*(\.[a-zA-Z](-?[a-zA-Z0-9])*)+$"))
            {
                MessageBox.Show("Vær venligst at indtaste en korrekt e-mail adresse", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            // Telefonnummer feltet
            if (Phone.Text == string.Empty)
            {
                MessageBox.Show("Telefon nr. skal udfyldes", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (Regex.IsMatch(Phone.Text, "[^0-9]"))
            {
                MessageBox.Show("Vær venligst at indtaste tal ved 'Telefon nr'", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                Phone.Text = Phone.Text.Remove(Phone.Text.Length - 1);
                return false;
            }

            // Adresse feltet
            if (Address.Text == string.Empty)
            {
                MessageBox.Show("Addresse skal udfyldes", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            // Postnummer feltet
            if (Zip.Text == string.Empty)
            {
                MessageBox.Show("Post nr. skal udfyldes", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (Regex.IsMatch(Zip.Text, "[^0-9](0-4)"))
            {
                MessageBox.Show("Vær venligst at kun indtaste tal ved 'Post nr'", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            // By feltet
            if (City.Text == string.Empty)
            {
                MessageBox.Show("Name is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (Regex.IsMatch(City.Text, @"[^ÆØÅæøåa-zA-Z^\s]"))
            {
                MessageBox.Show("Vær venligst at indtaste bogstaver ved 'By'", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }


        private void Tilfoej_deltager_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (IsValid())
                {
                    admin.MakeNewRunner(Name.Text, Mail.Text, int.Parse(Phone.Text), Address.Text, int.Parse(Zip.Text), City.Text);
                MessageBox.Show("Løber gemt"); //måske behøver vi ikke dette......
                Clear();
                //LoadGrid_Runner();
                Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Clear()
        {
            Name.Clear();
            Mail.Clear();
            Phone.Clear();
            Address.Clear();
            Zip.Clear();
            City.Clear();
        }


        void Create_runner_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow secondWindow = new MainWindow();
            secondWindow.Show();
        }

        private void Nulstil_Felter_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }
        //public void LoadGrid_Runner()
        //{
        //    SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=Golf; Integrated Security=True");

        //    SqlCommand cmd = new SqlCommand("SELECT * FROM Participant", con);
        //    DataTable dt = new DataTable();
        //    con.Open();
        //    SqlDataReader sdr = cmd.ExecuteReader();
        //    dt.Load(sdr);
        //    con.Close();
        //    daragrid.ItemsSource = dt.DefaultView;





        //    //admin.ShowRunner();

        //    //daragrid.ItemsSource = dt.DefaultView;

        //}
    }

}
