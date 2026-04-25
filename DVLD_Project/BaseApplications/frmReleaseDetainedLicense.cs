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
    public partial class frmReleaseDetainedLicense : Form
    {
        int _DetainID = -1;
        int _LicenseID;
        public clsDetainedLicense DetainedLicense
        { 
            get
            { 
                return findLocalLicenseInfoByLicenseID1.LicenseInfo.DetainedInfo; 
            } 
        }
        public frmReleaseDetainedLicense(int DetainID = -1)
        {
            
            InitializeComponent();
            _DetainID = DetainID;
            if (DetainID == -1)
            {
                findLocalLicenseInfoByLicenseID1.gbFilterEnabled = true;
            }
            else
            {
                findLocalLicenseInfoByLicenseID1.LoadLicenseInfo(clsDetainedLicense.Find(_DetainID).LicenseID);
                findLocalLicenseInfoByLicenseID1.gbFilterEnabled = false;
            }
        }
      
        void _LoadData()
        {
            lblApplicationFees.Text = clsApplicationType.GetApplicationFees(
                (int)clsApplicationType.enApplicationType.ReleaseDetainedLicense).ToString();
            btnRelease.Enabled = true;
            lblDetainID.Text = DetainedLicense.DetainID.ToString();
            lblDetainDate.Text = DetainedLicense.DetainDate.ToString();
            lblFineFees.Text = DetainedLicense.FineFees.ToString();
            lblTotalFees.Text = (float.Parse(lblApplicationFees.Text.ToString()) + float.Parse(lblFineFees.Text.ToString())).ToString();
        }
        private void frmReleaseDetainedLicense_Load(object sender, EventArgs e)
        {
            lblApplicationFees.Text = clsApplicationType.GetApplicationFees(
                (int)clsApplicationType.enApplicationType.ReleaseDetainedLicense).ToString();
            lblCreatedByUserName.Text = DVLDSettings.CurrentUser.UserName;
            if (findLocalLicenseInfoByLicenseID1.LicenseID != -1)
            {
                lblLicenseID.Text = findLocalLicenseInfoByLicenseID1.LicenseID.ToString();
                llShowLicensesHistory.Enabled = true;
                llShowLicenseInfo.Enabled = true;
            }
           

            if (findLocalLicenseInfoByLicenseID1.LicenseID != -1 && DetainedLicense.IsReleased)
            {
                MessageBox.Show("Selected License is not Detained, Choose another License",
                    "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRelease.Enabled = false;
                return;
            }


            if (_DetainID!=-1)
            {
                _LoadData();
            }
        }
        void _ResetData()
        {
            btnRelease.Enabled = false;
            lblDetainID.Text = "[????]";
            lblDetainDate.Text = "[????]";
            lblFineFees.Text = "[$$$$]";
            lblTotalFees.Text = "[$$$$]";
            lblLicenseID.Text = "[????]";

        }
        private void findLocalLicenseInfoByLicenseID1_OnLicenseSelected(int obj)
        {
            int LicenseID = obj;

            if (LicenseID==-1)
            {
                _ResetData();
                return;

            }
            if(DetainedLicense==null||DetainedLicense.IsReleased)
            {
                MessageBox.Show("Selected License is not Detained, Choose another one",
                    "Not Allowed",MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRelease.Enabled = false;
                return;
            }
            _LoadData();

        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            if
               (
                  MessageBox.Show($"Are You You Want To Release The Detained License With ID[{DetainedLicense.LicenseID}]",
                  "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes
               )
            {
                int ReleaseApplicationID = -1;

                if (findLocalLicenseInfoByLicenseID1.LicenseInfo.Release(
                    ref ReleaseApplicationID, DVLDSettings.CurrentUser.UserID)
)
                {

                    MessageBox.Show($"Detained License Released Successfully", "Detained License Released", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Error Occurred,Failed To Release Driving License", "Error Occurred", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                lblApplicationID.Text = ReleaseApplicationID.ToString();

            }

            btnRelease.Enabled = false;
            findLocalLicenseInfoByLicenseID1.gbFilterEnabled = false;
            llShowLicenseInfo.Enabled = true;

        }
        

        private void llShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int PersonID = findLocalLicenseInfoByLicenseID1.LicenseInfo.DriverInfo.PersonID;
            frmShowLicensesHistory licensesHistory = new frmShowLicensesHistory(PersonID);
            licensesHistory.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseInfo licenseInfo = new frmLicenseInfo(findLocalLicenseInfoByLicenseID1.LicenseID);
            licenseInfo.ShowDialog();
        }
    }
}
