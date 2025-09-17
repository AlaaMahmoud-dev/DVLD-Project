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
    public partial class frmManageInternationalLicenseApplications : Form
    {
        DataTable dtInternationalLicenses = null;
        string _Filtering_Item = "None";
        public frmManageInternationalLicenseApplications()
        {
            InitializeComponent();
        }

        void _dgvFillInternationalLicensesData()
        {

            dtInternationalLicenses = clsInternationalDrivingLicenses.GetInternationalLicensesList();
            dgvInternationalLicenses.Rows.Clear();

            DataView dvInternationalLicenses = dtInternationalLicenses.DefaultView;


            for (int i = 0; i < dtInternationalLicenses.Rows.Count; i++)
            {
                dgvInternationalLicenses.Rows.Add();
            }

            for (int i = 0; i < dtInternationalLicenses.Rows.Count; i++)
            {
                for (int j = 0; j < dtInternationalLicenses.Columns.Count; j++)
                {
                    if (j == 6)
                    {
                        if (bool.TryParse(dvInternationalLicenses[i][j].ToString(),out bool value))
                        {
                            dgvInternationalLicenses.Rows[i].Cells[j].Value =value;
                        }
                        
                        continue;
                    }

                    dgvInternationalLicenses.Rows[i].Cells[j].Value = dvInternationalLicenses[i][j].ToString();
                }
            }
            lblRecords.Text = dgvInternationalLicenses.Rows.Count.ToString();
        }

        void _LoadData()
        {
            _dgvFillInternationalLicensesData();
            cbFilterBy.SelectedIndex = 0;
            txtFilterBy.Visible = false;
            cbIsActive.Visible = false;
        }

        private void frmManageInternationalLicenseApplications_Load(object sender, EventArgs e)
        {
            _LoadData();
        }
        private bool _Find(string Value, ref DataView dataView)
        {
            foreach (DataRow Record in dtInternationalLicenses.Rows)
            {
                if (Record[_Filtering_Item].ToString().ToLower() == Value.ToLower())
                {
                    dataView.RowFilter = ($"{_Filtering_Item} = '{Record[_Filtering_Item]}'");
                    return true;
                }
            }

            return false;
        }
        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataView dvIntLicenses = dtInternationalLicenses.DefaultView;

            if (cbIsActive.SelectedItem.ToString() == "All")
            {

                _dgvFillInternationalLicensesData();
                return;
            }
            else if (cbIsActive.SelectedItem.ToString() == "Yes")
            {
                dvIntLicenses.RowFilter = "IsActive=1";
            }
            else
            {
                dvIntLicenses.RowFilter = "IsActive=0";
            }
            dgvInternationalLicenses.Rows.Clear();
            for (int i = 0; i < dvIntLicenses.Count; i++)
            {
                dgvInternationalLicenses.Rows.Add();
            }


            for (int i = 0; i < dvIntLicenses.Count; i++)
            {
                for (int j = 0; j < dgvInternationalLicenses.Columns.Count; j++)
                {
                    dgvInternationalLicenses.Rows[i].Cells[j].Value = dvIntLicenses[i][j].ToString();
                }
            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {


            switch (cbFilterBy.SelectedIndex)
            {
                case 1:
                    _Filtering_Item = "InternationalLicenseID";
                    break;
                    case 2:
                    _Filtering_Item = "IssuedUsingLocalLicenseID";
                    break;
                    case 3:
                    _Filtering_Item = "ApplicationID";
                    break;
                    case 4:
                    
                    _Filtering_Item = "DriverID";
                    break;
                    case 5:
                    _Filtering_Item = "IsActive";
                    break;
            }


           

            if (cbFilterBy.SelectedItem.ToString() == "None")
            {
                _dgvFillInternationalLicensesData();
                txtFilterBy.Visible = false;
                cbIsActive.Visible = false;
            }
            else
            {

                if (cbFilterBy.SelectedItem.ToString() == "Is Active")
                {

                    cbIsActive.Visible = true;
                    txtFilterBy.Visible = false;
                    return;
                }


                cbIsActive.Visible = false;
                txtFilterBy.Visible = true;

            }
        }

        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFilterBy.Text.ToString()))
            {
                _dgvFillInternationalLicensesData();
               
            }

            else
            {
                DataView dataView = dtInternationalLicenses.DefaultView;
                if (_Find(txtFilterBy.Text.ToString(), ref dataView))
                {


                    dgvInternationalLicenses.Rows.Clear();
                    for (int i = 0; i < dataView.Count; i++)
                    {
                        dgvInternationalLicenses.Rows.Add();
                    }

                    for (int i = 0; i < dataView.Count; i++)
                    {
                        for (int j = 0; j < dgvInternationalLicenses.Columns.Count; j++)
                        {
                            dgvInternationalLicenses.Rows[i].Cells[j].Value = dataView[i][j].ToString();
                        }
                    }



                    lblRecords.Text = dataView.Count.ToString();
                  
                }
                else
                {
                    dgvInternationalLicenses.Rows.Clear();
                    lblRecords.Text = "0";
                    
                }


            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowLicensesHistory personLicenseHistory = new frmShowLicensesHistory(clsApplications.Find(int.Parse(dgvInternationalLicenses.CurrentRow.Cells[1].Value.ToString())).ApplicantPersonID);
            personLicenseHistory.ShowDialog();
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPersonDetails personDetails = new frmPersonDetails(clsApplications.Find(int.Parse(dgvInternationalLicenses.CurrentRow.Cells[1].Value.ToString())).ApplicantPersonID);
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
    }
}
