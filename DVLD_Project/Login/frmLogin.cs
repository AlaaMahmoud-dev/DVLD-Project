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
        
        public frmLogin()
        {

            InitializeComponent();
            //clsUser user1 = clsUser.FindUserByUserID(1);
            //clsUser user15 = clsUser.FindUserByUserID(15);
            //user1.Password = clsCryptography.ComputeHash(user1.Password);
            //user15.Password = clsCryptography.ComputeHash(user15.Password);
            //user1.Save(); user15.Save();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            txtUserName.Text = "";
            txtPassword.Text = "";
            chkRememberMe.Checked = true;
            this.FormBorderStyle = FormBorderStyle.None;
           

            string UserName = "", Password = "";

            if(clsGlobal.GetStoredCredentialNew(ref UserName,ref Password))
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
            

            clsUser LoggedUser = clsUser.FindUserByUserName(txtUserName.Text.Trim());

            if (LoggedUser == null || LoggedUser.Password != clsCryptography.ComputeHash(txtPassword.Text.Trim()))
            {
                MessageBox.Show("Invalid UserName/Password!!", "Wrong credentials", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
           
            if (!LoggedUser.isActive)
            {
                MessageBox.Show("Your Account is Deactivated Please Contact Your Admin!!", "Deactivated User", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (chkRememberMe.Checked)
            {
                clsGlobal.RememberUsernameAndPasswordNew(txtUserName.Text.ToString().Trim(),txtPassword.Text.ToString().Trim());
            }
            else
            {
                clsGlobal.RememberUsernameAndPasswordNew("", "");
            }
            DVLDSettings.CurrentUser = LoggedUser;
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
