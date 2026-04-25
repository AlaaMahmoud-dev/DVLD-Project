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
    public partial class ctrlLocalLicenseInfoWithFilter : UserControl
    {
        public event Action<int> OnLicenseSelected;
        //protected virtual void LicenseSelected(int LicenseID)
        //{
        //    Action<int> hundler = OnLicenseSelected;
        //    if (hundler != null)
        //    {
        //        hundler(LicenseID);
        //    }
        //}

        bool _gbFilterEnabled = true;
        public bool gbFilterEnabled
        {
            set
            {
                _gbFilterEnabled = value;
                gbFilter.Enabled = value;
            }
            get { return _gbFilterEnabled; }
        }
        public int LicenseID
        {
            get { return ctrlLicenseInfo1.LicenseID; }
        }
        public  clsLicense LicenseInfo
        {
            get { return ctrlLicenseInfo1.SelectedLicenseInfo; }
        }
        public ctrlLocalLicenseInfoWithFilter()
        {
            InitializeComponent();

        }
        public void LoadLicenseInfo(int LicenseID)
        {

            txtLicenseIDValue.Text = LicenseID.ToString();
            _FindNow();
        }

            
        void _FindNow()
        {
            ctrlLicenseInfo1.LoadDriverLicenseInfo(int.Parse(txtLicenseIDValue.Text));
            if (OnLicenseSelected != null)
            {
                OnLicenseSelected(ctrlLicenseInfo1.LicenseID);
            }
        }
        private void txtLicenseIDValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnFindNow.PerformClick();
                return;
            }
            e.Handled =( !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) );
        }

        private void btnFindNow_Click(object sender, EventArgs e)
        {
            _FindNow();           
        }

        private void txtLicenseIDValue_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLicenseIDValue.Text.ToString()))
            {
                btnFindNow.Enabled = false;
                return;
            }
            btnFindNow.Enabled=true;
        }

      
    }
}
