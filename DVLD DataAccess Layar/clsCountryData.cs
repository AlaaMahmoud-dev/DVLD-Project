using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess_Layar
{
    public class clsCountryData
    {
      
        public static bool FindCountryByID(int CountryID,ref string CountryName)
        {
            bool isFound = false;


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select CountryName from Countries where CountryID=@CountryID";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("CountryID", CountryID);
            try
            {
                connection.Open();

                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    CountryName = (string)result;
                    isFound = true;
                }
                else
                {
                    isFound = false;
                }

            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex.Message);
                return false;
            }

            finally { connection.Close(); }

            return isFound;


        }
        public static bool FindCountryByName(ref int CountryID,  string CountryName)
        {
            bool isFound = false;


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select CountryName from Countries where CountryName=@CountryName";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("CountryName", CountryName);
            try
            {
                connection.Open();

                object result = cmd.ExecuteScalar();

                if (result != null&&int.TryParse(result.ToString(),out int ID))
                {
                    CountryID = ID;
                    isFound = true;
                }
                else
                {
                    isFound = false;
                }

            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex.Message);
                return false;
            }

            finally { connection.Close(); }

            return isFound;


        }

        public static string GetCountryName(int CountryID)
        {
            string CountryName = "";


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select CountryName from Countries where CountryID=@CountryID";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("CountryID", CountryID);
            try
            {
                connection.Open();

                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    CountryName = (string)result;
                }
                else
                {
                    Console.WriteLine("Country Not Found");
                }

            }
            catch (Exception ex) {
                clsEventLogger.LogEvent(ex.Message);
                return "";
            }

            finally { connection.Close(); }

            return CountryName;


        }


        public static int GetCountryID(string CountryName)
        {
            int CountryID = -1;


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select CountryID from Countries where CountryName=@CountryName";



            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("CountryName", CountryName);


            try
            {
                connection.Open();

                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    CountryID = (int)result;
                }
                else
                {
                    Console.WriteLine("Country Not Found");
                }

            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex.Message);
                return -1;
            }

            finally { connection.Close(); }

            return CountryID;


        }
        public static DataTable GetCountriesList()
        {

            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string query = "Select CountryName from Countries";

            SqlCommand cmd = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();

            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex.Message);
                return null;
            }
            finally { connection.Close(); }
            return dt;
        }

      
    }
}
