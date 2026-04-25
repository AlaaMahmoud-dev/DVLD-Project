using DVLD_DataAccess_Layar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Layar
{
    public class clsTestAppointment
    {
        public int TestAppointmentID { get; set; }

        public int TestTypeID { get; set; }
        public clsTestType TestTypeInfo { get; set; }

        public int LocalDrivingLicenseApplicationID { get; set; }
        public clsLDLApplication LocalDrivingLicenseApplicationInfo { get; set; }

        public DateTime AppointmentDate { get; set; }

        public float PaidFees { get; set; }

        public int CreatedByUserID { get; set; }

        public bool isLocked { get; set; }
        public int RetakeTestApplicationID { get; set; }
        public clsApplication RetakeTestApplicationInfo { set; get; }
        enum enMode
        {
            AddNew = 1,
            Update = 2
        }

        private enMode _Mode = enMode.Update;


        public clsTestAppointment()
        {
            TestAppointmentID = -1;
            TestTypeID = -1;
            LocalDrivingLicenseApplicationID = -1;
            AppointmentDate = DateTime.Now;
            PaidFees = 0;
            CreatedByUserID = -1;
            isLocked = false;
            this.RetakeTestApplicationID = -1;
            RetakeTestApplicationInfo = new clsApplication();
            TestTypeInfo = new clsTestType();
            LocalDrivingLicenseApplicationInfo = new clsLDLApplication();
            _Mode = enMode.AddNew;

        }

        private clsTestAppointment(int TestAppointmentID, int TestTypeID,
            int LocalDrivingLicenseApplicationID, DateTime AppointmentDate,
            float PaidFees, int CreatedByUserID, bool isLocked,int RetakeTestApplicationID)
        {
            this.TestAppointmentID = TestAppointmentID;
            this.isLocked = isLocked;
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.TestTypeID = TestTypeID;
            this.TestTypeInfo = clsTestType.FindTestTypeInfoByID(TestTypeID);
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.CreatedByUserID = CreatedByUserID;
            this.RetakeTestApplicationID = RetakeTestApplicationID;
            RetakeTestApplicationInfo = clsApplication.Find(RetakeTestApplicationID);
            LocalDrivingLicenseApplicationInfo = clsLDLApplication.FindLocalDrivingLicenseAppInfoByID(LocalDrivingLicenseApplicationID);
            _Mode = enMode.Update;

        }

        public static clsTestAppointment FindAppointment(int AppointmentID)
        {

            int TestTypeID = -1;
            int LocalDrivingLicenseApplicationID = -1;

            DateTime AppointmentDate = DateTime.Now;
            float PaidFees = 0;
            int CreatedByUserID = -1;
            bool isLocked = false;
            int RetakeTestAppointmentID = -1;

            if (clsTestAppointmentData.FindAppointment(AppointmentID,
                ref TestTypeID, ref LocalDrivingLicenseApplicationID, 
                ref AppointmentDate, ref PaidFees, ref CreatedByUserID
                , ref isLocked, ref RetakeTestAppointmentID))
            {
                return new clsTestAppointment(AppointmentID, TestTypeID,
                    LocalDrivingLicenseApplicationID, AppointmentDate,
                    PaidFees, CreatedByUserID, isLocked,RetakeTestAppointmentID);
            }
            else { return null; }
        }


        public static DataTable GetAppointmentsListPerApplicationAndTestType(int LDLApplicationID, int TestType)
        {
            return clsTestAppointmentData.GetAppointmentsListPerApplicationAndTestType(LDLApplicationID, TestType);
        }

        public static bool hasAppointment(int LDLApplicationID, int TestType)
        {
            return clsTestAppointmentData.hasAppointment(LDLApplicationID, TestType);
        }

        public static bool HasActiveTestAppointment(int LDLApplicationID, int TestType)
        {
            return clsTestAppointmentData.HasActiveTestAppointment(LDLApplicationID, TestType);
        }

        public static bool isTestPassed(int LDLApplicationID, int TestType)
        {
            return clsTestAppointmentData.isTestPassed(LDLApplicationID, TestType);
        }

        public static int TotalNumberOfTrials(int LDLApplicationID, int TestTypeID)
        {
            return clsTestAppointmentData.GetTotalNumberOfTrials(LDLApplicationID, TestTypeID);
        }


        bool _Update()
        {
            return clsTestAppointmentData.UpdateAppointment(this.TestAppointmentID,
                this.TestTypeID, this.LocalDrivingLicenseApplicationID,
                this.AppointmentDate, this.PaidFees,
                this.CreatedByUserID, this.isLocked, this.RetakeTestApplicationID);
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
            this.TestAppointmentID = 
                clsTestAppointmentData.AddNewTestAppointment(this.TestTypeID,
                this.LocalDrivingLicenseApplicationID, this.AppointmentDate,
                this.PaidFees, this.CreatedByUserID, this.isLocked,this.RetakeTestApplicationID);

            return TestAppointmentID != -1;
        }


    }
}
