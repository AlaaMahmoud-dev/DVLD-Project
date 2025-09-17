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
    public partial class frmIssueDriverLicenseForTheFirstTime : Form
    {
        int _LDLApplicationID=-1;
        clsLDLApplications _LDLApplication=null;
        public frmIssueDriverLicenseForTheFirstTime(int LDLApplicaitonID)
        {
            InitializeComponent();
            _LDLApplicationID= LDLApplicaitonID;
            _LDLApplication =new clsLDLApplications();
        }

        private void frmIssueDriverLicenseForTheFirstTime_Load(object sender, EventArgs e)
        {
            ctrlLDLAndBasicApplicationsDetails1.LoadDLAndBasicApplicationsInfo(_LDLApplicationID);
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            _LDLApplication = clsLDLApplications.Find(_LDLApplicationID);
            clsDrivingLicense NewDrivingLicense = new clsDrivingLicense();
            clsApplications BasicApplication = clsApplications.Find(_LDLApplication.ApplicationID);


            NewDrivingLicense.ApplicationID = _LDLApplication.ApplicationID;
            NewDrivingLicense.Notes=txtNotes.Text.ToString();
            NewDrivingLicense.IssueDate = DateTime.Now;
            int ValidityLength = clsDrivingLicense.GetLicenseValidityLength(_LDLApplication.LicenseClassID);
            NewDrivingLicense.ExpirationDate = NewDrivingLicense.IssueDate.AddYears(ValidityLength);
            NewDrivingLicense.IssueReasone = 1;
            NewDrivingLicense.PaidFees = clsDrivingLicense.GetLicenseClassFees(_LDLApplication.LicenseClassID);
            NewDrivingLicense.LicenseClass = _LDLApplication.LicenseClassID;
            NewDrivingLicense.CreatedByUserID=DVLDSettings.CurrentUser.UserID;
            NewDrivingLicense.isActive=true;

            int DriverID = clsDrivers.GetDriverID(BasicApplication.ApplicantPersonID);
            if (DriverID != -1)
            {
                NewDrivingLicense.DriverID = DriverID;
            }
            else
            {
                clsDrivers NewDriver = new clsDrivers();
                NewDriver.CreatedDate = DateTime.Now;
                NewDriver.CreatedByUserID = DVLDSettings.CurrentUser.UserID;
                NewDriver.PersonID = BasicApplication.ApplicantPersonID;
                if (NewDriver.Save())
                {
                    NewDrivingLicense.DriverID = NewDriver.DriverID;
                }
                else
                {
                    MessageBox.Show("Error Occured, Faild Saving Data ", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;

                }
            }

            BasicApplication.ApplicationStatus = "Completed";
            if (NewDrivingLicense.Save()&&BasicApplication.Save())
            {
                MessageBox.Show($"New Driving License Was Issued Successfully With ID[{NewDrivingLicense.LicenseID}]", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Saving Data Faild", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
               
            }

            this.Close();   
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();

        }
    }
}
