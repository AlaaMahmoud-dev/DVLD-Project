using DVLD_DataAccess_Layar;
using System;
using System.Data;

namespace DVLD_Business_Layar
{
    public class clsInternationalDrivingLicense:clsApplication
    {
        public int InternationalLicenseID { get; set; }
        
        public int LocalLicenseID { get; set; }
        public int DriverID { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public clsDriver DriverInfo { get; set; }
        public clsLicense LocalLicenseInfo { get; set; }
        public bool IsActive { get; set; }

        new enum enMode
        {
            AddNew = 1,
            Update = 2
        }

        private enMode _Mode = enMode.Update;
        public clsInternationalDrivingLicense()
        {
            this.InternationalLicenseID = -1;
            this.LocalLicenseID = -1;
            this.DriverID = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.IsActive = false;
            this.DriverInfo = new clsDriver();
            _Mode = enMode.AddNew;

        }
        public clsInternationalDrivingLicense(int ApplicationID, int ApplicantPersonID,
            DateTime ApplicationDate, int ApplicationTypeID,
            byte ApplicationStatus, DateTime LastStatusDate,
            float PiadFees, int CreatedByUserID,
            int InternationalLicenseID, int LocalLicenseID, int DriverID, DateTime IssueDate
            , DateTime ExpirationDate, bool IsActive)
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
            this.InternationalLicenseID = InternationalLicenseID;
            this.LocalLicenseID = LocalLicenseID;
            this.DriverID = DriverID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive;
            this.DriverInfo = clsDriver.GetDriverInfoByID(DriverID);
            this.LocalLicenseInfo = clsLicense.FindDriverLicense(LocalLicenseID);
            _Mode = enMode.Update;
        }
        public static DataTable GetInternationalLicensesList()
        {
            return clsInternationalLicenseData.GetInternationalLicensesList();
        }

        public static clsInternationalDrivingLicense FindInfoByID(int IntLicenseID)
        {

            int ApplicationID = -1;
            int LocalLicenseID = -1;
            int DriverID = -1;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            bool IsActive = false;
            int CreatedByUserID = -1;

            bool isFound = clsInternationalLicenseData.FindInternationalLicenseInfoByID(IntLicenseID,
                ref ApplicationID, ref LocalLicenseID, ref DriverID, ref IssueDate,
                ref ExpirationDate, ref IsActive, ref CreatedByUserID);
            if (isFound)
            {
                clsApplication BaseAppInfo = clsApplication.Find(ApplicationID);
                if (BaseAppInfo!=null)
                {
                    return new clsInternationalDrivingLicense(ApplicationID, BaseAppInfo.ApplicantPersonID,
                    BaseAppInfo.ApplicationDate, (int)BaseAppInfo.ApplicationTypeID,
                    (byte)BaseAppInfo.ApplicationStatus, BaseAppInfo.LastStatusDate,
                    BaseAppInfo.PaidFees, BaseAppInfo.CreatedByUserID, IntLicenseID,
                    LocalLicenseID, DriverID, IssueDate, ExpirationDate, IsActive);

                }
                return null;
            }
            return null;

        }
        public static clsInternationalDrivingLicense FindInfoByDriverID(int DriverID)
        {

            int ApplicationID = -1;
            int LocalLicenseID = -1;
            int IntLicenseID = -1;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            bool IsActive = false;
            int CreatedByUserID = -1;

            bool isFound = clsInternationalLicenseData.FindInternationalLicenseInfoByDriverID(ref IntLicenseID, 
                ref ApplicationID, DriverID, ref LocalLicenseID,  ref IssueDate,
                ref ExpirationDate, ref IsActive, ref CreatedByUserID);
            if (isFound)
            {
                clsApplication BaseAppInfo = clsApplication.Find(ApplicationID);
                if (BaseAppInfo != null)
                {
                    return new clsInternationalDrivingLicense(ApplicationID, BaseAppInfo.ApplicantPersonID,
                    BaseAppInfo.ApplicationDate, (int)BaseAppInfo.ApplicationTypeID,
                    (byte)BaseAppInfo.ApplicationStatus, BaseAppInfo.LastStatusDate,
                    BaseAppInfo.PaidFees, BaseAppInfo.CreatedByUserID, DriverID,
                    LocalLicenseID, IntLicenseID, IssueDate, ExpirationDate, IsActive);

                }
                return null;
            }
            return null;

        }

        public static int GetActiveInternationalLicenseIDForDriver(int DriverID)
        {
            return clsInternationalLicenseData.GetActiveInternationalLicenseIDForDriver(DriverID);
        }
        public static DataTable GetInternationalDrivingLicensesHistory(int DriverID)
        {
            return clsInternationalLicenseData.GetInternationalDrivingLicensesHistory(DriverID);
        }
        bool _AddNew()
        {
            this.InternationalLicenseID = clsInternationalLicenseData.AddNewInternationalLicense(
                this.ApplicationID, this.DriverID, this.LocalLicenseID, this.IssueDate,
                this.ExpirationDate, this.IsActive, this.CreatedByUserID);
            return this.InternationalLicenseID != -1;
        }
        bool _Update()
        {

            return clsInternationalLicenseData.UpdateInternationalLicenseInfo(this.InternationalLicenseID,
                this.ApplicationID, this.DriverID, this.LocalLicenseID, this.IssueDate,
                this.ExpirationDate, this.IsActive, this.CreatedByUserID);
        }

        public static clsInternationalDrivingLicense IssueInternationalDrivingLicense(int LicenseID)
        {
            clsLicense LocalLicenseInfo = clsLicense.FindDriverLicense(LicenseID);
            if (LocalLicenseInfo == null)
            {
                return null;
            }
            clsInternationalDrivingLicense internationalDrivingLicense = new clsInternationalDrivingLicense();
            internationalDrivingLicense.ApplicationDate = DateTime.Now;
            internationalDrivingLicense.LastStatusDate = DateTime.Now;
            internationalDrivingLicense.ApplicationStatus = clsApplication.enApplicationStatus.New;
            internationalDrivingLicense.PaidFees = clsApplicationType.GetApplicationFees(
                (int)clsApplicationType.enApplicationType.NewInternationalDrivingLicense);
            internationalDrivingLicense.ApplicationTypeID = clsApplicationType.enApplicationType.NewInternationalDrivingLicense;
            internationalDrivingLicense.CreatedByUserID = DVLDSettings.CurrentUser.UserID;
            internationalDrivingLicense.ApplicantPersonID = LocalLicenseInfo.DriverInfo.PersonID;
            internationalDrivingLicense.IssueDate = DateTime.Now;
            internationalDrivingLicense.ExpirationDate = DateTime.Now.AddYears(1);
            internationalDrivingLicense.DriverID = LocalLicenseInfo.DriverID;
            internationalDrivingLicense.CreatedByUserID = DVLDSettings.CurrentUser.UserID;
            internationalDrivingLicense.IsActive = true;
            internationalDrivingLicense.LocalLicenseID = LicenseID;

            if (!internationalDrivingLicense.Save())
            {
                return null;
            }
            return internationalDrivingLicense;
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
    }
}
