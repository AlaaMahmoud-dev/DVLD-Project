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
    public partial class frmAddEditLDLApplication : Form
    {
        int _PersonID=-1;
        int _LDLApplicationID = -1;
        clsLDLApplications _LDLApplication = null;
        clsApplications _BasicApplication = null;
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
                _LDLApplication = new clsLDLApplications();
                _BasicApplication = new clsApplications();  
            }
            else
            {
                _Mode = enMode.Update;
                _LDLApplication = clsLDLApplications.Find(LDLApplicationID);
                _BasicApplication= clsApplications.Find(_LDLApplication.ApplicationID);
                _PersonID =_BasicApplication.ApplicantPersonID;
                
            }


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_PersonID != -1)
                tcAddLDLApp.SelectedTab = tpApplicationInfo;
            else
                MessageBox.Show("");
        }

        private void ctrlFindPersonByFilter1_OnPersonSelected(int obj)
        {
            _PersonID = obj;
        }

        private void tpApplicationInfo_Click(object sender, EventArgs e)
        {
            if (_PersonID != -1)
                tcAddLDLApp.SelectedTab=tpApplicationInfo;
            else
                MessageBox.Show("");
        }

        void _LoadData()
        {

            if (_Mode == enMode.Update)
            {
                ctrlFindPersonByFilter1.cbFilterSelectedItem = "PersonID";
                ctrlFindPersonByFilter1.txtFilterValue =_PersonID.ToString();
                ctrlFindPersonByFilter1.FillPersonInfo(_PersonID);
                ctrlFindPersonByFilter1.FilterEnabled = false;

                lblLDLApplicationID.Text = _LDLApplicationID.ToString();
                lblApplicationDate.Text=_BasicApplication.ApplicationDate.ToString();
                lblbApplicationFees.Text=_BasicApplication.ApplicationFees.ToString();
                lblCreatorUser.Text=clsUsers.FindUserByUserID(_BasicApplication.CreatedByUserID).UserName.ToString();
                cbLicenseClass.SelectedIndex = _LDLApplication.LicenseClassID - 1;
                lblMode.Text = "Update Local Driving License Application";
                this.Text = "Update Local Driving License Application";
                return;
            }

            ctrlFindPersonByFilter1.FilterEnabled = true;
            lblApplicationDate.Text = DateTime.Now.ToString();
            cbLicenseClass.SelectedIndex = 2;
            lblbApplicationFees.Text = clsApplicationTypes.ApplicationFees(1).ToString();
            lblCreatorUser.Text = DVLDSettings.CurrentUser.UserName.ToString();

        }
        private void frmAddNewLDLApplication_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            _LDLApplication.LicenseClassID = cbLicenseClass.SelectedIndex+1;
            _BasicApplication.ApplicationTypeID = 1;
           
                int ApplicationID = clsApplications.GetApplicationIDIFPersonHasOpenedApplication(_PersonID, _LDLApplication.LicenseClassID, _BasicApplication.ApplicationTypeID);
            if (ApplicationID != -1 && _BasicApplication.ApplicationID != ApplicationID)
            {
                MessageBox.Show($"The Selected Person With ID [{_PersonID}] is already has an Active Application at this class With ID [{ApplicationID}],Choose another License Class", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ApplicationID=clsApplications.GetApplicationIDIFPersonHasCompletedApplication(_PersonID, _LDLApplication.LicenseClassID,_BasicApplication.ApplicationTypeID);


            if (ApplicationID != -1)
            {
                MessageBox.Show($"The Selected Person With ID [{_PersonID}] is already has a License with the same applied License class, Choose another License Class", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }




            if (_Mode==enMode.AddNew)
            {
                _BasicApplication.ApplicantPersonID = _PersonID;
                _BasicApplication.ApplicationDate = DateTime.Now;
                _BasicApplication.ApplicationStatus = "New";
                _BasicApplication.ApplicationTypeID = 1;
                _BasicApplication.CreatedByUserID = DVLDSettings.CurrentUser.UserID;
                _BasicApplication.PiadFees = clsApplicationTypes.ApplicationFees(1);
            }
            
           



            if (_BasicApplication.Save())
            {
               _LDLApplication.ApplicationID = _BasicApplication.ApplicationID;
                if(_LDLApplication.Save())
                {
                    MessageBox.Show("Data Saved Successfully","Saved",MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("LDL Saving Data Process Faild", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("App Saving Data Process Faild", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if(_Mode==enMode.AddNew)
            {
                _Mode = enMode.Update;
                _LDLApplication.ApplicationID = _BasicApplication.ApplicationID;
                lblLDLApplicationID.Text=_LDLApplication.LDLApplicationID.ToString() ;
                ctrlFindPersonByFilter1.FilterEnabled = false;
               lblMode.Text = "Update Local Driving License Application";
                this.Text = "Update Local Driving License Application";
            }



        }
    }
}
