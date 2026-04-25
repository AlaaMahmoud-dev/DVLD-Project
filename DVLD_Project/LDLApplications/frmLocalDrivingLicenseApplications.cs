using DVLD_Business_Layar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DVLD_Project
{
    public partial class frmLocalDrivingLicenseApplications : Form
    {
        DataTable _dtLDLApplications;
        public frmLocalDrivingLicenseApplications()
        {
            InitializeComponent();
            
        }

       

        

     
        void _dgvFillApplications()
        {

            _dtLDLApplications = clsLDLApplication.LDLApplicationsList();
            dgvLDLApplications.DataSource = _dtLDLApplications;
            lblApplicationsCount.Text = dgvLDLApplications.RowCount.ToString();
            if (dgvLDLApplications.RowCount>0)
            {
                dgvLDLApplications.Columns[0].HeaderText = "L.D.L App ID";
                dgvLDLApplications.Columns[0].Width = 130;
                dgvLDLApplications.Columns[1].HeaderText = "License Class";
                dgvLDLApplications.Columns[1].Width = 300;
                dgvLDLApplications.Columns[2].HeaderText = "National No";
                dgvLDLApplications.Columns[2].Width = 150;
                dgvLDLApplications.Columns[3].HeaderText = "Full Name";
                dgvLDLApplications.Columns[3].Width = 380;
                dgvLDLApplications.Columns[4].HeaderText = "Application Date";
                dgvLDLApplications.Columns[4].Width = 150;
                dgvLDLApplications.Columns[5].HeaderText = "Passed Tests";
                dgvLDLApplications.Columns[5].Width = 100;
                dgvLDLApplications.Columns[6].HeaderText = "Status";
                dgvLDLApplications.Columns[6].Width = 120;

            }

        }
        private void frmLocalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
            cbFilterBy.Items.Add("None");
            cbFilterBy.Items.Add("L.D.L AppID");
            cbFilterBy.Items.Add("National No");
            cbFilterBy.Items.Add("Full Name");
            cbFilterBy.Items.Add("Status");

            cbFilterBy.SelectedItem = "None";
            _dgvFillApplications();

           

        }

        

            private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterBy.Text = string.Empty;
            txtFilterBy.Visible = (cbFilterBy.SelectedItem.ToString() != "None");
            txtFilterBy.Focus();

        }

        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtFilterBy.Text.ToString()))
            {
                _dgvFillApplications();
                return;
            }

            string FilterItem = "None";

            switch (cbFilterBy.SelectedItem)
            {
                case "L.D.L App ID":
                    FilterItem = "LocalDrivingLicenseApplicationID";
                    break;
                case "License Class":
                    FilterItem = "ClassName";
                    break;
                case "National No":
                    FilterItem = "NationalNo";
                    break;
                case "Full Name":
                    FilterItem = "FullName";
                    break;
                case "Application Date":
                    FilterItem = "ApplicationDate";
                    break;
                case "Passed Tests":
                    FilterItem = "PassedTests";
                    break;
                case "Status":
                    FilterItem = "ApplicationStatus";
                    break;
                default:
                    FilterItem = "None";
                    break;


            }
            
            if(FilterItem == "LocalDrivingLicenseApplicationID" || FilterItem == "PassedTests")
            {
                _dtLDLApplications.DefaultView.RowFilter = string.Format("{0}={1}", FilterItem, txtFilterBy.Text.Trim());
            }
            else
            {
                _dtLDLApplications.DefaultView.RowFilter = string.Format("{0} LIKE '{1}%'", FilterItem, txtFilterBy.Text.Trim());

            }

        }
        

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (
                cbFilterBy.SelectedItem != null && (cbFilterBy.SelectedItem.ToString() == "L.D.L App ID")|| (cbFilterBy.SelectedItem.ToString() == "Passed Tests")
               )
            {

                e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));
                
            }
        }

      

        private void cancelApplicationToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You sure you want to cancel this Application??", "Confirmation Message",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                clsLDLApplication lDLApplication = clsLDLApplication.FindLocalDrivingLicenseAppInfoByID(Convert.ToInt32(dgvLDLApplications.CurrentRow.Cells[0].Value.ToString()));
                if (lDLApplication.ChangeStatus(clsApplication.enApplicationStatus.Canceled))
                {
                    MessageBox.Show("Application Cancelled Successfully", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _dgvFillApplications();
                }

                else
                {
                    MessageBox.Show("Cancellation Failed", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void visionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTestAppointmentsList scheduleTest = new frmTestAppointmentsList(
                int.Parse(dgvLDLApplications.CurrentRow.Cells[0].Value.ToString())
                ,clsTestType.enTestType.VisionTest);
            scheduleTest.ShowDialog();
            
        }

        private void writtenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTestAppointmentsList scheduleTest = new frmTestAppointmentsList(
                int.Parse(dgvLDLApplications.CurrentRow.Cells[0].Value.ToString())
                , clsTestType.enTestType.WrittenTest);
            scheduleTest.ShowDialog();
            
        }

        private void practicalTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTestAppointmentsList scheduleTest = new frmTestAppointmentsList(
                int.Parse(dgvLDLApplications.CurrentRow.Cells[0].Value.ToString())
                , clsTestType.enTestType.PracticalTest);
            scheduleTest.ShowDialog();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

            visionTestToolStripMenuItem.Enabled = false;
            writtenTestToolStripMenuItem.Enabled = false;
            practicalTestToolStripMenuItem.Enabled = false;
            editToolStripMenuItem.Enabled = false;
            deleteApplicationToolStripMenuItem.Enabled = true;
            cancelApplicationToolStripMenuItem1.Enabled = true;
            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
            showLicenseToolStripMenuItem.Enabled = false;



            int LDLApplicationID = int.Parse(dgvLDLApplications.CurrentRow.Cells[0].Value.ToString());
            clsLDLApplication lDLApplicationInfo = clsLDLApplication.FindLocalDrivingLicenseAppInfoByID(LDLApplicationID);
            bool DoesPassedAllTests = lDLApplicationInfo.DoesPassedAllTests();
            int PassedTestsCount = lDLApplicationInfo.PassedTests();
            bool HasLicenseIssued = lDLApplicationInfo.HasLicenseIssued();
            if (lDLApplicationInfo.ApplicationStatus == clsApplication.enApplicationStatus.Canceled)
            {
                cancelApplicationToolStripMenuItem1.Enabled = false;
                return;

            }
            if (DoesPassedAllTests)
            {
                visionTestToolStripMenuItem.Enabled = false;
                writtenTestToolStripMenuItem.Enabled = false;
                practicalTestToolStripMenuItem.Enabled = false;
                issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = !HasLicenseIssued;
                showLicenseToolStripMenuItem.Enabled = HasLicenseIssued;
                cancelApplicationToolStripMenuItem1.Enabled = lDLApplicationInfo.ApplicationStatus
                    != clsApplication.enApplicationStatus.Completed;
            }
            else
            {
                editApplicationToolStripMenuItem.Enabled = (PassedTestsCount == 0);
                visionTestToolStripMenuItem.Enabled = (PassedTestsCount == 0);
                writtenTestToolStripMenuItem.Enabled = (PassedTestsCount == 1);
                practicalTestToolStripMenuItem.Enabled = (PassedTestsCount == 2);

            }
        }
            
        

        private void contextMenuStrip1_Opened(object sender, EventArgs e)
        {

           
        }

        private void issueDrivingLicenseFirstTimrToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmIssueDriverLicenseForTheFirstTime driverLicenseForTheFirstTime = new frmIssueDriverLicenseForTheFirstTime(int.Parse(dgvLDLApplications.CurrentRow.Cells[0].Value.ToString()));
            driverLicenseForTheFirstTime.ShowDialog();
            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled= false;
            showLicenseToolStripMenuItem.Enabled = true;
            _dgvFillApplications();
        }

        private void showApplicationDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowLDLApplicationDetails lDLApplicationDetails = new frmShowLDLApplicationDetails(int.Parse(dgvLDLApplications.CurrentRow.Cells[0].Value.ToString()));
            lDLApplicationDetails.ShowDialog();
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            frmLicenseInfo LicenseInfo = new frmLicenseInfo(clsLicense.FindDriverLicenseByApplicationID
                (
                clsLDLApplication.FindLocalDrivingLicenseAppInfoByID(int.Parse(dgvLDLApplications.CurrentRow.Cells[0].Value.ToString())).ApplicationID
                ).LicenseID
                );
            LicenseInfo.ShowDialog();
        }

        private void btnAddNewLDLApp_Click(object sender, EventArgs e)
        {
            frmAddEditLDLApplication newLDLApplication = new frmAddEditLDLApplication(-1);
            newLDLApplication.ShowDialog();
            _dgvFillApplications();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditLDLApplication editLDLApplication = new frmAddEditLDLApplication(
                int.Parse(dgvLDLApplications.CurrentRow.Cells[0].Value.ToString()));
            editLDLApplication.ShowDialog();
            _dgvFillApplications();

        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = clsLDLApplication.FindLocalDrivingLicenseAppInfoByID(
                       int.Parse(dgvLDLApplications.CurrentRow.Cells[0].Value.ToString())
                    ).ApplicantPersonID;

            frmShowLicensesHistory showLicensesHistory = new frmShowLicensesHistory(PersonID);
            showLicensesHistory.ShowDialog();
        
        }

        private void deleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = int.Parse(dgvLDLApplications.CurrentRow.Cells[0].Value.ToString());
            clsLDLApplication LocalDrivingLicenseApplicationInfo = clsLDLApplication.FindLocalDrivingLicenseAppInfoByID(LocalDrivingLicenseApplicationID);
            if (MessageBox.Show("Are you sure you want to delete L.D.L Application with ID [" + LocalDrivingLicenseApplicationID + "]??",
               "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
               == DialogResult.Yes)
            {
                if (LocalDrivingLicenseApplicationInfo.Delete())
                {
                    MessageBox.Show("Application With ID [" + LocalDrivingLicenseApplicationID + "] Deleted Successfully");
                    _dgvFillApplications();
                }
                else
                {
                    MessageBox.Show("Deleting L.D.L Application With ID [" + LocalDrivingLicenseApplicationID.ToString() + "]was not Completed Successfully");
                }
            }

        }
    }
}
