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
    public partial class ctrlLDLAndBasicApplicationsDetails : UserControl
    {
        clsApplications _BasicApplicationInfo = null;
        public ctrlLDLAndBasicApplicationsDetails()
        {
            InitializeComponent();
        }
       
        public void LoadDLAndBasicApplicationsInfo(int LDLApplicationID)
        {
            clsLDLApplications LDLApplication = clsLDLApplications.Find(LDLApplicationID);

            if (LDLApplication == null )
            {
                MessageBox.Show($"Cannot Find L.D.L Application With ID [{LDLApplicationID}]", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            clsApplications BasicApplication=clsApplications.Find(LDLApplication.ApplicationID);
            _BasicApplicationInfo = BasicApplication;

            lblDLAppID.Text=LDLApplicationID.ToString();
            lblLicenseClass.Text = LDLApplication.LicenseClassName;
            lblCountPassedTests.Text = LDLApplication.PassedTests().ToString();

            lblBasicAppID.Text = BasicApplication.ApplicationID.ToString();
            lblStatus.Text=BasicApplication.ApplicationStatus.ToString();
            lblAppFees.Text=BasicApplication.PiadFees.ToString();
            lblAppType.Text = clsApplications.ApplicationTypeTitle(BasicApplication.ApplicationTypeID);
            lblPersonName.Text = clsPerson.FindPerson(BasicApplication.ApplicantPersonID).FullName;
           
            DateTime date=BasicApplication.ApplicationDate;
            date.ToShortDateString();
            lblAppDate.Text = date.ToString();
            lblStatusDate.Text = BasicApplication.LastStatusDate.ToString();
            lblCreatorUserName.Text=clsUsers.FindUserByUserID(BasicApplication.CreatedByUserID).UserName.ToString();



        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void ctrlLDLAndBasicApplicationsDetails_Load(object sender, EventArgs e)
        {

        }

        private void llPersonCard_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmPersonDetails personDetails = new frmPersonDetails(_BasicApplicationInfo.ApplicantPersonID);
            personDetails.ShowDialog();
        }
    }
}
