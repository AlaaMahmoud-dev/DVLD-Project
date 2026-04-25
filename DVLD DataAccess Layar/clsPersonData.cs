using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess_Layar
{
    public class clsPersonData
    {

     
        public static DataTable GetPeopleList()
        {

            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string query = @"SELECT People.PersonID, People.NationalNo,
                             People.FirstName, People.SecondName, People.ThirdName, People.LastName,
                             People.DateOfBirth, People.Gendor,
                             case when People.Gendor= 0 then 'Male' else 'Femail'
                             end as GendorCaption,
                             People.Address, People.Phone, People.Email, People.ImagePath, 
                             Countries.CountryName, People.NationalityCountryID
                             FROM 
                             Countries INNER JOIN
                             People ON Countries.CountryID = People.NationalityCountryID
";

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
            catch(Exception ex)
            {
                clsEventLogger.LogEvent(ex.Message);
                return null;
             
            }
            finally { connection.Close(); }
            return dt;
        }

    

        public static bool FindPersonByID(int ID,ref string NationalNo,
            ref string FirstName,ref string SecondName,
            ref string ThirdName,ref string LastName,
            ref DateTime DateOfBirth,ref byte Gendor,
            ref string Address,ref string Phone,
            ref string Email,ref int CountryID,
            ref string ImagePath)
        {

            bool isFound=false;


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from People Where PersonID=@PersonID";

            SqlCommand sqlCommand = new SqlCommand(query, connection);

            sqlCommand.Parameters.AddWithValue("PersonID", ID);

            try
            {

                connection.Open();

                SqlDataReader reader = sqlCommand.ExecuteReader();
                
                if(reader.Read())
                {

                  
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    ThirdName = (string)reader["ThirdName"];
                    LastName = (string)reader["LastName"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    NationalNo = (string)reader["NationalNo"];
                    Gendor = (byte)reader["Gendor"];
                    
                    Address = (string)reader["Address"];
                    Phone = (string)reader["Phone"];
                    Email = (string)reader["Email"];
                    CountryID = (int)reader["NationalityCountryID"];

                    if (reader["ImagePath"]==System.DBNull.Value)
                    {
                        ImagePath = "";
                    }
                    else
                    {
                        ImagePath = (string)reader["ImagePath"];
                    }


                    isFound = true;
                }
                reader.Close();




            }
            catch(Exception ex)
            {

                clsEventLogger.LogEvent(ex.Message);
               return false;

            }
            finally { connection.Close(); }

            return isFound;

        }


        public static bool FindPersonByNationalNo(ref int ID,  string NationalNo,
            ref string FirstName, ref string SecondName, ref string ThirdName,
            ref string LastName, ref DateTime DateOfBirth, ref byte Gendor,
            ref string Address, ref string Phone, ref string Email,
            ref int CountryID, ref string ImagePath)
        {

            bool isFound = false;


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from People Where NationalNo=@NationalNo";

            SqlCommand sqlCommand = new SqlCommand(query, connection);

            sqlCommand.Parameters.AddWithValue("NationalNo", NationalNo);

            try
            {

                connection.Open();

                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {

                    ID = (int)reader["PersonID"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    ThirdName = (string)reader["ThirdName"];
                    LastName = (string)reader["LastName"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gendor = (byte)reader["Gendor"];
                    Address = (string)reader["Address"];
                    Phone = (string)reader["Phone"];
                    Email = (string)reader["Email"];
                    CountryID = (int)reader["NationalityCountryID"];

                    if (reader["ImagePath"] == System.DBNull.Value)
                    {
                        ImagePath = "";
                    }
                    else
                    {
                        ImagePath = (string)reader["ImagePath"];
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

        public static int AddNewPerson(  string NationalNo,  string FirstName, 
            string SecondName,  string ThirdName,  string LastName,
            DateTime DateOfBirth, byte Gendor,  string Address, 
            string Phone,  string Email,
            int CountryID,  string ImagePath)
        {

            int PersonID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string query = @"Insert into People (NationalNo,FirstName,SecondName,ThirdName,LastName,DateOfBirth,Gendor,Email,Phone,Address,NationalityCountryID,ImagePath)
                           Values(@NationalNo,@FirstName,@SecondName,@ThirdName,@LastName,@DateOfBirth,@Gendor,@Email,@Phone,@Address,@CountryID,@ImagePath);
                            select Scope_Identity();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("NationalNo", NationalNo);
            command.Parameters.AddWithValue("FirstName", FirstName);
            command.Parameters.AddWithValue("SecondName", SecondName);
            command.Parameters.AddWithValue("ThirdName", ThirdName);
            command.Parameters.AddWithValue("LastName", LastName);
            command.Parameters.AddWithValue("DateOfBirth", DateOfBirth);
            
            command.Parameters.AddWithValue("Address", Address);
            command.Parameters.AddWithValue("Phone", Phone);
            command.Parameters.AddWithValue("Email", Email);
            command.Parameters.AddWithValue("CountryID", CountryID);
            command.Parameters.AddWithValue("Gendor", Gendor);


            if(ImagePath=="")
            {
                command.Parameters.AddWithValue("ImagePath", System.DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("ImagePath", ImagePath);
            }
            try
            {
                connection.Open();

                object result= command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    PersonID = insertedID;
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
            return PersonID;

        }

       

        public static bool UpdatePerson(int PersonID,string NationalNo, string FirstName, 
            string SecondName, string ThirdName, string LastName,
            DateTime DateOfBirth, byte Gendor, string Address,
            string Phone, string Email,
            int CountryID, string ImagePath)
        {
            int RowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update People 
                           set NationalNo=@NationalNo, 
                           FirstName=@FirstName,
                           SecondName=@SecondName,
                           ThirdName=@ThirdName,
                           LastName=@LastName,
                           Gendor=@Gendor,
                           Email=@Email,
                           Phone=@Phone,
                           Address=@Address,
                           DateOfBirth=@DateOfBirth,
                           NationalityCountryID=@CountryID,
                           ImagePath=@ImagePath
                           where PersonID=@PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("PersonID", PersonID);
            command.Parameters.AddWithValue("NationalNo", NationalNo);
            command.Parameters.AddWithValue("FirstName", FirstName);
            command.Parameters.AddWithValue("SecondName", SecondName);
            command.Parameters.AddWithValue("ThirdName", ThirdName);
            command.Parameters.AddWithValue("LastName", LastName);
            command.Parameters.AddWithValue("DateOfBirth", DateOfBirth);

            command.Parameters.AddWithValue("Address", Address);
            command.Parameters.AddWithValue("Phone", Phone);
            command.Parameters.AddWithValue("Email", Email);
            command.Parameters.AddWithValue("CountryID", CountryID);
            command.Parameters.AddWithValue("Gendor", Gendor);
            if (ImagePath == "")
            {
                command.Parameters.AddWithValue("ImagePath", System.DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("ImagePath", ImagePath);
            }
            try
            {
                connection.Open();

                RowsAffected=command.ExecuteNonQuery();



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

        public static DataTable GetAllNationalNumbers()
        {
            DataTable dtNationalNumbers = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select NationalNo from People";

            SqlCommand command = new SqlCommand(query,connection);
            
            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dtNationalNumbers.Load(reader);
                }
                reader.Close();

            }
            catch(Exception ex)
            {
                clsEventLogger.LogEvent(ex.Message);
                return null;
            }
            finally { connection.Close(); }

            return dtNationalNumbers;
        }
        public static bool IsPersonExistsByPersonID(int PersonID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string query = "select found=1 from People where PersonID=@ID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("ID", PersonID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                {
                    IsFound = true;
                }


            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex.Message);
                return false;
            }
            finally { connection.Close(); }


            return IsFound;






        }
        public static bool IsPersonExistsByNationalNo(string NationalNo)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string query = "select found=1 from People where NationalNo=@NationalNo";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("NationalNo", NationalNo);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                {
                    IsFound = true;
                }


            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex.Message);
                return false;
            }
            finally { connection.Close(); }


            return IsFound;






        }

        public static bool DeletePerson(int PersonID)
        {
            int RowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "delete from People where PersonID=@ID";

            SqlCommand cmd = new SqlCommand(query,connection);

            cmd.Parameters.AddWithValue("ID", PersonID);

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


        public static int GetDriverID(int PersonID)
        {
            int DriverID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string query = "select DriverID from drivers where PersonID=@ID";

            SqlCommand command = new SqlCommand(query,connection);

            command.Parameters.AddWithValue("ID", PersonID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null&&int.TryParse(result.ToString(),out int DRIVERID))
                {
                    DriverID = DRIVERID;
                }


            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex.Message);
                return -1;
            }
            finally { connection.Close(); }


            return DriverID;






        }


    }
}
