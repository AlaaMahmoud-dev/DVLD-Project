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
    public partial class frmShowLDLApplicationDetails : Form
    {

        int _LDLApplicationID = -1;
        public frmShowLDLApplicationDetails(int LDLApplicationID)
        {
            InitializeComponent();
            _LDLApplicationID=LDLApplicationID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmShowLDLApplicationDetails_Load(object sender, EventArgs e)
        {
            ctrlLDLAndBasicApplicationsDetails1.LoadDLAndBasicApplicationsInfo(_LDLApplicationID);
        }
    }
}
