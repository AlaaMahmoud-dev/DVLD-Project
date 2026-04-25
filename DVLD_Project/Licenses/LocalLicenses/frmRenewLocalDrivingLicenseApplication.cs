using DVLD_Business_Layar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project
{
    public partial class frmRenewLocalDrivingLicenseApplication : Form
    {
        private int _NewLicenseID;
        public frmRenewLocalDrivingLicenseApplication()
        {
            InitializeComponent();
       
        }


        void _LoadData()
        {
            lblOldLicenseID.Text = ctrlLocalLicenseInfoWithFilter1.LicenseID.ToString();
            DateTime ExpirationDate = DateTime.Now.AddYears(clsLicense.GetLicenseValidityLength(ctrlLocalLicenseInfoWithFilter1.LicenseInfo.LicenseClass));
            lblExpirationDate.Text = ( ExpirationDate ).ToString("dd,MMM,yyyy");
            lblLicenseFees.Text=clsLicenseClass.GetLicenseClassFees(ctrlLocalLicenseInfoWithFilter1.LicenseInfo.LicenseClass).ToString();
            lblTotalFees.Text=(float.Parse(lblApplicationFees.Text.ToString())+float.Parse(lblLicenseFees.Text.ToString())).ToString();
            txtNotes.Text = ctrlLocalLicenseInfoWithFilter1.LicenseInfo.Notes;
            btnRenew.Enabled = true;
            llShowLicensesHistory.Enabled = true;
        }

        void _ResetData()
        {
            lblLicenseFees.Text = "[$$$$]";
            lblTotalFees.Text = "[$$$$]";
            lblExpirationDate.Text = "[????]";
            lblOldLicenseID.Text = "[????]";
            txtNotes.Text=string.Empty;
            llShowLicensesHistory.Enabled = false;


        }

        private void frmRenewLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            lblCreatedByUserName.Text = DVLDSettings.CurrentUser.UserName;
            lblApplicationFees.Text = clsApplicationType.GetApplicationFees((int)clsApplicationType.enApplicationType.RenewDrivingLicense).ToString();
            lblApplicationDate.Text = DateTime.Now.ToString("dd,MMM,yyyy");
            lblIssueDate.Text = DateTime.Now.ToString("dd,MMM,yyyy");
            
        }

        private void findLocalLicenseInfoByLicenseID1_OnLicenseSelected(int obj)
        {

            int LicenseID=obj;
            if (LicenseID==-1)
            {
                _ResetData();
                return;
            }
            if (!ctrlLocalLicenseInfoWithFilter1.LicenseInfo.IsLicenseExpired())
            {
                _ResetData();
                MessageBox.Show($"This Driving License is not Expired Yet," +
                    $" it will expired in {ctrlLocalLicenseInfoWithFilter1.LicenseInfo.ExpirationDate.ToString("dd,MMM,yyyy")}", "Not Expired Yet"
                        , MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRenew.Enabled = false;
                return;
            }
            _LoadData();
          
            

        }

        private void btnRenew_Click(object sender, EventArgs e)
        {

            if (!ctrlLocalLicenseInfoWithFilter1.LicenseInfo.isActive)
            {
                MessageBox.Show($"Selected License With ID={ctrlLocalLicenseInfoWithFilter1.LicenseID} is not Active",
                    "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if
                (
                   MessageBox.Show($"Are You Sure You Want To Renew The Selected License With ID[{ctrlLocalLicenseInfoWithFilter1.LicenseID}]",
                   "Confirmation Message"
                   , MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes
                )
            {


                clsLicense NewDrivingLicense = ctrlLocalLicenseInfoWithFilter1.LicenseInfo.RenewLicense(txtNotes.Text, DVLDSettings.CurrentUser.UserID);

                if (NewDrivingLicense != null)
                {
                    _NewLicenseID = NewDrivingLicense.LicenseID;
                    lblNewLicenseID.Text = NewDrivingLicense.LicenseID.ToString();
                    lblRenewLicenseApplicationID.Text = NewDrivingLicense.ApplicationID.ToString();
                    llShowNewLicenseInfo.Enabled = true;
                    MessageBox.Show($"License Renewed Successfully With ID={NewDrivingLicense.LicenseID}",
                        "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error Occurred, Failed To Issue New Driving License",
                        "Error Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
            ctrlLocalLicenseInfoWithFilter1.gbFilterEnabled = false;
            btnRenew.Enabled = false;
            txtNotes.Enabled = false;
        }
        
        private void llShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicensesHistory licensesHistory = new frmShowLicensesHistory(ctrlLocalLicenseInfoWithFilter1.LicenseInfo.ApplicationInfo.ApplicantPersonID);
            licensesHistory.ShowDialog();
        }

        private void llShowNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseInfo NewLicenseInfo = new frmLicenseInfo(_NewLicenseID);
            NewLicenseInfo.ShowDialog();
        }
    }
}
