using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DVLD_DataAccess_Layar;
namespace DVLD_Business_Layar
{
    public class clsPerson
    {

        public int ID { get; set; }

      
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public char Gendor {  get; set; }

        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }


        public int CountryID { get; set; }

        public string ImagePath { get; set; }

        public string FullName
        {
            get
            {

                return FirstName + " " + SecondName + " " + ThirdName + " " + LastName;
            }
        }
        enum enMode
        {
            AddNew = 1,
            Update = 2
        }

        private enMode _Mode = enMode.Update;
        public clsPerson()
        {
            ID = -1;
            NationalNo = string.Empty;
            FirstName = string.Empty;
            SecondName = string.Empty;
            ThirdName = string.Empty;
            LastName = string.Empty;
            DateOfBirth = DateTime.Now;
            Gendor = 'M';
            Address = string.Empty;
            Phone = string.Empty;
            Email = string.Empty;
            CountryID = 90;

            ImagePath = string.Empty;



            _Mode = enMode.AddNew;

        }

        private clsPerson(int ID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, char Gendor, string Address
            , string Phone, string Email, int CountryID, string ImagePath)
        {
            this.ID = ID;
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Gendor = Gendor;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.CountryID = CountryID;
            this.ImagePath = ImagePath;

            _Mode = enMode.Update;

        }

        public static clsPerson FindPerson(int PersonID)
        {

            string NationalNo = "";
            string FirstName = "";
            string SecondName = "";
            string ThirdName = "";
            string LastName = "";
            DateTime DateOfBirth = DateTime.Now;
            char Gendor = 'M';
            string Address = "";
            string Phone = "";
            string Email = "";
            int CountryID = 90;
            string ImagePath = "";

            if (clsDataAccess.FindPerson(PersonID, ref NationalNo, ref FirstName, ref SecondName, ref ThirdName, ref LastName, ref DateOfBirth, ref Gendor, ref Address
                , ref Phone, ref Email, ref CountryID, ref ImagePath))
            {
                return new clsPerson(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gendor, Address, Phone, Email, CountryID, ImagePath);
            }
            else { return null; }
        }



        public static clsPerson FindPerson(string NationalNo)
        {
            

            int PersonID = -1;
            string FirstName = "";
            string SecondName = "";
            string ThirdName = "";
            string LastName = "";
            DateTime DateOfBirth = DateTime.Now;
            char Gendor = 'M';
            string Address = "";
            string Phone = "";
            string Email = "";
            int CountryID = 90;
            string ImagePath = "";

            if (clsDataAccess.FindPerson(ref PersonID,  NationalNo, ref FirstName, ref SecondName, ref ThirdName, ref LastName, ref DateOfBirth, ref Gendor, ref Address
                , ref Phone, ref Email, ref CountryID, ref ImagePath))
            {
                return new clsPerson(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gendor, Address, Phone, Email, CountryID, ImagePath);
            }
            else { return null; }
        }

        public static DataTable PeopleList()
        {

            return clsDataAccess.GetPeopleList();
        }

        public static DataTable CountriesList()
        {
            return clsDataAccess.GetCountriesList();
        }

        public static string GetCountryNameByCountryID(int CountryID)
        {
            return clsDataAccess.GetCountryName(CountryID);
        }

        public static int GetCountryID(string CountryName) 
        {
            return clsDataAccess.GetCountryID(CountryName);
        }
        bool _AddNew()
        {
            this.ID = clsDataAccess.AddNewPerson(this.NationalNo,this.FirstName, this.SecondName, this.ThirdName, this.LastName, this.DateOfBirth, this.Gendor, this.Address, this.Phone, this.Email
                , this.CountryID, this.ImagePath);

            return this.ID != -1;
            

        }
        bool _Update()
        {
            return clsDataAccess.UpdatePerson(this.ID,this.NationalNo, this.FirstName, this.SecondName, this.ThirdName, this.LastName, this.DateOfBirth, this.Gendor, this.Address, this.Phone, this.Email
                , this.CountryID, this.ImagePath);
        }
        public bool Save()
        {
            switch(_Mode)
            {
                case enMode.AddNew:
                    if (_AddNew())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _Update();

                   
            }
            return false;
        }
        public static DataTable NationalNoList()
        {
            return clsDataAccess.GetAllNationalNumbers();
        }
        public static bool DeletePerson(int PersonID)
        {
           return clsDataAccess.DeletePerson(PersonID);
           
        }

        public static int GetDriverID(int PersonID)
        {
            return clsDataAccess.GetDriverID(PersonID);
        }



    }
    public class clsUsers
    {

        public int UserID;
        public int PersonID;
        public string UserName;
        public string Password;
        public bool isActive;

        enum enMode
        {
            AddNew = 1,
            Update = 2
        }

        private enMode _Mode = enMode.Update;
        public clsUsers()
        {
            UserID = -1;
            PersonID = -1;
            UserName = "";
            Password = "";
            isActive = false;

            _Mode = enMode.AddNew;

        }

        private clsUsers(int UserID, int PersonID, string UserName, string Password, bool isActive)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.UserName = UserName;
            this.Password = Password;
            this.isActive = isActive;


            _Mode = enMode.Update;

        }

        public static clsUsers FindUserByUserID(int UserID)
        {

            int PersonID = -1;
            string UserName = "";
            string Password = "";
            bool isActive = false;

            if (clsDataAccess.FindUserByUserID(UserID, ref PersonID, ref UserName, ref Password, ref isActive))
            {
                return new clsUsers(UserID, PersonID, UserName, Password, isActive);
            }
            else { return null; }
        }

        public static clsUsers FindUserByUserName(string UserName)
        {
            int UserID = -1;
            int PersonID = -1;
          
            string Password = "";
            bool isActive = false;

            if (clsDataAccess.FindUserByUserName(ref UserID, ref PersonID,  UserName, ref Password, ref isActive))
            {
                return new clsUsers(UserID, PersonID, UserName, Password, isActive);
            }
            else { return null; }
        }


        public static bool isPersonAnUser(int PersonID)
        {


            return clsDataAccess.isPersonAnUser(PersonID);


        }

        public static DataTable UsersList()
        {

            return clsDataAccess.GetUsersList();
        }



        bool _AddNew()
        {
            this.UserID = clsDataAccess.AddNewUser(this.PersonID, this.UserName, this.Password, this.isActive);

            return this.UserID != -1;


        }
        bool _Update()
        {
            return clsDataAccess.UpdateUser(this.UserID, this.PersonID, this.UserName, this.Password, this.isActive);
        }
        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNew())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _Update();


            }
            return false;
        }

        public static bool DeleteUser(int UserID)
        {
            if (clsDataAccess.DeleteUser(UserID))
            {
                return true;
            }
            return false;

        }
        

    }

    public class clsLogin
    {
        public int LoginID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool isActive { get; set; }  
       
        public bool isRememberMeChecked {  get; set; }

        public clsLogin()
        {
            LoginID = -1;
            UserID = -1;
            UserName = "";
            Password = "";
            isActive = false;
            isRememberMeChecked = false;
        }
        clsLogin(int LoginID, int UserID, string UserName, string Password, bool isActive, bool isRememberMeChecked)
        {
            this.LoginID = LoginID;
            this.UserID = UserID;
            this.UserName = UserName;
            this.Password = Password;
            this.isRememberMeChecked = isRememberMeChecked;
            this.isActive = isActive;
        }
        public static clsLogin GetLastLoginInfo() 
        {
            int LoginID = -1;
            int UserID = -1;
            string UserName= "";
            string Password = "";
            bool isActive = false;
            bool isRememberMeChecked = false;

           

            if (clsDataAccess.GetLastLoginInfo(ref LoginID,ref UserID,ref UserName,ref Password,ref isActive,ref isRememberMeChecked))
            {
                return new clsLogin(LoginID, UserID, UserName, Password, isActive, isRememberMeChecked);

            }
            else { return null; }


        }

        public bool SaveLoginProcess()
        {
            return clsDataAccess.SaveLoginData(this.UserID,this.UserName,this.Password,this.isActive,this.isRememberMeChecked);
        }

    }

    public class clsApplicationTypes
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public double Fees { get; set; }


        public static DataTable ApplicationTypesList()
        {
            return clsDataAccess.GetApplicationTypesList();
        }

        public static float ApplicationFees(float ApplicationTypeID)
        {
            return clsDataAccess.GetApplicationFees(ApplicationTypeID);
        }

        bool _Update()
        {
            return clsDataAccess.UpdateApplicationTypeInfo(this.ID,this.Title,this.Fees);
        }
        public bool Save()
        {
            return _Update();
        }

    }
    public class clsTestTypes
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public double Fees { get; set; }
        public string Description { get; set; }

        public static string GetTestTypeTitle(int TestTypeID)
        {
           
                switch (TestTypeID)
                {
                    case 1:
                        return "Vision Test";
                    case 2:
                        return "Written Test";
                    case 3:
                        return "Practical Test";
                    default:
                        return "";
                }
            
        }

        public static DataTable TestTypesList()
        {
            return clsDataAccess.GetTestTypesList();
        }

        bool _Update()
        {
            return clsDataAccess.UpdateTestTypeInfo(this.ID, this.Title, this.Description,this.Fees);
        }
        public bool Save()
        {
            return _Update();
        }


        public static float TestFees(int TestTypeID)
        {
            return clsDataAccess.GetTestFees(TestTypeID);
        }

    }
    public class clsApplications
    {
        public int ApplicationID { get; set; }
        public int ApplicantPersonID { get; set; }

        public DateTime ApplicationDate { get; set; }
        public int ApplicationTypeID { get; set; }
        public string ApplicationStatus { get; set; }
        public DateTime LastStatusDate { get; set; }

        public float PiadFees { get; set; }

        public int CreatedByUserID { get; set; }

        public float ApplicationFees {
            get
            {
                return clsDataAccess.GetApplicationFees(this.ApplicationTypeID);
            }
        }

        enum enMode
        {
            AddNew = 1,
            Update = 2
        }

        private enMode _Mode = enMode.Update;

        public clsApplications()
        {
            ApplicationID = -1;
            ApplicantPersonID = 0;
            ApplicationDate = DateTime.Now;
            ApplicationTypeID = 0;
            ApplicationStatus = string.Empty;
            LastStatusDate = DateTime.Now;
            PiadFees = 0;
            CreatedByUserID = 0;
            _Mode = enMode.AddNew;  
        }

        clsApplications(int ApplicationID,  int ApplicantPersonID,  DateTime ApplicationDate,  int ApplicationTypeID,  string ApplicationStatus,
             DateTime LastStatusDate,  float PiadFees,  int CreatedByUserID)
        {
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PiadFees = PiadFees;
            this.CreatedByUserID = CreatedByUserID;
            _Mode= enMode.Update;
        }

        public static clsApplications Find(int ApplicationID)
        {
           
            int ApplicantPersonID = -1;

            DateTime ApplicationDate = DateTime.Now;
            int ApplicationTypeID = -1;
            string ApplicationStatus = "";
            DateTime LastStatusDate = DateTime.Now;
          
            float PiadFees = -1;

            int CreatedByUserID = -1;


            if (clsDataAccess.FindApplicationInfo( ApplicationID, ref  ApplicantPersonID, ref  ApplicationDate, ref  ApplicationTypeID, ref  ApplicationStatus,
            ref  LastStatusDate, ref   PiadFees, ref  CreatedByUserID))
            {
                return new clsApplications(ApplicationID, ApplicantPersonID,  ApplicationDate,  ApplicationTypeID,  ApplicationStatus, LastStatusDate, PiadFees, CreatedByUserID);
            }
            return null;


        }
    public static DataTable ApplicationsList()
        {
            return clsDataAccess.GetApplicationsList();
        }

        bool _AddNew()
        {



            this.ApplicationID = clsDataAccess.AddNewApplication(this.ApplicantPersonID, this.ApplicationDate, this.ApplicationTypeID, this.ApplicationStatus
                , this.LastStatusDate, this.PiadFees,DVLDSettings.CurrentUser.UserID);

            return ApplicationID != -1;
        }

        bool _Update()
        {
            return clsDataAccess.UpdateApplicationInfo(this.ApplicationID, this.ApplicantPersonID, this.ApplicationDate, this.ApplicationTypeID, this.ApplicationStatus,
                this.LastStatusDate, this.PiadFees,this.CreatedByUserID);
        }

        public bool Save() 
        {

            switch (_Mode)
            {
                case enMode.AddNew:
                    _Mode = enMode.Update;
                    return _AddNew();
                    case enMode.Update:
                    return _Update();
            }
            return false;

            
        }

        public static int GetApplicationIDIFPersonHasOpenedApplication(int PersonID,int LicenseClassID,int ApplicationType)
        {
            return clsDataAccess.HasOpenedApplication(PersonID,LicenseClassID,ApplicationType);
        }

        public static int GetApplicationIDIFPersonHasCompletedApplication(int PersonID, int LicenseClassID, int ApplicationType)
        {
            return clsDataAccess.HasCompletedApplication(PersonID, LicenseClassID, ApplicationType);
        }

        public static bool CancelApplication(int ApplicationID)
        {
            return clsDataAccess.CancelApplication(ApplicationID);
        }

        public static string ApplicationTypeTitle(int ApplicationTypeID)
        {
            return clsDataAccess.GetApplicationTypeTitle(ApplicationTypeID);
        }
    }

    public class clsLDLApplications
    {
        public int LDLApplicationID { get; set; }

        public int ApplicationID { get; set; }

        public int LicenseClassID { get; set; }

        public string LicenseClassName
        {
            get
            {
                switch (LicenseClassID)
                {
                    case 1:
                        return "Class 1 - Small Motorcycle";
                        case 2:
                        return "Class 2 - Heavy Motorcycle License";
                    case 3:
                        return "Class 3 - Ordinary driving license";
                    case 4:
                        return "Class 4 - Commercial";
                    case 5:
                        return "Class 5 - Agricultural";
                    case 6:
                        return "Class 6 - Small and medium bus";
                    case 7:
                        return "Class 7 - Truck and heavy vehicle";
                    default:
                        return "";

                }
            }
            
        }
        enum enMode
        {
            AddNew = 1,
            Update = 2
        }

        private enMode _Mode = enMode.Update;
        //public int PassedTests
        //{
        //    get
        //    {
        //        // return clsLDLApplications
        //    }
        //}


        public clsLDLApplications()
        {
            this.LDLApplicationID = -1;
            this.ApplicationID = -1;
            this.LicenseClassID = -1;
            _Mode=enMode.AddNew;
        }

        clsLDLApplications(int lDLApplicationID, int applicationID, int licenseClassID)
        {
            LDLApplicationID = lDLApplicationID;
            ApplicationID = applicationID;
            LicenseClassID = licenseClassID;
            _Mode = enMode.Update;
        }

        public static clsLDLApplications Find(int LDLApplicationID)
        {

         

            int ApplicationID = -1;
           
            int LicenseClassID = -1;


            if (clsDataAccess.FindLDLApplicationInfo(LDLApplicationID,ref ApplicationID, ref LicenseClassID))
            {
                return new clsLDLApplications(LDLApplicationID, ApplicationID, LicenseClassID);
            }
            return null;


        }

        public static DataTable LDLApplicationsList()
        {
            return clsDataAccess.GetLDLApplicationsList();
        }

        //public static int LDLApplicationsPassedTestCount(int LDLApplicationID)
        //{
        //    return clsDataAccess.GetPassedTests(LDLApplicationID);
        //}

        bool _AddNew()
        {



            this.LDLApplicationID = clsDataAccess.AddNewLDLApplication(this.ApplicationID, this.LicenseClassID);

            return LDLApplicationID != -1;
        }

        bool _Update()
        {
            
            return clsDataAccess.UpdateLDLApplication(this.LDLApplicationID, this.ApplicationID, this.LicenseClassID);
        }
        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNew())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _Update();


            }
            return false;
        }

        //public static bool DeleteLDLApplication(int LDLApplicationID)
        //{
        //    return clsDataAccess.DeleteLDLApplication(LDLApplicationID);
        //}

        public int PassedTests()
        {
            return clsDataAccess.GetPassedTests(this.LDLApplicationID);
        }

       


    }

    public class clsTestAppointments
    {
        public int TestAppointmentID { get; set; }

        public int TestTypeID { get; set; }

        public int LocalDrivingLicenseApplicationID { get; set; }

        public DateTime AppointmentDate { get; set; }

        public float PaidFees { get; set; }

        public int CreatedByUserID { get; set; }

        public bool isLocked { get; set; }

        enum enMode
        {
            AddNew = 1,
            Update = 2
        }

        private enMode _Mode = enMode.Update;






        public clsTestAppointments()
        {
            TestAppointmentID = -1;
            TestTypeID = -1;
            LocalDrivingLicenseApplicationID = -1;
            AppointmentDate = DateTime.Now;
            PaidFees = 0;
            CreatedByUserID = -1;
            isLocked = false;
         



            _Mode = enMode.AddNew;

        }

        private clsTestAppointments(int TestAppointmentID, int TestTypeID, int LocalDrivingLicenseApplicationID,  DateTime AppointmentDate , float PaidFees
            , int CreatedByUserID, bool isLocked)
        {
           this.TestAppointmentID = TestAppointmentID;
            this.isLocked = isLocked;
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.TestTypeID = TestTypeID;
            this.LocalDrivingLicenseApplicationID= LocalDrivingLicenseApplicationID;
            this.CreatedByUserID = CreatedByUserID;

            _Mode = enMode.Update;

        }

        public static clsTestAppointments FindAppointment(int AppointmentID)
        {

            int TestTypeID = -1;
            int LocalDrivingLicenseApplicationID = -1;
           
            DateTime AppointmentDate = DateTime.Now;
            float PaidFees = 0;
            int CreatedByUserID = -1;
            bool isLocked = false;
          

            if (clsDataAccess.FindAppointment(AppointmentID, ref TestTypeID, ref LocalDrivingLicenseApplicationID, ref AppointmentDate, ref PaidFees, ref CreatedByUserID
                , ref isLocked))
            {
                return new clsTestAppointments(AppointmentID, TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, isLocked);
            }
            else { return null; }
        }


        public static DataTable AppointmentsList(int LDLApplicationID,int TestType)
        {
            return clsDataAccess.GetAppointmentsList(LDLApplicationID, TestType);
        }

        public static bool hasAppointment(int LDLApplicationID, int TestType)
        {
            return clsDataAccess.hasAppointment(LDLApplicationID, TestType);
        }

        public static bool hasAppointmentNotLocked(int LDLApplicationID, int TestType)
        {
            return clsDataAccess.hasAppointmentNotLocked(LDLApplicationID, TestType);
        }

        public static bool isTestPassed(int LDLApplicationID, int TestType)
        {
            return clsDataAccess.isTestPassed(LDLApplicationID, TestType);
        }

        public static int TotalNumberOfTrials(int LDLApplicationID,int TestTypeID)
        {
            return clsDataAccess.GetTotalNumberOfTrials(LDLApplicationID, TestTypeID);
        }













        bool _Update()
        {
            return clsDataAccess.UpdateAppointment(this.TestAppointmentID, this.TestTypeID, this.LocalDrivingLicenseApplicationID, this.AppointmentDate, this.PaidFees,
                this.CreatedByUserID, this.isLocked);
        }
        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNew())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _Update();


            }
            return false;
        }








        bool _AddNew()
        {



            this.TestAppointmentID = clsDataAccess.AddNewTestAppointment(this.TestTypeID, this.LocalDrivingLicenseApplicationID,this.AppointmentDate
                ,this.PaidFees,this.CreatedByUserID,this.isLocked);

            return TestAppointmentID != -1;
        }

      


    }

    public class clsTests
    {
        public int TestID {  get; set; }
        public int TestAppointmentID { get; set; }

        public bool TestResult { get; set; }

        public string Notes{ get; set; }

        public int CreatedByUserID { get; set; }


       public clsTests()
        {
            this.TestResult = false;
            this.TestID = -1;
            this.Notes = "";
            this.CreatedByUserID = -1;
            this.TestAppointmentID=-1;
        }

        bool _AddNewTest()
        {
            this.TestID = clsDataAccess.AddNewTest(this.TestAppointmentID,this.TestResult,this.Notes,this.CreatedByUserID);
            return TestID != -1;
        }

        public bool Save()
        {
            return _AddNewTest();
        }


    }

    public class clsDrivingLicense
    {
        public int LicenseID { get; set; }
        public int ApplicationID { get; set; }

        public int DriverID { get; set; }

        public int LicenseClass { get; set; }

        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public string Notes {  get; set; }
        public float PaidFees {  get; set; }

        public bool isActive { get; set; }

        public int IssueReasone {  get; set; }
        public int CreatedByUserID { get; set; }


        public string LicenseClassName
        {
            get
            {
                switch (LicenseClass)
                {
                    case 1:
                        return "Class 1 - Small Motorcycle";
                    case 2:
                        return "Class 2 - Heavy Motorcycle License";
                    case 3:
                        return "Class 3 - Ordinary driving license";
                    case 4:
                        return "Class 4 - Commercial";
                    case 5:
                        return "Class 5 - Agricultural";
                    case 6:
                        return "Class 6 - Small and medium bus";
                    case 7:
                        return "Class 7 - Truck and heavy vehicle";
                    default:
                        return "";

                }
            }

        }

        public static string GetLicenseClassName(int LicenseClassID)
        {
            switch (LicenseClassID)
            {
                case 1:
                    return "Class 1 - Small Motorcycle";
                case 2:
                    return "Class 2 - Heavy Motorcycle License";
                case 3:
                    return "Class 3 - Ordinary driving license";
                case 4:
                    return "Class 4 - Commercial";
                case 5:
                    return "Class 5 - Agricultural";
                case 6:
                    return "Class 6 - Small and medium bus";
                case 7:
                    return "Class 7 - Truck and heavy vehicle";
                default:
                    return "";

            }
        }



        enum enMode
        {
            AddNew=1,
            Update=2
        }
        enMode _Mode = enMode.AddNew;
        public clsDrivingLicense()
        {
           
            this.LicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.LicenseClass = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.Notes = "";
            this.PaidFees = 0;
            this.isActive = false;
            this.IssueReasone = -1;
            this.CreatedByUserID = -1;
            _Mode = enMode.AddNew;
        }


        private clsDrivingLicense(int LicenseID, int ApplicationID, int DriverID, int LicenseClass, DateTime IssueDate
          , DateTime ExpirationDate, string Notes,float PaidFees,bool isActive,int IssueReasone,int CreatedByUserID)
        {
            this.LicenseID = LicenseID;
            this.Notes = Notes;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.LicenseClass = LicenseClass;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.PaidFees = PaidFees;

            this.isActive = isActive;
            this.IssueReasone = IssueReasone;
          
            this.CreatedByUserID = CreatedByUserID;

            _Mode = enMode.Update;

        }

        public static clsDrivingLicense FindDriverLicense(int LicenseID)
        {



            int ApplicationID = -1;
            int DriverID = -1;
            int LicenseClass = -1;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            string Notes = "";
            float PaidFees = 0;
            bool isActive = false;
            int IssueReasone = -1;
            int CreatedByUserID = -1;


            if (clsDataAccess.FindDriverLicense(LicenseID, ref ApplicationID, ref DriverID, ref LicenseClass, ref IssueDate, ref ExpirationDate
                , ref Notes, ref PaidFees, ref isActive, ref IssueReasone, ref CreatedByUserID))
            {
                return new clsDrivingLicense(LicenseID,  ApplicationID,  DriverID,  LicenseClass,  IssueDate,  ExpirationDate
                ,  Notes,  PaidFees,  isActive, IssueReasone, CreatedByUserID);
            }
            else { return null; }
        }

        public static clsDrivingLicense FindDriverLicenseByApplicationID(int ApplicationID)
        {



            int LicenseID = -1;
            int DriverID = -1;
            int LicenseClass = -1;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            string Notes = "";
            float PaidFees = 0;
            bool isActive = false;
            int IssueReasone = -1;
            int CreatedByUserID = -1;


            if (clsDataAccess.FindDriverLicenseByApplicationID(ref LicenseID,  ApplicationID, ref DriverID, ref LicenseClass, ref IssueDate, ref ExpirationDate
                , ref Notes, ref PaidFees, ref isActive, ref IssueReasone, ref CreatedByUserID))
            {
                return new clsDrivingLicense(LicenseID, ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate
                , Notes, PaidFees, isActive, IssueReasone, CreatedByUserID);
            }
            else { return null; }
        }
        bool _AddNewLicense()
        {
            this.LicenseID = clsDataAccess.AddNewLicense(this.ApplicationID, this.DriverID, this.LicenseClass,this.IssueDate,this.ExpirationDate,this.Notes,this.PaidFees,
                this.isActive,this.IssueReasone, this.CreatedByUserID);
            return LicenseID != -1;
        }
        bool _UpdateLicense()
        {
            return (clsDataAccess.UpdateDrivingLicenseInfo(this.LicenseID,this.ApplicationID, this.DriverID, this.LicenseClass, this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees,
                this.isActive, this.IssueReasone, this.CreatedByUserID));
          
        }
      
            public bool Save()
            {
                switch (_Mode)
                {
                    case enMode.AddNew:
                        if (_AddNewLicense())
                        {
                            _Mode = enMode.Update;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    case enMode.Update:
                        return _UpdateLicense();


                }
                return false;
            }
        

        public static int GetLicenseValidityLength(int LicenseClassID)
        {
            return clsDataAccess.GetLicenseValidityLength(LicenseClassID);
        }
        public static float GetLicenseClassFees(int LicenseClassID)
        {
            return clsDataAccess.GetLicenseClassFees(LicenseClassID);
        }

        public static bool isLicenseDetained(int LicenseID)
        {
            return clsDataAccess.isLicenseDetained(LicenseID);
        } 


        public static DataTable GetLocalDrivingLicensesHistory(int DriverID)
        {
            return clsDataAccess.GetLocalDrivingLicensesHistory(DriverID);
        }
        public static DataTable GetInternationalDrivingLicensesHistory(int DriverID)
        {
            return clsDataAccess.GetInternationalDrivingLicensesHistory(DriverID);
        }
    }

    public class clsDrivers
    {
        public int DriverID { get; set; }

        public int PersonID { get; set; }

        public int CreatedByUserID { get; set; }

        public DateTime CreatedDate { get; set; }

        public clsDrivers()
        {

            this.DriverID = -1;
            this.PersonID = -1;
            this.CreatedByUserID = -1;
            this.CreatedDate = DateTime.Now;

        }

        public static int GetDriverID(int PersonID)
        {

            return clsDataAccess.GetDriverID(PersonID);


        }

        bool _AddNewDriver()
        {
            this.DriverID = clsDataAccess.AddNewDriver(this.PersonID, this.CreatedByUserID, this.CreatedDate);
            return DriverID != -1;
        }

        public bool Save()
        {
            return _AddNewDriver();
        }


    }


    public class clsInternationalDrivingLicenses
    {
        public int InternationalLicenseID { get; set; }
        public int ApplicationID { get; set; }

        public int LocalLicenseID { get; set; }
        public int DriverID { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public int CreatedByUserID{ get; set; }

        public bool IsActive { get; set; }


        public clsInternationalDrivingLicenses()
        {
            this.InternationalLicenseID = -1;
            this.ApplicationID = -1;
            this.LocalLicenseID = -1;
            this.DriverID = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.IsActive = false;
            this.CreatedByUserID = -1;

        }
        public clsInternationalDrivingLicenses(int InternationalLicenseID,int ApplicationID, int LocalLicenseID, int DriverID, DateTime IssueDate
            , DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
        {
            this.InternationalLicenseID =InternationalLicenseID;
            this.ApplicationID = ApplicationID;
            this.LocalLicenseID = LocalLicenseID;
            this.DriverID = DriverID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive;
            this.CreatedByUserID = CreatedByUserID;

        }
        public static DataTable GetInternationalLicensesList()
        {
            return clsDataAccess.GetInternationalLicensesList();
        }

        public static clsInternationalDrivingLicenses Find(int IntLicenseID)
        {
            
            int ApplicationID = -1;
            int LocalLicenseID = -1;
            int DriverID = -1;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            bool IsActive = false;
            int CreatedByUserID = -1;

            if (clsDataAccess.FindInternationalLicenseInfo(IntLicenseID,ref ApplicationID, ref LocalLicenseID, ref DriverID, ref IssueDate, ref ExpirationDate, ref IsActive, ref CreatedByUserID))
            {
                return new clsInternationalDrivingLicenses(IntLicenseID, ApplicationID, LocalLicenseID, DriverID, IssueDate, ExpirationDate, IsActive, CreatedByUserID);
            }
            return null;

        }

        public static int GetInternationalLicenseIdWhenDriverHasAnActiveOne(int DriverID)
        {
            return clsDataAccess.isDriverHasAnActiveInternationalLicense(DriverID);
        }

        bool _AddNew()
        {
            this.InternationalLicenseID = clsDataAccess.AddNewInternationalLicense(this.ApplicationID,this.DriverID,this.LocalLicenseID,this.IssueDate,this.ExpirationDate,this.IsActive,this.CreatedByUserID);
            return this.InternationalLicenseID != -1;
        }

        public bool Save()
        {
            return _AddNew();
        } 

    }

    public class clsDetainedLicenses
    {

        //DetainID
        //LicenseID
        //DetainDate
        //FineFees
        //CreatedByUserID
        //IsReleased
        //ReleaseDate
        //ReleasedByUserID
        //ReleaseApplicationID


        public int DetainID { get; set; }
        public int LicenseID { get; set; }
        public DateTime DetainDate { get; set; }
        public float FineFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsReleased { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int ReleasedByUserID { get; set; }
        public int ReleaseApplicationID { get; set; }

        enum enMode
        {
            AddNew = 1,
            Update = 2
        }

        enMode _Mode= enMode.AddNew;
        public clsDetainedLicenses()
        {
            DetainID = -1;
            LicenseID = -1;
            DetainDate = DateTime.Now;
            FineFees = 0;
            CreatedByUserID = -1;
            IsReleased = true;
            ReleaseDate = DateTime.Now;
            ReleasedByUserID = -1;
            ReleaseApplicationID = -1;
            _Mode = enMode.AddNew;
        }

        clsDetainedLicenses(int DetainID, int LicenseID, DateTime DetainDate, float FineFees, int CreatedByUserID, bool IsReleased, DateTime ReleaseDate, int ReleasedByUserID, int ReleaseApplicationID)
        {
            this.DetainID = DetainID;
            this.LicenseID = LicenseID;
            this.DetainDate = DetainDate;
            this.FineFees = FineFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsReleased = IsReleased;
            this.ReleaseDate = ReleaseDate;
            this.ReleasedByUserID = ReleasedByUserID;
            this.ReleaseApplicationID = ReleaseApplicationID;
            _Mode = enMode.Update;
        }


        public static clsDetainedLicenses Find(int DetainID)
        {



            int LicenseID = -1;
            DateTime DetainDate = new DateTime();
            float FineFees = 0;
            int CreatedByUserID = -1;
            bool IsReleased = false;
            DateTime ReleaseDate = new DateTime();
            int ReleasedByUserID = -1;
            int ReleaseApplicationID = -1;
        

            if (clsDataAccess.FindDetainedlLicenseInfo(DetainID,ref LicenseID,ref DetainDate,ref FineFees,ref CreatedByUserID,ref IsReleased,ref ReleaseDate,
                ref ReleasedByUserID,ref ReleaseApplicationID))
            {
                return new clsDetainedLicenses(DetainID, LicenseID, DetainDate, FineFees, CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID);
            }
            return null;
        }

        public static clsDetainedLicenses FindByLicenseID(int LicenseID)
        {



            int DetainID = -1;
            DateTime DetainDate = new DateTime();
            float FineFees = 0;
            int CreatedByUserID = -1;
            bool IsReleased = false;
            DateTime ReleaseDate = new DateTime();
            int ReleasedByUserID = -1;
            int ReleaseApplicationID = -1;


            if (clsDataAccess.FindDetainedlLicenseInfo(ref DetainID,  LicenseID, ref DetainDate, ref FineFees, ref CreatedByUserID, ref IsReleased, ref ReleaseDate,
                ref ReleasedByUserID, ref ReleaseApplicationID))
            {
                return new clsDetainedLicenses(DetainID, LicenseID, DetainDate, FineFees, CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID);
            }
            return null;
        }
        bool _AddNewDetainedLicense()
        {
            this.DetainID = clsDataAccess.AddNewDetainedLicense(this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID, this.IsReleased, this.ReleaseDate, this.ReleasedByUserID,
                this.ReleaseApplicationID);

            return this.DetainID != -1;
        }
        bool _UpdateDetainedLicenseInfo()
        {
            return (clsDataAccess.UpdateDetainedLicenseInfo(this.DetainID, this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID, this.IsReleased, this.ReleaseDate, this.ReleasedByUserID,
                this.ReleaseApplicationID));

        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDetainedLicense())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateDetainedLicenseInfo();


            }
            return false;
        }

        public static DataTable GetDetainedLicensesList()
        {
            return clsDataAccess.GetDetainedLicensesList();
        }




    }
    public static class DVLDSettings
        {
            public static clsUsers CurrentUser;

        }
    
}
