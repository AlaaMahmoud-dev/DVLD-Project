using DVLD_DataAccess_Layar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Layar
{
    public class clsLDLApplication:clsApplication
    {
        public int LDLApplicationID { get; set; }
        public int LicenseClassID { get; set; }
        public clsLicenseClass LicenseClassInfo { set; get; }
        
       
        new enum enMode
        {
            AddNew = 1,
            Update = 2
        }

        private enMode _Mode = enMode.Update;
      


        public clsLDLApplication():base()
        {
            
            this.LDLApplicationID = -1;
            
            this.LicenseClassID = -1;
            LicenseClassInfo = new clsLicenseClass();
            _Mode = enMode.AddNew;
        }

        clsLDLApplication(int ApplicationID, int ApplicantPersonID,
            DateTime ApplicationDate, int ApplicationTypeID,
            byte ApplicationStatus, DateTime LastStatusDate,
            float PiadFees, int CreatedByUserID, int LDLApplicationID, int LicenseClassID)
        {
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicantPersonInfo = clsPerson.FindPersonByID(ApplicantPersonID);
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = (clsApplicationType.enApplicationType)ApplicationTypeID;
            this.ApplicationTypeInfo = clsApplicationType.FindApplicationTypeInfoByID(ApplicationTypeID);
            this.ApplicationStatus = (enApplicationStatus)ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PiadFees;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedByUserInfo = clsUser.FindUserByUserID(CreatedByUserID);
            
            this.LDLApplicationID = LDLApplicationID;
           
            this.LicenseClassID = LicenseClassID;
            LicenseClassInfo = clsLicenseClass.FindLicenseClassByID(LicenseClassID);
            _Mode = enMode.Update;
        }
        public string GetLicenseClassText()
        {
            return clsLicenseClass.GetLicenseClassName((clsLicenseClass.enLicenseClass)this.LicenseClassID);
        }
        
        public static clsLDLApplication FindLocalDrivingLicenseAppInfoByID(int LDLApplicationID)
        { 

            int ApplicationID = -1;
            int LicenseClassID = -1;
            bool isFound = false;

            isFound = clsLocalDrivingLicenseApplicationData.FindLDLApplicationInfoByID(LDLApplicationID, ref ApplicationID, ref LicenseClassID);

            if (isFound)
            {
                clsApplication BaseAppInfo = clsApplication.Find(ApplicationID);


                return new clsLDLApplication(ApplicationID,BaseAppInfo.ApplicantPersonID,
                    BaseAppInfo.ApplicationDate,(int)BaseAppInfo.ApplicationTypeID,
                    (byte)BaseAppInfo.ApplicationStatus, BaseAppInfo.LastStatusDate,
                    BaseAppInfo.PaidFees, BaseAppInfo.CreatedByUserID,
                    LDLApplicationID, LicenseClassID);
            }
            return null;
            
         


        }
        public static clsLDLApplication FindLocalDrivingLicenseAppInfoByBaseAppID(int ApplicationID)
        {
            
            int LDLApplicationID = -1;
            int LicenseClassID = -1;
            bool isFound = false;

            isFound = clsLocalDrivingLicenseApplicationData.FindLDLApplicationInfoByBaseAppID(ref LDLApplicationID, ApplicationID, ref LicenseClassID);

            if (isFound)
            {
                clsApplication BaseAppInfo = clsApplication.Find(ApplicationID);


                return new clsLDLApplication(ApplicationID, BaseAppInfo.ApplicantPersonID,
                    BaseAppInfo.ApplicationDate, (int)BaseAppInfo.ApplicationTypeID,
                    (byte)BaseAppInfo.ApplicationStatus, BaseAppInfo.LastStatusDate,
                    BaseAppInfo.PaidFees, BaseAppInfo.CreatedByUserID,
                    LDLApplicationID, LicenseClassID);
            }
            return null;




        }
        public static DataTable LDLApplicationsList()
        {
            return clsLocalDrivingLicenseApplicationData.GetLDLApplicationsList();
        }

        //public static int LDLApplicationsPassedTestCount(int LDLApplicationID)
        //{
        //    return clsDataAccess.GetPassedTests(LDLApplicationID);
        //}

        bool _AddNew()
        {



            this.LDLApplicationID = clsLocalDrivingLicenseApplicationData.AddNewLDLApplication(this.ApplicationID, this.LicenseClassID);

            return LDLApplicationID != -1;
        }

        bool _Update()
        {

            return clsLocalDrivingLicenseApplicationData.UpdateLDLApplication(this.LDLApplicationID, this.ApplicationID, this.LicenseClassID);
        }
        public new bool Save()
        {
            base.Mode = (clsApplication.enMode)_Mode;
            if (!base.Save())
            {
                return false;
            }
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

        public new bool Delete()
        {
            return clsLocalDrivingLicenseApplicationData.DeleteLDLApplication(this.LDLApplicationID);
        }
        public static bool DoesPassTest(int LDLApplicationID, clsTestType.enTestType TestType)
        {
            return clsLocalDrivingLicenseApplicationData.DoesPassTest(LDLApplicationID, (int)TestType);
        }
        public static bool DoesPassPreviousTest(int LDLApplicationID, clsTestType.enTestType CurrentTestType)
        {
            switch (CurrentTestType)
            {
                case clsTestType.enTestType.VisionTest:
                    return true;
                case clsTestType.enTestType.WrittenTest:
                    return DoesPassTest(LDLApplicationID,clsTestType.enTestType.VisionTest);
                case clsTestType.enTestType.PracticalTest:
                    return DoesPassTest(LDLApplicationID, clsTestType.enTestType.WrittenTest);
            }
            return false;
        }
        
           
        public bool DoesPassPreviousTest(clsTestType.enTestType CurrentTestType)
        {
            return DoesPassPreviousTest(LDLApplicationID, CurrentTestType);
        }
        public static bool IsThereAnActiveScheduledTest(int LDLApplicationID, int TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.IsThereAnActiveScheduledTest(LDLApplicationID, TestTypeID);
        }

        public static bool DoesAttendTestType(int LDLApplicationID, clsTestType.enTestType TestType)
        {
            return clsLocalDrivingLicenseApplicationData.DoesAttendTestType(LDLApplicationID, (int)TestType);
        }
        public static int TotalTrialsPerTest(int LDLApplicationID, int TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.TotalTrialsPerTest(LDLApplicationID, TestTypeID);
        }
        public int PassedTests()
        {
            return clsLocalDrivingLicenseApplicationData.GetPassedTests(this.LDLApplicationID);
        }
        public bool DoesPassedAllTests()
        {
            return clsLocalDrivingLicenseApplicationData.GetPassedTests(this.LDLApplicationID)==3;
        }
        public bool HasLicenseIssued()
        {
            return clsLocalDrivingLicenseApplicationData.HasLicenseIssued(this.ApplicationID);
        }
      
        public int IssueDrivingLicenseForTheFirstTime(string Notes)
        {
            if (HasLicenseIssued())
            {
                return -1;
            }
            clsLicense NewDrivingLicense = new clsLicense();
            NewDrivingLicense.ApplicationID = ApplicationID;
            NewDrivingLicense.Notes = Notes;
            NewDrivingLicense.IssueDate = DateTime.Now;
            NewDrivingLicense.ExpirationDate = DateTime.Now.AddYears(LicenseClassInfo.DefaultValidityLength);
            NewDrivingLicense.IssueReasone = clsLicense.enIssueReason.FirstTime;
            NewDrivingLicense.PaidFees = LicenseClassInfo.ClassFees;
            NewDrivingLicense.LicenseClass = LicenseClassID;
            NewDrivingLicense.CreatedByUserID = DVLDSettings.CurrentUser.UserID;
            NewDrivingLicense.isActive = true;
            
            clsDriver DriverInfo = clsDriver.GetDriverInfoByPersonID(this.ApplicantPersonID);
            if (DriverInfo == null)
            {
                DriverInfo = new clsDriver();
                DriverInfo.PersonID = ApplicantPersonID;
                DriverInfo.CreatedDate = DateTime.Now;
                DriverInfo.CreatedByUserID = DVLDSettings.CurrentUser.UserID;
                if (DriverInfo.Save())
                {
                    NewDrivingLicense.DriverID = DriverInfo.DriverID;
                }
                else
                {
                    return -1;
                }

            }
            else
            {
                NewDrivingLicense.DriverID = DriverInfo.DriverID;
                
            }

            if (NewDrivingLicense.Save())
            {
                this.ChangeStatus(enApplicationStatus.Completed);
                return NewDrivingLicense.LicenseID;
            }
                
            return -1;
        
        }
    }
}
