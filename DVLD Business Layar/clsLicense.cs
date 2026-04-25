using DVLD_DataAccess_Layar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Layar
{
    public class clsLicense
    {
        public int LicenseID { get; set; }
        public int ApplicationID { get; set; }
        public clsApplication ApplicationInfo { set; get; }
        public int DriverID { get; set; }
        public clsDriver DriverInfo { get; set; }
        public int LicenseClass { get; set; }
        public clsLicenseClass LicenseClassInfo { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public string Notes { get; set; }
        public float PaidFees { get; set; }

        public bool isActive { get; set; }

        public enIssueReason IssueReasone { get; set; }
        public int CreatedByUserID { get; set; }

        public clsDetainedLicense DetainedInfo { set; get; }
        public bool IsDetained
        {
            get
            {
                return clsDetainedLicense.IsLicenseDetained(this.LicenseID);
            }
        }
        public enum enIssueReason 
        {
            FirstTime=1,
            Renew=2,
            DamagedReplacement=3,
            LostReplacement=4
        }
       

        public static string GetIssueReasonText(enIssueReason IssueReason)
        {
            switch (IssueReason)
            {
                case enIssueReason.Renew:
                    return "Renew";
                case enIssueReason.FirstTime:
                    return "First Time";
                case enIssueReason.LostReplacement:
                    return "Lost Replacement";
                case enIssueReason.DamagedReplacement:
                    return "DamagedReplacement";
               
                default:
                    return "Unknown";

            }
        }

        public string GetIssueReasonText()
        {
            return GetIssueReasonText(this.IssueReasone);
        }


        enum enMode
        {
            AddNew = 1,
            Update = 2
        }
        enMode _Mode = enMode.AddNew;
        public clsLicense()
        {

            this.LicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.DriverInfo = new clsDriver();
            this.LicenseClass = -1;
            this.LicenseClassInfo = new clsLicenseClass();
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.Notes = "";
            this.PaidFees = 0;
            this.isActive = false;
            this.IssueReasone = enIssueReason.FirstTime;
            this.CreatedByUserID = -1;
            this.DetainedInfo = new clsDetainedLicense();
            this.ApplicationInfo = new clsApplication();
            _Mode = enMode.AddNew;
        }


        private clsLicense(int LicenseID, int ApplicationID, int DriverID, int LicenseClass, DateTime IssueDate
          , DateTime ExpirationDate, string Notes, float PaidFees, bool isActive, int IssueReasone, int CreatedByUserID)
        {
            this.LicenseID = LicenseID;
            this.Notes = Notes;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.DriverInfo = clsDriver.GetDriverInfoByID(DriverID);
            this.LicenseClass = LicenseClass;
            this.LicenseClassInfo = clsLicenseClass.FindLicenseClassByID(LicenseClass);
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.PaidFees = PaidFees;

            this.isActive = isActive;
            this.IssueReasone = (enIssueReason)IssueReasone;

            this.CreatedByUserID = CreatedByUserID;
            this.DetainedInfo = clsDetainedLicense.FindByLicenseID(LicenseID);
            this.ApplicationInfo = clsApplication.Find(ApplicationID);
            _Mode = enMode.Update;

        }

        public static clsLicense FindDriverLicense(int LicenseID)
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


            if (clsLicenseData.FindDriverLicense(LicenseID, ref ApplicationID, ref DriverID, ref LicenseClass, ref IssueDate, ref ExpirationDate
                , ref Notes, ref PaidFees, ref isActive, ref IssueReasone, ref CreatedByUserID))
            {
                return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate
                , Notes, PaidFees, isActive, IssueReasone, CreatedByUserID);
            }
            else { return null; }
        }

        public static clsLicense FindDriverLicenseByApplicationID(int ApplicationID)
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


            if (clsLicenseData.FindDriverLicenseByApplicationID(ref LicenseID, ApplicationID, ref DriverID, ref LicenseClass, ref IssueDate, ref ExpirationDate
                , ref Notes, ref PaidFees, ref isActive, ref IssueReasone, ref CreatedByUserID))
            {
                return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate
                , Notes, PaidFees, isActive, IssueReasone, CreatedByUserID);
            }
            else { return null; }
        }
        public static clsLicense FindActiveDriverLicenseByApplicationID(int ApplicationID)
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


            if (clsLicenseData.FindActiveDriverLicenseByApplicationID(ref LicenseID, ApplicationID, ref DriverID, ref LicenseClass, ref IssueDate, ref ExpirationDate
                , ref Notes, ref PaidFees, ref isActive, ref IssueReasone, ref CreatedByUserID))
            {
                return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate
                , Notes, PaidFees, isActive, IssueReasone, CreatedByUserID);
            }
            else { return null; }
        }
        bool _AddNewLicense()
        {
            this.LicenseID = clsLicenseData.AddNewLicense(this.ApplicationID, this.DriverID, this.LicenseClass, this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees,
                this.isActive, (int)this.IssueReasone, this.CreatedByUserID);
            return LicenseID != -1;
        }
        bool _UpdateLicense()
        {
            return (clsLicenseData.UpdateDrivingLicenseInfo(this.LicenseID, this.ApplicationID, this.DriverID, this.LicenseClass, this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees,
                this.isActive, (int)this.IssueReasone, this.CreatedByUserID));

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

        public static bool IsActiveLicenseExistByPersonID(int PersonID, int LicenseClassID)
        {
            return clsLicenseData.FindActiveLicenseIDForPersonAndLicenseClass(PersonID, LicenseClassID)!=-1;
        }
        public static int FindActiveLicenseIDForPersonAndLicenseClass(int PersonID, int LicenseClassID)
        {
            return clsLicenseData.FindActiveLicenseIDForPersonAndLicenseClass(PersonID, LicenseClassID);
        }
        public static bool DeactivateLicense(int LicenseID)
        {
            return clsLicenseData.DeactivateLicense(LicenseID);
        }

        public bool DeactivateLicense()
        {
            return clsLicenseData.DeactivateLicense(this.LicenseID);
        }


        public static int GetLicenseValidityLength(int LicenseClassID)
        {
            return clsLicenseData.GetLicenseValidityLength(LicenseClassID);
        }
       

        public static bool isLicenseDetained(int LicenseID)
        {
            return clsLicenseData.isLicenseDetained(LicenseID);
        }
        public  bool IsLicenseExpired()
        {
            return(ExpirationDate < DateTime.Now);
        }

        public static DataTable GetLocalDrivingLicensesHistory(int DriverID)
        {
            return clsLicenseData.GetLocalDrivingLicensesHistory(DriverID);
        }
      
        public int Detain(float FineFees, int CreatedByUserID)
        {
            clsDetainedLicense detainedLicense = new clsDetainedLicense();
            detainedLicense.LicenseID = LicenseID;
            detainedLicense.DetainDate = DateTime.Now;
            detainedLicense.FineFees = FineFees;
            detainedLicense.CreatedByUserID = CreatedByUserID;

            if (!detainedLicense.Save())
            {
                return -1;
            }
            
            this.DetainedInfo = clsDetainedLicense.Find(detainedLicense.DetainID);
            return detainedLicense.DetainID;
        }
        public bool Release(ref int ApplicationID, int ReleasedByUserID)
        {
            clsApplication Application = new clsApplication();
            Application.ApplicantPersonID = this.DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.PaidFees = clsApplicationType.GetApplicationFees((int)clsApplicationType.enApplicationType.ReleaseDetainedLicense);
            Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            Application.LastStatusDate = DateTime.Now;
            Application.ApplicationTypeID = clsApplicationType.enApplicationType.ReleaseDetainedLicense;
            Application.CreatedByUserID = ReleasedByUserID;

            if (!Application.Save())
            {
                ApplicationID = -1;
                return false;
            }

             ApplicationID = Application.ApplicationID;


            return this.DetainedInfo.ReleaseDetainedLicense(Application.ApplicationID, ReleasedByUserID);

        }

        public clsLicense Replace(enIssueReason issueReason, int CreatedByUserID)
        {
            clsApplication Application = new clsApplication();
            Application.ApplicantPersonID = this.DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationTypeID = (issueReason== enIssueReason.DamagedReplacement) ?
                clsApplicationType.enApplicationType.ReplacementForDamagedDrivingLicense:
                clsApplicationType.enApplicationType.ReplacementForLostDrivingLicense;
            Application.PaidFees = clsApplicationType.GetApplicationFees((int)Application.ApplicationTypeID);
            Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            Application.LastStatusDate = DateTime.Now;
            
            Application.CreatedByUserID = CreatedByUserID;

            if (!Application.Save())
            {
                
                return null;
            }
            clsLicense NewLicense = new clsLicense();
            NewLicense.ApplicationID = Application.ApplicationID;
            NewLicense.DriverID = this.DriverID;
            NewLicense.LicenseClass = this.LicenseClass;
            NewLicense.LicenseClassInfo = clsLicenseClass.FindLicenseClassByID(this.LicenseClass);
            NewLicense.IssueDate = DateTime.Now;
            NewLicense.ExpirationDate = DateTime.Now.AddYears(NewLicense.LicenseClassInfo.DefaultValidityLength);
            NewLicense.Notes = this.Notes;
            NewLicense.PaidFees = 0;
            NewLicense.isActive = true;
            NewLicense.IssueReasone = issueReason;
            NewLicense.CreatedByUserID = CreatedByUserID;

            if (!NewLicense.Save())
            {
                return null;
            }
            DeactivateLicense();
            return NewLicense;
        }
        public clsLicense RenewLicense(string Notes, int CreatedByUserID)
        {
            clsApplication Application = new clsApplication();
            Application.ApplicantPersonID = this.DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationTypeID = clsApplicationType.enApplicationType.RenewDrivingLicense;
            Application.PaidFees = clsApplicationType.GetApplicationFees((int)Application.ApplicationTypeID);
            Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            Application.LastStatusDate = DateTime.Now;

            Application.CreatedByUserID = CreatedByUserID;

            if (!Application.Save())
            {

                return null;
            }
            clsLicense NewLicense = new clsLicense();
            NewLicense.ApplicationID = Application.ApplicationID;
            NewLicense.DriverID = this.DriverID;
            NewLicense.LicenseClass = this.LicenseClass;
            NewLicense.LicenseClassInfo = clsLicenseClass.FindLicenseClassByID(this.LicenseClass);
            NewLicense.IssueDate = DateTime.Now;
            NewLicense.ExpirationDate = DateTime.Now.AddYears(NewLicense.LicenseClassInfo.DefaultValidityLength);
            NewLicense.Notes = Notes;
            NewLicense.PaidFees = NewLicense.LicenseClassInfo.ClassFees;
            NewLicense.isActive = true;
            NewLicense.IssueReasone = enIssueReason.Renew;
            NewLicense.CreatedByUserID = CreatedByUserID;

            if (!NewLicense.Save())
            {
                return null;
            }
            DeactivateLicense();
            return NewLicense;
        }

    }
}
