using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_DataAccess_Layar
{
    public class clsLicenseData
    {
        
        public static bool isLicenseDetained(int LicenseID)
        {

            bool isDetained = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select top 1 IsReleased
                             from DetainedLicenses 
                             where
                             LicenseID=@LicenseID
                             order by DetainID desc ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("LicenseID", LicenseID);



            try
            {
                connection.Open();


                object result = command.ExecuteScalar();


                if (result != null && bool.TryParse(result.ToString(), out bool isReleased))
                {
                    isDetained = !isReleased;

                }


            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex.Message);
                return false;
            }

            finally { connection.Close(); }
            return isDetained;
        }
       

        public static bool FindDriverLicense(int LicenseID, ref int ApplicationID,
            ref int DriverID, ref int LicenseClass, ref DateTime IssueDate,
            ref DateTime ExpirationDate, ref string Notes, 
            ref float PaidFees, ref bool isActive, 
            ref int IssueReasone, ref int CreatedByUserID)
        {


            bool isFound = false;


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from Licenses Where LicenseID=@LicenseID";

            SqlCommand sqlCommand = new SqlCommand(query, connection);

            sqlCommand.Parameters.AddWithValue("LicenseID", LicenseID);

            try
            {

                connection.Open();

                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {

                    ApplicationID = (int)reader["ApplicationID"];
                    DriverID = (int)reader["DriverID"];
                    LicenseClass = (int)reader["LicenseClass"];

                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    IssueDate = (DateTime)reader["IssueDate"];

                    if (reader["Notes"] == System.DBNull.Value)
                    {
                        Notes = "";
                    }
                    else
                    {
                        Notes = (string)reader["Notes"];
                    }
                    IssueReasone = int.Parse(reader["IssueReason"].ToString());



                    if (reader["IsActive"] != null && bool.TryParse(reader["IsActive"].ToString(), out bool Status))
                    {
                        isActive = Status;
                    }





                    CreatedByUserID = (int)reader["CreatedByUserID"];

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

        public static bool FindDriverLicenseByApplicationID(ref int LicenseID, 
            int ApplicationID, ref int DriverID, ref int LicenseClass,
            ref DateTime IssueDate, ref DateTime ExpirationDate
              , ref string Notes, ref float PaidFees, ref bool isActive,
            ref int IssueReasone, ref int CreatedByUserID)
        {



            bool isFound = false;


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from Licenses Where ApplicationID=@ApplicationID";

            SqlCommand sqlCommand = new SqlCommand(query, connection);

            sqlCommand.Parameters.AddWithValue("ApplicationID", ApplicationID);

            try
            {

                connection.Open();

                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {

                    LicenseID = (int)reader["LicenseID"];
                    DriverID = (int)reader["DriverID"];
                    LicenseClass = (int)reader["LicenseClass"];

                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    IssueDate = (DateTime)reader["IssueDate"];

                    if (reader["Notes"] == System.DBNull.Value)
                    {
                        Notes = "";
                    }
                    else
                    {
                        Notes = (string)reader["Notes"];
                    }

                    IssueReasone = int.Parse(reader["IssueReason"].ToString());



                    if (reader["IsActive"] != null && bool.TryParse(reader["IsActive"].ToString(), out bool Status))
                    {
                        isActive = Status;
                    }





                    CreatedByUserID = (int)reader["CreatedByUserID"];

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
        
        public static bool FindActiveDriverLicenseByApplicationID(ref int LicenseID,
            int ApplicationID, ref int DriverID, ref int LicenseClass,
            ref DateTime IssueDate, ref DateTime ExpirationDate
              , ref string Notes, ref float PaidFees, ref bool isActive,
            ref int IssueReasone, ref int CreatedByUserID)
        {



            bool isFound = false;


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from Licenses Where ApplicationID=@ApplicationID And IsActive=1";

            SqlCommand sqlCommand = new SqlCommand(query, connection);

            sqlCommand.Parameters.AddWithValue("ApplicationID", ApplicationID);

            try
            {

                connection.Open();

                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {

                    LicenseID = (int)reader["LicenseID"];
                    DriverID = (int)reader["DriverID"];
                    LicenseClass = (int)reader["LicenseClass"];

                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    IssueDate = (DateTime)reader["IssueDate"];

                    if (reader["Notes"] == System.DBNull.Value)
                    {
                        Notes = "";
                    }
                    else
                    {
                        Notes = (string)reader["Notes"];
                    }

                    IssueReasone = int.Parse(reader["IssueReason"].ToString());



                    if (reader["IsActive"] != null && bool.TryParse(reader["IsActive"].ToString(), out bool Status))
                    {
                        isActive = Status;
                    }





                    CreatedByUserID = (int)reader["CreatedByUserID"];

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
        public static int AddNewLicense(int ApplicationID, int DriverID,
            int LicenseClass, DateTime IssueDate, DateTime ExpirationDate, 
            string Notes, float PaidFees, bool isActive,
            int IssueReasone, int CreatedByUserID)
        {
            int LicenseID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string query = @"Insert into Licenses (ApplicationID,DriverID,LicenseClass,IssueDate,ExpirationDate,Notes,PaidFees,IsActive,IssueReason,CreatedByUserID)
                           Values(@ApplicationID,@DriverID,@LicenseClass,@IssueDate,@ExpirationDate,@Notes,@PaidFees,@isActive,@IssueReasone,@CreatedByUserID);
                            select Scope_Identity();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("DriverID", DriverID);
            command.Parameters.AddWithValue("LicenseClass", LicenseClass);
            command.Parameters.AddWithValue("IssueDate", IssueDate);
            command.Parameters.AddWithValue("ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("PaidFees", PaidFees);
            command.Parameters.AddWithValue("IssueReasone", IssueReasone);

            if (string.IsNullOrWhiteSpace(Notes))
                command.Parameters.AddWithValue("Notes", System.DBNull.Value);
            else
                command.Parameters.AddWithValue("Notes", Notes);

            command.Parameters.AddWithValue("CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("isActive"
                    , isActive ? 1 : 0);









            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    LicenseID = insertedID;
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
            return LicenseID;
        }

        public static bool UpdateDrivingLicenseInfo(int LicenseID, int ApplicationID,
            int DriverID, int LicenseClass, DateTime IssueDate,
            DateTime ExpirationDate, string Notes, float PaidFees,
            bool isActive, int IssueReason, int CreatedByUserID)
        {
            int RowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update Licenses 
                           set 
                           ApplicationID=@ApplicationID,
                           DriverID=@DriverID,
                           LicenseClass=@LicenseClass,
                           IssueDate=@IssueDate,
                           ExpirationDate=@ExpirationDate,
                           Notes=@Notes,
                           PaidFees=@PaidFees,
                           IsActive=@isActive,
                           IssueReason=@IssueReason,
                           CreatedByUserID=@CreatedByUserID
                          
                         
                           where LicenseID=@LicenseID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("LicenseID", LicenseID);
            command.Parameters.AddWithValue("ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("DriverID", DriverID);
            command.Parameters.AddWithValue("LicenseClass", LicenseClass);
            command.Parameters.AddWithValue("IssueDate", IssueDate);
            command.Parameters.AddWithValue("ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("PaidFees", PaidFees);
            command.Parameters.AddWithValue("isActive", isActive);
            command.Parameters.AddWithValue("IssueReason", IssueReason);
            command.Parameters.AddWithValue("CreatedByUserID", CreatedByUserID);

            if (string.IsNullOrWhiteSpace(Notes))
            {
                command.Parameters.AddWithValue("Notes", System.DBNull.Value);

            }
            else
            {
                command.Parameters.AddWithValue("Notes", Notes);
            }
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


        public static int FindActiveLicenseIDForPersonAndLicenseClass(int PersonID, int LicenseClass)
        {


            int LicenseID = -1;


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select LicenseID from Licenses
                             JOIN Drivers 
                             on Drivers.DriverID=Licenses.DriverID
                             Where LicenseClass=@LicenseClass 
                             AND Drivers.PersonID=@PersonID
                             And IsActive=1";

            SqlCommand sqlCommand = new SqlCommand(query, connection);

            sqlCommand.Parameters.AddWithValue("@PersonID", PersonID);
            sqlCommand.Parameters.AddWithValue("@LicenseClass", LicenseClass);
            try
            {

                connection.Open();

                object result = sqlCommand.ExecuteScalar();

                if (result!=null&&int.TryParse(result.ToString(),out int RetrievedLicenseID))
                {

                    LicenseID = RetrievedLicenseID;
                }
              




            }
            catch (Exception ex)
            {

                clsEventLogger.LogEvent(ex.Message);
                return -1;


            }
            finally { connection.Close(); }

            return LicenseID;
        }

        public static bool DeactivateLicense(int LicenseID)
        {
            int RowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update Licenses 
                           set 
                           IsActive=0
                           where LicenseID=@LicenseID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("LicenseID", LicenseID);
           

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

        public static DataTable GetLocalDrivingLicensesHistory(int DriverID)
        {

            DataTable dtLicensesHistory = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select Licenses.LicenseID,Licenses.ApplicationID,
                             LicenseClasses.ClassName,Licenses.IssueDate,
                             Licenses.ExpirationDate,Licenses.IsActive 
                             from Licenses
                             JOIN LicenseClasses 
                             on LicenseClasses.LicenseClassID= Licenses.LicenseClass
                             where Licenses.DriverID=@DriverID Order By IsActive desc , ExpirationDate desc";

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

        public static int GetLicenseValidityLength(int LicenseClassID)
        {




            int ValidityLength = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string query = @"select DefaultValidityLength from LicenseClasses 
                             where LicenseClassID=@LicenseClassID ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("LicenseClassID", LicenseClassID);


            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int defaultValidityLength))
                {
                    ValidityLength = defaultValidityLength;
                }


            }




            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex.Message);
                return -1;
            }

            finally { connection.Close(); }
            return ValidityLength;


        }
        
    }
}
