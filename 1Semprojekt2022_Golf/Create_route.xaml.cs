using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Configuration;
using System.Text.RegularExpressions;


namespace _1Semprojekt2022_Golf
{
    /// <summary>
    /// Interaction logic for Create_route.xaml
    /// </summary>
    public partial class Create_route : Window
    {
        private List<Route> list = new List<Route>();
        public Administrator admin;
        public Create_route(Administrator amn)
        {
            admin = amn;
            InitializeComponent();
            this.Closing += new System.ComponentModel.CancelEventHandler(Create_route_Closing);
        }


        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["data"].ConnectionString);




        void Create_route_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow secondWindow = new MainWindow();
            secondWindow.Show();
        }



        public void clearData()
        {
            R_name_txt.Clear();
            R_year_txt.Clear();
            R_starttimeHour_txt.Clear();
            R_starttimeMinute_txt.Clear();
            R_distance_txt.Clear();
        }
  

        private SqlParameter CreateParam(string name, object value, SqlDbType type)
        {
            SqlParameter param = new SqlParameter(name, type);
            param.Value = value;
            return param;
        }

        private void ClearDataBtn_Click(object sender, RoutedEventArgs e)
        {
            clearData();
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

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (IsValid())
                {
                    admin.AddRoute(R_name_txt.Text, int.Parse(R_year_txt.Text), int.Parse(R_starttimeHour_txt.Text), int.Parse(R_starttimeMinute_txt.Text), int.Parse(R_distance_txt.Text));
                    MessageBox.Show("Oprettet!");
                    Close();
                }
                else
                {
                    if (R_name_txt.Text == string.Empty)
                    {
                        R_navn_error.Text = "Name skal udfyldes";
                    }
                    else if (Regex.IsMatch(R_name_txt.Text, "[^æøåÆØÅa-zA-Z ]"))
                    {
                        R_navn_error.Text = "Name skal stå med bogstaver";
                    }
                    else
                    {
                        R_navn_error.Text = "";
                    }

                    if (R_year_txt.Text == string.Empty)
                    {
                        R_year__error.Text = "År skal udfyldes";
                    }
                    else if (Regex.IsMatch(R_year__error.Text, "[^0-9]"))
                    {
                        R_year__error.Text = "År skal stå med tal";
                    }
                    else
                    {
                        R_year__error.Text = "";
                    }

                    if (R_starttimeHour_txt.Text == string.Empty || R_starttimeMinute_txt.Text == string.Empty)
                    {
                        R_starttime_error.Text = "Starttid skal udfyldes";
                    }
                    else if (Regex.IsMatch(R_year__error.Text, "[^0-9]"))
                    {
                        R_year__error.Text = "Starttid skal stå med tal";
                    }
                    else
                        R_starttime_error.Text = "";
                }

                if (R_distance_txt.Text == string.Empty)
                {
                    R_distance_error.Text = "Længde skal udfyldes";
                }
                else if (Regex.IsMatch(R_year__error.Text, "[^0-9]"))
                {
                    R_year__error.Text = "Længde skal stå med tal";
                }
                else
                {
                    R_distance_error.Text = "";
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}