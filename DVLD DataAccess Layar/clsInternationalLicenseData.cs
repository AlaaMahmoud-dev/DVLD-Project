using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess_Layar
{
    public class clsInternationalLicenseData
    {
        public static DataTable GetInternationalDrivingLicensesHistory(int DriverID)
        {

            DataTable dtLicensesHistory = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select InternationalLicenseID,IssuedUsingLocalLicenseID,ApplicationID,IssueDate,ExpirationDate,IsActive  from InternationalLicenses
                            where DriverID=@DriverID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("DriverID", DriverID);


            try
            {
                connection.Open();


                SqlDataReader reader = command.ExecuteReader();


                if (reader.HasRows)
                {

                    dtLicensesHistory.Load(reader);


                }
                reader.Close();


            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex.Message);
                return null;
            }

            finally { connection.Close(); }
            return dtLicensesHistory;

        }
      
        public static DataTable GetInternationalLicensesList()
        {

            DataTable dtInternationalLicenses = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select InternationalLicenseID,ApplicationID,DriverID,
                             IssuedUsingLocalLicenseID,IssueDate,ExpirationDate,IsActive 
                             from InternationalLicenses
                             Order by IsActive , ExpirationDate desc";

            SqlCommand command = new SqlCommand(query, connection);



            try
            {
                connection.Open();


                SqlDataReader reader = command.ExecuteReader();


                if (reader.HasRows)
                {

                    dtInternationalLicenses.Load(reader);


                }
                reader.Close();


            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex.Message);
                return null;
            }

            finally { connection.Close(); }
            return dtInternationalLicenses;

        }
        public static DataTable GetDriverInternationalLicenses(int DriverID)
        {

            DataTable dtInternationalLicenses = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select InternationalLicenseID,ApplicationID,DriverID,
                             IssuedUsingLocalLicenseID,IssueDate,ExpirationDate,IsActive 
                             from InternationalLicenses
                             Where DriverID=@DriverID
                             Order by IsActive and ExpirationDate desc";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                connection.Open();


                SqlDataReader reader = command.ExecuteReader();


                if (reader.HasRows)
                {

                    dtInternationalLicenses.Load(reader);


                }
                reader.Close();


            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex.Message);
                return null;
            }

            finally { connection.Close(); }
            return dtInternationalLicenses;

        }
        public static int AddNewInternationalLicense(int ApplicationID, int DriverID,
            int IssuedUsingLocalLicenseID, DateTime IssueDate, DateTime ExpirationDate
               , bool isActive, int CreatedByUserID)
        {
            int InternationalLicenseID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string query = @"
                            Update InternationalLicenses 
                            set IsActive=0 
                            where DriverID=@DriverID;

                            Insert into InternationalLicenses (ApplicationID,
                            IssuedUsingLocalLicenseID,DriverID,IssueDate,
                            ExpirationDate,IsActive,CreatedByUserID)
                            Values(@ApplicationID,@IssuedUsingLocalLicenseID,
                            @DriverID,@IssueDate,@ExpirationDate,
                            @isActive,@CreatedByUserID);
                            select Scope_Identity();";

            SqlCommand command = new SqlCommand(query, connection);


            command.Parameters.AddWithValue("ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
            command.Parameters.AddWithValue("DriverID", DriverID);
            command.Parameters.AddWithValue("IssueDate", IssueDate);
            command.Parameters.AddWithValue("ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("isActive", isActive);









            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    InternationalLicenseID = insertedID;
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
            return InternationalLicenseID;
        }

        public static bool UpdateInternationalLicenseInfo(int InternationalLicenseID, int ApplicationID, int DriverID,
            int IssuedUsingLocalLicenseID, DateTime IssueDate, DateTime ExpirationDate
               , bool isActive, int CreatedByUserID)
        {
            int RowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update InternationalLicenses 
                           set 
                           DriverID=@DriverID,
                           IssuedUsingLocalLicenseID=@IssuedUsingLocalLicenseID,
                           IssueDate=@IssueDate,
                           ExpirationDate=@ExpirationDate,
                           isActive=@isActive,
                           CreatedByUserID=@CreatedByUserID
                           where InternationalLicenseID=@InternationalLicenseID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("InternationalLicenseID", InternationalLicenseID);
            command.Parameters.AddWithValue("ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("DriverID", DriverID);
            command.Parameters.AddWithValue("IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
            command.Parameters.AddWithValue("IssueDate", IssueDate);
            command.Parameters.AddWithValue("ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("isActive", isActive);
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

        public static bool FindInternationalLicenseInfoByID(int InternationalLicenseID,
            ref int ApplicationID, ref int DriverID, ref int IssuedUsingLocalLicenseID,
            ref DateTime IssueDate, ref DateTime ExpirationDate
             , ref bool isActive, ref int CreatedByUserID)
        {



            bool isFound = false;


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from InternationalLicenses Where InternationalLicenseID=@InternationalLicenseID";

            SqlCommand sqlCommand = new SqlCommand(query, connection);

            sqlCommand.Parameters.AddWithValue("InternationalLicenseID", InternationalLicenseID);

            try
            {

                connection.Open();

                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {
                    ApplicationID = (int)reader["ApplicationID"];
                    DriverID = (int)reader["DriverID"];
                    IssuedUsingLocalLicenseID = (int)reader["IssuedUsingLocalLicenseID"];

                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    IssueDate = (DateTime)reader["IssueDate"];

                    if (reader["IsActive"] != null && bool.TryParse(reader["IsActive"].ToString(), out bool Status))
                    {
                        isActive = Status;
                    }

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
        public static bool FindInternationalLicenseInfoByDriverID(ref int InternationalLicenseID,
            ref int ApplicationID,  int DriverID, ref int IssuedUsingLocalLicenseID,
            ref DateTime IssueDate, ref DateTime ExpirationDate
           , ref bool isActive, ref int CreatedByUserID)
        {



            bool isFound = false;


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from InternationalLicenses Where DriverID=@DriverID";

            SqlCommand sqlCommand = new SqlCommand(query, connection);

            sqlCommand.Parameters.AddWithValue("DriverID", DriverID);

            try
            {

                connection.Open();

                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {
                    ApplicationID = (int)reader["ApplicationID"];
                    InternationalLicenseID = (int)reader["InternationalLicenseID"];
                    IssuedUsingLocalLicenseID = (int)reader["IssuedUsingLocalLicenseID"];

                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    IssueDate = (DateTime)reader["IssueDate"];


                    isActive = (bool)reader["IsActive"];


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

        public static int GetActiveInternationalLicenseIDForDriver(int DriverID)
        {
            int InternationalLicenseID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select InternationalLicenseID 
                             from InternationalLicenses where DriverID=@DriverID
                             AND IsActive=1 AND GETDATE() Between
                             IssueDate and ExpirationDate";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("DriverID", DriverID);

            try
            {

                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int IntLicID))
                {
                    InternationalLicenseID = IntLicID;
                }



            }
            catch (Exception ex)
            {

                clsEventLogger.LogEvent(ex.Message);
                return -1;

            }
            finally { connection.Close(); }

            return InternationalLicenseID;


        }
    }
}
