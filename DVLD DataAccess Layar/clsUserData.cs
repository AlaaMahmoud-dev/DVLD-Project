using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess_Layar
{
    public class clsUserData
    {

        public static bool FindUserByUserID(int UserID, ref int PersonID, ref string UserName, ref string Password, ref bool isActive)
        {

            bool isFound = false;


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from Users Where UserID=@UserID";

            SqlCommand sqlCommand = new SqlCommand(query, connection);

            sqlCommand.Parameters.AddWithValue("UserID", UserID);

            try
            {

                connection.Open();

                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {

                    PersonID = (int)reader["PersonID"];
                    UserName = (string)reader["UserName"];
                    Password = (string)reader["Password"];


                    if (bool.TryParse(reader["IsActive"].ToString(), out bool ActiveOrNot))
                    {
                        isActive = ActiveOrNot;
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

        public static bool FindUserByUserName(ref int UserID, ref int PersonID, string UserName, ref string Password, ref bool isActive)
        {

            bool isFound = false;


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from Users Where UserName=@UserName";

            SqlCommand sqlCommand = new SqlCommand(query, connection);

            sqlCommand.Parameters.AddWithValue("UserName", UserName);

            try
            {

                connection.Open();

                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {

                    PersonID = (int)reader["PersonID"];
                    UserID = (int)reader["UserID"];
                    Password = (string)reader["Password"];


                    if (bool.TryParse(reader["IsActive"].ToString(), out bool ActiveOrNot))
                    {
                        isActive = ActiveOrNot;
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

        public static bool FindUserByPersonID(ref int UserID, int PersonID, ref string UserName, ref string Password, ref bool isActive)
        {

            bool isFound = false;


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from Users Where PersonID=@PersonID";

            SqlCommand sqlCommand = new SqlCommand(query, connection);

            sqlCommand.Parameters.AddWithValue("PersonID", PersonID);

            try
            {

                connection.Open();

                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {

                    UserName = (string)reader["UserName"];
                    UserID = (int)reader["UserID"];
                    Password = (string)reader["Password"];


                    if (bool.TryParse(reader["IsActive"].ToString(), out bool ActiveOrNot))
                    {
                        isActive = ActiveOrNot;
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

        public static bool IsUserExists(int UserID)
        {

            bool isFound = false;


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select Found=1 from Users Where UserID=@UserID";

            SqlCommand sqlCommand = new SqlCommand(query, connection);

            sqlCommand.Parameters.AddWithValue("UserID", UserID);

            try
            {

                connection.Open();

                object Result = sqlCommand.ExecuteScalar();

                if (Result != null)
                    isFound = true;


            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex.Message);
                return false;
            }
            finally { connection.Close(); }

            return isFound;

        }
        public static bool IsUserExists(string UserName)
        {

            bool isFound = false;


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select Found=1 from Users Where UserName=@UserName";

            SqlCommand sqlCommand = new SqlCommand(query, connection);

            sqlCommand.Parameters.AddWithValue("UserName", UserName);

            try
            {

                connection.Open();

                object Result = sqlCommand.ExecuteScalar();

                if (Result != null)
                    isFound = true;


            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex.Message);
                return false;
            }
            finally { connection.Close(); }

            return isFound;

        }

        public static bool IsUserExistsForPersonID(int PersonID)
        {

            bool isFound = false;


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select Found=1 from Users Where PersonID=@PersonID";

            SqlCommand sqlCommand = new SqlCommand(query, connection);

            sqlCommand.Parameters.AddWithValue("PersonID", PersonID);

            try
            {

                connection.Open();

                object Result = sqlCommand.ExecuteScalar();

                if (Result != null)
                    isFound = true;
                   

            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex.Message);
                return false;
            }
            finally { connection.Close(); }

            return isFound;

        }

        public static DataTable GetUsersList()
        {

            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string query = @"SELECT  Users.UserID,  Users.PersonID, People.FirstName 
                            + ' ' + People.SecondName + ' ' + People.ThirdName + ' ' 
                            + People.LastName as FullName
                          , Users.UserName, Users.IsActive 
                            from People INNER JOIN
                            Users ON People.PersonID = Users.PersonID";



            SqlCommand cmd = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

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
            finally { connection.Close(); }
            return dt;
        }

        public static int AddNewUser(int PersonID, string UserName, string Password, bool isActive)
        {

            int UserID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string query = @"Insert into Users (PersonID,UserName,Password,IsActive)
                           Values(@PersonID,@UserName,@Password,@isActive);
                            select Scope_Identity();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("PersonID", PersonID);
            command.Parameters.AddWithValue("UserName", UserName);
            command.Parameters.AddWithValue("Password", Password);
            command.Parameters.AddWithValue("isActive", Convert.ToInt16(isActive));





            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    UserID = insertedID;
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
            return UserID;

        }

        public static bool UpdateUser(int UserID, int PersonID, string UserName, string Password, bool isActive)
        {
            int RowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update Users 
                           set PersonID=@PersonID, 
                           UserName=@UserName,
                           Password=@Password,
                           IsActive=@isActive
                           where UserID=@UserID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("UserID", UserID);
            command.Parameters.AddWithValue("PersonID", PersonID);
            command.Parameters.AddWithValue("UserName", UserName);
            command.Parameters.AddWithValue("Password", Password);
            command.Parameters.AddWithValue("isActive", Convert.ToInt16(isActive));

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

        public static bool ChangePassword(string UserName, string NewPassword)
        {
            int RowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update Users 
                           set
                           Password=@NewPassword
                           
                           where UserName=@UserName";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserName", UserName);
           
            command.Parameters.AddWithValue("@NewPassword", NewPassword);
           

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

        public static bool DeleteUser(int UserID)
        {
            int RowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "delete from Users where UserID=@ID";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("ID", UserID);

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

    }
}
