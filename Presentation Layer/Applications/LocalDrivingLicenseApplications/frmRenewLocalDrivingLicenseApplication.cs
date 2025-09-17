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
        clsDrivingLicense _OldDrivingLicenseInfo = null;
        clsApplications _OldLicenseApplication=null;
        public frmRenewLocalDrivingLicenseApplication()
        {
            InitializeComponent();
       
            _OldDrivingLicenseInfo =new clsDrivingLicense();
            _OldLicenseApplication=new clsApplications();

        }


        void _LoadData()
        {
           
            lblOldLicenseID.Text = _OldDrivingLicenseInfo.LicenseID.ToString();

            DateTime ExpirationDate = DateTime.Now.AddYears(clsDrivingLicense.GetLicenseValidityLength(_OldDrivingLicenseInfo.LicenseClass));

            lblExpirationDate.Text = ( ExpirationDate ).ToString("dd,MMM,yyyy");
           
            lblLicenseFees.Text=clsDrivingLicense.GetLicenseClassFees(_OldDrivingLicenseInfo.LicenseClass).ToString();
            lblTotalFees.Text=(float.Parse(lblApplicationFees.Text.ToString())+float.Parse(lblLicenseFees.Text.ToString())).ToString();
            
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
            lblApplicationFees.Text = clsApplicationTypes.ApplicationFees(2).ToString();
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
            _OldDrivingLicenseInfo = clsDrivingLicense.FindDriverLicense(LicenseID);
            _OldLicenseApplication=clsApplications.Find(_OldDrivingLicenseInfo.ApplicationID);
            if (_OldDrivingLicenseInfo != null&&_OldLicenseApplication!=null)
            {
                if (DateTime.Compare(_OldDrivingLicenseInfo.ExpirationDate,DateTime.Now)==1)
                {
                    MessageBox.Show($"This Driving License is not Expired Yet, it will expired in {_OldDrivingLicenseInfo.ExpirationDate.ToString("dd,MMM,yyyy")}", "Not Expired Yet"
                        , MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnRenew.Enabled = false;

                }
                else
                {
                    _LoadData();
                    btnRenew.Enabled = true;
                    llShowLicensesHistory.Enabled = true;
                }
                
               

            }
           
          
        }

        private void btnRenew_Click(object sender, EventArgs e)
        {

            if (!_OldDrivingLicenseInfo.isActive)
            {
                MessageBox.Show($"Selected License With ID={_OldDrivingLicenseInfo.LicenseID} is not Active", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if
                (
                   MessageBox.Show($"Are You You Want To Renew The Selected License With ID[{_OldDrivingLicenseInfo.LicenseID}]", "Confirmation Message"
                   , MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes
                )
            {
               // _OldLicenseApplication = clsApplications.Find(_OldDrivingLicenseInfo.ApplicationID);

                clsApplications RenewLicenseApplication = new clsApplications();
                RenewLicenseApplication.ApplicantPersonID = _OldLicenseApplication.ApplicantPersonID;
                RenewLicenseApplication.ApplicationDate = DateTime.Now;
                RenewLicenseApplication.ApplicationTypeID = 2;

                RenewLicenseApplication.ApplicationStatus = "New";
                RenewLicenseApplication.LastStatusDate = DateTime.Now;

                RenewLicenseApplication.PiadFees = float.Parse(lblApplicationFees.Text.ToString());
                RenewLicenseApplication.CreatedByUserID = DVLDSettings.CurrentUser.UserID;



                clsDrivingLicense NewDrivingLicense = new clsDrivingLicense();

                NewDrivingLicense.DriverID = _OldDrivingLicenseInfo.DriverID;
                NewDrivingLicense.LicenseClass = _OldDrivingLicenseInfo.LicenseClass;
                NewDrivingLicense.IssueDate = DateTime.Now;
                NewDrivingLicense.ExpirationDate = DateTime.Now.AddYears(clsDrivingLicense.GetLicenseValidityLength(NewDrivingLicense.LicenseClass));
                NewDrivingLicense.Notes = txtNotes.Text.ToString();
                NewDrivingLicense.PaidFees = float.Parse(lblLicenseFees.Text.ToString());
                NewDrivingLicense.isActive = true;
                NewDrivingLicense.IssueReasone = 2;
                NewDrivingLicense.CreatedByUserID = DVLDSettings.CurrentUser.UserID;

                _OldDrivingLicenseInfo.isActive = false;

                if (RenewLicenseApplication.Save())
                {
                    NewDrivingLicense.ApplicationID = RenewLicenseApplication.ApplicationID;

                    if (_OldDrivingLicenseInfo.Save() && NewDrivingLicense.Save())
                    {
                        lblNewLicenseID.Text = NewDrivingLicense.LicenseID.ToString();
                        lblRenewLicenseApplicationID.Text = RenewLicenseApplication.ApplicationID.ToString();
                        llShowLicensesHistory.Enabled = true;
                        llShowNewLicenseInfo.Enabled = true;
                        MessageBox.Show($"License Renewed Successfully With ID={NewDrivingLicense.LicenseID}", "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                btnRenew.Enabled = false;
                txtNotes.Enabled = false;
            }
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
