using DVLD_Business_Layar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project
{
    public partial class frmScheduleTest : Form
    {
        clsApplications _BasicApplication = null;
        clsLDLApplications _LDLApplication=null;
        clsTestAppointments _TestAppointment = null;
        int _AppointmentID = -1;
        int _LDLApplicationID = -1;
        int _TestTypeID = 0;
        bool _isRetakeTest=false;
        enum enTestType
        {
            Vision = 1,
            Written = 2,
            Street = 3
        }

        enTestType testType = enTestType.Vision;

        enum enMode
        {
            AddNew = 1,
            Update = 2
            
        }

        enMode Mode = enMode.AddNew;

        public frmScheduleTest(int AppointmentID,int LDLApplicationID, int TestTypeID, bool isRetakeTest = false)
        {
            InitializeComponent();
            _BasicApplication = new clsApplications();
            _LDLApplication = new clsLDLApplications();
            _TestAppointment = new clsTestAppointments();
            _AppointmentID = AppointmentID;
            _LDLApplicationID = LDLApplicationID;
            _TestTypeID = TestTypeID;

            if(_AppointmentID==-1)
            {
                Mode = enMode.AddNew;
            }
            else
            {
                Mode = enMode.Update;
            }


            if (TestTypeID == 1)
            {
                testType = enTestType.Vision;
                gbScheduleTestType.Text = "Vision Test";
                pbTestTypePic.BackgroundImage = Properties.Resources.Vision_512;
            }
            else if (TestTypeID == 2)
            {
                testType = enTestType.Written;
                gbScheduleTestType.Text = "Written Test";
                pbTestTypePic.BackgroundImage = Properties.Resources.Written_Test_512;
            }
            else
            {

                testType = enTestType.Street;
                gbScheduleTestType.Text = "Street Test";
                pbTestTypePic.BackgroundImage = Properties.Resources.Street_Test_32;
            }

            if (isRetakeTest)
            {

                lblRetakeApplicationFees.Text = clsApplicationTypes.ApplicationFees(8).ToString();
                gbRetakeApplicationInfo.Enabled = true;
                _isRetakeTest = true;
            }

        }

        private void frmScheduleTest_Load(object sender, EventArgs e)
        {

            if (Mode==enMode.Update)
            {
                _TestAppointment = clsTestAppointments.FindAppointment(_AppointmentID);
                dtpTestAppointmentDate.Value = _TestAppointment.AppointmentDate;
                if (_TestAppointment.isLocked)
                {
                    btnSave.Enabled = false;
                    dtpTestAppointmentDate.Enabled = false;
                    lblTestLockedMessege.Visible = true;
                }
            }

            _LDLApplication = clsLDLApplications.Find(_LDLApplicationID);
             _BasicApplication = clsApplications.Find(_LDLApplication.ApplicationID);

            if (_LDLApplication == null)
            {
                MessageBox.Show($"Cannot Find L.D.L Application With ID [{_LDLApplicationID}]", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            lblDLAppID.Text=_LDLApplication.LDLApplicationID.ToString();
            lblLicenseClass.Text=_LDLApplication.LicenseClassName.ToString();
            lblPersonName.Text = clsPerson.FindPerson(_BasicApplication.ApplicantPersonID).FullName;
            lblTestFees.Text = clsTestTypes.TestFees(_TestTypeID).ToString();
            lblCountTrials.Text = clsTestAppointments.TotalNumberOfTrials(_LDLApplicationID,_TestTypeID).ToString();
            if (!_isRetakeTest)
                lblRetakeApplicationFees.Text = "0";

            lblTotalFees.Text =( Convert.ToInt32(lblTestFees.Text.ToString()) + Convert.ToInt32(lblRetakeApplicationFees.Text.ToString())).ToString();


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
             
            _TestAppointment.TestTypeID= _TestTypeID;
            _TestAppointment.LocalDrivingLicenseApplicationID = _LDLApplicationID;
            _TestAppointment.AppointmentDate = dtpTestAppointmentDate.Value;
            _TestAppointment.PaidFees=float.Parse (lblTestFees.Text.ToString());
            _TestAppointment.CreatedByUserID=DVLDSettings.CurrentUser.UserID;
            _TestAppointment.isLocked=false;





            if (_isRetakeTest)
            {
                clsApplications RetakeTestApplicaiton = new clsApplications();
                RetakeTestApplicaiton.ApplicantPersonID = _BasicApplication.ApplicantPersonID;
                RetakeTestApplicaiton.ApplicationDate = DateTime.Now;
                RetakeTestApplicaiton.ApplicationTypeID = 8;
                RetakeTestApplicaiton.ApplicationStatus = "New";
                RetakeTestApplicaiton.LastStatusDate = DateTime.Now;
                RetakeTestApplicaiton.PiadFees = float.Parse(lblRetakeApplicationFees.Text.ToString());
                RetakeTestApplicaiton.CreatedByUserID = DVLDSettings.CurrentUser.UserID;
                if (RetakeTestApplicaiton.Save() && _TestAppointment.Save())
                {
                    MessageBox.Show("Data Saved Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblRetakeApplicationID.Text = RetakeTestApplicaiton.ApplicationID.ToString();
                   
                }
                else
                {
                    MessageBox.Show("Saving Data Faild", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                if (_TestAppointment.Save())
                {
                    MessageBox.Show("Data Saved Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Saving Data Faild", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            btnSave.Enabled = false;
            
           

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
