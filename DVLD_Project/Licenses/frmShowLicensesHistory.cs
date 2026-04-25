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
        public frmShowLicensesHistory(int PersonID = -1)
        {
            InitializeComponent();
            _PersonID = PersonID;
            
            dtLocalLicensesHistory = new DataTable();
            dtInternationalHistory = new DataTable();
            
        }
       
        void _LoadData()
        {
            _DriverID = clsPerson.GetDriverID(_PersonID);
            ctrlFindPersonByFilter1.FillPersonInfo(_PersonID);
            ctrlPersonLicensesHistory1.LoadPersonLicensesHistory(_PersonID);
        }

       

        private void tcLicensesHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void ctrlFindPersonByFilter1_OnPersonSelected(int obj)
        {
            int PersonID = obj;
            if (PersonID != -1)
                ctrlPersonLicensesHistory1.LoadPersonLicensesHistory(PersonID);
            else
                ctrlPersonLicensesHistory1.Clear();
        }

        private void frmShowLicensesHistory_Load(object sender, EventArgs e)
        {
            if (_PersonID != -1)
                _LoadData();
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
