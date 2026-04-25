using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess_Layar
{
    public class clsLocalDrivingLicenseApplicationData
    {
     
        public static bool FindLDLApplicationInfoByID(int LDLApplicationID, ref int ApplicationID, ref int LicenseClassID)
        {



            bool isFound = false;


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from LocalDrivingLicenseApplications Where LocalDrivingLicenseApplicationID=@LDLApplicationID";

            SqlCommand sqlCommand = new SqlCommand(query, connection);

            sqlCommand.Parameters.AddWithValue("LDLApplicationID", LDLApplicationID);

            try
            {

                connection.Open();

                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {

                    ApplicationID = (int)reader["ApplicationID"];

                    LicenseClassID = (int)reader["LicenseClassID"];


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
        public static bool FindLDLApplicationInfoByBaseAppID(ref int LDLApplicationID,  int ApplicationID, ref int LicenseClassID)
        {



            bool isFound = false;


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from LocalDrivingLicenseApplications Where ApplicationID=@ApplicationID";

            SqlCommand sqlCommand = new SqlCommand(query, connection);

            sqlCommand.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {

                connection.Open();

                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {

                    LDLApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];

                    LicenseClassID = (int)reader["LicenseClassID"];


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
        
        public static int AddNewLDLApplication(int ApplicationID, int LicenseClassID)
        {
            int LDLApplicationID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string query = @"Insert into LocalDrivingLicenseApplications (ApplicationID,LicenseClassID)
                           Values(@ApplicationID,@LicenseClassID);
                            select Scope_Identity();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("LicenseClassID", LicenseClassID);





            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    LDLApplicationID = insertedID;
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
            return LDLApplicationID;
        }

        public static bool UpdateLDLApplication(int LocalDrivingLicenseApplicationID, int ApplicationID, int LicenseClassID)
        {
            int RowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update LocalDrivingLicenseApplications 
                           set ApplicationID=@ApplicationID, 
                           LicenseClassID=@LicenseClassID
                          
                         
                           where LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("LicenseClassID", LicenseClassID);



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
       
      
        public static bool DeleteLDLApplication(int LocalDrivingLicenseApplicationID)
        {
            int RowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "delete from LocalDrivingLicenseApplications where LocalDrivingLicenseApplicationID=@ID";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@ID", LocalDrivingLicenseApplicationID);

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

        public static DataTable GetLDLApplicationsList()
        {
            DataTable dtLDLApplications = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"
                            SELECT        LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID, 
                            LicenseClasses.ClassName, People.NationalNo, People.FirstName+' '+
                            People.SecondName+' '+ ISNULL(People.ThirdName,'')+' '+
                            People.LastName as FullName, Applications.ApplicationDate, 
                            (					 
                            select count(TestAppointments.TestTypeID)as PassedTests from
                            TestAppointments
                            JOIN Tests on TestAppointments.TestAppointmentID=Tests.TestAppointmentID
                            where LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID
                            =TestAppointments.LocalDrivingLicenseApplicationID AND Tests.TestResult=1
                            )as PassedTest,(Case When ApplicationStatus=1 then 'New' when  ApplicationStatus=2 then 'Canceled' when  ApplicationStatus=3 then 'Completed' END)as ApplicationStatus
                            FROM            LocalDrivingLicenseApplications INNER JOIN
                            Applications ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID INNER JOIN
                            LicenseClasses ON LocalDrivingLicenseApplications.LicenseClassID = LicenseClasses.LicenseClassID INNER JOIN
                            People ON Applications.ApplicantPersonID = People.PersonID 



";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();


                SqlDataReader reader = command.ExecuteReader();


                if (reader.HasRows)
                {

                    dtLDLApplications.Load(reader);


                }
                reader.Close();


            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex.Message);
                return null;
            }

            finally { connection.Close(); }
            return dtLDLApplications;
        }

        public static bool DoesPassTest(int LDLApplicationID,int TestTypeID)
        {
            bool result = false;


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select TestResult from Tests Join TestAppointments
                             on Tests.TestAppointmentID=TestAppointments.TestAppointmentID
                             Join LocalDrivingLicenseApplications
     on LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID=TestAppointments.LocalDrivingLicenseApplicationID
     where Tests.TestResult=1 and TestAppointments.TestTypeID=@TestTypeID and LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID=@LDLApplicationID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LDLApplicationID", LDLApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();


                object Result = command.ExecuteScalar();

                if (Result != null)
                    result = true;

            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex.Message);
                return false;
            }

            finally { connection.Close(); }
            return result;
        }
        public static bool IsThereAnActiveScheduledTest(int LDLApplicationID, int TestTypeID)
        {
            bool result = false;


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select found=1 from TestAppointments Join LocalDrivingLicenseApplications
on LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID=TestAppointments.LocalDrivingLicenseApplicationID
where LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID and TestAppointments.TestTypeID=@TestTypeID and TestAppointments.IsLocked=0";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LDLApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();


                object Result = command.ExecuteScalar();

                if (Result != null)
                    result = true;

            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex.Message);
                return false;
            }

            finally { connection.Close(); }
            return result;
        }
        
        public static bool DoesAttendTestType(int LDLApplicationID, int TestTypeID)
        {
            bool result = false;


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select  top 1 found=1 from Tests
                             Join TestAppointments
                             on Tests.TestAppointmentID=TestAppointments.TestAppointmentID
                             where
                             TestAppointments.LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID 
                             and TestAppointments.TestTypeID=@TestTypeID ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LDLApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();


                object Result = command.ExecuteScalar();

                if (Result != null)
                    result = true;

            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex.Message);
                return false;

            }

            finally { connection.Close(); }
            return result;
        }
        public static int TotalTrialsPerTest(int LDLApplicationID, int TestTypeID)
        {
            int TotalTrialsPerTest = 0;


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select  COUNT(TestID) from Tests
                             Join TestAppointments
                             on Tests.TestAppointmentID=TestAppointments.TestAppointmentID
                             where
                             TestAppointments.LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID 
                             and TestAppointments.TestTypeID=@TestTypeID ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LDLApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();


                object Result = command.ExecuteScalar();

                if (Result != null && int.TryParse(Result.ToString(), out int Total))
                    TotalTrialsPerTest = Total;

            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex.Message);
                return -1;
            }

            finally { connection.Close(); }
            return TotalTrialsPerTest;
        }

        public static int GetPassedTests(int LDLApplicationID)
        {
            int PassedTestsCount = 0;


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select  COUNT(TestID) from Tests
                             Join TestAppointments
                             on Tests.TestAppointmentID=TestAppointments.TestAppointmentID
                             where
                             TestAppointments.LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID 
                              AND TestResult=1
                             ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LDLApplicationID);

            try
            {
                connection.Open();


                object Result = command.ExecuteScalar();

                if (Result != null && int.TryParse(Result.ToString(), out int Total))
                    PassedTestsCount = Total;

            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex.Message);
                return -1;
            }

            finally { connection.Close(); }
            return PassedTestsCount;
        }
        
             public static bool HasLicenseIssued(int ApplicationID)
        {
            bool result = false;


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select found=1 from Licenses
                             where
                             ApplicationID=@ApplicationID 
                             ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();


                object Result = command.ExecuteScalar();

                if (Result != null)
                    result = true;

            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex.Message);
                return false;

            }

            finally { connection.Close(); }
            return result;
        }
    }
}
