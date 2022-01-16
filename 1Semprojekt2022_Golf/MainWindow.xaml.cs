using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using _1Semprojekt2022_Golf.View;
using static _1Semprojekt2022_Golf.Administrator;

namespace _1Semprojekt2022_Golf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Administrator admin = new Administrator();
        private List<Zipcode> list = new List<Zipcode>();

        public MainWindow()
        {
            InitializeComponent();
            LoadGrid_Runner();
            LoadGrid_Route();
            LoadGrid_Time();
            Changed_index();
        }
        SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=Golf; Integrated Security=True");

        private void Refresh()
        {
            daragrid_deltager.ItemsSource = new ObservableCollection<Zipcode>(list);
        }
        private class Zipcode
        {
            public string P_id { get; set; }
            public string P_name { get; set; }
            public string P_mail { get; set; }
            public string P_phone { get; set; }
            public string P_address { get; set; }
            public string P_zip { get; set; }
            public string P_city { get; set; }
        }

        private void Clear()
        {
            daragrid_deltager.SelectedIndex = -1;
            LoadGrid_Runner();
        }

        public void LoadGrid_Runner()
        {
            //SqlConnection con = null;
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Participant", con);
            DataTable dt = new DataTable();
            con.Open();
            //SqlDataReader sdr = cmd.ExecuteReader();
            SqlDataReader sdr = cmd.ExecuteReader();
            list.Clear();
            while (sdr.Read()) list.Add(new Zipcode { P_id = sdr[0].ToString(), P_name = sdr[1].ToString(), P_mail = sdr[2].ToString(), P_phone = sdr[3].ToString(), P_address = sdr[4].ToString(), P_zip = sdr[5].ToString(), P_city = sdr[6].ToString()});
            Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
            }
        }
        private void Btn_Runner_Search_Click(object sender, RoutedEventArgs e)
        {
            string runner_id = Search_txt.Text;
            //SqlCommand cmd = new SqlCommand("SELECT * FROM Participant WHERE P_id = '" + runner_id + "'", con);
            SqlCommand cmd = new SqlCommand("SELECT * FROM Participant WHERE P_name like '%" + runner_id + "%' or P_id like '%" + runner_id + "%'", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            daragrid_deltager.ItemsSource = dt.DefaultView;

            if (Search_txt.Text != string.Empty)
            {

                ClearDataBtn.Visibility = Visibility.Visible;
                SearchDataBtn.Visibility = Visibility.Hidden;
            }


        }





        public void LoadGrid_Time()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Route", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            daragrid_tidstagning.ItemsSource = dt.DefaultView;
        }

        public void LoadGrid_Route()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Registered", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            daragrid_løberute.ItemsSource = dt.DefaultView;

        }

        public void Changed_index()
        {
            int index = LastIndex.foo;

            GridCursor.Margin = new Thickness(150 * index, 0, 0, 0);
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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);

            GridCursor.Margin = new Thickness(150 * index, 0, 0, 0);

            switch (index)
            {
                case 0:
                    Title.Text = "Deltager";
                    page_deltager.Visibility = Visibility.Visible;
                    page_tidstagning.Visibility = Visibility.Hidden;
                    page_løberute.Visibility = Visibility.Hidden;
                    LastIndex.foo = index;
                    break;

                case 1:
                    Title.Text = "Løberute";
                    page_deltager.Visibility = Visibility.Hidden;
                    page_tidstagning.Visibility = Visibility.Visible;
                    page_løberute.Visibility = Visibility.Hidden;
                    LastIndex.foo = index;
                    break;
                case 2:
                    Title.Text = "Tidstagning";
                    page_deltager.Visibility = Visibility.Hidden;
                    page_tidstagning.Visibility = Visibility.Hidden;
                    page_løberute.Visibility = Visibility.Visible;
                    LastIndex.foo = index;
                    break;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Closing += new System.ComponentModel.CancelEventHandler(Create_runner_open);
            this.Close();
        }

        void Create_runner_open(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Create_runner secondWindow = new Create_runner(admin);
            secondWindow.Show();
        }


        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Closing += new System.ComponentModel.CancelEventHandler(Create_route_open);
            this.Close();
        }

        void Create_route_open(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Create_route secondWindow = new Create_route(admin);
            secondWindow.Show();
        }


        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.Closing += new System.ComponentModel.CancelEventHandler(Update_runner_open);
            this.Close();
        }

        void Update_runner_open(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Update_runner secondWindow = new Update_runner();
            secondWindow.Show();
        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {
            this.Closing += new System.ComponentModel.CancelEventHandler(Test_runner_open);
            this.Close();

        }
        void Test_runner_open(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Popup_tes_window secondWindow = new Popup_tes_window();
            secondWindow.Show();
        }

        private void ClearDataBtn_Click(object sender, RoutedEventArgs e)
        {
            Search_txt.Clear();
            LoadGrid_Runner();
            LoadGrid_Route();
            LoadGrid_Time();
            ClearDataBtn.Visibility = Visibility.Hidden;
            SearchDataBtn.Visibility = Visibility.Visible;

        }





        private void grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int n = daragrid_deltager.SelectedIndex;
            if (n >= 0)
            {
                //txtCode.Text = list[n].Code;
                //txtCity.Text = list[n].Cityd;
            }
        }



        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message.ToString());
            }
            int n = daragrid_deltager.SelectedIndex;
            if (n >= 0)
            {
                string hej = list[n].P_id;
                ZipWindow win2 = new ZipWindow(hej);
                win2.Show();
            }
        }

        private void btnDelete_Click_2(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;
            //}
            //catch (Exception ex)
            //{
            //    //MessageBox.Show(ex.Message.ToString());
            //}
            //string error = "";
            //string selected_id = list[grid.SelectedIndex].Code;
            //SqlConnection connection = null;
            //try
            //{
            //    connection = new SqlConnection(ConfigurationManager.ConnectionStrings["post"].ConnectionString);
            //    SqlCommand command = new SqlCommand("Delete FROM Zipcodes WHERE Code = @Code", connection);
            //    command.Parameters.Add(CreateParam("@Code", selected_id.Trim(), SqlDbType.NVarChar));
            //    connection.Open();
            //    if (command.ExecuteNonQuery() == 1)
            //    {
            //        Clear();
            //        return;
            //    }
            //    error = "Illegal database operation";
            //}
            //catch (Exception ex)
            //{
            //    error = ex.Message;
            //}
            //finally
            //{
            //    if (connection != null) connection.Close();
            //}
            //MessageBox.Show(error);
        }
    }
}