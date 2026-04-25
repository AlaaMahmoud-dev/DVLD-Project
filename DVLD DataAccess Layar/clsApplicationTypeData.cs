using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess_Layar
{
    public class clsApplicationTypeData
    {
       
    
        public static bool GetApplicationTypeInfoByID(int ApplicationTypeID, ref string ApplicationTypeTitle, ref float ApplicationFees)
        {

            bool isFound = false;


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from ApplicationTypes Where ApplicationTypeID=@ApplicationTypeID";

            SqlCommand sqlCommand = new SqlCommand(query, connection);

            sqlCommand.Parameters.AddWithValue("ApplicationTypeID", ApplicationTypeID);

            try
            {

                connection.Open();

                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {

                    ApplicationTypeTitle = (string)reader["ApplicationTypeTitle"];
                    ApplicationFees = (float)reader["ApplicationFees"];
                   

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

        public static float GetApplicationFees(int ApplicationTypeID)
        {
            float ApplicationTypeFees = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select ApplicationFees from ApplicationTypes where ApplicationTypeID=@ApplicationTypeID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("ApplicationTypeID", ApplicationTypeID);
            try
            {
                connection.Open();


                object result = command.ExecuteScalar();


                if (result != null && float.TryParse(result.ToString(), out float Fees))
                {

                    ApplicationTypeFees = Fees;


                }



            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex.Message);
                return -1;
            }

            finally { connection.Close(); }
            return ApplicationTypeFees;
        }

        public static DataTable GetApplicationTypesList()
        {

            DataTable dtApplicationTypes = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from ApplicationTypes";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();


                SqlDataReader reader = command.ExecuteReader();


                if (reader.HasRows)
                {

                    dtApplicationTypes.Load(reader);


                }
                reader.Close();


            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex.Message);
                return null;
            }

            finally { connection.Close(); }
            return dtApplicationTypes;
        }

        public static bool UpdateApplicationTypeInfo(int ApplicationTypeID, string Title, double Fees)
        {

            int RowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update ApplicationTypes set
                           ApplicationTypeTitle= @Title ,                  
                           ApplicationFees=@Fees
                           where ApplicationTypeID= @ApplicationTypeID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("Title", Title);
            command.Parameters.AddWithValue("Fees", Fees);
            command.Parameters.AddWithValue("ApplicationTypeID", ApplicationTypeID);
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

        public static int AddNewApplicationType(string Title, double Fees)
        {

            int ApplicationTypeID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Insert Into ApplicationTypes(ApplicationTypeTitle,ApplicationFees) Values
                           (@Title ,@Fees);
                           Select Scope_IDENTITY()";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Title", Title);
            command.Parameters.AddWithValue("@Fees", Fees);
            
            try
            {
                connection.Open();

                object result= command.ExecuteScalar();

                if (result != null&&int.TryParse(result.ToString(),out int InsertedID))
                {
                    ApplicationTypeID = InsertedID;
                }
                


            }

            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex.Message);
                return -1;
            }

            finally { connection.Close(); }
            return ApplicationTypeID;
        }





    }
}
