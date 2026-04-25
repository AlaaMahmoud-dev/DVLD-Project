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
        private int _UserID;

        public int UserID
        {
            get
            {
                return _UserID;
            }
        }
        public clsUser _UserInfo;
        public ctrlUserDetails()
        {
            InitializeComponent();
            _ResetDefaultValues();
            
        }

        private void _ResetDefaultValues()
        {
            lblActiveOrNot.Text = "[????]";
            lblUserID.Text = "[????]";
            lblUsername.Text = "[????]";
            _UserID = -1;
            _UserInfo = new clsUser();
        }

        public void LoadUserInfo(int UserID)
        {
            _UserID = UserID;
            _UserInfo = clsUser.FindUserByUserID(UserID);
            if (_UserInfo == null)
            {
                _ResetDefaultValues();
                MessageBox.Show("User With ID [" + UserID + "] is not found");
                
            }
            else
            {
                _FillInfo();
            }




        }
        void _FillInfo()
        {

            lblUserID.Text = _UserInfo.UserID.ToString();
            lblUsername.Text = _UserInfo.UserName.ToString();

            lblActiveOrNot.Text = _UserInfo.isActive ? "Yse" : "No";
            ctrlPersonDetails1.FillPersonInfo(_UserInfo.PersonID);
        }


    }
}
