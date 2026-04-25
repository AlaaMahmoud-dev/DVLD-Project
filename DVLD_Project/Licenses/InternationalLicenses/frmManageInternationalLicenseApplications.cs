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

namespace DVLD_Project
{
    public partial class frmManageInternationalLicenseApplications : Form
    {
        DataTable dtInternationalLicenses = null;
        public frmManageInternationalLicenseApplications()
        {
            InitializeComponent();
        }

        void _dgvFillInternationalLicensesData()
        {

            dtInternationalLicenses = clsInternationalDrivingLicense.GetInternationalLicensesList();
            dgvInternationalLicenses.DataSource = dtInternationalLicenses;
            if (dgvInternationalLicenses.Rows.Count > 0)
            {
                dgvInternationalLicenses.Columns[0].HeaderText = "Int.License ID";
                dgvInternationalLicenses.Columns[0].Width = 120;

                dgvInternationalLicenses.Columns[1].HeaderText = "Application ID";
                dgvInternationalLicenses.Columns[1].Width = 100;

                dgvInternationalLicenses.Columns[2].HeaderText = "Driver ID";
                dgvInternationalLicenses.Columns[2].Width = 120;

                dgvInternationalLicenses.Columns[3].HeaderText = "L.License ID";
                dgvInternationalLicenses.Columns[3].Width = 120;


                dgvInternationalLicenses.Columns[4].HeaderText = "Issue Date";
                dgvInternationalLicenses.Columns[4].Width = 170;


                dgvInternationalLicenses.Columns[5].HeaderText = "Expiration Date";
                dgvInternationalLicenses.Columns[5].Width = 170;


                dgvInternationalLicenses.Columns[6].HeaderText = "Is Active";
                dgvInternationalLicenses.Columns[6].Width = 120;


                lblRecords.Text = dgvInternationalLicenses.Rows.Count.ToString();
            }
        }

        void _LoadData()
        {
            _dgvFillInternationalLicensesData();
         
            cbFilterBy.Items.Add("None");
            cbFilterBy.Items.Add("Int.License ID");
            cbFilterBy.Items.Add("L.D.L ID");
            cbFilterBy.Items.Add("App ID");
            cbFilterBy.Items.Add("Driver ID");
            cbFilterBy.Items.Add("Is Active");
            cbFilterBy.SelectedIndex = 0;
            txtFilterBy.Visible = false;
            cbIsActive.Visible = false;
        }

        private void frmManageInternationalLicenseApplications_Load(object sender, EventArgs e)
        {
            _LoadData();
        }
       
        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtInternationalLicenses.DefaultView.RowFilter = cbIsActive.SelectedItem.ToString() == "Yes" ? "isActive=1" :
                cbIsActive.SelectedItem.ToString() == "No" ? "isActive=0" : "";
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtInternationalLicenses.DefaultView.RowFilter = "";
         




            dtInternationalLicenses.DefaultView.RowFilter = "";
            txtFilterBy.Visible = (cbFilterBy.SelectedItem.ToString() != "None")
                && (cbFilterBy.SelectedItem.ToString() != "Is Active");
            cbIsActive.Visible = (cbFilterBy.SelectedItem.ToString() != "None")
               && (!txtFilterBy.Visible);

        }
        

        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFilterBy.Text.ToString()))
            {
                _dgvFillInternationalLicensesData();
               
            }
            string FilterColumn = "None";


            switch (cbFilterBy.SelectedItem)
            {
                case "Int.License ID":
                    FilterColumn = "InternationalLicenseID";
                    break;
                case "L.D.L ID":
                    FilterColumn = "IssuedUsingLocalLicenseID";
                    break;
                case "App ID":
                    FilterColumn = "ApplicationID";
                    break;
                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;
                case "Is Active":
                    FilterColumn = "isActive";
                    break;
                default:
                    FilterColumn = "None";
                    break;
            }
            if (string.IsNullOrWhiteSpace(txtFilterBy.Text.Trim()) || FilterColumn == "None")
            {
                dtInternationalLicenses.DefaultView.RowFilter = "";
                lblRecords.Text = dgvInternationalLicenses.Rows.Count.ToString();
                return;

            }
            if (FilterColumn == "InternationalLicenseID" || FilterColumn == "IssuedUsingLocalLicenseID" 
                || FilterColumn == "ApplicationID" || FilterColumn == "DriverID")
            {
                dtInternationalLicenses.DefaultView.RowFilter = string.Format("{0}={1}", FilterColumn, txtFilterBy.Text);
            }
            else
            {
                dtInternationalLicenses.DefaultView.RowFilter = string.Format("{0} LIKE '{1}%'", FilterColumn, txtFilterBy.Text);

            }
            lblRecords.Text = dgvInternationalLicenses.Rows.Count.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowLicensesHistory personLicenseHistory = new frmShowLicensesHistory(clsApplication.Find(int.Parse(dgvInternationalLicenses.CurrentRow.Cells[1].Value.ToString())).ApplicantPersonID);
            personLicenseHistory.ShowDialog();
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPersonDetails personDetails = new frmPersonDetails(clsApplication.Find(int.Parse(dgvInternationalLicenses.CurrentRow.Cells[1].Value.ToString())).ApplicantPersonID);
            personDetails.ShowDialog();
        }

        private void btnAddNewIntnationalLicense_Click(object sender, EventArgs e)
        {
            frmIssueNewInternationalDrivingLicense newInternationalDrivingLicense = new frmIssueNewInternationalDrivingLicense();
            newInternationalDrivingLicense.ShowDialog();
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowInternationalDrivingLicenseDetails InternationalLicenseDetails = new frmShowInternationalDrivingLicenseDetails(int.Parse(dgvInternationalLicenses.CurrentRow.Cells[0].Value.ToString()));
            InternationalLicenseDetails.ShowDialog();
        }

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (
              cbFilterBy.SelectedItem != null && (cbFilterBy.SelectedItem.ToString() == "Int.License ID" 
              || cbFilterBy.SelectedItem.ToString() == "L.D.L ID" || cbFilterBy.SelectedItem.ToString() == "App ID" ||
              cbFilterBy.SelectedItem.ToString() == "Driver ID")
             )
            {
                e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));
            }
        }
    }
}
