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
    public partial class frmShowUserDetails : Form
    {

        public int UserID = -1;
        public clsUsers user = null;
        public frmShowUserDetails(int UserID)
        {
            InitializeComponent();

            this.UserID = UserID;
            user = new clsUsers();
        }

        private void frmShowUserDetails_Load(object sender, EventArgs e)
        {
            ctrlUserDetails1.FillUserInfo(UserID);
        
    }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
