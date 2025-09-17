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
        int _LicenseID = -1;
        clsDrivingLicense _LocalLicenseInfo = null;
        clsApplications _LocalLicenseApplicationInfo = null;
        public frmIssueNewInternationalDrivingLicense()
        {
            InitializeComponent();
            _LocalLicenseInfo=new clsDrivingLicense();
            _LocalLicenseApplicationInfo=new clsApplications();
        }

        void _ResetApplicationInfo()
        {
            lblApplicationID.Text = "[????]";
            lblInternationalLicenseID.Text = lblApplicationID.Text;
            lblLocalLicenseID.Text = lblApplicationID.Text;
        }

        void _LoadData()
        {
            lblApplicationDate.Text = DateTime.Now.ToString("dd,MMM,yyyy");
            lblIssueDate.Text = DateTime.Now.ToString("dd,MMM,yyyy");
            lblCreatedByUserName.Text = DVLDSettings.CurrentUser.UserName;
            lblExpirationDate.Text = new DateTime(DateTime.Now.Year + 1, DateTime.Now.Month, DateTime.Now.Day).ToString("dd,MMM,yyyy");
            lblFees.Text=clsApplicationTypes.ApplicationFees(6).ToString();

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
            _LicenseID = obj;


            if (_LicenseID == -1)
            {
                _ResetApplicationInfo();
            }
            else
            {
                btnIssue.Enabled = true;
                _LocalLicenseInfo = clsDrivingLicense.FindDriverLicense(_LicenseID);
                _LocalLicenseApplicationInfo = clsApplications.Find(_LocalLicenseInfo.ApplicationID);
                if (_LocalLicenseInfo == null || _LocalLicenseApplicationInfo == null)
                {
                    MessageBox.Show("Error Occured While Trying to Issue New International Driving License, Process Faild", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnIssue.Enabled = false;
                }
                else
                {
                    llShowLicensesHistory.Enabled = true;
                    lblLocalLicenseID.Text = _LicenseID.ToString();
                    if (_LocalLicenseInfo.LicenseClass != 3)
                    {
                        MessageBox.Show("Connot Issue International Driving License On This License Class , Please Select License With 'Ordenary Driving License' Class", "Wrong License Class", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        btnIssue.Enabled = false;
                    }

                    else if (_LocalLicenseInfo.isActive==false)
                    {
                        MessageBox.Show("Connot Issue International Driving License, Please Renew Driver's Local Driving License First", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }
                    int ActiveInternationalLicenseID = clsInternationalDrivingLicenses.GetInternationalLicenseIdWhenDriverHasAnActiveOne(_LocalLicenseInfo.DriverID);

                    if (ActiveInternationalLicenseID != -1)
                    {
                        MessageBox.Show($"Person Already Has an Active International License With ID [{ActiveInternationalLicenseID}]", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        btnIssue.Enabled = false;
                    }
                   
                }
               

            }

        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (_LicenseID==-1)
            {
                MessageBox.Show("Please Select Local License First", "No License Selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }




            if (_LocalLicenseInfo == null||_LocalLicenseApplicationInfo==null)
            {
                MessageBox.Show("Error Occured While Trying to Issue New International Driving License, Process Faild", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if
                (
                   MessageBox.Show($"Are You You Want To Issue a New International License for a Local License With ID[{_LocalLicenseInfo.LicenseID}]", "Confirmation Message"
                   , MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes
                )
            {


                clsApplications InternationalLicenseApplication = new clsApplications();



                InternationalLicenseApplication.ApplicationDate = DateTime.Now;
                InternationalLicenseApplication.LastStatusDate = DateTime.Now;
                InternationalLicenseApplication.ApplicantPersonID = _LocalLicenseApplicationInfo.ApplicantPersonID;
                InternationalLicenseApplication.ApplicationStatus = "New";
                InternationalLicenseApplication.PiadFees = float.Parse(lblFees.Text.ToString());
                InternationalLicenseApplication.ApplicationTypeID = 6;
                InternationalLicenseApplication.CreatedByUserID = DVLDSettings.CurrentUser.UserID;

                clsInternationalDrivingLicenses internationalDrivingLicense = new clsInternationalDrivingLicenses();

                internationalDrivingLicense.IssueDate = DateTime.Now;
                internationalDrivingLicense.ExpirationDate = DateTime.Now.AddYears(1);
                internationalDrivingLicense.DriverID = _LocalLicenseInfo.DriverID;
                internationalDrivingLicense.CreatedByUserID = DVLDSettings.CurrentUser.UserID;
                internationalDrivingLicense.IsActive = true;
                internationalDrivingLicense.LocalLicenseID = _LocalLicenseInfo.LicenseID;

                if (InternationalLicenseApplication.Save())
                {
                    internationalDrivingLicense.ApplicationID = InternationalLicenseApplication.ApplicationID;

                    if (internationalDrivingLicense.Save())
                    {
                        MessageBox.Show($"New International License Was Issued Successfully With ID[{internationalDrivingLicense.InternationalLicenseID}]", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lblApplicationID.Text = InternationalLicenseApplication.ApplicationID.ToString();
                        lblInternationalLicenseID.Text = internationalDrivingLicense.InternationalLicenseID.ToString();
                        btnIssue.Enabled = false;
                        llShowLicenseInfo.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Error Occured While Trying to Issue New International Driving License, Process Faild", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                else
                {
                    MessageBox.Show("Error Occured While Trying to Issue New International Driving License, Process Faild", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }


        }

        private void llShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicensesHistory licensesHistory = new frmShowLicensesHistory(_LocalLicenseApplicationInfo.ApplicantPersonID);
            licensesHistory.ShowDialog();
        }
    }
}
