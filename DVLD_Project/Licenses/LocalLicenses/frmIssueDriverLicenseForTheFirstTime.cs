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
        clsLDLApplication _LDLApplication;
        public frmIssueDriverLicenseForTheFirstTime(int LDLApplicaitonID)
        {
            InitializeComponent();
            _LDLApplicationID= LDLApplicaitonID;
            _LDLApplication =new clsLDLApplication();
        }

        private void frmIssueDriverLicenseForTheFirstTime_Load(object sender, EventArgs e)
        {
            ctrlLDLAndBasicApplicationsDetails1.LoadLocalDrivingLicenseApplicationInfo(_LDLApplicationID);
            if (ctrlLDLAndBasicApplicationsDetails1.LocalDrivingLicenseApplicationInfo==null)
            {
                MessageBox.Show("Missing Data");
                this.Close();
                return;
            }
            if (ctrlLDLAndBasicApplicationsDetails1.LocalDrivingLicenseApplicationInfo.HasLicenseIssued())
            {
                MessageBox.Show("This Driver Already have license Issued before", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            if (!ctrlLDLAndBasicApplicationsDetails1.LocalDrivingLicenseApplicationInfo.DoesPassedAllTests())
            {
                MessageBox.Show("This person is not passed all the test.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            int LicenseID = ctrlLDLAndBasicApplicationsDetails1.LocalDrivingLicenseApplicationInfo.
                IssueDrivingLicenseForTheFirstTime(txtNotes.Text);
            if (LicenseID!=-1)
            {
                MessageBox.Show($"New Driving License Was Issued Successfully With ID[{LicenseID}]",
                    "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            else
            {

                MessageBox.Show("Error, Issue new driving license was not completed.", "Saving Data Failed.",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            btnIssue.Enabled = false;   
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void ctrlLDLAndBasicApplicationsDetails1_Load(object sender, EventArgs e)
        {

        }
    }
}
