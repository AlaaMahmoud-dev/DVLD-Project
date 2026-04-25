using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Business_Layar;

namespace DVLD_Project
{
    public partial class frmAddEditUser : Form
    {

        enum enMode
        {
            AddNew = 1,
            Update = 2
        }

        enMode mode = enMode.AddNew;
        int _PersonID = -1;
        int _UserID = -1;
        clsUser _UserInfo;
        public frmAddEditUser(int UserID)
        {
            InitializeComponent();
           

            if (UserID==-1)
            {
                mode = enMode.AddNew;
               
            }
            else
            {
                mode= enMode.Update;
                this._UserID = UserID;
               
            }
            
        }
       


        private bool MoveToLoginPageValidation()
        {
            if (_PersonID == -1)
            {
                tcAddEditUser.SelectedIndex = 0;
                MessageBox.Show("Please Select a person first", "Select Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (mode==enMode.AddNew&&clsUser.IsUserExistsForPersonID(_PersonID))
            {
                tcAddEditUser.SelectedIndex = 0;
                _PersonID = -1;
                MessageBox.Show("The Selected person is already an User", "User Exist", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            
            return true;
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            MoveToLoginPageValidation();
           
        }

        private void ctrlFindPersonByFilter1_OnPersonSelected(int PersonID)
        {
            this._PersonID = PersonID;
            if (PersonID==-1)
            {
                btnSave.Enabled = false; 
            }
            else
            {
                if(MoveToLoginPageValidation())
                btnSave.Enabled = true;
            }
           



        }

        void _LoadData()
        {
          
            _UserInfo = clsUser.FindUserByUserID(_UserID);
            if (_UserInfo==null)
            {
                MessageBox.Show("Can't find user with ID[" + _UserID + "]!!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
           
            ctrlFindPersonByFilter1.FillPersonInfo(_PersonID);
            txtUserName.Text=_UserInfo.UserName.ToString();
            txtPassword.Text=_UserInfo.Password.ToString();
            txtConfirmPass.Text = _UserInfo.Password.ToString();
            lblUserID.Text = _UserInfo.UserID.ToString();
            chkIsActive.Checked = _UserInfo.isActive;
           
        }
        void _ResetDefaultValues()
        {
            btnSave.Enabled = false;
            txtConfirmPass.Text = "";
            txtPassword.Text = "";
            txtUserName.Text = "";
            if (mode == enMode.AddNew)
            {
                this.Text = "AddNew User";
                lblMode.Text = "AddNew User";
                ctrlFindPersonByFilter1.FilterEnabled = true;
                _UserInfo = new clsUser();
                return;

            }
            lblMode.Text = "Update User";
            this.Text = "Update User";
            ctrlFindPersonByFilter1.FilterEnabled = false;
        }
        private void frmAddEditUser_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();
            if (mode == enMode.Update)
                _LoadData();
        }

        private void tpLoginInfo_Click(object sender, EventArgs e)
        {
            if (MoveToLoginPageValidation())
                tcAddEditUser.SelectedTab = tpLoginInfo;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //if(_PersonID==-1)
            //{
            //    MessageBox.Show("Data save not complete, Missing Information","Save Failed",MessageBoxButtons.OK,MessageBoxIcon.Error);
            //    return;
            //}
            if (!this.ValidateChildren())
            {
                MessageBox.Show("There is Missing Data, Please check for red icons.",
                   "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _UserInfo.PersonID = _PersonID;
            _UserInfo.UserName = txtUserName.Text.ToString();
            _UserInfo.Password = txtPassword.Text.ToString();
            _UserInfo.isActive=chkIsActive.Checked;
           
            
                if ( _UserInfo.Save() )
                {
                MessageBox.Show("Data Saved Successfully", "Save Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            if (mode == enMode.AddNew)
            {
                lblMode.Text = "Update User Info";
                this.Text = "Update User";
                lblUserID.Text = _UserInfo.UserID.ToString();
                ctrlFindPersonByFilter1.FilterEnabled = false;
            }
        }

        private void tcAddEditUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(tcAddEditUser.SelectedTab==tpLoginInfo)
            {
                MoveToLoginPageValidation();
            }
        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserName.Text.ToString()))
            {
                errorProvider1.SetError(txtUserName, "Username Field is required");
                txtUserName.Focus();
            }
            else
            {
                if (clsUser.IsUserExists(txtUserName.Text.Trim())
                    && _UserInfo.UserName != txtUserName.Text.Trim())
                {
                    errorProvider1.SetError(txtUserName, "Username is already used, choose another one");
                    txtUserName.Focus();
                }
                else
                    errorProvider1.SetError(txtUserName, "");
            }

        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text.ToString()))
            {
                errorProvider1.SetError(txtPassword, "Password Should have a value");
                txtPassword.Focus();
            }
            else
            {
                errorProvider1.SetError(txtPassword, "");
            }
        }

        private void txtConfirmPass_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtConfirmPass.Text.ToString()))
            {
                errorProvider1.SetError(txtConfirmPass, "Confirm Password is required");
                txtConfirmPass.Focus();
            }
            else
            {

                if (txtConfirmPass.Text.ToString() != txtPassword.Text.ToString())
                {
                    errorProvider1.SetError(txtConfirmPass, "Wrong Password, please confirm your password correctly");
                    txtConfirmPass.Focus();
                }
                else
                    errorProvider1.SetError(txtConfirmPass, "");
            }
        
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            tcAddEditUser.SelectedTab = tpPersonalInfo;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmManagePeople frmManagePeople = new frmManagePeople();
            frmManagePeople.ShowDialog();
        }
    }
}
