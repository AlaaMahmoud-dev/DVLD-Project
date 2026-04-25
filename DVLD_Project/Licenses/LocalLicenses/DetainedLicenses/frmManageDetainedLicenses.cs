using DVLD_Business_Layar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project
{
    public partial class frmManageDetainedLicenses : Form
    {

        DataTable _dtDetainedLicensesList=null;
        public frmManageDetainedLicenses()
        {
            InitializeComponent();

            _dtDetainedLicensesList = new DataTable();
        }
        void _dgvFillDetainedLicense()
        {
            _dtDetainedLicensesList = clsDetainedLicense.GetDetainedLicensesList();
            dgvDetainedLicensesList.DataSource = _dtDetainedLicensesList;
            if (dgvDetainedLicensesList.Rows.Count>0)
            {

                dgvDetainedLicensesList.Columns[0].HeaderText = "Detain ID";
                dgvDetainedLicensesList.Columns[0].Width = 100;

                dgvDetainedLicensesList.Columns[1].HeaderText = "License ID";
                dgvDetainedLicensesList.Columns[1].Width = 100;

                dgvDetainedLicensesList.Columns[2].HeaderText = "Detain Data";
                dgvDetainedLicensesList.Columns[2].Width = 170;

                dgvDetainedLicensesList.Columns[3].HeaderText = "Is Released";
                dgvDetainedLicensesList.Columns[3].Width = 120;


                dgvDetainedLicensesList.Columns[4].HeaderText = "Fine Fees";
                dgvDetainedLicensesList.Columns[4].Width = 120;


                dgvDetainedLicensesList.Columns[5].HeaderText = "Release Date";
                dgvDetainedLicensesList.Columns[5].Width = 170;


                dgvDetainedLicensesList.Columns[6].HeaderText = "National No";
                dgvDetainedLicensesList.Columns[6].Width = 120;



                dgvDetainedLicensesList.Columns[7].HeaderText = "Full Name";
                dgvDetainedLicensesList.Columns[7].Width = 280;


                dgvDetainedLicensesList.Columns[8].HeaderText = "Release.App ID";
                dgvDetainedLicensesList.Columns[8].Width = 100;
            }


            lblRecordsCount.Text = (dgvDetainedLicensesList.RowCount).ToString();
        }
        private void frmManageDetainedLicenses_Load(object sender, EventArgs e)
        {
            _dgvFillDetainedLicense();
        }



        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {



            string ColumnFilter = "None";

            switch (cbFilterBy.SelectedItem)
            {
                case "Detain ID":
                    ColumnFilter = "DetainID";
                    break;
                case "License ID":
                    ColumnFilter = "LicenseID";
                    break;
                case "Detain Date":
                    ColumnFilter = "DetainDate";
                    break;
                case "Is Released":
                    ColumnFilter = "IsReleased";
                    break;
                case "Release Date":
                    ColumnFilter = "ReleaseDate";
                    break;
                case "National No":
                    ColumnFilter = "NationalNo";
                    break;
                case "Full Name":
                    ColumnFilter = "FullName";
                    break;
                case "Release.App ID":
                    ColumnFilter = "ReleaseApplicationID";
                    break;

                default:
                    ColumnFilter = "None";
                    break;
            }

            if (string.IsNullOrWhiteSpace(txtFilterBy.Text.Trim()) || ColumnFilter == "None")
            {
                _dtDetainedLicensesList.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvDetainedLicensesList.Rows.Count.ToString();
                return;

            }
            if (ColumnFilter == "DetainID" || ColumnFilter == "LicenseID" || ColumnFilter == "ReleaseApplicationID")
            {
                _dtDetainedLicensesList.DefaultView.RowFilter = string.Format("{0}={1}", ColumnFilter, txtFilterBy.Text);
            }
            else
            {
                _dtDetainedLicensesList.DefaultView.RowFilter = string.Format("{0} LIKE '{1}%'", ColumnFilter, txtFilterBy.Text);

            }
            lblRecordsCount.Text = dgvDetainedLicensesList.Rows.Count.ToString();

        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            _dtDetainedLicensesList.DefaultView.RowFilter = "";
            txtFilterBy.Visible = (cbFilterBy.SelectedItem.ToString() != "None") 
                && (cbFilterBy.SelectedItem.ToString() != "Is Released");
            cbIsReleased.Visible = (cbFilterBy.SelectedItem.ToString() != "None")
               && (!txtFilterBy.Visible);
         
        }

        private void cbIsReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (
               cbFilterBy.SelectedItem != null && (cbFilterBy.SelectedItem.ToString() == "Detain ID" || cbFilterBy.SelectedItem.ToString() == "License ID" || cbFilterBy.SelectedItem.ToString() == "Release.App ID")
              )
            {
                e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));
            }
        }

        private void cbIsReleased_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            _dtDetainedLicensesList.DefaultView.RowFilter = cbIsReleased.SelectedItem.ToString() == "Yes" ? "IsReleased=1" :
                cbIsReleased.SelectedItem.ToString() == "No" ? "IsReleased=0" : "";

        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = clsLicense.FindDriverLicense(
                int.Parse(dgvDetainedLicensesList.CurrentRow.Cells[1].Value.ToString())
                ).DriverInfo.PersonID;
               
            frmPersonDetails personDetails = new frmPersonDetails(PersonID);
            personDetails.ShowDialog();
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLicenseInfo licenseInfo = new frmLicenseInfo(int.Parse(dgvDetainedLicensesList.CurrentRow.Cells[1].Value.ToString()));
            licenseInfo.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = clsLicense.FindDriverLicense(
                 int.Parse(dgvDetainedLicensesList.CurrentRow.Cells[1].Value.ToString())
                 ).DriverInfo.PersonID;
            frmShowLicensesHistory PersonLicensesHistory = new frmShowLicensesHistory(PersonID);
            PersonLicensesHistory.ShowDialog();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            int LicenseID = int.Parse(dgvDetainedLicensesList.CurrentRow.Cells[1].Value.ToString());
            releaseDetainedLicenseToolStripMenuItem.Enabled = clsLicense.isLicenseDetained(LicenseID);
            
           
        }

        private void btnDetainLicense_Click(object sender, EventArgs e)
        {
            frmDetainDrivingLicense DetainDrivingLicense = new frmDetainDrivingLicense();
            DetainDrivingLicense.ShowDialog();
            _dgvFillDetainedLicense();
        }

        private void btnReleaseLicense_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense ReleaseDetainDrivingLicense = new frmReleaseDetainedLicense();
            ReleaseDetainDrivingLicense.ShowDialog();
            _dgvFillDetainedLicense();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense ReleaseDetainDrivingLicense = new frmReleaseDetainedLicense(int.Parse(dgvDetainedLicensesList.CurrentRow.Cells[0].Value.ToString()));
            ReleaseDetainDrivingLicense.ShowDialog();
            _dgvFillDetainedLicense();

        }
    }
}
