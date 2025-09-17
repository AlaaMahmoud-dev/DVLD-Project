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
    public partial class frmShowLicensesHistory : Form
    {
        int _PersonID=-1;
        int _DriverID = -1;
        DataTable dtLocalLicensesHistory=null;
        DataTable dtInternationalHistory=null;
        //DataTable dtLDLApplications;
        public frmShowLicensesHistory(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
            _DriverID = clsPerson.GetDriverID(_PersonID);
            dtLocalLicensesHistory = new DataTable();
            dtInternationalHistory = new DataTable();
            
        }

        void _SetRecordsCount()
        {

            if (tcLicensesHistory.SelectedTab.Text == "Local")
            {
                lblRecordsCount.Text = dtLocalLicensesHistory.Rows.Count.ToString();

            }
            else
            {
                lblRecordsCount.Text = dtInternationalHistory.Rows.Count.ToString();

            }
        }
        void _dgvLoadLocalLicensesHistory()
        {

            dtLocalLicensesHistory = clsDrivingLicense.GetLocalDrivingLicensesHistory(_DriverID);
            dgvLocalLicensesHistory.Rows.Clear();

            DataView dvLocalLicensesHistory = dtLocalLicensesHistory.DefaultView;


            for (int i = 0; i < dtLocalLicensesHistory.Rows.Count; i++)
            {
                dgvLocalLicensesHistory.Rows.Add();
            }

            for (int i = 0; i < dtLocalLicensesHistory.Rows.Count; i++)
            {
                for (int j = 0; j < dtLocalLicensesHistory.Columns.Count; j++)
                {
                    if (j == 2)
                    {

                        dgvLocalLicensesHistory.Rows[i].Cells[j].Value = clsDrivingLicense.GetLicenseClassName(int.Parse(dvLocalLicensesHistory[i][j].ToString()));

                        continue;
                    }

                    dgvLocalLicensesHistory.Rows[i].Cells[j].Value = dvLocalLicensesHistory[i][j].ToString();
                }
            }
            _SetRecordsCount();
        }

        void _dgvLoadInternationalLicensesHistory()
        {
            dtInternationalHistory = clsDrivingLicense.GetInternationalDrivingLicensesHistory(_DriverID);
            dgvInternationalLicensesHistory.Rows.Clear();

            DataView dvInternationalLicensesHistory = dtInternationalHistory.DefaultView;


            for (int i = 0; i < dtInternationalHistory.Rows.Count; i++)
            {
                dgvInternationalLicensesHistory.Rows.Add();
            }

            for (int i = 0; i < dtInternationalHistory.Rows.Count; i++)
            {
                for (int j = 0; j < dtInternationalHistory.Columns.Count; j++)
                {
                    if (j == 2)
                    {

                        dgvInternationalLicensesHistory.Rows[i].Cells[j].Value = clsDrivingLicense.GetLicenseClassName(int.Parse(dvInternationalLicensesHistory[i][j].ToString()));

                        continue;
                    }

                    dgvInternationalLicensesHistory.Rows[i].Cells[j].Value = dvInternationalLicensesHistory[i][j].ToString();
                }
            }
            _SetRecordsCount();
        }

        void _LoadData()
        {
            ctrlFindPersonByFilter1.FilterEnabled = false;
            ctrlFindPersonByFilter1.cbFilterSelectedItem = "PersonID";
            ctrlFindPersonByFilter1.txtFilterValue = _PersonID.ToString();
            ctrlFindPersonByFilter1.FindNow();
            _dgvLoadInternationalLicensesHistory();
            _dgvLoadLocalLicensesHistory();
        }

        private void frmShowLicensesHistory_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void tcLicensesHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            _SetRecordsCount();
        }
    }
}
