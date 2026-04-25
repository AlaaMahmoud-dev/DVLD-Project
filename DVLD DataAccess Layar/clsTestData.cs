using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess_Layar
{
    public class clsTestData
    {

        public static bool FindTestByID( int TestID,ref int TestAppointmentID,
            ref bool TestResult, ref string Notes, ref int CreatedByUserID)
        {

            bool isFound = false;


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from Tests Where TestID=@TestID";

            SqlCommand sqlCommand = new SqlCommand(query, connection);

            sqlCommand.Parameters.AddWithValue("TestID", TestID);

            try
            {

                connection.Open();

                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {

                    TestAppointmentID = (int)reader["TestAppointmentID"];
                    TestResult = (bool)reader["TestResult"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];

                    if (reader["Notes"] == DBNull.Value)
                        Notes = "";
                    else
                        Notes = (string)reader["Notes"];


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
        
              public static bool FindTestByAppointmentID(int TestAppointmentID, ref int TestID,
            ref bool TestResult, ref string Notes, ref int CreatedByUserID)
        {

            bool isFound = false;


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from Tests Where TestAppointmentID=@TestAppointmentID";

            SqlCommand sqlCommand = new SqlCommand(query, connection);

            sqlCommand.Parameters.AddWithValue("TestAppointmentID", TestAppointmentID);

            try
            {

                connection.Open();

                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {

                    TestID = (int)reader["TestID"];
                    TestResult = (bool)reader["TestResult"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];

                    if (reader["Notes"] == DBNull.Value)
                        Notes = "";
                    else
                        Notes = (string)reader["Notes"];


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
        public static int AddNewTest(int TestAppointmentID, bool TestResult, 
            string Notes,  int CreatedByUserID)

        {
            int TestID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string query = @"Insert into Tests
                            (TestAppointmentID,TestResult,Notes,CreatedByUserID)
                            Values
                            (@TestAppointmentID,@TestResult,@Notes,@CreatedByUserID);
                            Update TestAppointments set IsLocked=1 
                            where TestAppointmentID=@TestAppointmentID;
                            select Scope_Identity();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("TestResult", TestResult);

            if (!string.IsNullOrEmpty(Notes))
                command.Parameters.AddWithValue("Notes", Notes);
            else
                command.Parameters.AddWithValue("Notes", DBNull.Value);



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


        public static bool UpdateTestInfo(int TestID, int TestAppointmentID, bool TestResult,
           string Notes, int CreatedByUserID)

        {
            int RowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string query = @"Update Tests
                             set TestAppointmentID=@TestAppointmentID,
                             TestResult=@TestResult,
                             Notes=@Notes,
                             CreatedByUserID=@CreatedByUserID 
                             where TestID=@TestID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("TestID", TestID);
            command.Parameters.AddWithValue("TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("TestResult", TestResult);

            if (!string.IsNullOrEmpty(Notes))
                command.Parameters.AddWithValue("Notes", Notes);
            else
                command.Parameters.AddWithValue("Notes", DBNull.Value);





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
            return RowsAffected>0;
        }


        public static int GetPassedTestsCount(int LDLApplicationID)
        {
            int NumberOfPassedTests = 0;


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select count(TestResult) 
        
                            FROM            
                            LocalDrivingLicenseApplications
                            INNER JOIN TestAppointments
                            ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID
                            = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                            Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID INNER JOIN
                            TestTypes ON TestAppointments.TestTypeID = TestTypes.TestTypeID
        			        where(LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID
                            =@LDLApplicationID and Tests.TestResult=1 )";

        						 
        

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("LDLApplicationID", LDLApplicationID);

            try
            {
                connection.Open();


                object Result = command.ExecuteScalar();

                if (Result != null && int.TryParse(Result.ToString(), out int PassedTests))
                    NumberOfPassedTests = PassedTests;

            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex.Message);
                return -1;
            }

            finally { connection.Close(); }
            return NumberOfPassedTests;

        }

           


    }
}
