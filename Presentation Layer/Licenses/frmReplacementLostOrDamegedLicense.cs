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
        clsDrivingLicense _OldDrivingLicenseInfo = null;
        clsApplications _OldLicenseApplication = null;
       
        public frmReplacementLostOrDamegedLicense()
        {
            InitializeComponent();

            _OldDrivingLicenseInfo = new clsDrivingLicense();
            _OldLicenseApplication = new clsApplications();

        }

        void _LoadData()
        {

            lblOldLicenseID.Text = _OldDrivingLicenseInfo.LicenseID.ToString();
            lblExpirationDate.Text = DateTime.Now.AddYears(clsDrivingLicense.GetLicenseValidityLength(_OldDrivingLicenseInfo.LicenseClass)).ToString("dd,mmm,yyyy");
            btnIssue.Enabled = true;
            llShowLicensesHistory.Enabled = true;
            lblLicenseFees.Text = clsDrivingLicense.GetLicenseClassFees(_OldDrivingLicenseInfo.LicenseClass).ToString();
            lblTotalFees.Text = (float.Parse(lblApplicationFees.Text.ToString()) + float.Parse(lblLicenseFees.Text.ToString())).ToString();
        }
        void _LoadDataAccordingToApplicationType()
        {
            if (!rbDamagedLicense.Checked && !rbLostLicense.Checked)
            {
                rbLostLicense.Checked = true;
            }

            lblApplicationType.Text = rbDamagedLicense.Checked ? "Replacement For Damaged" : "Replacement For Lost";
            lblApplicationFees.Text = rbDamagedLicense.Checked ? clsApplicationTypes.ApplicationFees(4).ToString() : clsApplicationTypes.ApplicationFees(3).ToString();

        }
        private void frmReplacementLostOrDamegedLicense_Load(object sender, EventArgs e)
        {
            _LoadDataAccordingToApplicationType();
            lblApplicationDate.Text = DateTime.Now.ToString("dd,MMM,yyyy");
            lblIssueDate.Text = DateTime.Now.ToString("dd,MMM,yyyy");
            lblCreatedByUserName.Text = DVLDSettings.CurrentUser.UserName;
        }

        void _ResetData()
        {

            _OldDrivingLicenseInfo = new clsDrivingLicense();
            _OldLicenseApplication = new clsApplications();
            lblLicenseFees.Text = "[$$$$]";
            lblTotalFees.Text = "[$$$$]";
            lblExpirationDate.Text = "[????]";
            lblOldLicenseID.Text = "[????]";
            txtNotes.Text = string.Empty;
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
            _OldDrivingLicenseInfo = clsDrivingLicense.FindDriverLicense(LicenseID);
            _OldLicenseApplication = clsApplications.Find(_OldDrivingLicenseInfo.ApplicationID);
            if (_OldLicenseApplication != null&&_OldDrivingLicenseInfo!=null) 
            {
                _LoadData();
            }




        }

        private void btnIssue_Click(object sender, EventArgs e)
        {

            if (!_OldDrivingLicenseInfo.isActive)
            {
                MessageBox.Show($"Selected License With ID={_OldDrivingLicenseInfo.LicenseID} is not Active, Select An active License", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if
               (
                  MessageBox.Show($"Are You You Want To Replace The Selected License With ID[{_OldDrivingLicenseInfo.LicenseID}]", "Confirmation Message"
                  , MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes
               )
            {
                // _OldLicenseApplication = clsApplications.Find(_OldDrivingLicenseInfo.ApplicationID);

                clsApplications ReplacementLicenseApplication = new clsApplications();
                ReplacementLicenseApplication.ApplicantPersonID = _OldLicenseApplication.ApplicantPersonID;
                ReplacementLicenseApplication.ApplicationDate = DateTime.Now;

                ReplacementLicenseApplication.ApplicationStatus = "New";
                ReplacementLicenseApplication.LastStatusDate = DateTime.Now;

                ReplacementLicenseApplication.PiadFees = float.Parse(lblApplicationFees.Text.ToString());
                ReplacementLicenseApplication.CreatedByUserID = DVLDSettings.CurrentUser.UserID;



                clsDrivingLicense NewDrivingLicense = new clsDrivingLicense();

                NewDrivingLicense.DriverID = _OldDrivingLicenseInfo.DriverID;
                NewDrivingLicense.LicenseClass = _OldDrivingLicenseInfo.LicenseClass;
                NewDrivingLicense.IssueDate = DateTime.Now;
                NewDrivingLicense.ExpirationDate = DateTime.Now.AddYears(clsDrivingLicense.GetLicenseValidityLength(NewDrivingLicense.LicenseClass));
                NewDrivingLicense.Notes = txtNotes.Text.ToString();
                NewDrivingLicense.PaidFees = float.Parse(lblLicenseFees.Text.ToString());
                NewDrivingLicense.isActive = true;
                NewDrivingLicense.CreatedByUserID = DVLDSettings.CurrentUser.UserID;

                NewDrivingLicense.IssueReasone = rbLostLicense.Checked ? 3 : 4;
                ReplacementLicenseApplication.ApplicationTypeID = rbLostLicense.Checked? 3 : 4;




                _OldDrivingLicenseInfo.isActive = false;

                if (ReplacementLicenseApplication.Save())
                {
                    NewDrivingLicense.ApplicationID = ReplacementLicenseApplication.ApplicationID;

                    if (_OldDrivingLicenseInfo.Save() && NewDrivingLicense.Save())
                    {
                        lblNewLicenseID.Text = NewDrivingLicense.LicenseID.ToString();
                        lblRenewLicenseApplicationID.Text = ReplacementLicenseApplication.ApplicationID.ToString();
                        llShowLicensesHistory.Enabled = true;
                        llShowNewLicenseInfo.Enabled = true;
                        MessageBox.Show($"License Replaced Successfully With ID={NewDrivingLicense.LicenseID}", "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error Occuerd, Faild To Issue New Driving License", "Error Occured", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }



                }
                else
                {
                    MessageBox.Show("Error Occuerd, Faild To Issue New Driving License", "Error Occured", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                findLocalLicenseInfoByLicenseID1.gbFilterEnabled = false;
                btnIssue.Enabled = false;
                txtNotes.Enabled = false;
            }
        }

        void _ReplacementReasonChanged()
        {
            lblApplicationFees.Text= rbDamagedLicense.Checked ? clsApplicationTypes.ApplicationFees(4).ToString() : clsApplicationTypes.ApplicationFees(3).ToString();

            if (_OldDrivingLicenseInfo.LicenseID!=-1)
            {
                lblTotalFees.Text = (float.Parse(lblApplicationFees.Text.ToString()) + float.Parse(lblLicenseFees.Text.ToString())).ToString();

            }

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
            frmShowLicensesHistory licensesHistory = new frmShowLicensesHistory(_OldLicenseApplication.ApplicantPersonID);
            licensesHistory.ShowDialog();
        }

        private void llShowNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseInfo NewLicenseInfo = new frmLicenseInfo(int.Parse(lblNewLicenseID.Text.ToString()));
            NewLicenseInfo.ShowDialog();
        }
    }
}
