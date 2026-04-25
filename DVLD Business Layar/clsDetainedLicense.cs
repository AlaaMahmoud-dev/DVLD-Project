using DVLD_DataAccess_Layar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_Business_Layar
{
    public class clsDetainedLicense
    {
        public int DetainID { get; set; }
        public int LicenseID { get; set; }
        public DateTime DetainDate { get; set; }
        public float FineFees { get; set; }
        public int CreatedByUserID { get; set; }
        public clsUser CreatedByUserInfo { get; set; }
        public bool IsReleased { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int ReleasedByUserID { get; set; }
        public int ReleaseApplicationID { get; set; }
        public clsApplication ReleaseApplicationInfo { get; set; }
        enum enMode
        {
            AddNew = 1,
            Update = 2
        }

        enMode _Mode = enMode.AddNew;
        public clsDetainedLicense()
        {
            DetainID = -1;
            LicenseID = -1;
            DetainDate = DateTime.Now;
            FineFees = 0;
            CreatedByUserID = -1;
            CreatedByUserInfo = new clsUser();
            IsReleased = true;
            ReleaseDate = DateTime.Now;
            ReleasedByUserID = -1;
            ReleaseApplicationID = -1;
            ReleaseApplicationInfo = new clsApplication();

            _Mode = enMode.AddNew;
        }

        clsDetainedLicense(int DetainID, int LicenseID, DateTime DetainDate,
            float FineFees, int CreatedByUserID, bool IsReleased,
            DateTime ReleaseDate, int ReleasedByUserID, int ReleaseApplicationID)
        {
            this.DetainID = DetainID;
            this.LicenseID = LicenseID;
            this.DetainDate = DetainDate;
            this.FineFees = FineFees;
            this.CreatedByUserID = CreatedByUserID;
            CreatedByUserInfo = clsUser.FindUserByUserID(ReleasedByUserID);
            this.IsReleased = IsReleased;
            this.ReleaseDate = ReleaseDate;
            this.ReleasedByUserID = ReleasedByUserID;
            this.ReleaseApplicationID = ReleaseApplicationID;
            ReleaseApplicationInfo = clsApplication.Find(ReleaseApplicationID);
            _Mode = enMode.Update;
        }


        public static clsDetainedLicense Find(int DetainID)
        {



            int LicenseID = -1;
            DateTime DetainDate = new DateTime();
            float FineFees = 0;
            int CreatedByUserID = -1;
            bool IsReleased = false;
            DateTime ReleaseDate = new DateTime();
            int ReleasedByUserID = -1;
            int ReleaseApplicationID = -1;


            if (clsDetainedLicenseData.FindDetainedLicenseInfoByID(DetainID, 
                ref LicenseID, ref DetainDate, ref FineFees, ref CreatedByUserID,
                ref IsReleased, ref ReleaseDate,
                ref ReleasedByUserID, ref ReleaseApplicationID))
            {
                return new clsDetainedLicense(DetainID, LicenseID, DetainDate
                    , FineFees, CreatedByUserID, IsReleased, ReleaseDate, 
                    ReleasedByUserID, ReleaseApplicationID);
            }
            return null;
        }

        public static clsDetainedLicense FindByLicenseID(int LicenseID)
        {



            int DetainID = -1;
            DateTime DetainDate = new DateTime();
            float FineFees = 0;
            int CreatedByUserID = -1;
            bool IsReleased = false;
            DateTime ReleaseDate = new DateTime();
            int ReleasedByUserID = -1;
            int ReleaseApplicationID = -1;


            if (clsDetainedLicenseData.FindDetainedLicenseInfoByLicenseID(
                ref DetainID, LicenseID, ref DetainDate, ref FineFees,
                ref CreatedByUserID, ref IsReleased, ref ReleaseDate,
                ref ReleasedByUserID, ref ReleaseApplicationID))
            {
                return new clsDetainedLicense(DetainID, LicenseID, DetainDate,
                    FineFees, CreatedByUserID, IsReleased, ReleaseDate,
                    ReleasedByUserID, ReleaseApplicationID);
            }
            return null;
        }
        bool _AddNewDetainedLicense()
        {
            this.DetainID = clsDetainedLicenseData.AddNewDetainedLicense(
                this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID);

            return this.DetainID != -1;
        }
        bool _UpdateDetainedLicenseInfo()
        {
            return (clsDetainedLicenseData.UpdateDetainedLicenseInfo(
                this.DetainID, this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID));

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
            return clsDetainedLicenseData.GetDetainedLicensesList();
        }
        public bool ReleaseDetainedLicense(int ApplicationID,int ReleasedByUserID)
        {
            return clsDetainedLicenseData.ReleaseDetainedLicense(this.DetainID,ApplicationID,ReleasedByUserID);
        }

        public static bool IsLicenseDetained(int LicenseID)
        {
            return clsDetainedLicenseData.IsLicenseDetained(LicenseID);
        }
    }
}
