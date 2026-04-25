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
    public partial class ctrlScheduledTest: UserControl
    {
        int _AppointmentID=-1;
        public int AppointmentID
        {
            get
            {
                return _AppointmentID;
            }
        }
        clsTestAppointment _TestAppointmentInfo;
        clsTestType.enTestType _TestTypeID;
        int _TestID=-1;
        public int TestID
        {
            private set { _TestID = value; }
            get
            {
                return _TestID;
            }
        }
        clsTest _TestInfo;

        public clsTestType.enTestType TestTypeID
        {
            set
            {
                _TestTypeID = value;
                if (_TestTypeID == clsTestType.enTestType.VisionTest)
                {
                    this.Text = "Vision Test Appointment";
                    lblAppointmentType.Text = "Vision Test Appointment";

                    pbTestTypePic.BackgroundImage = Properties.Resources.Vision_512;
                }
                else if (_TestTypeID == clsTestType.enTestType.WrittenTest)
                {
                    this.Text = "Written Test Appointment";
                    lblAppointmentType.Text = "Written Test Appointment";
                    pbTestTypePic.BackgroundImage = Properties.Resources.Written_Test_512;
                }
                else
                {
                    this.Text = "Street Test Appointment";

                    lblAppointmentType.Text = "Street Test Appointment";
                    pbTestTypePic.BackgroundImage = Properties.Resources.Street_Test_32;

                }
            }
            get
            {
                return _TestTypeID;
            }
        }
        public ctrlScheduledTest()
        {
            InitializeComponent();
        }
        private void _LoadTestInfo()
        {
            _TestInfo = clsTest.FindTestInfoByAppointmentID(AppointmentID);
            if (_TestInfo==null)
            {
                TestID = -1;
                return;
            }
            TestID = _TestInfo.TestID;
            lblTestID.Text = TestID.ToString();
        }
        public void LoadData(int AppointmentID)
        {
            _AppointmentID = AppointmentID;
            _TestAppointmentInfo = clsTestAppointment.FindAppointment(AppointmentID);
            if (_TestAppointmentInfo==null)
            {
                _AppointmentID = -1;
                MessageBox.Show("Test Appointment Data not found", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _LoadTestInfo();
           
            lblDLAppID.Text = _TestAppointmentInfo.LocalDrivingLicenseApplicationID.ToString();
            lblLicenseClass.Text=_TestAppointmentInfo.LocalDrivingLicenseApplicationInfo.GetLicenseClassText();
            lblPersonName.Text = _TestAppointmentInfo.LocalDrivingLicenseApplicationInfo.ApplicantPersonInfo.FullName;
            lblDate.Text = _TestAppointmentInfo.AppointmentDate.ToString();
            lblCountTrials.Text = clsTestAppointment.TotalNumberOfTrials(_TestAppointmentInfo.LocalDrivingLicenseApplicationID, _TestAppointmentInfo.TestTypeID).ToString();
            lblTestFees.Text = clsTestType.TestFees(_TestAppointmentInfo.TestTypeID).ToString();
            

        }
    }
}
