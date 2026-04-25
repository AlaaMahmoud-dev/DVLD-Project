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
using System.Windows.Media;

namespace DVLD_Project
{
    public partial class frmAddEditLDLApplication : Form
    {
        
        int _LDLApplicationID;
        clsLDLApplication _LDLApplication;
        enum enMode
        {
            AddNew = 1,
            Update = 2
        }

        private enMode _Mode = enMode.Update;
        public frmAddEditLDLApplication(int LDLApplicationID)
        {
            InitializeComponent();

            _LDLApplicationID= LDLApplicationID;


            if (LDLApplicationID==-1)
            {
                _Mode = enMode.AddNew;
                _LDLApplication = new clsLDLApplication();
            }
            else
            {
                _Mode = enMode.Update;
                _LDLApplication = clsLDLApplication.FindLocalDrivingLicenseAppInfoByID(LDLApplicationID);
                
                
            }


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            _ValidateMovingToNextPageIsAllowed();
        }

        private void ctrlFindPersonByFilter1_OnPersonSelected(int obj)
        {
            if (obj==-1)
            {
                btnSave.Enabled = false;
            }
            else
            {
                _ValidateMovingToNextPageIsAllowed();
            }
        }
        private void _ValidateMovingToNextPageIsAllowed()
        {
            if (_Mode==enMode.Update)
            {
                btnSave.Enabled = true;
                return;
            }
            if (ctrlFindPersonByFilter1.PersonID == -1)
            {
                tcAddLDLApp.SelectedIndex = 0;
                btnSave.Enabled = false;
                MessageBox.Show("Please Select a person first", "Select Person", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            else
            {
                if (clsLDLApplication.DoesPersonHaveApplicationWithSpecificStatus(
                    ctrlFindPersonByFilter1.PersonID,
                    clsApplicationType.enApplicationType.NewLocalDrivingLicense,
                    clsApplication.enApplicationStatus.New))
                {
                    tcAddLDLApp.SelectedIndex = 0;
                    btnSave.Enabled = false;
                    MessageBox.Show("Selected Person already has an active Local Driving License Application",
                        "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    
                    btnSave.Enabled = true;
                }

            }
        }
        private void tpApplicationInfo_Click(object sender, EventArgs e)
        {
            _ValidateMovingToNextPageIsAllowed();
           
        }
        private void _FillLicenseClassesInComboBox()
        {
            DataTable dtLicenseClasses = clsLicenseClass.GetAllLicenseClasses();

            foreach (DataRow row in dtLicenseClasses.Rows)
            {
                cbLicenseClass.Items.Add(row["ClassName"]);
            }
        }
        void _LoadData()
        {
            _FillLicenseClassesInComboBox();

            if (_Mode == enMode.Update)
            {
                _LDLApplication = clsLDLApplication.FindLocalDrivingLicenseAppInfoByID(_LDLApplicationID);
                
                if (_LDLApplication.ApplicationStatus==clsApplication.enApplicationStatus.Completed)
                {
                    MessageBox.Show("This Application is already completed and the applicant person has passed all tests, So this form will be closed",
                        "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    this.Close();
                    return;
                }
                ctrlFindPersonByFilter1.FillPersonInfo(_LDLApplication.ApplicantPersonID);
                lblLDLApplicationID.Text = _LDLApplicationID.ToString();
                lblApplicationDate.Text = _LDLApplication.ApplicationDate.ToShortDateString();
                lblbApplicationFees.Text = _LDLApplication.ApplicationFees.ToString();
                lblCreatorUser.Text = clsUser.FindUserByUserID(_LDLApplication.CreatedByUserID).UserName.ToString();
                cbLicenseClass.SelectedItem = clsLicenseClass.GetLicenseClassName(
                    (clsLicenseClass.enLicenseClass)_LDLApplication.LicenseClassID);
                lblMode.Text = "Update Local Driving License Application";
                this.Text = "Update Local Driving License Application";
                return;
            }


            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            cbLicenseClass.SelectedItem = clsLicenseClass.GetLicenseClassName(
                clsLicenseClass.enLicenseClass.OrdinaryDrivingLicense);
            lblbApplicationFees.Text = clsApplicationType.GetApplicationFees(
                (int)clsApplicationType.enApplicationType.NewLocalDrivingLicense).ToString();
            lblCreatorUser.Text = DVLDSettings.CurrentUser.UserName.ToString();

        }
        private void frmAddNewLDLApplication_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
            _LDLApplication.LicenseClassID = clsLicenseClass.FindLicenseClassByClassName(
                cbLicenseClass.SelectedItem.ToString()).LicenseClassID;
            _LDLApplication.ApplicationTypeID = clsApplicationType.enApplicationType.NewLocalDrivingLicense;
            
            if (clsLDLApplication.DoesPersonHaveApplicationWithSpecificStatusAndLicenseClass
                (ctrlFindPersonByFilter1.PersonID,_LDLApplication.LicenseClassID,
                clsApplicationType.enApplicationType.NewLocalDrivingLicense,
                clsApplication.enApplicationStatus.Completed))
            {
                MessageBox.Show("Selected Person already has Completed Local Driving License Application with the selected License Class",
                       "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            

            _LDLApplication.ApplicantPersonID = ctrlFindPersonByFilter1.PersonID;
            _LDLApplication.PaidFees = clsApplicationType.GetApplicationFees((int)clsApplicationType.enApplicationType.NewLocalDrivingLicense);
            _LDLApplication.ApplicationStatus = clsApplication.enApplicationStatus.New;
            _LDLApplication.CreatedByUserID = DVLDSettings.CurrentUser.UserID;

            

            if (_LDLApplication.Save())
            {

                _Mode = enMode.Update;
                MessageBox.Show("Data Saved Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("LDL Saving Data Process Failed", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            
            if(_Mode==enMode.AddNew)
            {
                _Mode = enMode.Update;
                
                lblLDLApplicationID.Text=_LDLApplication.LDLApplicationID.ToString() ;
                ctrlFindPersonByFilter1.FilterEnabled = false;
               lblMode.Text = "Update Local Driving License Application";
                this.Text = "Update Local Driving License Application";
            }



        }
    }
}
