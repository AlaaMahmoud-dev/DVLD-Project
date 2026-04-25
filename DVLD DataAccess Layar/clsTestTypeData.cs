using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess_Layar
{
    public class clsTestTypeData
    {
        public static DataTable GetTestTypesList()
        {

            DataTable dtTestTypes = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from TestTypes";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();


                SqlDataReader reader = command.ExecuteReader();


                if (reader.HasRows)
                {

                    dtTestTypes.Load(reader);


                }
                reader.Close();


            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex.Message);
                return null;
            }

            finally { connection.Close(); }
            return dtTestTypes;
        }
        public static bool GetTestTypeInfoByID(int TestTypeID, ref string TestTypeTitle, ref float TestTypeFees
            , ref string TestTypeDescription)
        {

            bool isFound = false;


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from TestTypes Where TestTypeID=@TestTypeID";

            SqlCommand sqlCommand = new SqlCommand(query, connection);

            sqlCommand.Parameters.AddWithValue("TestTypeID", TestTypeID);

            try
            {

                connection.Open();

                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {

                    TestTypeTitle = (string)reader["TestTypeTitle"];
                    TestTypeFees = (float)reader["TestTypeFees"];
                    TestTypeDescription = (string)reader["TestTypeDescription"];

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

        public static bool UpdateTestTypeInfo(int TestTypeID, string Title, string Description, double Fees)
        {

            int RowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update TestTypes set
                           TestTypeTitle= @Title , 
                           TestTypeDescription=@Description,
                           TestTypeFees=@Fees
                           where TestTypeID= @TestTypeID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("Title", Title);
            command.Parameters.AddWithValue("Fees", Fees);
            command.Parameters.AddWithValue("Description", Description);
            command.Parameters.AddWithValue("TestTypeID", TestTypeID);
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
        public static int AddNewTestType(string Title, double Fees, string Description)
        {

            int TestTypeID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Insert Into TestTypes(TestTypeTitle,TestTypeFees,TestTypeDescription) Values
                           (@Title ,@Fees,@Description);
                           Select Scope_IDENTITY()";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Title", Title);
            command.Parameters.AddWithValue("@Fees", Fees);
            command.Parameters.AddWithValue("@Description", Description);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                {
                    TestTypeID = InsertedID;
                }



            }

            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex.Message);
                return -1;
            }

            finally { connection.Close(); }
            return TestTypeID;
        }

        public static float GetTestFees(int TestTypeID)
        {
            float Fees = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string query = "SELECT TestTypeFees FROM TestTypes where TestTypes.TestTypeID=@TestTypeID ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && float.TryParse(result.ToString(), out float TestFees))
                {
                    Fees = TestFees;
                }


            }




            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex.Message);
                return -1;
            }

            finally { connection.Close(); }
            return Fees;





        }
    }
}
