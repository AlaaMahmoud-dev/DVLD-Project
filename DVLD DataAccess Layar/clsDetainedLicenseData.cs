using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DVLD_DataAccess_Layar
{
    public class clsDetainedLicenseData
    {
        public static int AddNewDetainedLicense(int LicenseID, DateTime DetainDate, float FineFees, int CreatedByUserID)
        {
            int DetainID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string query = @"Insert into DetainedLicenses (LicenseID,DetainDate,FineFees,CreatedByUserID,IsReleased)
                           Values(@LicenseID,@DetainDate,@FineFees,@CreatedByUserID,@IsReleased);
                            select Scope_Identity();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);
           
            command.Parameters.AddWithValue("@DetainDate", DetainDate);
           
            command.Parameters.AddWithValue("@FineFees", FineFees);
           
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsReleased", 0);


            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    DetainID = insertedID;
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
            return DetainID;
        }

        public static bool UpdateDetainedLicenseInfo(int DetainID, int LicenseID,
            DateTime DetainDate, float FineFees, int CreatedByUserID)
        {
            int RowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update DetainedLicenses 
                           set 
                           LicenseID=@LicenseID,
                           DetainDate=@DetainDate,
                           FineFees=@FineFees,
                           CreatedByUserID=@CreatedByUserID
                           where DetainID=@DetainID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("DetainID", DetainID);
            command.Parameters.AddWithValue("LicenseID", LicenseID);
            command.Parameters.AddWithValue("DetainDate", DetainDate);
            command.Parameters.AddWithValue("FineFees", FineFees);

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


        public static bool FindDetainedLicenseInfoByID(int DetainID, ref int LicenseID,
            ref DateTime DetainDate, ref float FineFees, ref int CreatedByUserID, 
            ref bool IsReleased, ref DateTime ReleaseDate,
            ref int ReleasedByUserID, ref int ReleaseApplicationID)
        {



            bool isFound = false;


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from DetainedLicenses Where DetainID=@DetainID";

            SqlCommand sqlCommand = new SqlCommand(query, connection);

            sqlCommand.Parameters.AddWithValue("DetainID", DetainID);

            try
            {

                connection.Open();

                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {
                    LicenseID = (int)reader["LicenseID"];
                    DetainDate = (DateTime)reader["DetainDate"];


                    if (reader["IsReleased"] != null && bool.TryParse(reader["IsReleased"].ToString(), out bool Status))
                    {
                        IsReleased = Status;
                    }

                    FineFees = float.Parse(reader["FineFees"].ToString());

                    if (reader["ReleaseDate"] == System.DBNull.Value)
                    {
                        ReleaseDate = new DateTime();

                    }
                    else
                    {
                        ReleaseDate = (DateTime)reader["ReleaseDate"];

                    }

                    if (reader["ReleaseApplicationID"] == System.DBNull.Value)
                    {
                        ReleaseApplicationID = -1;

                    }
                    else
                    {
                        ReleaseApplicationID = (int)reader["ReleaseApplicationID"];

                    }

                    if (reader["ReleasedByUserID"] == System.DBNull.Value)
                    {
                        ReleasedByUserID = -1;

                    }
                    else
                    {
                        ReleasedByUserID = (int)reader["ReleasedByUserID"];

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

        public static bool FindDetainedLicenseInfoByLicenseID(ref int DetainID, int LicenseID,
            ref DateTime DetainDate, ref float FineFees, ref int CreatedByUserID,
            ref bool IsReleased, ref DateTime ReleaseDate, ref int ReleasedByUserID,
            ref int ReleaseApplicationID)
        {



            bool isFound = false;


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select top 1 * from DetainedLicenses Where LicenseID=@LicenseID order by DetainID desc";

            SqlCommand sqlCommand = new SqlCommand(query, connection);

            sqlCommand.Parameters.AddWithValue("LicenseID", LicenseID);

            try
            {

                connection.Open();

                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {
                    DetainID = (int)reader["DetainID"];
                    DetainDate = (DateTime)reader["DetainDate"];


                    if (reader["IsReleased"] != null && bool.TryParse(reader["IsReleased"].ToString(), out bool Status))
                    {

                        IsReleased = Status;
                    }

                    FineFees = float.Parse(reader["FineFees"].ToString());

                    if (reader["ReleaseDate"] == System.DBNull.Value)
                    {
                        ReleaseDate = new DateTime();

                    }
                    else
                    {
                        ReleaseDate = (DateTime)reader["ReleaseDate"];

                    }

                    if (reader["ReleaseApplicationID"] == System.DBNull.Value)
                    {
                        ReleaseApplicationID = -1;

                    }
                    else
                    {
                        ReleaseApplicationID = (int)reader["ReleaseApplicationID"];

                    }


                    if (reader["ReleasedByUserID"] == System.DBNull.Value)
                    {
                        ReleasedByUserID = -1;

                    }
                    else
                    {
                        ReleasedByUserID = (int)reader["ReleasedByUserID"];

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


        public static bool ReleaseDetainedLicense(int DetainID, int ReleaseApplicationID, int ReleasedByUserID)
        {
            int RowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string query = @"Update DetainedLicenses 
                           set 
                           ReleaseDate=@ReleaseDate,
                           IsReleased=@IsReleased,
                           ReleaseApplicationID=@ReleaseApplicationID,
                           ReleasedByUserID=@ReleasedByUserID
                           where DetainID=@DetainID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("DetainID", DetainID);
            
           
            command.Parameters.AddWithValue("ReleaseDate", DateTime.Now);
           
            command.Parameters.AddWithValue("IsReleased", 1);
            command.Parameters.AddWithValue("ReleaseApplicationID", ReleaseApplicationID);
            

            command.Parameters.AddWithValue("ReleasedByUserID", ReleasedByUserID);


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

        public static bool IsLicenseDetained(int LicenseID)
        {
            bool IsDetained = false;


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select Detain=1 from DetainedLicenses Where LicenseID=@LicenseID AND IsReleased=0";

            SqlCommand sqlCommand = new SqlCommand(query, connection);

            sqlCommand.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {

                connection.Open();

                 object result = sqlCommand.ExecuteScalar();

                if (result!=null)
                {
                    IsDetained = Convert.ToBoolean(result);
                    
                }
                
            }
            catch (Exception ex)
            {

                clsEventLogger.LogEvent(ex.Message);
                return false;
            }
            finally { connection.Close(); }

            return IsDetained;
        }

        public static DataTable GetDetainedLicensesList()
        {

            DataTable dtDetainedLicenses = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT        DetainedLicenses.DetainID, DetainedLicenses.LicenseID,
                            DetainedLicenses.DetainDate, DetainedLicenses.IsReleased,
                            DetainedLicenses.FineFees, DetainedLicenses.ReleaseDate, People.NationalNo,
                            ( People.FirstName+' '+ People.SecondName+' '+ ISNULL(People.ThirdName,'')+' '+ People.LastName)as FullName, 
                            DetainedLicenses.ReleaseApplicationID
                             FROM            DetainedLicenses INNER JOIN
                             Licenses ON DetainedLicenses.LicenseID = Licenses.LicenseID INNER JOIN
                             Drivers ON Licenses.DriverID = Drivers.DriverID INNER JOIN
                             People ON Drivers.PersonID = People.PersonID order by DetainID desc";

            SqlCommand command = new SqlCommand(query, connection);



            try
            {
                connection.Open();


                SqlDataReader reader = command.ExecuteReader();


                if (reader.HasRows)
                {

                    dtDetainedLicenses.Load(reader);


                }
                reader.Close();


            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex.Message);
                return null;
            }

            finally { connection.Close(); }
            return dtDetainedLicenses;

        }
    }
}
