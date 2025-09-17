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

    public partial class frmLogin : Form
    {
        
        clsLogin loginInfo=null;
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            // loginInfo=clsLogin.GetLastLoginInfo();
            //if (loginInfo==null)
            //{
            //    chkRememberMe.Checked = false;
            //    loginInfo = new clsLogin();
            //}
            //else
            //{
            //    if (loginInfo.isRememberMeChecked)
            //    {
            //        chkRememberMe.Checked =true;
            //        txtUserName.Text = loginInfo.UserName;
            //        txtPassword.Text = loginInfo.Password;
            //    }
            //    chkRememberMe.Checked = loginInfo.isRememberMeChecked;


            //}

            string UserName = "", Password = "";

            if(clsGlobal.GetStoredCredential(ref UserName,ref Password))
            {
                txtUserName.Text = UserName;
                txtPassword.Text = Password;
                chkRememberMe.Checked = true;
            }
            else
            {
                chkRememberMe.Checked = false;
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            clsUsers LogedUser = clsUsers.FindUserByUserName(txtUserName.Text.ToString().Trim());

            if (LogedUser == null || LogedUser.Password != txtPassword.Text.ToString().Trim())
            {
                MessageBox.Show("Invalid UserName/Password!!", "Wrong credentials", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
           
            if (!LogedUser.isActive)
            {
                MessageBox.Show("Your Account is Deactivated Please Contact Your Admin!!", "Deactive User", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //loginInfo.isRememberMeChecked = chkRememberMe.Checked;
            //loginInfo.UserName = txtUserName.Text.ToString();
            //loginInfo.Password = txtPassword.Text.ToString();
            //loginInfo.isActive = true;
            //loginInfo.UserID = LogedUser.UserID;

            if (chkRememberMe.Checked)
            {
                clsGlobal.RememberUsernameAndPassword(txtUserName.Text.ToString().Trim(),txtPassword.Text.ToString().Trim());
            }
            else
            {
                clsGlobal.RememberUsernameAndPassword("", "");
            }
            DVLDSettings.CurrentUser = LogedUser;
          this.Close();
        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtUserName.Text.ToString()))
            {
                errorProvider1.SetError(txtUserName, "Please Enter a Valid UserName");

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
                errorProvider1.SetError(txtPassword, "Please Enter Your Password");

            }
            else
            {
                errorProvider1.SetError(txtPassword, "");
            }

        }
    }
}
