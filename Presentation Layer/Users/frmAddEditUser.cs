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
        int PersonID = -1;
        
        clsUsers user = new clsUsers();
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
                user=clsUsers.FindUserByUserID(UserID);
                PersonID = user.PersonID;
               
            }
            user.UserID = UserID;
        }
       


        private void MoveToLoginPageValidation()
        {
            if (PersonID == -1)
            {
                tcAddEditUser.SelectedIndex = 0;
                MessageBox.Show("Please Select a person first", "Select Person", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            if (mode==enMode.AddNew&&clsUsers.isPersonAnUser(PersonID))
            {
                tcAddEditUser.SelectedIndex = 0;

                MessageBox.Show("The Selected person is already an User", "User Exist", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            tcAddEditUser.SelectedTab = tpLoginInfo;
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            MoveToLoginPageValidation();
           
        }

        private void ctrlFindPersonByFilter1_OnPersonSelected(int obj)
        {
             PersonID = obj;
            user.PersonID = PersonID;
        }

        void _LoadData()
        {
            if (mode==enMode.AddNew)
            {
                this.Text = "AddNew User";
                lblMode.Text = "AddNew User";
                ctrlFindPersonByFilter1.FilterEnabled = true;

                return;

            }

            lblMode.Text = "Update User";
            this.Text = "Update User";
            ctrlFindPersonByFilter1 .FilterEnabled = false;
            ctrlFindPersonByFilter1.cbFilterSelectedItem = "PersonID";
            ctrlFindPersonByFilter1.txtFilterValue=PersonID.ToString();
            ctrlFindPersonByFilter1.FillPersonInfo(PersonID);
            //txtUserName.Text=user.UserName.ToString();
            //txtPassword.Text=user.Password.ToString();
            //txtConfirmPass.Text = user.Password.ToString();
            lblUserID.Text = user.UserID.ToString();
            chkIsActive.Checked = user.isActive;
        }

        private void frmAddEditUser_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void tpLoginInfo_Click(object sender, EventArgs e)
        {
            MoveToLoginPageValidation();
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(PersonID==-1)
            {
                MessageBox.Show("Data save not complete, Missing Information","Save Faild",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

         
            user.PersonID = PersonID;
            user.UserName = txtUserName.Text.ToString();
            user.Password = txtPassword.Text.ToString();
            user.isActive=chkIsActive.Checked;
           
            
                if ( user.Save() )
                {
                MessageBox.Show("Data Saved Successfully", "Save Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            if (mode == enMode.AddNew)
            {
                lblMode.Text = "Update User Info";
                this.Text = "Update User";
                lblUserID.Text = user.UserID.ToString();
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
                errorProvider1.SetError(txtUserName, "UserName Should have a value");
                txtUserName.Focus();
            }
            else
            {
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
