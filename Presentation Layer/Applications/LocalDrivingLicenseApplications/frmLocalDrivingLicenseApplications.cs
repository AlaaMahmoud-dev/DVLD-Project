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
    public partial class frmLocalDrivingLicenseApplications : Form
    {
        string _Filtering_Item = "None";
        DataTable dtLDLApplications = null;
        public frmLocalDrivingLicenseApplications()
        {
            InitializeComponent();
            dtLDLApplications = clsLDLApplications.LDLApplicationsList();
        }

        void _dgvFillApplications()
        {

            dtLDLApplications = clsLDLApplications.LDLApplicationsList();
            dgvLDLApplications.Rows.Clear();

            DataView dvLDLApplications = dtLDLApplications.DefaultView;


            for (int i = 0; i < dtLDLApplications.Rows.Count; i++)
            {
                dgvLDLApplications.Rows.Add();
            }

            for (int i = 0; i < dtLDLApplications.Rows.Count; i++)
            {
                for (int j = 0; j < dtLDLApplications.Columns.Count; j++)
                {
                    if (j == 6)
                    {
                        switch (int.Parse(dvLDLApplications[i][j].ToString()))
                        {
                            case 1:
                                dgvLDLApplications.Rows[i].Cells[j].Value = "New";
                                break;
                            case 2:
                                dgvLDLApplications.Rows[i].Cells[j].Value = "Canceled";
                                break;
                            case 3:
                                dgvLDLApplications.Rows[i].Cells[j].Value = "Completed";
                                break;
                            default:
                                break;
                        }
                        continue;
                    }

                    dgvLDLApplications.Rows[i].Cells[j].Value = dvLDLApplications[i][j].ToString();
                }
            }
            lblRecords.Text = dgvLDLApplications.Rows.Count.ToString();
        }
        private void frmLocalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
            _dgvFillApplications();

            cbFilterBy.SelectedItem = "None";

        }

        private bool _Find(string Value, ref DataView dataView)
        {
            foreach (DataRow Record in dtLDLApplications.Rows)
            {
                if (Record[_Filtering_Item].ToString().ToLower() == Value.ToLower())
                {
                    dataView.RowFilter = ($"{_Filtering_Item} = '{Record[_Filtering_Item]}'");
                    return true;
                }
            }

            return false;






            }

            private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            _dgvFillApplications();
            txtFilterBy.Text = string.Empty;
            switch (cbFilterBy.SelectedItem)
            {
                case "None":
                    txtFilterBy.Visible = false;
                    break;
                case "L.D.L AppID":
                    txtFilterBy.Visible = true;
                    _Filtering_Item = "LocalDrivingLicenseApplicationID";
                    break;
                case "Status":
                    txtFilterBy.Visible = true;
                    _Filtering_Item = "ApplicationStatus";
                    break;
                default:
                    txtFilterBy.Visible = true;
                    _Filtering_Item = cbFilterBy.SelectedItem.ToString();
                    break;

            }


        }

        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtFilterBy.Text.ToString()))
            {
                _dgvFillApplications();

            }

            else
            {
                DataView dataView = dtLDLApplications.DefaultView;
                if (_Find(txtFilterBy.Text.ToString(), ref dataView))
                {


                    dgvLDLApplications.Rows.Clear();
                    for (int i = 0; i < dataView.Count; i++)
                    {
                        dgvLDLApplications.Rows.Add();
                    }

                    for (int i = 0; i < dataView.Count; i++)
                    {
                        for (int j = 0; j < dgvLDLApplications.Columns.Count; j++)
                        {

                            if (j == 6)
                            {
                                switch (int.Parse(dataView[i][j].ToString()))
                                {
                                    case 1:
                                        dgvLDLApplications.Rows[i].Cells[j].Value = "New";
                                        break;
                                    case 2:
                                        dgvLDLApplications.Rows[i].Cells[j].Value = "Canceled";
                                        break;
                                    case 3:
                                        dgvLDLApplications.Rows[i].Cells[j].Value = "Completed";
                                        break;
                                    default:
                                        break;
                                }
                                continue;
                            }
                            dgvLDLApplications.Rows[i].Cells[j].Value = dataView[i][j].ToString();
                        }
                    }



                    lblRecords.Text = dataView.Count.ToString();

                }
                else
                {
                    dgvLDLApplications.Rows.Clear();
                    lblRecords.Text = "0";

                }


            }


            //else
            //{
            //    DataView dataView = dtLDLApplications.DefaultView;


            //    dataView.RowFilter = ($"{_Filtering_Item} like '{txtFilterBy.Text.ToString()}%'");
            //            dgvLDLApplications.Rows.Clear();
            //    for (int i = 0; i < dataView.Count; i++)
            //    {
            //        dgvLDLApplications.Rows.Add();
            //    }

            //    for (int i = 0; i < dataView.Count; i++)
            //    {
            //        for (int j = 0; j < dgvLDLApplications.Columns.Count; j++)
            //        {
            //            dgvLDLApplications.Rows[i].Cells[j].Value = dataView[i][j].ToString();
            //        }
            //    }



            //    lblRecords.Text = dataView.Count.ToString();

            //}
            


        }
        

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (
                cbFilterBy.SelectedItem != null && (cbFilterBy.SelectedItem.ToString() == "L.D.L AppID")
               )
            {
                int Letter_Unicode_Value = Convert.ToInt32(e.KeyChar);

                int BackSpace_Unicode_Value = 8;

                if (!(Letter_Unicode_Value >= 48 && Letter_Unicode_Value <= 57))
                {
                    if (Letter_Unicode_Value == BackSpace_Unicode_Value)
                    {
                        return;
                    }
                    e.Handled = true;
                }
            }
        }

      

        private void cancelApplicationToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You sure you want to cancel this Application??", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question,MessageBoxDefaultButton.Button2)==DialogResult.Yes)
            {
                clsLDLApplications lDLApplication = clsLDLApplications.Find(Convert.ToInt32(dgvLDLApplications.CurrentRow.Cells[0].Value.ToString()));
                if (clsApplications.CancelApplication(lDLApplication.ApplicationID))
                {
                    MessageBox.Show("Application Cancelled Successfully", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _dgvFillApplications();
                }

                else
                {
                    MessageBox.Show("Cancellation Faild", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void visionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTakeAppointment scheduleTest = new frmTakeAppointment(int.Parse(dgvLDLApplications.CurrentRow.Cells[0].Value.ToString()),1);
            scheduleTest.ShowDialog();
            _dgvFillApplications();
        }

        private void writtenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTakeAppointment scheduleTest = new frmTakeAppointment(int.Parse(dgvLDLApplications.CurrentRow.Cells[0].Value.ToString()), 2);
            scheduleTest.ShowDialog();
            _dgvFillApplications();
        }

        private void practicalTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTakeAppointment scheduleTest = new frmTakeAppointment(int.Parse(dgvLDLApplications.CurrentRow.Cells[0].Value.ToString()), 3);
            scheduleTest.ShowDialog();
            _dgvFillApplications();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

            showApplicationDetailsToolStripMenuItem.Enabled = true;
            visionTestToolStripMenuItem.Enabled = true;
            writtenTestToolStripMenuItem.Enabled = true;
            practicalTestToolStripMenuItem.Enabled = true;
            editToolStripMenuItem.Enabled = true;
            deleteApplicationToolStripMenuItem.Enabled = true;
            cancelApplicationToolStripMenuItem1.Enabled = true;
            scheduleTestsToolStripMenuItem.Enabled = true;
            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = true;
            showLicenseToolStripMenuItem.Enabled = true;

            if (int.Parse(dgvLDLApplications.CurrentRow.Cells[5].Value.ToString()) != 3)
            {
                issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled=false;
                showLicenseToolStripMenuItem.Enabled=false;

                if (int.Parse(dgvLDLApplications.CurrentRow.Cells[5].Value.ToString()) == 0)
                {
                    visionTestToolStripMenuItem.Enabled = true;
                    writtenTestToolStripMenuItem.Enabled = false;
                    practicalTestToolStripMenuItem.Enabled = false;
                }
                else if (int.Parse(dgvLDLApplications.CurrentRow.Cells[5].Value.ToString()) == 1)
                {
                    visionTestToolStripMenuItem.Enabled = false;
                    writtenTestToolStripMenuItem.Enabled = true;
                    practicalTestToolStripMenuItem.Enabled = false;
                }
                else 
                {
                    visionTestToolStripMenuItem.Enabled = false;
                    writtenTestToolStripMenuItem.Enabled = false;
                    practicalTestToolStripMenuItem.Enabled = true;
                }
            }
            else
            {
                scheduleTestsToolStripMenuItem.Enabled = false;

                if (dgvLDLApplications.CurrentRow.Cells[6].Value.ToString().ToLower()=="completed")
                {

                    issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
                    showLicenseToolStripMenuItem.Enabled = true;
                    editToolStripMenuItem.Enabled = false;
                    deleteApplicationToolStripMenuItem.Enabled = false;
                    cancelApplicationToolStripMenuItem1.Enabled = false;
                }
                else
                    showLicenseToolStripMenuItem.Enabled= false;

                //int DriverID = clsPerson.GetDriverID(clsApplications.Find(clsLDLApplications.Find(int.Parse(dgvLDLApplications.CurrentRow.Cells[0].Value.ToString())).ApplicationID).ApplicantPersonID);

                //if (DriverID == -1)//then application is not completed
                //{

                //    issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = true;
                //    showLicenseToolStripMenuItem.Enabled = false;

                //}
                //else
                //{

                   

                //}





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
            frmLicenseInfo LicenseInfo = new frmLicenseInfo(clsDrivingLicense.FindDriverLicenseByApplicationID
                (
                clsLDLApplications.Find(int.Parse(dgvLDLApplications.CurrentRow.Cells[0].Value.ToString())).ApplicationID
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
            frmAddEditLDLApplication editLDLApplication = new frmAddEditLDLApplication(int.Parse(dgvLDLApplications.CurrentRow.Cells[0].Value.ToString()));
            editLDLApplication.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = 
                
                clsApplications.Find
                (
                    clsLDLApplications.Find
                    (
                       int.Parse(dgvLDLApplications.CurrentRow.Cells[0].Value.ToString())
                    ).ApplicationID
                ).ApplicantPersonID
                ;

            frmShowLicensesHistory showLicensesHistory = new frmShowLicensesHistory(PersonID);
            showLicensesHistory.ShowDialog();
        
        }
    }
}
