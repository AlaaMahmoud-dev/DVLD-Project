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
    public partial class frmDetainDrivingLicense : Form
    {
        int _LicenseID = -1;
        public frmDetainDrivingLicense()
        {
            InitializeComponent();
        }
        void _ResetData()
        {
            _LicenseID = -1;
            btnDetain.Enabled = false;
            txtFineFees.Text = string.Empty;
            lblLicenseID.Text = "[????]";
            llShowLicensesHistory.Enabled = false;

        }
        void _LoadData()
        {
            findLocalLicenseInfoByLicenseID1.gbFilterEnabled=true;
            lblDetainDate.Text = DateTime.Now.ToString("dd,MMM,yyyy");
            lblCreatedByUserName.Text = DVLDSettings.CurrentUser.UserName;
            
        }
        private void findLocalLicenseInfoByLicenseID1_OnLicenseSelected(int obj)
        {
            _LicenseID = obj;
            if (_LicenseID==-1)
            {
                _ResetData();
                return;
            }
            lblLicenseID.Text=_LicenseID.ToString();
            if (clsDrivingLicense.isLicenseDetained(_LicenseID))
            {
                MessageBox.Show("Selected License is already Detained, Choose another License", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnDetain.Enabled = false;
                llShowLicensesHistory.Enabled = true;
                return;
            }
            btnDetain.Enabled=true;
            llShowLicensesHistory.Enabled=true;
        }

        private void frmDetainDrivingLicense_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
           
            if
                (
                   MessageBox.Show($"Are You You Want To Detain The Selected License With ID[{_LicenseID}]", "Confirmation Message"
                   , MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes
                )
            {

                clsDetainedLicenses DetainDrivingLicense = new clsDetainedLicenses();
                DetainDrivingLicense.DetainDate = DateTime.Now;
                DetainDrivingLicense.FineFees = float.Parse(txtFineFees.Text.ToString());
                DetainDrivingLicense.CreatedByUserID = DVLDSettings.CurrentUser.UserID;
                DetainDrivingLicense.IsReleased = false;
                DetainDrivingLicense.LicenseID = _LicenseID;

                if (DetainDrivingLicense.Save())
                {

                    MessageBox.Show($"License Detained Successfully With ID={DetainDrivingLicense.DetainID}", "License Detained", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblDetainID.Text = DetainDrivingLicense.DetainID.ToString();
                    llShowLicenseInfo.Enabled = true;
                }
                btnDetain.Enabled = false;
                findLocalLicenseInfoByLicenseID1.gbFilterEnabled = false;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int PersonID =
                clsApplications.Find
                (
                    clsDrivingLicense.FindDriverLicense(_LicenseID).ApplicationID
                    ).ApplicantPersonID;

            frmShowLicensesHistory showLicensesHistory = new frmShowLicensesHistory(PersonID);
            showLicensesHistory.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseInfo licenseInfo = new frmLicenseInfo(_LicenseID);
            licenseInfo.ShowDialog();
        }
    }
}
