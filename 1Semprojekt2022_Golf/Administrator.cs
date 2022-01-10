using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace _1Semprojekt2022_Golf
{
    class Administrator
    {
        public static void RegisterTime(int idOfRunner) //måske skal tiden føres ind som param, måske ikke PRØVER AT LAVE PÅ DENNE
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["data"].ConnectionString);
                SqlCommand cmd = new SqlCommand(      );

                connection.Open();

            }
            catch
            {

            }
            finally
            {
                if(connection != null)
                {
                    connection.Close();
                }
            }
        }

        public static void MakeNewRunner(string name) //mangler flere params self
        {

        }

        public static void DeleteRunner(int id)
        {

        }

        public static void UpdateRunner(int id) //mangler flere params...
        {

        }

        public static void AddRoute(string nameOfRoute, int length) //måske flere params
        {

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
    }

    
}
