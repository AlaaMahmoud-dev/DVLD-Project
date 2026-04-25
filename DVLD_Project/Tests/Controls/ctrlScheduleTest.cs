using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Business_Layar;

namespace DVLD_Project.Tests.Controls
{
    public partial class ctrlScheduleTest: UserControl
    {
        public enum enMode
        {
            AddNew,
            Update
        }
        public enum enScheduleMode
        {
            FirstTimeTestSchedule,
            RetakeTestSchedule
        }

        private enMode _Mode;
        private enScheduleMode _ScheduleMode;

        private clsTestType.enTestType _TestType;

        public clsTestType.enTestType TestType
        {
            get
            {
                return _TestType;
            }
            set
            {
                _TestType = value;
                switch (_TestType)
                {
                    case clsTestType.enTestType.VisionTest:
                        pbTestTypePic.Image = Properties.Resources.Vision_512;
                        gbScheduleTestType.Text = "Vision Test";
                        break;
                    case clsTestType.enTestType.WrittenTest:
                        pbTestTypePic.Image = Properties.Resources.Written_Test_512;
                        gbScheduleTestType.Text = "Written Test";
                        break;
                    case clsTestType.enTestType.PracticalTest:
                        pbTestTypePic.Image = Properties.Resources.Street_Test_32;
                        gbScheduleTestType.Text = "Practical Test";
                        break;
                }
            }
        }

        private int _LocalDrivingLicenseApplicationID;
        private clsLDLApplication _LDLApplicationInfo;
        private clsTestAppointment _TestAppointmentInfo;
        private int _TestAppointmentID;
        public ctrlScheduleTest()
        {
            InitializeComponent();
          

        }
        private bool _HandleAppointmentLockedConstraint()
        {
            if (_TestAppointmentInfo.isLocked)
            {
                lblUserMessage.Visible = true;
                lblUserMessage.Text = "person already sat for the test, appointment locked";
                btnSave.Enabled = false;
                dtpTestAppointmentDate.Enabled = false;
                return false;
            }
            return true;
        }
        private bool _HandleActiveScheduleSituation()
        {
            if (_Mode==enMode.AddNew && clsLDLApplication.IsThereAnActiveScheduledTest(_LocalDrivingLicenseApplicationID, (int)TestType))
            {
                lblUserMessage.Visible = true;
                lblUserMessage.Text = "There is already an active schedule for this LDL Application";
                btnSave.Enabled = false;
                dtpTestAppointmentDate.Enabled = false;
                return false;

            }
            return true;
        }
        private bool _HandlePreviousTestNotPassed()
        {
            if (!clsLDLApplication.DoesPassPreviousTest(_LocalDrivingLicenseApplicationID,TestType))
            {
                lblUserMessage.Visible = true;
                lblUserMessage.Text = $"Cannot schedule, {clsTestType.GetTestTypeTitle(clsTestType.GetPreviousTestTypeID(_TestType))} should be passed first";
                btnSave.Enabled = false;
                dtpTestAppointmentDate.Enabled = false;
                return false;
            }
            return true;
        }
        private bool _HandleTestPassed()
        {
            if (clsLDLApplication.DoesPassTest(_LocalDrivingLicenseApplicationID, TestType))
            {
                lblUserMessage.Visible = true;
                lblUserMessage.Text = "This test type is already passed for the selected Local Driving License Application";
                btnSave.Enabled = false;
                dtpTestAppointmentDate.Enabled = false;
                return false; 
            }
            return true;
        }
        
       
        private bool _LoadTestAppointmentData()
        {

            _TestAppointmentInfo = clsTestAppointment.FindAppointment(_TestAppointmentID);
            if (_TestAppointmentInfo == null)
            {
                MessageBox.Show("Appointment is not found", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return false;
            }
            lblTestFees.Text = _TestAppointmentInfo.PaidFees.ToString();
            

            if (DateTime.Compare(DateTime.Now, _TestAppointmentInfo.AppointmentDate) < 0)
            {
                dtpTestAppointmentDate.MinDate = DateTime.Now;
            }
            else
            {
                dtpTestAppointmentDate.MinDate = _TestAppointmentInfo.AppointmentDate;
            }
            dtpTestAppointmentDate.Value = _TestAppointmentInfo.AppointmentDate;

            if (_TestAppointmentInfo.RetakeTestApplicationID!=-1)
            {
                lblRetakeApplicationID.Text = _TestAppointmentInfo.RetakeTestApplicationInfo.ApplicationID.ToString();
                lblRetakeApplicationFees.Text = _TestAppointmentInfo.RetakeTestApplicationInfo.PaidFees.ToString();
            }
            else
            {
                lblRetakeApplicationID.Text = "N/A";
                lblRetakeApplicationFees.Text = "0";
            }
            

            if (!_HandleActiveScheduleSituation())
                return false;
            if (!_HandlePreviousTestNotPassed())
                return false;
            if (!_HandleTestPassed())
                return false;
            if (!_HandleAppointmentLockedConstraint())
                return false;
            
            return true;


        }
        private void _ResetDefaultValues()
        {
            _LDLApplicationInfo = new clsLDLApplication();
            _TestAppointmentInfo = new clsTestAppointment();
            _TestAppointmentID = -1;
            _LocalDrivingLicenseApplicationID = -1;
            lblRetakeApplicationID.Text = "N/A";
            lblRetakeApplicationFees.Text = "0";
            lblTotalFees.Text = "0";
            lblDLAppID.Text = "N/A";
            lblMode.Text = "[????]";
            lblCountTrials.Text = "0";
            lblLicenseClass.Text = "[????]";
            lblPersonName.Text = "[????]";
            lblUserMessage.Visible = false;
        }
        public void LoadData(int LocalDrivingLicenseApplicationID, int AppointmentID)
        {
            _ResetDefaultValues();
            _Mode = AppointmentID == -1 ? enMode.AddNew : enMode.Update;
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestAppointmentID = AppointmentID;
            _LDLApplicationInfo = clsLDLApplication.FindLocalDrivingLicenseAppInfoByID(_LocalDrivingLicenseApplicationID);
            if (_LDLApplicationInfo == null)
            {
                MessageBox.Show("Local Driving License Application is not found", "Not Found",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return;
            }
            if (clsLDLApplication.DoesAttendTestType(_LocalDrivingLicenseApplicationID, TestType))
                _ScheduleMode = enScheduleMode.RetakeTestSchedule;
            else
                _ScheduleMode = enScheduleMode.FirstTimeTestSchedule;

            

            lblDLAppID.Text = _LDLApplicationInfo.LDLApplicationID.ToString();
            lblLicenseClass.Text = clsLicenseClass.GetLicenseClassName((clsLicenseClass.enLicenseClass)_LDLApplicationInfo.LicenseClassID).ToString();
            lblPersonName.Text = _LDLApplicationInfo.ApplicantPersonInfo.FullName;
            lblCountTrials.Text = clsTestAppointment.TotalNumberOfTrials(_LocalDrivingLicenseApplicationID,
                (int)_TestType).ToString();
            gbRetakeApplicationInfo.Enabled = _ScheduleMode == enScheduleMode.RetakeTestSchedule;

            if (_ScheduleMode == enScheduleMode.RetakeTestSchedule)
            {
                lblRetakeApplicationFees.Text = clsApplicationType.GetApplicationFees(
                    (int)clsApplicationType.enApplicationType.RetakeTest).ToString();
                lblRetakeApplicationID.Text = "N/A";
                lblMode.Text = "Schedule Retake Test";

            }
            else
            {
                lblRetakeApplicationFees.Text = "0";
                lblMode.Text = "Schedule Test";
            }

            if (_Mode == enMode.AddNew)
            {

                _TestAppointmentInfo = new clsTestAppointment();
                dtpTestAppointmentDate.MinDate = DateTime.Now;
                lblTestFees.Text = clsTestType.TestFees((int)_TestType).ToString();
            }
            else
            {
                if (!_LoadTestAppointmentData())
                    return;
            }
            lblTotalFees.Text = (float.Parse(lblTestFees.Text) + float.Parse(lblRetakeApplicationFees.Text)).ToString();
            
        }
        private bool _HandleRetakeTest()
        {
            if (_ScheduleMode==enScheduleMode.RetakeTestSchedule&&_Mode==enMode.AddNew)
            {
                clsApplication RetakeTestApplication = new clsApplication();
                RetakeTestApplication.ApplicationTypeID = clsApplicationType.enApplicationType.RetakeTest;
                RetakeTestApplication.ApplicantPersonID = _LDLApplicationInfo.ApplicantPersonID;
                RetakeTestApplication.ApplicationDate = DateTime.Now;
                RetakeTestApplication.PaidFees = clsApplicationType.GetApplicationFees((int)clsApplicationType.enApplicationType.RetakeTest);
                RetakeTestApplication.ApplicationStatus = clsApplication.enApplicationStatus.New;
                RetakeTestApplication.LastStatusDate = DateTime.Now;
                RetakeTestApplication.CreatedByUserID = DVLDSettings.CurrentUser.UserID;
                
                if (!RetakeTestApplication.Save())
                {
                    MessageBox.Show("Error: Saving Retake Test Application's Data Failed", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                _TestAppointmentInfo.RetakeTestApplicationID = RetakeTestApplication.ApplicationID;
                
            }
            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_HandleRetakeTest())
            {
                return;
            }
            
           
            _TestAppointmentInfo.AppointmentDate = dtpTestAppointmentDate.Value;
            _TestAppointmentInfo.LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplicationID;
            _TestAppointmentInfo.CreatedByUserID = DVLDSettings.CurrentUser.UserID;
            _TestAppointmentInfo.TestTypeID = (int)_TestType;
            _TestAppointmentInfo.PaidFees = Convert.ToSingle(lblTestFees.Text);
            _TestAppointmentInfo.isLocked = false;


            if (_TestAppointmentInfo.Save())
            {
                _Mode = enMode.Update;
                MessageBox.Show("Data Saved Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error: Saving Data Failed", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
