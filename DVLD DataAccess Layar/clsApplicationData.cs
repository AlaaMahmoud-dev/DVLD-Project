using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess_Layar
{
    public class clsApplicationData
    {
        public static bool FindApplicationInfo(int ApplicationID, ref int ApplicantPersonID,
            ref DateTime ApplicationDate, ref int ApplicationTypeID,
            ref byte ApplicationStatus, ref DateTime LastStatusDate,
            ref float PaidFees, ref int CreatedByUserID)
        {



            bool isFound = false;


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from Applications Where ApplicationID=@ApplicationID";

            SqlCommand sqlCommand = new SqlCommand(query, connection);

            sqlCommand.Parameters.AddWithValue("ApplicationID", ApplicationID);

            try
            {

                connection.Open();

                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {

                    ApplicantPersonID = (int)reader["ApplicantPersonID"];
                    ApplicationDate = (DateTime)reader["ApplicationDate"];
                    ApplicationTypeID = (int)reader["ApplicationTypeID"];
                    LastStatusDate = (DateTime)reader["LastStatusDate"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];

                    if (reader["ApplicationStatus"] != null && byte.TryParse(reader["ApplicationStatus"].ToString(), out byte Status))
                    {
                        ApplicationStatus = Status;
                    }

                    if (reader["PaidFees"] != null && float.TryParse(reader["PaidFees"].ToString(), out float Fees))
                    {
                        PaidFees = Fees;
                    }

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
        public static DataTable GetApplicationsList()
        {
            DataTable dtApplications = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from Applications";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();


                SqlDataReader reader = command.ExecuteReader();


                if (reader.HasRows)
                {

                    dtApplications.Load(reader);


                }
                reader.Close();


            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex.Message);
                return null;
            }

            finally { connection.Close(); }
            return dtApplications;
        }

        public static int AddNewApplication(int ApplicantPersonID, DateTime ApplicationDate,
            int ApplicationTypeID, byte ApplicationStatus, DateTime LastStatusDate,
            float PaidFees, int CreatedBuUserID)

        {
            int ApplicationID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string query = @"Insert into Applications (ApplicantPersonID,ApplicationDate,ApplicationTypeID,ApplicationStatus,LastStatusDate,PaidFees,CreatedByUserID)
                           Values(@ApplicantPersonID,@ApplicationDate,@ApplicationTypeID,@ApplicationStatus,@LastStatusDate,@PaidFees,@CreatedByUserID);
                            select Scope_Identity();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("ApplicationTypeID", ApplicationTypeID);


            command.Parameters.AddWithValue("ApplicationStatus"
                , ApplicationStatus);




            command.Parameters.AddWithValue("LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("PaidFees", PaidFees);
            command.Parameters.AddWithValue("CreatedByUserID", CreatedBuUserID);




            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    ApplicationID = insertedID;
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
            return ApplicationID;
        }


        public static bool UpdateApplicationInfo(int ApplicationID, int ApplicantPersonID,
            DateTime ApplicationDate, int ApplicationTypeID, byte ApplicationStatus
                , DateTime LastStatusDate, float PaidFees, int CreatedByUserID)
        {

            int RowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update Applications 
                           set ApplicantPersonID=@ApplicantPersonID, 
                           ApplicationDate=@ApplicationDate,
                           ApplicationTypeID=@ApplicationTypeID,
                           ApplicationStatus=@ApplicationStatus,
                           LastStatusDate=@LastStatusDate,
                           PaidFees=@PaidFees,
                           CreatedByUserID=@CreatedByUserID
                          
                           where ApplicationID=@ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("PaidFees", PaidFees);






            command.Parameters.AddWithValue("ApplicationStatus", ApplicationStatus);

            command.Parameters.AddWithValue("CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("LastStatusDate", LastStatusDate);



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
        public static bool DeleteApplication(int ApplicationID)
        {
            int RowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "delete from Applications where ApplicationID=@ID";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@ID", ApplicationID);

            try
            {
                connection.Open();

                RowsAffected = cmd.ExecuteNonQuery();




            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex.Message);
                return false;
            }
            finally { connection.Close(); }


            return RowsAffected > 0;


        }

        public static int GetApplicationIdForSpecificStatus(int PersonID, int ApplicationType, byte Status)
        {
            int ApplicationID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select Applications.ApplicationID from Applications
                             where ApplicationStatus=@Status And ApplicantPersonID=@PersonID 
                             And ApplicationTypeID=@ApplicationType";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@ApplicationType", ApplicationType);
            command.Parameters.AddWithValue("@Status", Status);
            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int AppIDResult))
                {
                    ApplicationID = AppIDResult;
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
            return ApplicationID;



        }
        public static bool DoesPersonHaveApplicationWithSpecificStatus(int PersonID, int ApplicationType, byte Status)
        {
            return GetApplicationIdForSpecificStatus(PersonID, ApplicationType, Status) != -1;

        }

        public static int GetApplicationIdForSpecificStatusAndLicenseClass(int PersonID, int LicenseClassID, int ApplicationType, byte Status)
        {
            int ApplicationIDWithStatusCompleted = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select Applications.ApplicationID from Applications 
                             inner join LocalDrivingLicenseApplications
                             on Applications.ApplicationID=LocalDrivingLicenseApplications.ApplicationID
                             where ApplicationStatus=@Status  And ApplicantPersonID=@PersonID and
                             ApplicationTypeID=@ApplicationType and LicenseClassID=@LicenseClassID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@ApplicationType", ApplicationType);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            command.Parameters.AddWithValue("@Status", Status);
            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int ApplicationID))
                {
                    ApplicationIDWithStatusCompleted = ApplicationID;
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
            return ApplicationIDWithStatusCompleted;



        }
        public static bool DoesPersonHaveApplicationWithSpecificStatusAndLicenseClass(int PersonID, int LicenseClassID, int ApplicationType, byte Status)
        {
            return GetApplicationIdForSpecificStatusAndLicenseClass(PersonID, LicenseClassID, ApplicationType, Status) != -1;

        }

        public static bool ChangeStatus(int ApplicationID, byte NewStatus)
        {
            int RowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"update Applications 
                             set ApplicationStatus=@NewStatus,
                             LastStatusDate=@LastStatusDate 
                             where  ApplicationID=@ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);


            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@NewStatus", NewStatus);
            command.Parameters.AddWithValue("@LastStatusDate", DateTime.Now);
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

            finally { connection.Close(); }
            return RowsAffected > 0;
        }

        public static string GetApplicationTypeTitle(int ApplicationTypeID)
        {
            string ApplicationTypeTitle = "";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select  ApplicationTypeTitle from ApplicationTypes where  ApplicationTypeID=@ApplicationTypeID";

            SqlCommand command = new SqlCommand(query, connection);


            command.Parameters.AddWithValue("ApplicationTypeID", ApplicationTypeID);

            try
            {
                connection.Open();


                object result = command.ExecuteScalar();

                if (result != null)
                {
                    ApplicationTypeTitle = result.ToString();
                }


            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex.Message);
                return "";
            }

            finally { connection.Close(); }
            return ApplicationTypeTitle;
        }

    }
}
