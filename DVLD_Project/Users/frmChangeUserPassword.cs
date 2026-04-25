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
    public partial class frmChangeUserPassword : Form
    {
        private int _UserID;
       
        public frmChangeUserPassword(int UserID)
        {
            InitializeComponent();
          
            this._UserID = UserID;
        }

        private void frmChangeUserPassword_Load(object sender, EventArgs e)
        {
            ctrlUserDetails1.LoadUserInfo(_UserID);
            if (ctrlUserDetails1.UserID==-1)
            {
                btnSave.Enabled = false;
            }
        }

        private void txtCurrentPass_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCurrentPass.Text))
            {
                errorProvider1.SetError(txtCurrentPass, "you must fill the current password");
                e.Cancel = true;
                return;
            }
            else
            {
                errorProvider1.SetError(txtCurrentPass, "");
                e.Cancel = false;
            }
            if (ctrlUserDetails1._UserInfo.Password.Trim()!=clsCryptography.ComputeHash(txtCurrentPass.Text.Trim()))
            {
                errorProvider1.SetError(txtCurrentPass, "Entered password does not match the current password");
                e.Cancel = true;
                
            }
            else
            {
                errorProvider1.SetError(txtCurrentPass, "");
                e.Cancel = false;
            }
        }

        private void txtConfirmPass_Validating(object sender, CancelEventArgs e)
        {

            if (txtConfirmPass.Text.ToString() != txtNewPass.Text.ToString())
            {
                errorProvider1.SetError(txtConfirmPass, "Password Confirmation is not correct");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtConfirmPass, "");
                e.Cancel = false;
            }
        }

       

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("There is Missing Data, Please check for red icons.",
                  "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ctrlUserDetails1._UserInfo.Password = clsCryptography.ComputeHash(txtNewPass.Text.Trim());


            if (ctrlUserDetails1._UserInfo.Save())
            {
                MessageBox.Show("Data Saved Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Data Saving not complete", "Error Occurred", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            txtConfirmPass.Text=null;
            txtNewPass.Text=null;
            txtCurrentPass.Text=null;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtNewPass_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNewPass.Text))
            {
                errorProvider1.SetError(txtNewPass, "Create a new password");
                e.Cancel = true;
                return;
            }
            else
            {
                errorProvider1.SetError(txtNewPass, "");
                e.Cancel = false;
            }
        }
    }
}
