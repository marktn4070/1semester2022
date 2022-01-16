using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using static _1Semprojekt2022_Golf.Administrator;
using System.Configuration;


namespace _1Semprojekt2022_Golf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Administrator admin = new Administrator();
        private List<Participant_strings> Participant_list = new List<Participant_strings>();
        private List<Route_strings> Route_list = new List<Route_strings>();

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
            datagrid_deltager.ItemsSource = new ObservableCollection<Participant_strings>(Participant_list);
            datagrid_løberute.ItemsSource = new ObservableCollection<Route_strings>(Route_list);
        }
        private class Participant_strings
        {
            public string P_id { get; set; }
            public string P_name { get; set; }
            public string P_mail { get; set; }
            public string P_phone { get; set; }
            public string P_address { get; set; }
            public string P_zip { get; set; }
            public string P_city { get; set; }
        }
        private class Route_strings
        {
            public string R_id { get; set; }
            public string R_name { get; set; }
            public string R_year { get; set; }
            public string R_starttime { get; set; }
            public string R_distance { get; set; }

        }

        private void Clear()
        {
            datagrid_deltager.SelectedIndex = -1;
            LoadGrid_Runner();
        }

        public void LoadGrid_Route()
        {
            //SqlConnection con = null;
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Route", con);
                DataTable dt = new DataTable();
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                Route_list.Clear();
                
                while (sdr.Read()) Route_list.Add(new Route_strings { R_id = sdr[0].ToString(), R_name = sdr[1].ToString(), R_year = sdr[2].ToString(), R_starttime = sdr[3].ToString(), R_distance = sdr[4].ToString() });
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




        public void LoadGrid_Runner()
        {
            //SqlConnection con = null;
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Participant", con);
                DataTable dt = new DataTable();
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                Participant_list.Clear();
                while (sdr.Read()) Participant_list.Add(new Participant_strings { P_id = sdr[0].ToString(), P_name = sdr[1].ToString(), P_mail = sdr[2].ToString(), P_phone = sdr[3].ToString(), P_address = sdr[4].ToString(), P_zip = sdr[5].ToString(), P_city = sdr[6].ToString() });
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
            datagrid_deltager.ItemsSource = dt.DefaultView;

            if (Search_txt.Text != string.Empty)
            {

                ClearDataBtn.Visibility = Visibility.Visible;
                SearchDataBtn.Visibility = Visibility.Hidden;
            }


        }





        public void LoadGrid_Time()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Registered", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            datagrid_tidstagning.ItemsSource = dt.DefaultView;
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
            int n = datagrid_deltager.SelectedIndex;
        }



        private void Route_btnView_Click(object sender, RoutedEventArgs e)
        {
            this.Closing += new System.ComponentModel.CancelEventHandler(Update_route_opdate);
            this.Close();
        }

        private void Update_route_opdate(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                //DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message.ToString());
            }
            int n = datagrid_løberute.SelectedIndex;
            if (n >= 0)
            {
                string R_name_sting = Route_list[n].R_name;
                string R_year_sting = Route_list[n].R_year;
                string R_starttime_sting = Route_list[n].R_starttime;
                string R_distance_sting = Route_list[n].R_distance;
                Update_runner win2 = new Update_route(R_name_sting, R_year_sting, R_starttime_sting, R_distance_sting);
                win2.Show();
            }
        }





        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            this.Closing += new System.ComponentModel.CancelEventHandler(Update_runner_opdate);
            this.Close();
        }

        private void Update_runner_opdate(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                //DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message.ToString());
            }
            int n = datagrid_deltager.SelectedIndex;
            if (n >= 0)
            {

                string P_name_sting = Participant_list[n].P_name;
                string P_mail_sting = Participant_list[n].P_mail;
                string P_phone_sting = Participant_list[n].P_phone;
                string P_address_sting = Participant_list[n].P_address;
                string P_zip_sting = Participant_list[n].P_zip;
                string P_city_sting = Participant_list[n].P_city;
                Update_runner win2 = new Update_runner(P_name_sting, P_mail_sting, P_phone_sting, P_address_sting, P_zip_sting, P_city_sting);
                win2.Show();
            }
        }
        private SqlParameter CreateParam(string name, object value, SqlDbType type)
        {
            SqlParameter param = new SqlParameter(name, type);
            param.Value = value;
            return param;
        }
        private void btnDelete_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                //DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message.ToString());
            }
            string error = "";
            string selected_id = Participant_list[datagrid_deltager.SelectedIndex].P_id;
            string selected_name = Participant_list[datagrid_deltager.SelectedIndex].P_name;
            int n = datagrid_deltager.SelectedIndex;


            var Result = MessageBox.Show("Er du sikker på, at du vil slette '" + selected_name + "'?", "", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (Result == MessageBoxResult.Yes)
            {
                SqlConnection connection = null;
                try
                {

                    connection = new SqlConnection(ConfigurationManager.ConnectionStrings["data"].ConnectionString);
                    SqlCommand command = new SqlCommand("Delete FROM Participant WHERE P_id = @P_id", connection);
                    command.Parameters.Add(CreateParam("@P_id", selected_id.Trim(), SqlDbType.NVarChar));
                    connection.Open();
                    if (command.ExecuteNonQuery() == 1)
                    {
                        Clear();
                        return;
                    }
                    error = "Illegal database operation";

                }
                catch (Exception ex)
                {
                    error = ex.Message;
                }
                finally
                {
                    if (connection != null) connection.Close();
                }

                MessageBox.Show(error);

            }
            else if (Result == MessageBoxResult.No)
            {
                LoadGrid_Runner();
            }
        }
        private void Route_btnDelete_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                //DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message.ToString());
            }
            string error = "";
            string selected_id = Route_list[datagrid_løberute.SelectedIndex].R_id;
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["data"].ConnectionString);
                SqlCommand command = new SqlCommand("Delete FROM Route WHERE R_id = @R_id", connection);
                command.Parameters.Add(CreateParam("@R_id", selected_id.Trim(), SqlDbType.NVarChar));
                connection.Open();
                if (command.ExecuteNonQuery() == 1)
                {
                    Clear();
                    return;
                }
                error = "Illegal database operation";
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            finally
            {
                if (connection != null) connection.Close();
            }
            MessageBox.Show(error);
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

        }
    }
}