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

        DataTable dtDetainedLicenseList=null;
        string _Filtering_Item = "None";
        public frmManageDetainedLicenses()
        {
            InitializeComponent();

            dtDetainedLicenseList = new DataTable();
        }
        void _dgvFillDetainedLicense()
        {
            dtDetainedLicenseList = clsDetainedLicenses.GetDetainedLicensesList();

            dgvDetainedLiscensesList.Rows.Clear();

            for (int i = 0; i < dtDetainedLicenseList.Rows.Count; i++)
            {
                dgvDetainedLiscensesList.Rows.Add();
            }

            DataView dvDetainedLicensesList = dtDetainedLicenseList.DefaultView;

            for (int i = 0; i < dvDetainedLicensesList.Count; i++)
            {


                for (int j = 0; j < dgvDetainedLiscensesList.Columns.Count; j++)
                {
                    if (j==3)
                    {
                        if (bool.TryParse(dvDetainedLicensesList[i][j].ToString(),out bool Status))
                        {
                            dgvDetainedLiscensesList.Rows[i].Cells[j].Value=Status;
                            continue;
                        }
                    }
                    if (j==2||j==5||j==8)
                    {
                        if (dvDetainedLicensesList[i][j]==System.DBNull.Value)
                        {
                            dgvDetainedLiscensesList.Rows[i].Cells[j].Value = "";
                            continue;

                        }
                    }

                    dgvDetainedLiscensesList.Rows[i].Cells[j].Value = dvDetainedLicensesList[i][j].ToString();

                }
            }

            lblRecords.Text = (dgvDetainedLiscensesList.RowCount).ToString();
        }
        private void frmManageDetainedLicenses_Load(object sender, EventArgs e)
        {
            _dgvFillDetainedLicense();
        }
        private bool _Find(string Value, ref DataView dataView)
        {
            foreach (DataRow Record in dtDetainedLicenseList.Rows)
            {
                if (Record[_Filtering_Item].ToString().ToLower() == Value.ToLower())
                {
                    dataView.RowFilter = ($"{_Filtering_Item} = '{Record[_Filtering_Item]}'");
                    return true;
                }
            }

            return false;
        }
        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {
           

            if (string.IsNullOrWhiteSpace(txtFilterBy.Text.ToString()))
            {
                _dgvFillDetainedLicense();
               
            }

            else
            {
                DataView dvDetainedLicensesList = dtDetainedLicenseList.DefaultView;
                if (_Find(txtFilterBy.Text.ToString(), ref dvDetainedLicensesList))
                {


                    dgvDetainedLiscensesList.Rows.Clear();
                    for (int i = 0; i < dvDetainedLicensesList.Count; i++)
                    {
                        dgvDetainedLiscensesList.Rows.Add();
                    }

                    for (int i = 0; i < dvDetainedLicensesList.Count; i++)
                    {
                        for (int j = 0; j < dgvDetainedLiscensesList.Columns.Count; j++)
                        {

                            if (j == 3)
                            {
                                if (bool.TryParse(dvDetainedLicensesList[i][j].ToString(), out bool Status))
                                {
                                    dgvDetainedLiscensesList.Rows[i].Cells[j].Value = Status;
                                    continue;
                                }
                            }
                            if (j == 2 || j == 5 || j == 8)
                            {
                                if (dvDetainedLicensesList[i][j] == System.DBNull.Value)
                                {
                                    dgvDetainedLiscensesList.Rows[i].Cells[j].Value = "";
                                    continue;
                                }
                               
                            }

                            dgvDetainedLiscensesList.Rows[i].Cells[j].Value = dvDetainedLicensesList[i][j].ToString();
                        }
                    }



                    lblRecords.Text = dvDetainedLicensesList.Count.ToString();
                    
                }
                else
                {
                    dgvDetainedLiscensesList.Rows.Clear();
                    lblRecords.Text = "0";
                   
                }


            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {

           

            switch (cbFilterBy.SelectedIndex)
            {
                case 0:
                    _Filtering_Item = "None";
                    break;
                case 1:
                    _Filtering_Item = "NationalNo";
                    break;
                case 2:
                    _Filtering_Item = "FullName";
                    break;
                case 3:
                    _Filtering_Item = "DetainID";
                    break;
                case 4:
                    _Filtering_Item = "LicenseID";
                    break;
                case 5:
                    _Filtering_Item = "ReleaseApplicationID";
                    break;
                case 6:
                    _Filtering_Item = "IsReleased";
                    break;
                default:
                    _Filtering_Item = "None";
                    break;
            }



            if (cbFilterBy.SelectedItem.ToString() == "None")
            {
                _dgvFillDetainedLicense();
                txtFilterBy.Visible = false;
                cbIsReleased.Visible = false;
            }
            else
            {

                if (cbFilterBy.SelectedItem.ToString() == "Is Released")
                {

                    cbIsReleased.Visible = true;
                    txtFilterBy.Visible = false;
                    return;
                }


                cbIsReleased.Visible = false;
                txtFilterBy.Visible = true;

            }
        }

        private void cbIsReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (
               cbFilterBy.SelectedItem != null && (cbFilterBy.SelectedItem.ToString() == "Detain.ID" || cbFilterBy.SelectedItem.ToString() == "License.ID" || cbFilterBy.SelectedItem.ToString() == "Release App.ID")
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

        private void cbIsReleased_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            DataView dvDetainedLicenses = dtDetainedLicenseList.DefaultView;

            if (cbIsReleased.SelectedItem.ToString() == "All")
            {

                _dgvFillDetainedLicense();
                return;
            }
            else if (cbIsReleased.SelectedItem.ToString() == "Yes")
            {
                dvDetainedLicenses.RowFilter = "IsReleased=1";
            }
            else
            {
                dvDetainedLicenses.RowFilter = "IsReleased=0";
            }
            dgvDetainedLiscensesList.Rows.Clear();
            for (int i = 0; i < dvDetainedLicenses.Count; i++)
            {
                dgvDetainedLiscensesList.Rows.Add();
            }


            for (int i = 0; i < dvDetainedLicenses.Count; i++)
            {


                for (int j = 0; j < dgvDetainedLiscensesList.Columns.Count; j++)
                {
                    if (j == 3)
                    {
                        if (bool.TryParse(dvDetainedLicenses[i][j].ToString(), out bool Status))
                        {
                            dgvDetainedLiscensesList.Rows[i].Cells[j].Value = Status;
                            continue;
                        }
                    }
                    if (j == 2 || j == 5 || j == 8)
                    {
                        if (dvDetainedLicenses[i][j] == System.DBNull.Value)
                        {
                            dgvDetainedLiscensesList.Rows[i].Cells[j].Value = "";
                            continue;
                        }
                       
                    }

                    dgvDetainedLiscensesList.Rows[i].Cells[j].Value = dvDetainedLicenses[i][j].ToString();
                }
            }
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID =
                clsApplications.Find
                (
                clsDrivingLicense.FindDriverLicense
                (
                int.Parse(dgvDetainedLiscensesList.CurrentRow.Cells[1].Value.ToString())
                ).ApplicationID
                ).ApplicantPersonID;
            frmPersonDetails personDetails = new frmPersonDetails(PersonID);
            personDetails.ShowDialog();
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLicenseInfo licenseInfo = new frmLicenseInfo(int.Parse(dgvDetainedLiscensesList.CurrentRow.Cells[1].Value.ToString()));
            licenseInfo.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID =
                clsApplications.Find
                (
                clsDrivingLicense.FindDriverLicense
                (
                int.Parse(dgvDetainedLiscensesList.CurrentRow.Cells[1].Value.ToString())
                ).ApplicationID
                ).ApplicantPersonID;
            frmShowLicensesHistory PersonLicensesHistory = new frmShowLicensesHistory(PersonID);
            PersonLicensesHistory.ShowDialog();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            
            if (bool.Parse(dgvDetainedLiscensesList.CurrentRow.Cells[3].Value.ToString()))
            {
                releaseDetainedLicenseToolStripMenuItem.Enabled = false;
            }
            else
            {
                releaseDetainedLicenseToolStripMenuItem.Enabled = true;
            }
        }

        private void btnDetainLicense_Click(object sender, EventArgs e)
        {
            frmDetainDrivingLicense DetainDrivingLicense = new frmDetainDrivingLicense();
            DetainDrivingLicense.ShowDialog();
            _dgvFillDetainedLicense();
        }

        private void btnReleaseLicense_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense ReleaseDetainDrivingLicense = new frmReleaseDetainedLicense(-1);
            ReleaseDetainDrivingLicense.ShowDialog();
            _dgvFillDetainedLicense();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense ReleaseDetainDrivingLicense = new frmReleaseDetainedLicense(int.Parse(dgvDetainedLiscensesList.CurrentRow.Cells[0].Value.ToString()));
            ReleaseDetainDrivingLicense.ShowDialog();
            _dgvFillDetainedLicense();

        }
    }
}
