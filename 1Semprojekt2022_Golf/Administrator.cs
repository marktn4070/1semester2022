using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace _1Semprojekt2022_Golf
{
    public class Administrator
    {
        public static void RegisterTime(int idOfRunner) //måske skal tiden føres ind som param, måske ikke PRØVER AT LAVE PÅ DENNE
        {
            SqlConnection connection = null;
            
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["data"].ConnectionString);
            SqlCommand cmd = new SqlCommand(string.Format(""), connection);

            connection.Open();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.ExecuteNonQuery();

            if (connection != null)
            {

            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }


        //private static void Print(SqlDataReader reader)
        //{
        //    Console.WriteLine();
        //    while (reader.Read())
        //    {
        //        Console.WriteLine("{0} {1}", reader["Code"], reader["City"]);
        //        Console.WriteLine();
        //    }
        //}
        //public static void ShowRunner()
        //{
        //    SqlConnection connection = null;
        //    connection = new SqlConnection(ConfigurationManager.ConnectionStrings["post"].ConnectionString);
        //    SqlCommand cmd = new SqlCommand(string.Format("SELECT * FROM Participant"), connection);
        //    DataTable dt = new DataTable();
        //    connection.Open();
        //    SqlDataReader sdr = cmd.ExecuteReader();
        //    dt.Load(sdr);
        //    Print(sdr);
        //    connection.Close();


        //}

        public void MakeNewRunner(string name, string mail, int phone, string address, int zip, string city)
        {
            SqlConnection connection = null;

            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["data"].ConnectionString);
            SqlCommand cmd = new SqlCommand(
            string.Format("INSERT INTO Participant (P_name, P_mail, P_phone, P_address, P_zip, P_city ) VALUES(@P_name, @P_mail, @P_phone, @P_address, @P_zip, @P_city)"),
            connection);
            cmd.Parameters.Add(CreateParam("@P_name", name, System.Data.SqlDbType.VarChar));
            cmd.Parameters.Add(CreateParam("@P_mail", mail, System.Data.SqlDbType.VarChar));
            cmd.Parameters.Add(CreateParam("@P_phone", phone, System.Data.SqlDbType.Int));
            cmd.Parameters.Add(CreateParam("@P_address", address, System.Data.SqlDbType.VarChar));
            cmd.Parameters.Add(CreateParam("@P_zip", zip, System.Data.SqlDbType.Int));
            cmd.Parameters.Add(CreateParam("@P_city", city, System.Data.SqlDbType.VarChar));
            connection.Open();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.ExecuteNonQuery();
            //xaml det lykkedes vindue

            if (connection != null)
            {
                connection.Close();
            }
        }

        public static void DeleteRunner(int id)
        {

        }

        public static void UpdateRunner(int id) //mangler flere params...
        {

        }

        public static void AddRoute(string nameOfRoute, int year, TimeSpan startTime, int length) //måske ikke timespan datatype...
        {
            SqlConnection connection = null;

            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["data"].ConnectionString);
            SqlCommand cmd = new SqlCommand(
            string.Format("INSERT INTO Route (R_name, R_year, R_starttime, R_distance) VALUES(@R_name, @R_year, @R_starttime, @R_distance)"),
            connection);
            cmd.Parameters.Add(CreateParam("@P_name", nameOfRoute, System.Data.SqlDbType.VarChar));
            cmd.Parameters.Add(CreateParam("@P_mail", year, System.Data.SqlDbType.Int));
            
            connection.Open();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.ExecuteNonQuery();
            //xaml det lykkedes vindue

            if (connection != null)
            {
                connection.Close();
            }
        }

        public static void DeleteRoute(string nameOfRoute)
        {

        }

        public static void ShowRoutes()
        {

        }

        public static void SearchRoutes(string searchTerm) //måske ikke en string?
        {

        }

        public static void SearchRunner(string runnerName, int idOfRunner)
        {

        }
        private static SqlParameter CreateParam(string name, object value, System.Data.SqlDbType type)
        {
            SqlParameter param = new SqlParameter(name, type);
            param.Value = value;
            return param;
        }
    }


}