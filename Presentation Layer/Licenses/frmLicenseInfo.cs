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
    public partial class frmLicenseInfo : Form
    {
        int _LicenseID = -1;
        public frmLicenseInfo(int LicenseID)
        {
            _LicenseID=LicenseID;
            InitializeComponent();
        }

        private void frmLicenseInfo_Load(object sender, EventArgs e)
        {
            ctrlLicenseInfo1.LoadDriverLicenseInfo(_LicenseID);
        }
    }
}
