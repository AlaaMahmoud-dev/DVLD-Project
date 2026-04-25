using DVLD_Business_Layar;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DVLD_Project.Licenses
{
    public partial class ctrlPersonLicensesHistory: UserControl
    {
        DataTable _dtLocalLicensesHistory;
        DataTable _dtInternationalHistory;
        int _DriverID;
        clsDriver _DriverInfo;
        public ctrlPersonLicensesHistory()
        {
            InitializeComponent();
            _dtInternationalHistory = new DataTable();
            _dtLocalLicensesHistory = new DataTable();
        }
        public void LoadDriverLicensesHistory(int DriverID)
        {
            _DriverID = DriverID;
            _DriverInfo = clsDriver.GetDriverInfoByID(DriverID);
          
            if (_DriverInfo == null)
            {
                MessageBox.Show($"There is no Driver with ID[{DriverID}]",
                    "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            _LoadLocalLicensesHistory();
            _LoadInternationalLicensesHistory();
        }
        public void LoadPersonLicensesHistory(int PersonID)
        {
            
            _DriverInfo = clsDriver.GetDriverInfoByPersonID(PersonID);
            if (_DriverInfo==null)
            {
                MessageBox.Show($"There is no Driver linked to person with ID[{PersonID}]",
                    "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            _DriverID = _DriverInfo.DriverID;
            _LoadLocalLicensesHistory();
            _LoadInternationalLicensesHistory();
        }
        void _SetRecordsCount()
        {

            if (tcLicensesHistory.SelectedTab.Text == "Local")
            {
                lblRecordsCount.Text = _dtLocalLicensesHistory.Rows.Count.ToString();

            }
            else
            {
                lblRecordsCount.Text = _dtInternationalHistory.Rows.Count.ToString();

            }
        }
        void _LoadLocalLicensesHistory()
        {
            _dtLocalLicensesHistory = clsLicense.GetLocalDrivingLicensesHistory(_DriverID);

            dgvLocalLicensesHistory.DataSource = _dtLocalLicensesHistory;
            if (dgvLocalLicensesHistory.Rows.Count > 0)
            {
                dgvLocalLicensesHistory.Columns[0].HeaderText = "Lic.ID";
                dgvLocalLicensesHistory.Columns[0].Width = 100;

                dgvLocalLicensesHistory.Columns[1].HeaderText = "App ID";
                dgvLocalLicensesHistory.Columns[1].Width = 100;

                dgvLocalLicensesHistory.Columns[2].HeaderText = "Class Name";
                dgvLocalLicensesHistory.Columns[2].Width = 250;

                dgvLocalLicensesHistory.Columns[3].HeaderText = "Issue Date";
                dgvLocalLicensesHistory.Columns[3].Width = 200;


                dgvLocalLicensesHistory.Columns[4].HeaderText = "Expiration Date";
                dgvLocalLicensesHistory.Columns[4].Width = 200;


                dgvLocalLicensesHistory.Columns[5].HeaderText = "Is Active";
                dgvLocalLicensesHistory.Columns[5].Width = 100;

            }
            _SetRecordsCount();
        }

        void _LoadInternationalLicensesHistory()
        {
            _dtInternationalHistory = clsInternationalDrivingLicense.GetInternationalDrivingLicensesHistory(_DriverID);

            dgvInternationalLicensesHistory.DataSource = _dtInternationalHistory;

            if (dgvInternationalLicensesHistory.Rows.Count > 0)
            {
                dgvInternationalLicensesHistory.Columns[0].HeaderText = "Int.Lic ID";
                dgvInternationalLicensesHistory.Columns[0].Width = 100;

                dgvInternationalLicensesHistory.Columns[1].HeaderText = "Lic.ID";
                dgvInternationalLicensesHistory.Columns[1].Width = 100;

                dgvInternationalLicensesHistory.Columns[2].HeaderText = "App ID";
                dgvInternationalLicensesHistory.Columns[2].Width = 100;

                dgvInternationalLicensesHistory.Columns[3].HeaderText = "Issue Date";
                dgvInternationalLicensesHistory.Columns[3].Width = 200;


                dgvInternationalLicensesHistory.Columns[4].HeaderText = "Expiration Date";
                dgvInternationalLicensesHistory.Columns[4].Width = 200;


                dgvInternationalLicensesHistory.Columns[5].HeaderText = "Is Active";
                dgvInternationalLicensesHistory.Columns[5].Width = 100;

            }
            
            _SetRecordsCount();
        }
        public void Clear()
        {
            _dtInternationalHistory.Clear();
            _dtLocalLicensesHistory.Clear();
        }
        private void tcLicensesHistory_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            _SetRecordsCount();
        }

        private void showLicenseInfoToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            frmLicenseInfo frmLicense = new frmLicenseInfo(int.Parse(dgvLocalLicensesHistory.CurrentRow.Cells[0].Value.ToString()));
            frmLicense.Show();
        }
    }
}
