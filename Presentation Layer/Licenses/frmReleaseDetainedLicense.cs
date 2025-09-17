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
        clsDetainedLicenses DetainedLicense = null;
        public frmReleaseDetainedLicense(int DetainID)
        {
            _DetainID = DetainID;
            
            InitializeComponent();

            if (DetainID == -1)
            {
                DetainedLicense = new clsDetainedLicenses();
                findLocalLicenseInfoByLicenseID1.gbFilterEnabled = true;
            }
            else
            {

                DetainedLicense = clsDetainedLicenses.Find(DetainID);
                findLocalLicenseInfoByLicenseID1.txtLicenseID = DetainedLicense.LicenseID.ToString();
                findLocalLicenseInfoByLicenseID1.LoadLicenseInfo(DetainedLicense.LicenseID);
                findLocalLicenseInfoByLicenseID1.gbFilterEnabled = false;
            }
        }
      
        void _LoadData()
        {


            lblDetainID.Text = DetainedLicense.DetainID.ToString();
            lblLicenseID.Text = DetainedLicense.LicenseID.ToString();
            lblDetainDate.Text = DetainedLicense.DetainDate.ToString();
            lblFineFees.Text = DetainedLicense.FineFees.ToString();
            lblApplicationFees.Text = clsApplicationTypes.ApplicationFees(5).ToString();
            lblTotalFees.Text = (float.Parse(lblApplicationFees.Text.ToString()) + float.Parse(lblFineFees.Text.ToString())).ToString();
            lblCreatedByUserName.Text = DVLDSettings.CurrentUser.UserName;
            llShowLicensesHistory.Enabled = true;



            
        }
        private void frmReleaseDetainedLicense_Load(object sender, EventArgs e)
        {
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
            DetainedLicense=clsDetainedLicenses.FindByLicenseID(LicenseID);
            if(DetainedLicense==null||DetainedLicense.IsReleased)
            {
                MessageBox.Show("Selected License is not Detained, Choose another one","Not Allowed",MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRelease.Enabled = false;
                return;
            }
            _LoadData();

        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            if
               (
                  MessageBox.Show($"Are You You Want To Release The Detained License With ID[{DetainedLicense.LicenseID}]", "Confirmation Message"
                  , MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes
               )
            {
                clsApplications ReleaseDetainedLicenseApp=new clsApplications();
                ReleaseDetainedLicenseApp.ApplicationStatus = "New";
                ReleaseDetainedLicenseApp.PiadFees = clsApplicationTypes.ApplicationFees(5);
                int PersonID = clsApplications.Find
                    (
                    clsDrivingLicense.FindDriverLicense(DetainedLicense.LicenseID).ApplicationID
                    ).ApplicantPersonID;
                ReleaseDetainedLicenseApp.ApplicantPersonID = PersonID;
                ReleaseDetainedLicenseApp.ApplicationDate = DateTime.Now;
                ReleaseDetainedLicenseApp.LastStatusDate = DateTime.Now;
                ReleaseDetainedLicenseApp.ApplicationTypeID = 5;
                ReleaseDetainedLicenseApp.CreatedByUserID=DVLDSettings.CurrentUser.UserID;

                DetainedLicense.IsReleased = true;
                DetainedLicense.ReleaseDate = DateTime.Now;
                DetainedLicense.ReleasedByUserID = DVLDSettings.CurrentUser.UserID;
                


                if(ReleaseDetainedLicenseApp.Save())
                {
                    DetainedLicense.ReleaseApplicationID=ReleaseDetainedLicenseApp.ApplicationID;
                    if (DetainedLicense.Save())
                    {


                        MessageBox.Show($"Detained License Released Successfully", "Detained License Released", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show($"Error Occured,Faild To Release Driving License", "Error Occured", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    lblApplicationID.Text = ReleaseDetainedLicenseApp.ApplicationID.ToString();

                }
                else
                {
                    MessageBox.Show($"Error Occured,Faild To Release Driving License", "Error Occured", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                btnRelease.Enabled = false;
                findLocalLicenseInfoByLicenseID1.gbFilterEnabled = false;
                llShowLicenseInfo.Enabled = true;
                
            }
        }

        private void llShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int PersonID = clsApplications.Find
                  (
                  clsDrivingLicense.FindDriverLicense(DetainedLicense.LicenseID).ApplicationID
                  ).ApplicantPersonID;
            frmShowLicensesHistory licensesHistory = new frmShowLicensesHistory(PersonID);
            licensesHistory.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseInfo licenseInfo = new frmLicenseInfo(DetainedLicense.LicenseID);
            licenseInfo.ShowDialog();
        }
    }
}
