using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace _1Semprojekt2022_Golf
{
    public class Administrator
    {
        public static void RegisterTime(int idOfRunner) //måske skal tiden føres ind som param, måske ikke PRØVER AT LAVE PÅ DENNE
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["data"].ConnectionString);
                SqlCommand cmd = new SqlCommand(string.Format(""), connection);

                connection.Open();

            }
            catch
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

        public static class LastIndex
        {
            public static int foo;
        }



        public static void UpdateRunner(int id) //mangler flere params...
        {

        }

        public void AddRoute(string nameOfRoute, int year, int startTimeHour, int startTimeMinute, int distance) 
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["data"].ConnectionString);
            SqlCommand cmd = new SqlCommand("INSERT INTO Route VALUES (@R_name, @R_year, @R_starttime, @R_distance)", con);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@R_name", nameOfRoute);
                cmd.Parameters.Add("@R_year", SqlDbType.Int).Value = year;
                cmd.Parameters.Add("@R_starttime", SqlDbType.Int).Value = (startTimeHour * 60 * 60 + startTimeMinute * 60);
                cmd.Parameters.Add("@R_distance", SqlDbType.Decimal).Value = distance;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
        }

        private static SqlParameter CreateParam(string name, object value, System.Data.SqlDbType type)
        {
            SqlParameter param = new SqlParameter(name, type);
            param.Value = value;
            return param;
        }
    }


}