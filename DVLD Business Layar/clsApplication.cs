using DVLD_DataAccess_Layar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Layar
{
    public class clsApplication
    {
        public int ApplicationID { get; set; }
        public int ApplicantPersonID { get; set; }

        public clsPerson ApplicantPersonInfo { set; get; }
        public DateTime ApplicationDate { get; set; }
        public clsApplicationType.enApplicationType ApplicationTypeID { get; set; }
        public clsApplicationType ApplicationTypeInfo { get; set; }
        public enApplicationStatus ApplicationStatus { get; set; }

        public string StatusText
        {
            
            get
            {
                switch(ApplicationStatus)
                {
                    case enApplicationStatus.New:
                        return "New";
                    case enApplicationStatus.Canceled:
                        return "Canceled";
                    case enApplicationStatus.Completed:
                        return "Completed";
                    default:
                        return "N/A";
                }
            }
        }
        public DateTime LastStatusDate { get; set; }

        public float PaidFees { get; set; }

        public int CreatedByUserID { get; set; }

        public clsUser CreatedByUserInfo { set; get; }
        public float ApplicationFees
        {
            get
            {
                return clsApplicationTypeData.GetApplicationFees((int)this.ApplicationTypeID);
            }
        }

        public enum enMode
        {
            AddNew = 1,
            Update = 2
        }

        public enMode 
            Mode = enMode.Update;

        public enum enApplicationStatus:byte
        {
            New=1,
            Canceled=2,
            Completed=3
        }
        public clsApplication()
        {
            ApplicationID = -1;
            ApplicantPersonID = 0;
            ApplicantPersonInfo = new clsPerson();
            ApplicationDate = DateTime.Now;
            ApplicationTypeID = 0;
            ApplicationTypeInfo = new clsApplicationType();
            ApplicationStatus = enApplicationStatus.New;
            LastStatusDate = DateTime.Now;
            PaidFees = 0;
            CreatedByUserID = 0;
            CreatedByUserInfo = new clsUser();
            Mode = enMode.AddNew;
        }

        clsApplication(int ApplicationID, int ApplicantPersonID,
            DateTime ApplicationDate, int ApplicationTypeID,
            byte ApplicationStatus,DateTime LastStatusDate,
            float PiadFees, int CreatedByUserID)
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
            Mode = enMode.Update;
        }

        public static clsApplication Find(int ApplicationID)
        {

            int ApplicantPersonID = -1;

            DateTime ApplicationDate = DateTime.Now;
            int ApplicationTypeID = -1;
            byte ApplicationStatus = 1;
            DateTime LastStatusDate = DateTime.Now;

            float PaidFees = -1;

            int CreatedByUserID = -1;


            if (clsApplicationData.FindApplicationInfo(ApplicationID, ref ApplicantPersonID,
                ref ApplicationDate, ref ApplicationTypeID, ref ApplicationStatus,
            ref LastStatusDate, ref PaidFees, ref CreatedByUserID))
            {
                return new clsApplication(ApplicationID, ApplicantPersonID, ApplicationDate,
                    ApplicationTypeID, ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID);
            }
            return null;


        }
        public static DataTable ApplicationsList()
        {
            return clsApplicationData.GetApplicationsList();
        }

        bool _AddNew()
        {



            this.ApplicationID = clsApplicationData.AddNewApplication(this.ApplicantPersonID,
                this.ApplicationDate, (int)this.ApplicationTypeID, (byte)this.ApplicationStatus,
                this.LastStatusDate, this.PaidFees, DVLDSettings.CurrentUser.UserID);

            return ApplicationID != -1;
        }

        bool _Update()
        {
            return clsApplicationData.UpdateApplicationInfo(this.ApplicationID,
                this.ApplicantPersonID, this.ApplicationDate,
                (int)this.ApplicationTypeID, (byte)this.ApplicationStatus,
                this.LastStatusDate, this.PaidFees, this.CreatedByUserID);
        }

        public bool Delete()
        {
            return clsApplicationData.DeleteApplication(this.ApplicationID);
        }
        public bool Save()
        {

            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNew())
                    {
                        Mode = enMode.Update;
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

        public static int GetApplicationIdForSpecificStatusAndLicenseClass(int PersonID,
            int LicenseClassID, clsApplicationType.enApplicationType ApplicationType,enApplicationStatus Status)
        {
            return clsApplicationData.GetApplicationIdForSpecificStatusAndLicenseClass(PersonID, LicenseClassID,
                (int)ApplicationType, (byte)Status);
        }

        public static bool DoesPersonHaveApplicationWithSpecificStatusAndLicenseClass(int PersonID,
           int LicenseClassID, clsApplicationType.enApplicationType ApplicationType, enApplicationStatus Status)
        {
            return clsApplicationData.DoesPersonHaveApplicationWithSpecificStatusAndLicenseClass(PersonID, LicenseClassID,
                (int)ApplicationType, (byte)Status);
        }


        public static int GetApplicationIdForSpecificStatus(int PersonID,
             clsApplicationType.enApplicationType ApplicationType, enApplicationStatus Status)
        {
            return clsApplicationData.GetApplicationIdForSpecificStatus(PersonID,
                (int)ApplicationType, (byte)Status);
        }

        public static bool DoesPersonHaveApplicationWithSpecificStatus(int PersonID,
            clsApplicationType.enApplicationType ApplicationType, enApplicationStatus Status)
        {
            return clsApplicationData.DoesPersonHaveApplicationWithSpecificStatus(PersonID,
                (int)ApplicationType, (byte)Status);
        }



        public bool ChangeStatus(enApplicationStatus NewStatus)
        {
            return clsApplicationData.ChangeStatus(this.ApplicationID,(byte)NewStatus);
        }

        public static string ApplicationTypeTitle(int ApplicationTypeID)
        {
            return clsApplicationData.GetApplicationTypeTitle(ApplicationTypeID);
        }
    }
}
