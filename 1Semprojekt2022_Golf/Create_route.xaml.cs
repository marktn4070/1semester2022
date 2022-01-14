using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Interaction logic for Create_route.xaml
    /// </summary>
    public partial class Create_route : Window
    {
        private List<Zipcode> list = new List<Zipcode>();
        public Create_route()
        {
            InitializeComponent();
            LoadGrid_Runner("", "", "", "");
            this.Closing += new System.ComponentModel.CancelEventHandler(Create_route_Closing);
        }


        SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=Golf; Integrated Security=True");

        private void Refresh()
        {
            daragrid.ItemsSource = new ObservableCollection<Zipcode>(list);
        }

        void Create_route_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow secondWindow = new MainWindow();
            secondWindow.Show();
        }



        public void clearData()
        {
            R_name_txt.Clear();
            R_year_txt.Clear();
            R_starttime_txt.Clear();
            R_distance_txt.Clear();
        }
        public void LoadGrid_Runner(string R_name_txt, string R_year_txt, string R_starttime_txt, string R_distance_txt)
        {
            SqlConnection con = null;
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Route WHERE ID LIKE @R_id AND Name LIKE @R_name AND Year LIKE @R_year AND Starttime LIKE @R_starttime AND Distance LIKE @R_distance", con);
                //int n = daragrid.SelectedIndex;
                //string tteeeeest_id = list[daragrid.SelectedIndex].Id;
                //cmd.Parameters.Add(CreateParam("@R_id", tteeeeest_id + "%", SqlDbType.Int));
                cmd.Parameters.Add(CreateParam("@R_name", R_name_txt + "%", SqlDbType.NVarChar));
                cmd.Parameters.Add(CreateParam("@R_year", R_year_txt + "%", SqlDbType.Int));
                cmd.Parameters.Add(CreateParam("@R_starttime", R_starttime_txt + "%", SqlDbType.SmallDateTime));
                cmd.Parameters.Add(CreateParam("@R_distance", R_distance_txt + "%", SqlDbType.Int));
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                list.Clear();
                while (reader.Read()) list.Add(new Zipcode { Id = (int)reader[0], Name = reader[1].ToString(), Year = (int)reader[2], Starttime = reader[3].ToString(), Distance = (int)reader[4] });
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
        //        if (cmd.ExecuteNonQuery() == 1)
        //        {
        //            clearData();
        //            return;
        //        }
        //        error = "Illegal database operation";
        //    }
        //    catch (Exception ex)
        //    {
        //        error = ex.Message;
        //    }
        //    finally
        //    {
        //        if (con != null) con.Close();
        //    }
        //    MessageBox.Show(error);
        //}


        //SqlDataReader reader = cmd.ExecuteReader();
        //            list.Clear();
        //            while (reader.Read()) list.Add(new Zipcode { Code = reader[0].ToString(), Cityd = reader[1].ToString() });
        //            Refresh();


        //            string R_name = R_name_txt.Text;
        //            con.Open();
        //            cmd.ExecuteNonQuery();
        //            con.Close();
        //            
        //Refresh();
        //            MessageBox.Show("'" + R_name + "' er nu oprettet", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
        //            clearData();


        //            DataTable dt = new DataTable();
        //            con.Open();
        //            SqlDataReader sdr = cmd.ExecuteReader();
        //            dt.Load(sdr);
        //            con.Close();
        //            daragrid.ItemsSource = dt.DefaultView;

        //        }
        private SqlParameter CreateParam(string name, object value, SqlDbType type)
        {
            SqlParameter param = new SqlParameter(name, type);
            param.Value = value;
            return param;
        }
        private class Zipcode
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Year { get; set; }
            public string Starttime { get; set; }
            public int Distance { get; set; }
        }
        private void grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int n = daragrid.SelectedIndex;
            if (n >= 0)
            {
                R_name_txt.Text = list[n].Name;
                R_year_txt.Text = list[n].Year.ToString();
                R_starttime_txt.Text = list[n].Starttime;
                R_distance_txt.Text = list[n].Distance.ToString();


            }
        }

        private void grid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                if (sender != null)
                {
                    DataGrid daragrid = sender as DataGrid;
                    if (daragrid != null && daragrid.SelectedItems != null && daragrid.SelectedItems.Count == 1)
                    {
                        //This is the code which helps to show the data when the row is double clicked.
                        DataGridRow dgr = daragrid.ItemContainerGenerator.ContainerFromItem(daragrid.SelectedItem) as DataGridRow;
                        DataRowView dr = (DataRowView)dgr.Item;

                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void ClearDataBtn_Click(object sender, RoutedEventArgs e)
        {
            clearData();
        }
        public bool IsValid()
        {
            if (R_name_txt.Text == string.Empty)
            {
                MessageBox.Show("En eller flere felter er ikke udfyldt", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (R_year_txt.Text == string.Empty)
            {
                MessageBox.Show("En eller flere felter er ikke udfyldt", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (R_starttime_txt.Text == string.Empty)
            {
                MessageBox.Show("En eller flere felter er ikke udfyldt", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (R_distance_txt.Text == string.Empty)
            {
                MessageBox.Show("En eller flere felter er ikke udfyldt", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
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

                    SqlCommand cmd = new SqlCommand("INSERT INTO Route VALUES (@R_name, @R_year, @R_starttime, @R_distance)", con);
                    cmd.CommandType = CommandType.Text;


                    cmd.Parameters.AddWithValue("@R_name", R_name_txt.Text);
                    cmd.Parameters.Add("@R_year", SqlDbType.Int).Value = R_year_txt.Text;
                    cmd.Parameters.Add("@R_starttime", SqlDbType.SmallDateTime).Value = R_starttime_txt.Text;
                    cmd.Parameters.Add("@R_distance", SqlDbType.Decimal).Value = R_distance_txt.Text;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    Refresh();
                    MessageBox.Show("'" + R_name_txt.Text + "' er nu oprettet", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                    clearData();
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

                    if (R_starttime_txt.Text == string.Empty)
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
            if (Search_txt.Text == "")
            {
                MessageBox.Show("Id'et skal udfyldes før sletningen kan ske", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("delete from Route where P_id = " + Search_txt.Text + " ", con);
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("'Id " + Search_txt.Text + "' deltageren er slettet", "Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                    con.Close();
                    clearData();

                    Refresh();
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
            if (Search_txt.Text == "")
            {
                MessageBox.Show("Id'et skal udfyldes før opdateringen kan ske", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update Route set R_name = '" + R_name_txt.Text + "', R_year = '" + R_year_txt.Text + "', R_starttime = '" + R_starttime_txt.Text + "', R_distance = '" + R_distance_txt.Text + "' WHERE P_id = '" + Search_txt.Text + "' ", con);
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

                    Refresh();

                }
            }

        }
        private void SearchDataBtn_Click(object sender, RoutedEventArgs e)
        {
            //con.Open();
            //SqlCommand cmd = new SqlCommand("SELECT * FROM Route WHERE P_id = " + Search_txt.Text, con);

            //DataTable dt = new DataTable();
            //con.Open();
            //SqlDataReader sdr = cmd.ExecuteReader();
            //dt.Load(sdr);
            //con.Close();
            //daragrid.ItemsSource = dt.DefaultView;




        }





    }
}