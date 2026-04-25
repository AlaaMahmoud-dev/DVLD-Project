using DVLD_DataAccess_Layar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Layar
{
    public class clsTest
    {
        public int TestID { get; set; }
        public int TestAppointmentID { get; set; }

        public clsTestAppointment TestAppointmentInfo { set; get; }
        public bool TestResult { get; set; }

        public string Notes { get; set; }

        public int CreatedByUserID { get; set; }

        enum enMode
        {
            AddNew = 1,
            Update = 2
        }

        private enMode _Mode = enMode.Update;
        public clsTest()
        {
            this.TestResult = false;
            this.TestID = -1;
            this.Notes = "";
            this.CreatedByUserID = -1;
            this.TestAppointmentID = -1;
            TestAppointmentInfo = new clsTestAppointment();
            _Mode = enMode.AddNew;
        }
        private clsTest(int TestID,int TestAppointmentID,bool Result,string Notes,int CreatedByUserID)
        {
            
            this.TestID = TestID;
            this.TestAppointmentID = TestAppointmentID;
            TestAppointmentInfo = clsTestAppointment.FindAppointment(TestAppointmentID);
            this.TestResult = Result;
            this.Notes = Notes;
            this.CreatedByUserID = CreatedByUserID;
            _Mode = enMode.Update;
        }

        public static clsTest FindTestInfoByID(int TestID)
        {

            int TestAppointmentID = -1;
            bool TestResult = false;
            string Notes = "";
            int CreatedByUserID = -1;
            

            if (clsTestData.FindTestByID(TestID,
                ref TestAppointmentID, ref TestResult,
                ref Notes, ref CreatedByUserID))
            {
                return new clsTest(TestID, TestAppointmentID,
                    TestResult, Notes,CreatedByUserID);
            }
            else { return null; }
        }
        public static clsTest FindTestInfoByAppointmentID(int AppointmentID)
        {

            int TestID = -1;
            bool TestResult = false;
            string Notes = "";
            int CreatedByUserID = -1;


            if (clsTestData.FindTestByAppointmentID(AppointmentID,
                ref TestID, ref TestResult,
                ref Notes, ref CreatedByUserID))
            {
                return new clsTest(TestID, AppointmentID,
                    TestResult, Notes, CreatedByUserID);
            }
            else { return null; }
        }
        bool _AddNewTest()
        {
            this.TestID = clsTestData.AddNewTest(this.TestAppointmentID,
                this.TestResult, this.Notes, this.CreatedByUserID);

            return TestID != -1;
        }
        bool _UpdateTest()
        {
            return clsTestData.UpdateTestInfo(this.TestID, this.TestAppointmentID, 
                this.TestResult, this.Notes, this.CreatedByUserID);
           
        }
        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTest())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateTest();


            }
            return false;
        }
        public static int GetPassedTestCount(int LDLApplicationID)
        {
            return clsTestData.GetPassedTestsCount(LDLApplicationID);
        }

        public static bool DoesPassedAllTests(int LDLApplicationID)
        {
            return GetPassedTestCount(LDLApplicationID)==3;
        }

    }
}
