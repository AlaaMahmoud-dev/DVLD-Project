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
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            frmLogin login = new frmLogin();
            login.ShowDialog();
            
            this.WindowState = FormWindowState.Maximized;
           
           
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmManagePeople frmManagePeople = new frmManagePeople();
            frmManagePeople.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmAddEdit frmAddEdit=new frmAddEdit(1);
            frmAddEdit.ShowDialog();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageUsers ManageUsers = new frmManageUsers();
            ManageUsers.ShowDialog();
        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManagePeople ManagePeople = new frmManagePeople();
            ManagePeople.ShowDialog();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            if (DVLDSettings.CurrentUser == null)
                this.Close();
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowUserDetails CurrentUserInfo=new frmShowUserDetails(DVLDSettings.CurrentUser.UserID);
            CurrentUserInfo.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangeUserPassword changeCurrentUserPassword=new frmChangeUserPassword(DVLDSettings.CurrentUser.UserID);
            changeCurrentUserPassword.ShowDialog();
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsGlobal.CurrentUser = null;
            this.Hide();
            frmLogin login = new frmLogin();
            login.ShowDialog();
            if (clsGlobal.CurrentUser != null)
                this.Show();
            else
                this.Close();
                
            

        }

        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageApplicationTypes applicationTypes = new frmManageApplicationTypes();
            applicationTypes.ShowDialog();
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageTestTypes TestTypes = new frmManageTestTypes();
            TestTypes.ShowDialog();
        }

        private void localDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocalDrivingLicenseApplications localDrivingLicenseApplications = new frmLocalDrivingLicenseApplications();
            localDrivingLicenseApplications.ShowDialog();
        }

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditLDLApplication newLDLApplication = new frmAddEditLDLApplication(-1);
            newLDLApplication.ShowDialog();
        }

        private void localDrivingLicenseApplicationsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmLocalDrivingLicenseApplications localDrivingLicenseApplications = new frmLocalDrivingLicenseApplications();
            localDrivingLicenseApplications.ShowDialog();
        }

        private void internationalLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageInternationalLicenseApplications InternationalDrivingLicenseApplications = new frmManageInternationalLicenseApplications();
            InternationalDrivingLicenseApplications.ShowDialog();
        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRenewLocalDrivingLicenseApplication RenewExpiredLocalDrivingLicense=new frmRenewLocalDrivingLicenseApplication();
            RenewExpiredLocalDrivingLicense.ShowDialog();
        }

        private void ReplacementlostOrDamagedDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReplacementLostOrDamegedLicense ReplacementDrivingLicense=new frmReplacementLostOrDamegedLicense();
            ReplacementDrivingLicense.ShowDialog();
        }

        private void manageDetainedLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageDetainedLicenses detainedLicensesList = new frmManageDetainedLicenses();
            detainedLicensesList.ShowDialog();
        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDetainDrivingLicense detainDrivingLicense = new frmDetainDrivingLicense();
            detainDrivingLicense.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense releaseDetainedLicense = new frmReleaseDetainedLicense(-1);
            releaseDetainedLicense.ShowDialog();
        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
    }
}
