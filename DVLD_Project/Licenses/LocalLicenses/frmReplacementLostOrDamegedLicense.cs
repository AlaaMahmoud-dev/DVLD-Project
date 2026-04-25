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
    public partial class frmReplacementLostOrDamegedLicense : Form
    {
        private int _NewLicenseID;
        public frmReplacementLostOrDamegedLicense()
        {
            InitializeComponent();

          

        }

        void _LoadData()
        {

            lblOldLicenseID.Text = ctrlLocalLicenseInfoWithFilter1.LicenseID.ToString();
            btnIssue.Enabled = true;
            llShowLicensesHistory.Enabled = true;
        }
        void _LoadDataAccordingToApplicationType()
        {
            

            lblTitle.Text = rbDamagedLicense.Checked ? "Replacement For Damaged" : "Replacement For Lost";
            this.Text = lblTitle.Text;
            lblApplicationFees.Text = rbDamagedLicense.Checked ?
                clsApplicationType.GetApplicationFees(
                    (int)clsApplicationType.enApplicationType.ReplacementForDamagedDrivingLicense).ToString() 
                    : clsApplicationType.GetApplicationFees(
                        (int)clsApplicationType.enApplicationType.ReplacementForLostDrivingLicense).ToString();

        }
        private void frmReplacementLostOrDamegedLicense_Load(object sender, EventArgs e)
        {
            rbDamagedLicense.Checked = true;
            _LoadDataAccordingToApplicationType();
            lblApplicationDate.Text = DateTime.Now.ToString("dd,MMM,yyyy");
            lblCreatedByUserName.Text = DVLDSettings.CurrentUser.UserName;
        }

        void _ResetData()
        {
            lblOldLicenseID.Text = "[????]";
            llShowLicensesHistory.Enabled = false;
            btnIssue.Enabled = false;
        }
        private void findLocalLicenseInfoByLicenseID1_OnLicenseSelected(int obj)
        {
            int LicenseID = obj;
            if (LicenseID == -1)
            {
                _ResetData();
                return;
            }
            if (!ctrlLocalLicenseInfoWithFilter1.LicenseInfo.isActive)
            {
                btnIssue.Enabled = false;
                MessageBox.Show($"Selected License With ID={ctrlLocalLicenseInfoWithFilter1.LicenseID} is not Active, Select An active License", 
                    "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _LoadData();
            
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {

            
            if
               (
                  MessageBox.Show($"Are You You Want To Replace The Selected License With ID[{ctrlLocalLicenseInfoWithFilter1.LicenseID}]", 
                  "Confirmation Message"
                  , MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes
               )
            {
                


                clsLicense NewDrivingLicense = rbDamagedLicense.Checked ?
                    ctrlLocalLicenseInfoWithFilter1.LicenseInfo.Replace(
                        clsLicense.enIssueReason.DamagedReplacement, DVLDSettings.CurrentUser.UserID)
                    :ctrlLocalLicenseInfoWithFilter1.LicenseInfo.Replace(
                        clsLicense.enIssueReason.LostReplacement, DVLDSettings.CurrentUser.UserID);


                if (NewDrivingLicense == null)
                {
                    MessageBox.Show("Error Occurred, Failed To Issue New Driving License", "Error Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
               
                _NewLicenseID = NewDrivingLicense.LicenseID;
                lblNewLicenseID.Text = NewDrivingLicense.LicenseID.ToString();
                lblRenewLicenseApplicationID.Text = NewDrivingLicense.ApplicationID.ToString();
                llShowLicensesHistory.Enabled = true;
                llShowNewLicenseInfo.Enabled = true;
                MessageBox.Show($"License Replaced Successfully With ID={NewDrivingLicense.LicenseID}",
                    "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);
                gbReplacementFor.Enabled = false;
                ctrlLocalLicenseInfoWithFilter1.gbFilterEnabled = false;
                btnIssue.Enabled = false;
            }
        }

        void _ReplacementReasonChanged()
        {
            _LoadDataAccordingToApplicationType();

        }
        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            _ReplacementReasonChanged();
        }

        private void rbLostLicense_CheckedChanged(object sender, EventArgs e)
        {
            _ReplacementReasonChanged();
        }

        private void llShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicensesHistory licensesHistory = new frmShowLicensesHistory(
                ctrlLocalLicenseInfoWithFilter1.LicenseInfo.ApplicationInfo.ApplicantPersonID);
            licensesHistory.ShowDialog();
        }

        private void llShowNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseInfo NewLicenseInfo = new frmLicenseInfo(_NewLicenseID);
            NewLicenseInfo.ShowDialog();
        }
    }
}
