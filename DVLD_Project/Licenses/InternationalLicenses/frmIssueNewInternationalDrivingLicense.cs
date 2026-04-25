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
    public partial class frmIssueNewInternationalDrivingLicense : Form
    {
        int _IntLicenseID = -1;
        public frmIssueNewInternationalDrivingLicense()
        {
            InitializeComponent();
         
        }

        void _ResetDefaultValues()
        {
            btnIssue.Enabled = false;
            lblApplicationID.Text = "[????]";
            lblInternationalLicenseID.Text = lblApplicationID.Text;
            lblLocalLicenseID.Text = lblApplicationID.Text;
        }

        void _LoadData()
        {
            
            llShowLicenseInfo.Enabled = false;
            llShowLicensesHistory.Enabled = false;
            lblApplicationDate.Text = DateTime.Now.ToString("dd,MMM,yyyy");
            lblIssueDate.Text = DateTime.Now.ToString("dd,MMM,yyyy");
            lblCreatedByUserName.Text = DVLDSettings.CurrentUser.UserName;
            lblExpirationDate.Text = DateTime.Now.AddYears(1).ToString("dd,MMM,yyyy");
            lblFees.Text=clsApplicationType.GetApplicationFees(
                (int)(clsApplicationType.enApplicationType.NewInternationalDrivingLicense)).ToString();

        }
        private void frmIssueNewInternationalDrivingLicense_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void findLocalLicenseInfoByLicenseID1_OnLicenseSelected(int obj)
        {
            int LicenseID = obj;


            if (LicenseID == -1)
            {
                _ResetDefaultValues();
                return;
            }

           
            llShowLicensesHistory.Enabled = true;
            lblLocalLicenseID.Text = LicenseID.ToString();
            if (findLocalLicenseInfoByLicenseID1.LicenseInfo.LicenseClass != (int)clsLicenseClass.enLicenseClass.OrdinaryDrivingLicense)
            {
                MessageBox.Show("Can't Issue International Driving License On This License Class , Please Select License With 'Ordinary Driving License' Class",
                    "Wrong License Class", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnIssue.Enabled = false;
                return;
            }

            if (findLocalLicenseInfoByLicenseID1.LicenseInfo.IsLicenseExpired() 
                || !findLocalLicenseInfoByLicenseID1.LicenseInfo.isActive)
            {
                MessageBox.Show("Selected License Is Expired or Deactivated, Select another one",
                    "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnIssue.Enabled = false;

                return;
            }
            int ActiveInternationalLicenseID = clsInternationalDrivingLicense.GetActiveInternationalLicenseIDForDriver(findLocalLicenseInfoByLicenseID1.LicenseInfo.DriverID);

            if (ActiveInternationalLicenseID != -1)
            {
                _IntLicenseID = ActiveInternationalLicenseID;
                MessageBox.Show($"Person Already Has an Active International License With ID [{ActiveInternationalLicenseID}]", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssue.Enabled = false;
                llShowLicenseInfo.Enabled = true;
                return;
            }
            btnIssue.Enabled = true;
        }
            
        private void btnIssue_Click(object sender, EventArgs e)
        {
            if
                (
                   MessageBox.Show($"Are You Sure You Want To Issue a New International License for a Local License With ID[{findLocalLicenseInfoByLicenseID1.LicenseID}]", "Confirmation Message"
                   , MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes
                )
            {
                clsInternationalDrivingLicense internationalDrivingLicense =
                    clsInternationalDrivingLicense.IssueInternationalDrivingLicense(
                        findLocalLicenseInfoByLicenseID1.LicenseID);

                if (internationalDrivingLicense == null)
                {
                    MessageBox.Show("Error Occurred While Trying to Issue New International Driving License, Process Failed",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show($"New International License Was Issued Successfully With ID[{internationalDrivingLicense.InternationalLicenseID}]",
                    "Saved Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _IntLicenseID = internationalDrivingLicense.InternationalLicenseID;
                lblApplicationID.Text = internationalDrivingLicense.ApplicationID.ToString();
                lblInternationalLicenseID.Text = internationalDrivingLicense.InternationalLicenseID.ToString();
                btnIssue.Enabled = false;
                llShowLicenseInfo.Enabled = true;
                findLocalLicenseInfoByLicenseID1.gbFilterEnabled = false;
            }

        }

        private void llShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicensesHistory licensesHistory = new frmShowLicensesHistory(
                findLocalLicenseInfoByLicenseID1.LicenseInfo.DriverInfo.PersonID);
            licensesHistory.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowInternationalDrivingLicenseDetails showInternationalDrivingLicenseDetails =
                new frmShowInternationalDrivingLicenseDetails(_IntLicenseID);
            showInternationalDrivingLicenseDetails.Show();
        }
    }
}
