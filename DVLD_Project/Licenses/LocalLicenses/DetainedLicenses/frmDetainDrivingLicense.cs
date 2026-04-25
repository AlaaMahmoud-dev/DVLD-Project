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
        int _DetainID;
        int _LicenseID = -1;
        public frmDetainDrivingLicense(int LicenseID = -1)
        {
            InitializeComponent();
            _DetainID = -1;
            _LicenseID = LicenseID;
        }
        void _ResetData()
        {
            _LicenseID = -1;
            btnDetain.Enabled = false;
            txtFineFees.Text = string.Empty;
            lblLicenseID.Text = "[????]";
            llShowLicensesHistory.Enabled = false;

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
            llShowLicenseInfo.Enabled = true;

            if (clsLicense.isLicenseDetained(_LicenseID))
            {
                MessageBox.Show("Selected License is already Detained, Choose another License",
                    "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnDetain.Enabled = false;
                llShowLicensesHistory.Enabled = true;
                return;
            }
            btnDetain.Enabled=true;
            llShowLicensesHistory.Enabled=true;
        }

        private void frmDetainDrivingLicense_Load(object sender, EventArgs e)
        {
            lblDetainDate.Text = DateTime.Now.ToString("dd,MMM,yyyy");
            lblCreatedByUserName.Text = DVLDSettings.CurrentUser.UserName;
            findLocalLicenseInfoByLicenseID1.gbFilterEnabled = _LicenseID == -1;
            btnDetain.Enabled = _LicenseID != -1;
            if (_LicenseID != -1)
            {
                lblLicenseID.Text = _LicenseID.ToString();
                llShowLicenseInfo.Enabled = true;


            }
            if (_LicenseID!=-1 && clsLicense.isLicenseDetained(_LicenseID))
            {
                MessageBox.Show("Selected License is already Detained, Choose another License", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnDetain.Enabled = false;
                llShowLicensesHistory.Enabled = true;
                return;
            }
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Missing Data, put mouse over red icons to see the error",
                    "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if
                (
                   MessageBox.Show($"Are You Sure You Want To Detain The Selected License With ID[{_LicenseID}]",
                   "Confirmation Message", MessageBoxButtons.YesNo, 
                   MessageBoxIcon.Question) == DialogResult.Yes
                )
            {
                _DetainID = findLocalLicenseInfoByLicenseID1.LicenseInfo.Detain(float.Parse(txtFineFees.Text), DVLDSettings.CurrentUser.UserID);
              

                if (_DetainID!=-1)
                {
                    MessageBox.Show($"License Detained Successfully With ID={_DetainID}", 
                        "License Detained", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblDetainID.Text = _DetainID.ToString();
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
            int PersonID = findLocalLicenseInfoByLicenseID1.LicenseInfo.DriverInfo.PersonID;
            frmShowLicensesHistory showLicensesHistory = new frmShowLicensesHistory(PersonID);
            showLicensesHistory.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseInfo licenseInfo = new frmLicenseInfo(_LicenseID);
            licenseInfo.ShowDialog();
        }

        private void txtFineFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFineFees.Text))
            {
                errorProvider1.SetError(txtFineFees, "Don't forget to set a fine on the selected license");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtFineFees,"");
                e.Cancel = false;
            }
        }
    }
}
