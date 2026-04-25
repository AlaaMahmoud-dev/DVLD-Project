using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess_Layar
{
    public class clsDriverData
    {
        
        public static bool GetDriverInfoByID(int DriverID, ref int PersonID, ref int CreatedByUserID, ref DateTime CreatedDate)
        {



            bool isFound = false;


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from Drivers Where DriverID=@DriverID";

            SqlCommand sqlCommand = new SqlCommand(query, connection);

            sqlCommand.Parameters.AddWithValue("DriverID", DriverID);

            try
            {

                connection.Open();

                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {

                    PersonID = (int)reader["PersonID"];
                    CreatedDate = (DateTime)reader["CreatedDate"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];

                 


                    isFound = true;
                }
                reader.Close();




            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex.Message);
                return false;
            }
            finally { connection.Close(); }

            return isFound;



        }
        public static bool GetDriverInfoByPersonID(ref int DriverID,  int PersonID, ref int CreatedByUserID, ref DateTime CreatedDate)
        {



            bool isFound = false;


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from Drivers Where PersonID=@PersonID";

            SqlCommand sqlCommand = new SqlCommand(query, connection);

            sqlCommand.Parameters.AddWithValue("PersonID", PersonID);

            try
            {

                connection.Open();

                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {

                    DriverID = (int)reader["DriverID"];
                    CreatedDate = (DateTime)reader["CreatedDate"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];




                    isFound = true;
                }
                reader.Close();




            }
            catch (Exception ex)
            {

                clsEventLogger.LogEvent(ex.Message);
                return false;
            }
            finally { connection.Close(); }

            return isFound;



        }

        public static int AddNewDriver(int PersonID, int CreatedByUserID)
        {


            int DriverID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string query = @"Insert into Drivers (PersonID,CreatedByUserID,CreatedDate)
                           Values(@PersonID,@CreatedByUserID,@CreatedDate);
                            select Scope_Identity();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("PersonID", PersonID);
            command.Parameters.AddWithValue("CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("CreatedDate", DateTime.Now);










            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    DriverID = insertedID;
                }

            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex.Message);
                return -1;
            }
            finally
            {
                connection.Close();
            }
            return DriverID;




        }

        public static bool UpdateDriverInfo(int DriverID, int PersonID, int CreatedByUserID)

        {
            int RowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string query = @"Update Drivers
                             set PersonID=@PersonID,
                             CreatedByUserID=@CreatedByUserID
                             
                             where DriverID=@DriverID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("DriverID", DriverID);
            command.Parameters.AddWithValue("PersonID", PersonID);
            command.Parameters.AddWithValue("CreatedByUserID", CreatedByUserID);
           
            try
            {
                connection.Open();

                RowsAffected = command.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }
            return RowsAffected > 0;
        }
        public static DataTable GetAllDrivers()
        {

            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string query = @"SELECT        Drivers.DriverID, Drivers.PersonID, People.NationalNo, 
                             People.FirstName, People.SecondName, People.ThirdName, People.LastName,
                             Drivers.CreatedDate,
                             (SELECT COUNT(LicenseID) AS NumberOfActiveLicenses
                             FROM            Licenses
                             WHERE        (DriverID = Drivers.DriverID) AND (IsActive = 1)) AS NumberOfActiveLicenses
                             FROM            Drivers INNER JOIN
                             People ON Drivers.PersonID = People.PersonID      ";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

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
            finally
            {
                connection.Close();
            }
            return dt;
        }

    }
}
