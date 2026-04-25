using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess_Layar
{
    public class clsTestAppointmentData
    {

        public static DataTable GetAppointmentsListPerApplicationAndTestType(int LDLApplicationID, int TestTypeID)
        {
            DataTable dtAppointmentsList = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select TestAppointments.TestAppointmentID,TestAppointments.AppointmentDate,TestAppointments.PaidFees,TestAppointments.IsLocked from TestAppointments
                            where LocalDrivingLicenseApplicationID=@LDLApplicationID and TestTypeID=@TestTypeID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("LDLApplicationID", LDLApplicationID);
            command.Parameters.AddWithValue("TestTypeID", TestTypeID);


            try
            {
                connection.Open();


                SqlDataReader reader = command.ExecuteReader();


                if (reader.HasRows)
                {

                    dtAppointmentsList.Load(reader);


                }
                reader.Close();


            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex.Message);
                return null;
            }

            finally { connection.Close(); }
            return dtAppointmentsList;
        }


        //Exists in LDL Class
        public static bool HasActiveTestAppointment(int LDLApplicationID, int TestTypeID)
        {
            int hasNotLockedAppointment = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"  select case 
                                when exists 
                                  (
                                      select  top 1 *  from TestAppointments  
                                      where(TestAppointments.LocalDrivingLicenseApplicationID=@LDLApplicationID and TestAppointments.TestTypeID=@TestTypeID and IsLocked=0 )
                                  )
                               then 1
                               else 0
                               end  ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("LDLApplicationID", LDLApplicationID);
            command.Parameters.AddWithValue("TestTypeID", TestTypeID);


            try
            {
                connection.Open();


                object result = command.ExecuteScalar();


                if (result != null && int.TryParse(result.ToString(), out int hasNotLocked))
                {
                    hasNotLockedAppointment = hasNotLocked;
                }


            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex.Message);
                return false;
            }

            finally { connection.Close(); }
            return hasNotLockedAppointment == 1;
        }


        //Exists in LDL Class
        public static bool isTestPassed(int LDLApplicationID, int TestTypeID)
        {
            int isTestPassed = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"  select case when exists (select top 1 * from  TestAppointments INNER JOIN
                         Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
						 where(TestAppointments.LocalDrivingLicenseApplicationID=@LDLApplicationID and TestAppointments.TestTypeID=@TestTypeID and TestResult=1)) then 1 else 0 end";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LDLApplicationID", LDLApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);


            try
            {
                connection.Open();


                object result = command.ExecuteScalar();


                if (result != null && int.TryParse(result.ToString(), out int isPassed))
                {

                    isTestPassed = isPassed;


                }


            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex.Message);
                return false;
            }

            finally { connection.Close(); }
            return isTestPassed == 1;
        }


        //Exists in LDL Class
        public static bool hasAppointment(int LDLApplicationID, int TestTypeID)
        {
            int hasAppointment = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select  Case 
                             when
                             exists
                             (
                                 select top 1 * from TestAppointments 
                                 where
                                       (
                                     TestAppointments.LocalDrivingLicenseApplicationID=@LDLApplicationID 
                                     and 
                                     TestAppointments.TestTypeID=@TestTypeID
                                       )
                             ) 
                             THEN 1
                             ELSE 0
                         END";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("LDLApplicationID", LDLApplicationID);
            command.Parameters.AddWithValue("TestTypeID", TestTypeID);


            try
            {
                connection.Open();


                object result = command.ExecuteScalar();


                if (result != null && int.TryParse(result.ToString(), out int HasAppointment))
                {
                    hasAppointment = HasAppointment;
                }


            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex.Message);
                return false;
            }

            finally { connection.Close(); }
            return hasAppointment == 1;
        }

        //Exists in LDL Class
        public static int GetTotalNumberOfTrials(int LDLApplicationID, int TestTypeID)
        {

            int TotalNumberOFTrials = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string query = @"select count(*) from 
                             (
                             select (TestAppointments.TestAppointmentID) from TestAppointments 
                             where TestAppointments.LocalDrivingLicenseApplicationID=@LDLApplicationID and  TestAppointments.TestTypeID=@TestTypeID and IsLocked=1
                             )TotalTrialsRelation ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("LDLApplicationID", LDLApplicationID);
            command.Parameters.AddWithValue("TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int TotalTrials))
                {
                    TotalNumberOFTrials = TotalTrials;
                }


            }




            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex.Message);
                return -1;
            }

            finally { connection.Close(); }
            return TotalNumberOFTrials;





        }


        public static int AddNewTestAppointment(int TestTypeID, int LDLApplicationID,
            DateTime AppointmentDate, float PaidFees, int CreatedByUserID,
            bool isLocked,int RetakeTestApplicationID)

        {
            int TestAppointmentID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string query = @"Insert into TestAppointments 
(TestTypeID,LocalDrivingLicenseApplicationID,AppointmentDate,PaidFees,CreatedByUserID,isLocked,RetakeTestApplicationID)
                           Values
(@TestTypeID,@LocalDrivingLicenseApplicationID,@AppointmentDate,@PaidFees,@CreatedByUserID,@isLocked,@RetakeTestApplicationID);
                            select Scope_Identity();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("LocalDrivingLicenseApplicationID", LDLApplicationID);
            command.Parameters.AddWithValue("AppointmentDate", AppointmentDate);

            command.Parameters.AddWithValue("PaidFees", PaidFees);
            command.Parameters.AddWithValue("CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("IsLocked"
                    , isLocked ? 1 : 0);

            if (RetakeTestApplicationID != -1)
                command.Parameters.AddWithValue("RetakeTestApplicationID", RetakeTestApplicationID);
            else
                command.Parameters.AddWithValue("RetakeTestApplicationID", DBNull.Value);





            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    TestAppointmentID = insertedID;
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
            return TestAppointmentID;
        }

        public static bool FindAppointment(int TestAppointmentID, ref int TestTypeID, ref int LocalDrivingLicenseApplicationID, ref DateTime AppointmentDate, ref float PaidFees
            , ref int CreatedByUserID, ref bool isLocked, ref int RetakeTestApplicationID)
        {

            bool isFound = false;


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from TestAppointments Where TestAppointmentID=@TestAppointmentID";

            SqlCommand sqlCommand = new SqlCommand(query, connection);

            sqlCommand.Parameters.AddWithValue("TestAppointmentID", TestAppointmentID);

            try
            {

                connection.Open();

                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {


                    TestTypeID = (int)reader["TestTypeID"];
                    LocalDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                    AppointmentDate = (DateTime)reader["AppointmentDate"];


                    PaidFees = float.Parse(reader["PaidFees"].ToString());


                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    if (bool.TryParse(reader["IsLocked"].ToString(), out bool Locked))
                    {
                        isLocked = Locked;
                    }

                    if (reader["RetakeTestApplicationID"] != DBNull.Value)
                        RetakeTestApplicationID = (int)reader["RetakeTestApplicationID"];
                    else
                        RetakeTestApplicationID = -1;


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

        public static bool GetLastTestAppointment(int TestTypeID,  int LocalDrivingLicenseApplicationID,ref int AppointmentID, ref DateTime AppointmentDate, ref float PaidFees
          , ref int CreatedByUserID, ref bool isLocked, ref int RetakeTestApplicationID)
        {

            bool isFound = false;


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select top 1 * from TestAppointments 
                            Where LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID
                            And TestTypeID=@TestTypeID order by AppointmentID desc";

            SqlCommand sqlCommand = new SqlCommand(query, connection);

            sqlCommand.Parameters.AddWithValue("LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            sqlCommand.Parameters.AddWithValue("TestTypeID", TestTypeID);
            try
            {

                connection.Open();

                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {


                    AppointmentID = (int)reader["AppointmentID"];

                    AppointmentDate = (DateTime)reader["AppointmentDate"];
                    PaidFees = float.Parse(reader["PaidFees"].ToString());
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    if (bool.TryParse(reader["IsLocked"].ToString(), out bool Locked))
                    {
                        isLocked = Locked;
                    }

                    if (reader["RetakeTestApplicationID"] != DBNull.Value)
                        RetakeTestApplicationID = (int)reader["RetakeTestApplicationID"];
                    else
                        RetakeTestApplicationID = -1;



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

        public static bool UpdateAppointment(int TestAppointmentID, int TestTypeID, int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, float PaidFees
            , int CreatedByUserID, bool isLocked, int RetakeTestApplicationID)
        {
            int RowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update TestAppointments 
                           set TestTypeID=@TestTypeID, 
                           LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID,
                           AppointmentDate=@AppointmentDate,
                           PaidFees=@PaidFees,
                           CreatedByUserID=@CreatedByUserID,
                           IsLocked=@isLocked,
                           RetakeTestApplicationID=@RetakeTestApplicationID
                         
                           where TestAppointmentID=@TestAppointmentID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("PaidFees", PaidFees);
            command.Parameters.AddWithValue("CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("isLocked", isLocked ? 1 : 0);
            if (RetakeTestApplicationID != -1)
                command.Parameters.AddWithValue("RetakeTestApplicationID", RetakeTestApplicationID);
            else
                command.Parameters.AddWithValue("RetakeTestApplicationID", DBNull.Value);

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


        //Move it to Test class
        public static int AddNewTest(int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            int TestID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string query = @"Insert into Tests (TestAppointmentID,TestResult,Notes,CreatedByUserID)
                           Values(@TestAppointmentID,@TestResult,@Notes,@CreatedByUserID);
                            select Scope_Identity();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("TestAppointmentID", TestAppointmentID);

            if (string.IsNullOrWhiteSpace(Notes))
                command.Parameters.AddWithValue("Notes", System.DBNull.Value);
            else
                command.Parameters.AddWithValue("Notes", Notes);

            command.Parameters.AddWithValue("CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("TestResult"
                    , TestResult ? 1 : 0);









            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    TestID = insertedID;
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
            return TestID;
        }

    }
}
