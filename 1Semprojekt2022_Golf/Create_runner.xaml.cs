using System;
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


        private void Tilfoej_deltager_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                admin.MakeNewRunner(Name.Text, Mail.Text, int.Parse(Phone.Text), Address.Text, int.Parse(Zip.Text), City.Text);
                MessageBox.Show("Løber gemt"); //måske behøver vi ikke dette......
                Clear();
                //LoadGrid_Runner();
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
