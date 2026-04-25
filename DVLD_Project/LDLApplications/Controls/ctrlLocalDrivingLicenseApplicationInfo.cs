using DVLD_Business_Layar;
using DVLD_Project.BaseApplications.Controls;
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
    public partial class ctrlLocalDrivingLicenseApplicationInfo : UserControl
    {
        clsLDLApplication _LocalDrivingLicenseApplicationInfo;
        int _LocalDrivingLicenseApplicationID;
        clsLicense _LocalDrivingLicenseInfo;
        int _LocalDrivingLicenseID;
        public int LocalDrivingLicenseApplicationID
        {
            get
            {
                return _LocalDrivingLicenseApplicationID;
            }
        }
        public clsLDLApplication LocalDrivingLicenseApplicationInfo
        {
            get
            {
                return _LocalDrivingLicenseApplicationInfo;
            }
        }
        public int LocalDrivingLicenseID
        {
            get
            {
                return _LocalDrivingLicenseID;
            }
        }
        public ctrlLocalDrivingLicenseApplicationInfo()
        {
            InitializeComponent();
        }

        private void _ResetDefaultValues()
        {
            _LocalDrivingLicenseID = -1;
            _LocalDrivingLicenseInfo = new clsLicense();
            _LocalDrivingLicenseApplicationInfo = new clsLDLApplication();
            _LocalDrivingLicenseApplicationID = -1;
            llLocalDrivingLicenseInfo.Visible = false;
            lblDLAppID.Text = "[????]";
            lblLicenseClass.Text = "[????]";
            lblCountPassedTests.Text = "[????]";
            ctrlBasicApplicationInfo1.ResetDefaultValues();
        }

        private void _FillData()
        {
            
            _LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplicationInfo.LDLApplicationID;
            _LocalDrivingLicenseInfo = clsLicense.FindActiveDriverLicenseByApplicationID(_LocalDrivingLicenseApplicationInfo.ApplicationID);
            llLocalDrivingLicenseInfo.Visible = (_LocalDrivingLicenseInfo != null);
            _LocalDrivingLicenseID = _LocalDrivingLicenseInfo != null ? _LocalDrivingLicenseInfo.LicenseID : -1;
            lblDLAppID.Text = _LocalDrivingLicenseApplicationID.ToString();
            lblLicenseClass.Text = clsLicenseClass.GetLicenseClassName(
                (clsLicenseClass.enLicenseClass)_LocalDrivingLicenseApplicationInfo.LicenseClassID);
            lblCountPassedTests.Text = _LocalDrivingLicenseApplicationInfo.PassedTests().ToString();
        }
        public void LoadLocalDrivingLicenseApplicationInfo(int LDLApplicationID)
        {
            _LocalDrivingLicenseApplicationInfo = clsLDLApplication.FindLocalDrivingLicenseAppInfoByID(LDLApplicationID);

            if (_LocalDrivingLicenseApplicationInfo == null )
            {
                _ResetDefaultValues();
                MessageBox.Show($"Cannot Find L.D.L Application With ID [{LDLApplicationID}]", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillData();

        }
        public void LoadLocalDrivingLicenseApplicationInfoByBaseApplicationID(int BaseApplicationID)
        {
            _LocalDrivingLicenseApplicationInfo = clsLDLApplication.FindLocalDrivingLicenseAppInfoByBaseAppID(BaseApplicationID);

            if (_LocalDrivingLicenseApplicationInfo == null)
            {
                _ResetDefaultValues();
                MessageBox.Show($"Cannot Find L.D.L Application With ID [{BaseApplicationID}]", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillData();


        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void ctrlLDLAndBasicApplicationsDetails_Load(object sender, EventArgs e)
        {

        }

        private void llPersonCard_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmPersonDetails personDetails = new frmPersonDetails(_LocalDrivingLicenseApplicationInfo.ApplicantPersonID);
            personDetails.ShowDialog();
        }

        private void llLocalDrivingLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseInfo licenseInfo = new frmLicenseInfo(_LocalDrivingLicenseID);
            licenseInfo.ShowDialog();
        }
    }
}
