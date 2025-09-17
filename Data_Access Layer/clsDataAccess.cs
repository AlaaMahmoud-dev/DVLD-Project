using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess_Layar
{
    public class clsDataAccess
    {

        // public static bool Find(ref int PersonID,)

        public static DataTable GetPeopleList()
        {

            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string query = @"SELECT        People.PersonID, People.NationalNo, People.FirstName, People.SecondName, People.ThirdName, People.LastName, People.DateOfBirth, People.Gendor,
                            case when People.Gendor= 0 then 'Male' else 'Femail'
                         end as GendorCaption,
                          People.Address, People.Phone, People.Email, People.ImagePath, 
                         Countries.CountryName, People.NationalityCountryID
FROM            Countries INNER JOIN
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
                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }
            return dt;
        }

        public static DataTable GetCountriesList()
        {

            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string query = "Select CountryName from Countries";

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
                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }
            return dt;
        }

        public static bool FindPerson(int ID,ref string NationalNo,ref string FirstName,ref string SecondName,ref string ThirdName,ref string LastName,ref DateTime DateOfBirth
            ,ref char Gendor,ref string Address,ref string Phone,ref string Email,ref int CountryID,ref string ImagePath)
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
                    if (bool.TryParse(reader["Gendor"].ToString(), out bool Type))
                    {
                        if (Type)
                        {
                            Gendor = 'F';
                        }

                        else
                        {
                            Gendor = 'M';
                        }
                    }
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

                Console.WriteLine(ex.Message);
                return false;

            }
            finally { connection.Close(); }

            return isFound;

        }


        public static bool FindPerson(ref int ID,  string NationalNo, ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName, ref DateTime DateOfBirth
           , ref char Gendor, ref string Address, ref string Phone, ref string Email, ref int CountryID, ref string ImagePath)
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
                    if (bool.TryParse(reader["Gendor"].ToString(), out bool Type))
                    {
                        if (Type)
                        {
                            Gendor = 'F';
                        }

                        else
                        {
                            Gendor = 'M';
                        }
                    }
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

                Console.WriteLine(ex.Message);
                return false;

            }
            finally { connection.Close(); }

            return isFound;

        }

        public static string GetCountryName(int CountryID)
        {
            string CountryName = "";


            SqlConnection connection = new SqlConnection(clsDataAccess.clsDataAccessSettings.ConnectionString);

            string query = "select CountryName from Countries where CountryID=@CountryID";

            SqlCommand cmd = new SqlCommand(query,connection);

            cmd.Parameters.AddWithValue("CountryID", CountryID);
            try
            {
                connection.Open();

                object result= cmd.ExecuteScalar();

                if(result!=null)
                {
                    CountryName = (string)result;
                }
                else
                {
                    Console.WriteLine("Country Not Found");
                }

            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

            finally { connection.Close(); }

            return CountryName;


        }


        public static int GetCountryID(string CountryName)
        {
            int CountryID= -1;


            SqlConnection connection = new SqlConnection(clsDataAccess.clsDataAccessSettings.ConnectionString);

            string query = "select CountryID from Countries where CountryName=@CountryName";

            
            
            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("CountryName", CountryName);


            try
            {
                connection.Open();

                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    CountryID = (int)result;
                }
                else
                {
                    Console.WriteLine("Country Not Found");
                }

            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

            finally { connection.Close(); }

            return CountryID;


        }
        public static int AddNewPerson(  string NationalNo,  string FirstName,  string SecondName,  string ThirdName,  string LastName,  DateTime DateOfBirth
            ,  char Gendor,  string Address,  string Phone,  string Email,  int CountryID,  string ImagePath)
        {

            int PersonID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccess.clsDataAccessSettings.ConnectionString);


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
            if (Gendor == 'M')
            {
                command.Parameters.AddWithValue("Gendor", 0);
            }
            else
            {
                command.Parameters.AddWithValue("Gendor", 1);
            }

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
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return PersonID;

        }

       

      


        public static bool UpdatePerson(int PersonID,string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth
            , char Gendor, string Address, string Phone, string Email, int CountryID, string ImagePath)
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
            if (Gendor == 'M')
            {
                command.Parameters.AddWithValue("Gendor", 0);
            }
            else
            {
                command.Parameters.AddWithValue("Gendor", 1);
            }

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
                Console.WriteLine(ex.Message);
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
                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }

            return dtNationalNumbers;
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
                Console.WriteLine(ex.Message);
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
                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }


            return DriverID;






        }


        /// 
        /// Users
        /// 


        public static bool FindUserByUserID( int UserID, ref int PersonID, ref string UserName, ref string Password, ref bool isActive)
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

                Console.WriteLine(ex.Message);
                return false;

            }
            finally { connection.Close(); }

            return isFound;

        }

        public static bool FindUserByUserName(ref int UserID, ref int PersonID,  string UserName, ref string Password, ref bool isActive)
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

                Console.WriteLine(ex.Message);
                return false;

            }
            finally { connection.Close(); }

            return isFound;

        }

        public static bool isPersonAnUser( int PersonID)
        {

            bool isFound = false;


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select PersonID from Users Where PersonID=@PersonID";

            SqlCommand sqlCommand = new SqlCommand(query, connection);

            sqlCommand.Parameters.AddWithValue("PersonID", PersonID);

            try
            {

                connection.Open();

                object Result = sqlCommand.ExecuteScalar();

                if (Result!=null&&int.TryParse(Result.ToString(),out int ID))
                {

                   if(ID==PersonID)
                    { 
                        isFound = true;
                    }
                   
                }
               
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return false;

            }
            finally { connection.Close(); }

            return isFound;

        }

        public static DataTable GetUsersList()
        {

            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string query = @"SELECT  Users.UserID,  Users.PersonID, People.FirstName + ' ' + People.SecondName + ' ' + People.ThirdName + ' ' + People.LastName as FullName
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
                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }
            return dt;
        }



        public static int AddNewUser(int PersonID, string UserName, string Password, bool isActive)
        {

            int UserID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccess.clsDataAccessSettings.ConnectionString);


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
                Console.WriteLine(ex.Message);
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
            command.Parameters.AddWithValue("isActive",Convert.ToInt16( isActive ));
            
            try
            {
                connection.Open();

                RowsAffected = command.ExecuteNonQuery();



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }


            return RowsAffected > 0;


        }

        public static bool GetLastLoginInfo(ref int LoginID,ref int UserID,ref string UserName,ref string Password,ref bool isActive,ref bool isRememberMeChecked)
        {
            bool isLoged = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query2 = @"select top 1 LoginID from Login
                           order by LoginID desc";

            SqlCommand command2 = new SqlCommand(query2, connection);

            object LastLoginID = null;
            try
            {
                connection.Open();

                 LastLoginID = command2.ExecuteScalar();



           
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }


            string query = "select * from Login Where LoginID=@LastLoginID";

            SqlCommand command = new SqlCommand(query, connection);

            if (LastLoginID!=null)
            {
                command.Parameters.AddWithValue("LastLoginID", Convert.ToInt16(LastLoginID.ToString()));
            }


            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    LoginID = (int)reader["LoginID"];
                    UserID = (int)reader["UserID"];
                    UserName = (string)reader["UserName"];
                    Password = (string)reader["Password"];


                    if (!string.IsNullOrWhiteSpace(reader["isActive"].ToString()))
                    {
                        if (reader["isActive"].ToString()=="1")
                            isActive = true;

                    }
                    if (!string.IsNullOrWhiteSpace(reader["isRememberChecked"].ToString()))
                    {
                        if (reader["isRememberChecked"].ToString() == "1")
                            isRememberMeChecked = true;

                    }

                   

                    isLoged = true;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return isLoged;
        }

        public static bool SaveLoginData(int UserID,string UserName,string Password,bool isActive,bool isRememberMeChecked)
        {
            int RowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"insert into Login (UserID,UserName,Password,isActive,isRememberChecked) Values
                           (@UserID,@UserName,@Password,@isActive,@isRememberChecked)";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue ("UserID", UserID);
            command.Parameters.AddWithValue("UserName", UserName);
            command.Parameters.AddWithValue("Password", Password);
            command.Parameters.AddWithValue("isActive", Convert.ToInt16( isActive) );
            command.Parameters.AddWithValue("isRememberChecked", Convert.ToInt16(isRememberMeChecked));

            try
            {
                connection.Open();

                RowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return RowsAffected>0;


        }

        /// <summary>
        /// Application Types
        /// </summary>
        /// 


        public static float GetApplicationFees(float ApplicationTypeID)
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

               
                if (result!=null&&float.TryParse(result.ToString(),out float Fees))
                {

                    ApplicationTypeFees = Fees;


                }
              


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            finally { connection.Close(); }
            return ApplicationTypeFees;
        }

        public static DataTable GetApplicationTypesList()
        {

            DataTable dtApplicationTypes = new DataTable();

            SqlConnection connection = new SqlConnection( clsDataAccessSettings.ConnectionString);

            string query = "select * from ApplicationTypes";

            SqlCommand command=new SqlCommand(query, connection);

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
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            finally { connection.Close(); }
            return dtApplicationTypes;
        }

        public static bool UpdateApplicationTypeInfo(int ApplicationTypeID,string Title,double Fees)
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
                Console.WriteLine(ex.Message);
            }

            finally { connection.Close(); }
            return RowsAffected > 0;
        }


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
                Console.WriteLine(ex.Message);
            }

            finally { connection.Close(); }
            return dtTestTypes;
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
                Console.WriteLine(ex.Message);
            }

            finally { connection.Close(); }
            return RowsAffected > 0;
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

                object result= command.ExecuteScalar();

                if(result!=null && float.TryParse(result.ToString(),out float TestFees))
                {
                    Fees = TestFees;
                }


            }




            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            finally { connection.Close(); }
            return Fees;





        }



        /// <summary>
        /// Applications
        /// </summary>

        public static bool FindApplicationInfo(int ApplicationID,ref int ApplicantPersonID, ref DateTime ApplicationDate, ref int ApplicationTypeID, ref string ApplicationStatus,
            ref DateTime LastStatusDate, ref float  PiadFees, ref int CreatedByUserID )
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

                    if (reader["ApplicationStatus"] != null && int.TryParse(reader["ApplicationStatus"].ToString(),out int Status))
                    {
                        ApplicationStatus = Status == 1 ? "New" : Status == 2 ? "Canceled" : "Completed";
                    }

                  


                    LastStatusDate = (DateTime)reader["LastStatusDate"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    
                    if (reader["PaidFees"] != null && float.TryParse(reader["PaidFees"].ToString(), out float Fees))
                    {
                        PiadFees= Fees;
                    }
                        isFound = true;
                    }
                    reader.Close();




                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
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
                Console.WriteLine(ex.Message);
            }

            finally { connection.Close(); }
            return dtApplications;
        }

        public static int AddNewApplication( int ApplicantPersonID,  DateTime ApplicationDate,  int ApplicationTypeID,  string ApplicationStatus
                ,  DateTime LastStatusDate,  float PiadFees,int CreatedBuUserID)
            
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
                    , ApplicationStatus == "New" ? 1 : ApplicationStatus == "Canceled" ? 2 : 3);

            
            
           
            command.Parameters.AddWithValue("LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("PaidFees", PiadFees);
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
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return ApplicationID;
        }


        public static bool UpdateApplicationInfo(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID, string ApplicationStatus
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

            
            
                
            

            command.Parameters.AddWithValue("ApplicationStatus", ApplicationStatus == "New" ?1:
                ApplicationStatus == "Canceled" ?2:3);

            command.Parameters.AddWithValue("CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("LastStatusDate", LastStatusDate);
        


            try
                {
                    connection.Open();

                    RowsAffected = command.ExecuteNonQuery();



                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
                return RowsAffected > 0;
            }
        

        public static int HasOpenedApplication(int PersonID,int LicenseClassID,int ApplicationType)
        {
            int ApplicationIDWithStatusNew = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select Applications.ApplicationID from Applications inner join LocalDrivingLicenseApplications
  on Applications.ApplicationID=LocalDrivingLicenseApplications.ApplicationID
where ApplicationStatus=1  And ApplicantPersonID=@PersonID and ApplicationTypeID=@ApplicationType and LicenseClassID=@LicenseClassID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("PersonID", PersonID);
            command.Parameters.AddWithValue("ApplicationType", ApplicationType);
            command.Parameters.AddWithValue("LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int ApplicationID))
                { 
                    ApplicationIDWithStatusNew = ApplicationID;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return ApplicationIDWithStatusNew;



        }


        public static int HasCompletedApplication(int PersonID, int LicenseClassID, int ApplicationType)
        {
            int ApplicationIDWithStatusCompleted = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select Applications.ApplicationID from Applications inner join LocalDrivingLicenseApplications
  on Applications.ApplicationID=LocalDrivingLicenseApplications.ApplicationID
where ApplicationStatus=3  And ApplicantPersonID=@PersonID and ApplicationTypeID=@ApplicationType and LicenseClassID=@LicenseClassID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("PersonID", PersonID);
            command.Parameters.AddWithValue("ApplicationType", ApplicationType);
            command.Parameters.AddWithValue("LicenseClassID", LicenseClassID);

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
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return ApplicationIDWithStatusCompleted;



        }

        public static bool CancelApplication(int ApplicationID)
        {
            int RowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "update  Applications set ApplicationStatus=2 where  ApplicationID=@ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);


            command.Parameters.AddWithValue("ApplicationID", ApplicationID);

            try
            {
                connection.Open();


                RowsAffected = command.ExecuteNonQuery();



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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


                object result= command.ExecuteScalar();

                if (result != null) 
                {
                    ApplicationTypeTitle = result.ToString();
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            finally { connection.Close(); }
            return ApplicationTypeTitle;
        }



        /// 
        /// Local Driving License Applications
        ///


        
             public static bool FindLDLApplicationInfo(int LDLApplicationID, ref int ApplicationID, ref int LicenseClassID)
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

                Console.WriteLine(ex.Message);
                return false;

            }
            finally { connection.Close(); }

            return isFound;
        }
        public static int AddNewLDLApplication(int ApplicationID,int LicenseClassID)
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
                Console.WriteLine(ex.Message);
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
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return RowsAffected > 0;
        }



        public static DataTable GetLDLApplicationsList()
        {
            DataTable dtLDLApplications = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"

SELECT        LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID, LicenseClasses.ClassName, People.NationalNo, People.FirstName+' '+ People.SecondName+' '+ People.ThirdName+' '+ People.LastName as FullName, Applications.ApplicationDate, 
                         ISNULL(COUNT(CASE WHEN Tests.TestResult = 1 THEN 1 END), 0) AS PassedTests ,Applications.ApplicationStatus 
FROM            Applications INNER JOIN
                         LocalDrivingLicenseApplications ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID INNER JOIN
                         LicenseClasses ON LocalDrivingLicenseApplications.LicenseClassID = LicenseClasses.LicenseClassID INNER JOIN
                         People ON Applications.ApplicantPersonID = People.PersonID left JOIN
                         TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID left JOIN
                         Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
						group by LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID,LicenseClasses.ClassName,people.nationalno,People.FirstName,People.SecondName,People.ThirdName, People.LastName
						,Applications.ApplicationDate, 
                         Applications.ApplicationStatus


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
                Console.WriteLine(ex.Message);
            }

            finally { connection.Close(); }
            return dtLDLApplications;
        }

        public static int GetPassedTests(int LDLApplicationID)
        {
            int NumberOfPassedTests = 0;


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select count(TestResult) from
        (
                                 SELECT        Tests.TestResult
                                 FROM            
                                 LocalDrivingLicenseApplications INNER JOIN
                                 TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                                 Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID INNER JOIN
                                 TestTypes ON TestAppointments.TestTypeID = TestTypes.TestTypeID

        						 group by TestAppointments.TestTypeID,LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID, Tests.TestResult, TestTypes.TestTypeTitle
        						 having(LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID=@LDLApplicationID and Tests.TestResult=1 )

        						 )R1
        ";

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
                Console.WriteLine(ex.Message);
            }

            finally { connection.Close(); }
            return NumberOfPassedTests;




            //        }

            //public static bool DeleteLDLApplication(int LDLApplicationID)
            //{
            //    int RowsAffected = 0;

            //    SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);



            //    string query = "Delete from LocalDrivingLicenseApplications where  LocalDrivingLicenseApplicationID=@LDLApplicationID";

            //    SqlCommand command = new SqlCommand(query, connection);


            //    command.Parameters.AddWithValue("LDLApplicationID", LDLApplicationID);

            //    try
            //    {
            //        connection.Open();


            //        RowsAffected = command.ExecuteNonQuery();



            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(ex.Message);
            //    }

            //    finally { connection.Close(); }
            //    return RowsAffected>0;


            //}

        }

        /// <summary>
        /// Test Appointments
        /// </summary>


        public static DataTable GetAppointmentsList(int LDLApplicationID,int TestTypeID)
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
                Console.WriteLine(ex.Message);
            }

            finally { connection.Close(); }
            return dtAppointmentsList;
        }

        public static bool hasAppointmentNotLocked(int LDLApplicationID, int TestTypeID)
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
                Console.WriteLine(ex.Message);
            }

            finally { connection.Close();}
            return hasNotLockedAppointment==1;
        }


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


                if (result!=null&&int.TryParse(result.ToString(),out int isPassed))
                {

                   isTestPassed = isPassed;


                }
                

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            finally { connection.Close(); }
            return isTestPassed==1;
        }


        
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
                Console.WriteLine(ex.Message);
            }

            finally { connection.Close(); }
            return hasAppointment==1;
        }


        public static int GetTotalNumberOfTrials(int LDLApplicationID,int TestTypeID)
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
                Console.WriteLine(ex.Message);
            }

            finally { connection.Close(); }
            return TotalNumberOFTrials;





        }


        public static int AddNewTestAppointment(int TestTypeID, int LDLApplicationID, DateTime AppointmentDate, float PaidFees, int CreatedByUserID, bool isLocked)

        {
            int TestAppointmentID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string query = @"Insert into TestAppointments (TestTypeID,LocalDrivingLicenseApplicationID,AppointmentDate,PaidFees,CreatedByUserID,isLocked)
                           Values(@TestTypeID,@LocalDrivingLicenseApplicationID,@AppointmentDate,@PaidFees,@CreatedByUserID,@isLocked);
                            select Scope_Identity();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("LocalDrivingLicenseApplicationID", LDLApplicationID);
            command.Parameters.AddWithValue("AppointmentDate", AppointmentDate);

            command.Parameters.AddWithValue("PaidFees", PaidFees);
            command.Parameters.AddWithValue("CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("IsLocked"
                    , isLocked ? 1 : 0);









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
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return TestAppointmentID;
        }

        public static bool FindAppointment(int TestAppointmentID,ref int TestTypeID,ref int LocalDrivingLicenseApplicationID,ref DateTime AppointmentDate,ref float PaidFees
            ,ref int CreatedByUserID,ref bool isLocked)
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
                  

                   


                    isFound = true;
                }
                reader.Close();




            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return false;

            }
            finally { connection.Close(); }

            return isFound;

        }



        public static bool UpdateAppointment(int TestAppointmentID, int TestTypeID, int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, float PaidFees
            , int CreatedByUserID, bool isLocked)
        {
            int RowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update TestAppointments 
                           set TestTypeID=@TestTypeID, 
                           LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID,
                           AppointmentDate=@AppointmentDate,
                           PaidFees=@PaidFees,
                           CreatedByUserID=@CreatedByUserID,
                           IsLocked=@isLocked
                         
                           where TestAppointmentID=@TestAppointmentID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("PaidFees", PaidFees);
            command.Parameters.AddWithValue("CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("isLocked", isLocked?1:0);

         
            try
            {
                connection.Open();

                RowsAffected = command.ExecuteNonQuery();



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return RowsAffected > 0;
        }


        public static int AddNewTest(int TestAppointmentID,bool TestResult,string Notes,int CreatedByUserID)
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
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return TestID;
        }

        /// <summary>
        /// Licenses
        /// </summary>


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
                Console.WriteLine(ex.Message);
            }

            finally { connection.Close(); }
            return isDetained ;
        }


        public static bool FindDriverLicense(int LicenseID, ref  int ApplicationID, ref int DriverID, ref int  LicenseClass, ref DateTime IssueDate, ref DateTime ExpirationDate
                , ref string Notes, ref float PaidFees, ref bool isActive, ref int IssueReasone, ref int CreatedByUserID)
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
                        isActive = Status  ;
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

                Console.WriteLine(ex.Message);
                return false;

            }
            finally { connection.Close(); }

            return isFound;
        }

        public static bool FindDriverLicenseByApplicationID(ref int LicenseID,  int ApplicationID, ref int DriverID, ref int LicenseClass, ref DateTime IssueDate, ref DateTime ExpirationDate
              , ref string Notes, ref float PaidFees, ref bool isActive, ref int IssueReasone, ref int CreatedByUserID)
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

                    if (reader["Notes"]==System.DBNull.Value)
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

                Console.WriteLine(ex.Message);
                return false;

            }
            finally { connection.Close(); }

            return isFound;
        }

        public static int AddNewLicense(int ApplicationID, int DriverID, int LicenseClass, DateTime IssueDate, DateTime ExpirationDate, string Notes, float PaidFees
                ,bool isActive, int IssueReasone, int CreatedByUserID)
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
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return LicenseID;
        }

        public static bool UpdateDrivingLicenseInfo(int LicenseID, int ApplicationID, int DriverID, int LicenseClass, DateTime IssueDate, DateTime ExpirationDate, string Notes, float PaidFees
                , bool isActive, int IssueReason, int CreatedByUserID)
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
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return RowsAffected > 0;
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
                Console.WriteLine(ex.Message);
            }

            finally { connection.Close(); }
            return ValidityLength;


        }
        

              public static float GetLicenseClassFees(int LicenseClassID)
        {




            float LicenseClassFees = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string query = @"select ClassFees from LicenseClasses 
                             where LicenseClassID=@LicenseClassID ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("LicenseClassID", LicenseClassID);


            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && float.TryParse(result.ToString(), out float ClassFees))
                {
                    LicenseClassFees = ClassFees;
                }


            }




            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            finally { connection.Close(); }
            return LicenseClassFees;


        }


        public static DataTable GetLocalDrivingLicensesHistory(int DriverID)
        {
            
                DataTable dtLicensesHistory = new DataTable();

                SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

                string query = @"select LicenseID,ApplicationID,LicenseClass,IssueDate,ExpirationDate,IsActive  from Licenses
                            where DriverID=@DriverID";

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
                    Console.WriteLine(ex.Message);
                }

                finally { connection.Close(); }
                return dtLicensesHistory;
            
        }


        public static DataTable GetInternationalDrivingLicensesHistory(int DriverID)
        {
         
            DataTable dtLicensesHistory = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select InternationalLicenseID,IssuedUsingLocalLicenseID,ApplicationID,IssueDate,ExpirationDate,IsActive  from InternationalLicenses
                            where DriverID=@DriverID";

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
                Console.WriteLine(ex.Message);
            }

            finally { connection.Close(); }
            return dtLicensesHistory;

        }
        /// <summary>
        /// Drivers
        /// </summary>


        public static int AddNewDriver(int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {


            int DriverID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string query = @"Insert into Drivers (PersonID,CreatedByUserID,CreatedDate)
                           Values(@PersonID,@CreatedByUserID,@CreatedDate);
                            select Scope_Identity();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("PersonID", PersonID);
            command.Parameters.AddWithValue("CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("CreatedDate", CreatedDate);
           









            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    DriverID = insertedID;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return DriverID;




        }
        /// <summary>
        /// InternationalLicenses
        /// </summary>


        public static DataTable GetInternationalLicensesList()
        {
            
            DataTable dtInternationalLicenses = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select InternationalLicenseID,ApplicationID,DriverID,IssuedUsingLocalLicenseID,IssueDate,ExpirationDate,IsActive  from InternationalLicenses";

            SqlCommand command = new SqlCommand(query, connection);



            try
            {
                connection.Open();


                SqlDataReader reader = command.ExecuteReader();


                if (reader.HasRows)
                {

                    dtInternationalLicenses.Load(reader);


                }
                reader.Close();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            finally { connection.Close(); }
            return dtInternationalLicenses;

        }

        public static int AddNewInternationalLicense(int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID,  DateTime IssueDate, DateTime ExpirationDate
               , bool isActive, int CreatedByUserID)
        {
            int InternationalLicenseID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string query = @"Insert into InternationalLicenses (ApplicationID,IssuedUsingLocalLicenseID,DriverID,IssueDate,ExpirationDate,IsActive,CreatedByUserID)
                           Values(@ApplicationID,@IssuedUsingLocalLicenseID,@DriverID,@IssueDate,@ExpirationDate,@isActive,@CreatedByUserID);
                            select Scope_Identity();";

            SqlCommand command = new SqlCommand(query, connection);


            command.Parameters.AddWithValue("ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
            command.Parameters.AddWithValue("DriverID", DriverID);
            command.Parameters.AddWithValue("IssueDate", IssueDate);
            command.Parameters.AddWithValue("ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("isActive"
                    , isActive ? 1 : 0);









            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    InternationalLicenseID = insertedID;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return InternationalLicenseID;
        }


        public static bool FindInternationalLicenseInfo( int InternationalLicenseID,ref int ApplicationID,  ref int DriverID, ref int IssuedUsingLocalLicenseID, ref DateTime IssueDate, ref DateTime ExpirationDate
             ,   ref bool isActive,ref int CreatedByUserID)
        {



            bool isFound = false;


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from InternationalLicenses Where InternationalLicenseID=@InternationalLicenseID";

            SqlCommand sqlCommand = new SqlCommand(query, connection);

            sqlCommand.Parameters.AddWithValue("InternationalLicenseID", InternationalLicenseID);

            try
            {

                connection.Open();

                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {
                    ApplicationID = (int)reader["ApplicationID"];
                    DriverID = (int)reader["DriverID"];
                    IssuedUsingLocalLicenseID = (int)reader["IssuedUsingLocalLicenseID"];

                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    IssueDate = (DateTime)reader["IssueDate"];

                    if (reader["IsActive"] != null && bool.TryParse(reader["IsActive"].ToString(), out bool Status))
                    {
                        isActive = Status;
                    }

                    CreatedByUserID = (int)reader["CreatedByUserID"];

                   
                    isFound = true;
                }
                reader.Close();




            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return false;

            }
            finally { connection.Close(); }

            return isFound;
        }

        public static int isDriverHasAnActiveInternationalLicense(int DriverID)
        {
            int InternationalLicenseID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select InternationalLicenseID from InternationalLicenses where DriverID=@DriverID and IsActive=1";

            SqlCommand command = new SqlCommand(query,connection);

            command.Parameters.AddWithValue("DriverID", DriverID);

            try
            {

                connection.Open();

               object result= command.ExecuteScalar();

                if (result!=null && int.TryParse(result.ToString(), out int IntLicID))
                {
                    InternationalLicenseID = IntLicID;
                }



            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                

            }
            finally { connection.Close(); }

            return InternationalLicenseID;


        }

        /// <summary>
        /// Detained Licenses
        /// </summary>
         //DetainID
        //LicenseID
        //DetainDate
        //FineFees
        //CreatedByUserID
        //IsReleased
        //ReleaseDate
        //ReleasedByUserID
        //ReleaseApplicationID
        public static int AddNewDetainedLicense(int LicenseID,  DateTime DetainDate, float FineFees, int CreatedByUserID, bool IsReleased, DateTime ReleaseDate 
             , int ReleasedByUserID, int ReleaseApplicationID)
        {
            int DetainID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string query = @"Insert into DetainedLicenses (LicenseID,DetainDate,ReleaseDate,FineFees,IsReleased,ReleaseApplicationID,CreatedByUserID,ReleasedByUserID)
                           Values(@LicenseID,@DetainDate,@ReleaseDate,@FineFees,@IsReleased,@ReleaseApplicationID,@CreatedByUserID,@ReleasedByUserID);
                            select Scope_Identity();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("LicenseID", LicenseID);
            command.Parameters.AddWithValue("ReleasedByUserID", System.DBNull.Value);
            command.Parameters.AddWithValue("DetainDate", DetainDate);
            command.Parameters.AddWithValue("ReleaseDate", System.DBNull.Value);
            command.Parameters.AddWithValue("FineFees", FineFees);
            command.Parameters.AddWithValue("ReleaseApplicationID", System.DBNull.Value);
            command.Parameters.AddWithValue("CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("IsReleased" , 0);


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
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return DetainID;
        }

        public static bool UpdateDetainedLicenseInfo(int DetainID, int LicenseID, DateTime DetainDate, float FineFees, int CreatedByUserID, bool IsReleased, DateTime ReleaseDate
             , int ReleasedByUserID, int ReleaseApplicationID)
        {
            int RowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update DetainedLicenses 
                           set 
                           LicenseID=@LicenseID,
                           DetainDate=@DetainDate,
                           ReleaseDate=@ReleaseDate,
                           FineFees=@FineFees,
                           IsReleased=@IsReleased,
                           ReleaseApplicationID=@ReleaseApplicationID,
                           CreatedByUserID=@CreatedByUserID,
                           ReleasedByUserID=@ReleasedByUserID
                         
                           where DetainID=@DetainID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("DetainID", DetainID);
            command.Parameters.AddWithValue("LicenseID", LicenseID);
            command.Parameters.AddWithValue("DetainDate", DetainDate);
            command.Parameters.AddWithValue("ReleaseDate", ReleaseDate);
            command.Parameters.AddWithValue("FineFees", FineFees);
            command.Parameters.AddWithValue("IsReleased", IsReleased);
            command.Parameters.AddWithValue("ReleaseApplicationID", ReleaseApplicationID);
            command.Parameters.AddWithValue("ReleasedByUserID", ReleasedByUserID);
          
            command.Parameters.AddWithValue("CreatedByUserID", CreatedByUserID);

           
            try
            {
                connection.Open();

                RowsAffected = command.ExecuteNonQuery();



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return RowsAffected > 0;
        }



        public static bool FindDetainedlLicenseInfo(int DetainID,ref int LicenseID,ref DateTime DetainDate,ref float FineFees,ref int CreatedByUserID,ref bool IsReleased,ref DateTime ReleaseDate
             ,ref int ReleasedByUserID,ref int ReleaseApplicationID)
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

                    if (reader["ReleaseDate"]==System.DBNull.Value)
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

                Console.WriteLine(ex.Message);
                return false;

            }
            finally { connection.Close(); }

            return isFound;
        }


        public static bool FindDetainedlLicenseInfo(ref int DetainID,  int LicenseID, ref DateTime DetainDate, ref float FineFees, ref int CreatedByUserID, ref bool IsReleased, ref DateTime ReleaseDate
           , ref int ReleasedByUserID, ref int ReleaseApplicationID)
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

                Console.WriteLine(ex.Message);
                return false;

            }
            finally { connection.Close(); }

            return isFound;
        }



        public static DataTable GetDetainedLicensesList()
        {

            DataTable dtDetainedLicenses = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT        DetainedLicenses.DetainID, DetainedLicenses.LicenseID, DetainedLicenses.DetainDate, DetainedLicenses.IsReleased, DetainedLicenses.FineFees, DetainedLicenses.ReleaseDate, People.NationalNo,
                       
                          ( People.FirstName+' '+ People.SecondName+' '+ People.ThirdName+' '+ People.LastName)as FullName, DetainedLicenses.ReleaseApplicationID
FROM            DetainedLicenses INNER JOIN
                         Licenses ON DetainedLicenses.LicenseID = Licenses.LicenseID INNER JOIN
                         Drivers ON Licenses.DriverID = Drivers.DriverID INNER JOIN
                         People ON Drivers.PersonID = People.PersonID";

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
                Console.WriteLine(ex.Message);
            }

            finally { connection.Close(); }
            return dtDetainedLicenses;

        }

        static class clsDataAccessSettings
        {
            public static string ConnectionString = "Server=.;Database=DVLD;User Id=sa;Password=sa123456;";
        }


        /*
         
         SELECT      TestAppointments.TestTypeID,  Tests.TestResult
FROM             
                         LocalDrivingLicenseApplications  INNER JOIN
                         TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                         Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID INNER JOIN
                         TestTypes ON TestAppointments.TestTypeID = TestTypes.TestTypeID

						 group by TestAppointments.TestTypeID,LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID, Tests.TestResult, TestTypes.TestTypeTitle
						 having(LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID=30 and Tests.TestResult=1 )
         
         */






    }
}
