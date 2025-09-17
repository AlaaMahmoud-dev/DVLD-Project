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
    public partial class frmShowInternationalDrivingLicenseDetails : Form
    {
        int _InternationalDrivingLicenseID=-1;
        public frmShowInternationalDrivingLicenseDetails(int InternationalDrivingLicenseID)
        {
            InitializeComponent();
            _InternationalDrivingLicenseID = InternationalDrivingLicenseID;
        }

        private void frmShowInternationalDrivingLicenseDetails_Load(object sender, EventArgs e)
        {
            
            ctrlInternationalLicenseDetails1.LoadInternationLicenseInfo(_InternationalDrivingLicenseID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
