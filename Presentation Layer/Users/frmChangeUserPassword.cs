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
        public int UserID = -1;
        public clsUsers user=null;
        public frmChangeUserPassword(int UserID)
        {
            InitializeComponent();
            user = clsUsers.FindUserByUserID(UserID);
            this.UserID = UserID;
        }

        private void frmChangeUserPassword_Load(object sender, EventArgs e)
        {
            ctrlUserDetails1.FillUserInfo(UserID);
            


        }

        private void txtCurrentPass_Validating(object sender, CancelEventArgs e)
        {
            if (user.Password.ToString()!=txtCurrentPass.Text.ToString())
            {
                errorProvider1.SetError(txtCurrentPass, "Password not correct");
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
                errorProvider1.SetError(txtConfirmPass, "Password Confimation is not correct");
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
            user.Password = txtNewPass.Text.ToString();


            if (user.Save())
            {
                MessageBox.Show("Data Saved Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Data Saving not complete", "Error Occured", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            txtConfirmPass.Text=null;
            txtNewPass.Text=null;
            txtCurrentPass.Text=null;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
