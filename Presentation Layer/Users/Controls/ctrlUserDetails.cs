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
    public partial class ctrlUserDetails : UserControl
    {
     
        public ctrlUserDetails()
        {
            InitializeComponent();
           
        }

        public void FillUserInfo(int UserID)
        {
           
           clsUsers user=clsUsers.FindUserByUserID(UserID);
            if (user==null)
            {
                MessageBox.Show("User With ID [" + UserID + "] is not found");
            }
            else
            {
                lblUserID.Text = user.UserID.ToString();
                lblUsername.Text=user.UserName.ToString();

                lblActiveOrNot.Text = user.isActive ? "Yse" : "No";
                ctrlPersonDetails1.FillPersonInfo(user.PersonID);
            }



        }


    }
}
