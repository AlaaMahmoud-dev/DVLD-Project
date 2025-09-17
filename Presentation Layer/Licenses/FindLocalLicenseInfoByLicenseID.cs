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
    public partial class FindLocalLicenseInfoByLicenseID : UserControl
    {
        public event Action<int> OnLicenseSelected;
        protected virtual void LicenseSelected(int LicenseID)
        {
            Action<int> hundler = OnLicenseSelected;
            if (hundler != null)
            {
                hundler(LicenseID);
            }
        }

        bool _gbFilterEnabled = true;
        string _txtLicenseID = "";
        public bool gbFilterEnabled
        {
            set
            {
                _gbFilterEnabled = value;
                gbFilter.Enabled = value;
            }
            get { return _gbFilterEnabled; }
        }
        public string txtLicenseID
        {
            set
            {
                _txtLicenseID = value;
                txtLicenseIDValue.Text = value;
            }
            get { return _txtLicenseID; }
        }
        public FindLocalLicenseInfoByLicenseID()
        {
            InitializeComponent();

        }
        public void LoadLicenseInfo(int LicenseID)
        {
            _FindNow( LicenseID);
        }

            
        void _FindNow(int LicenseID)
        {
            ctrlLicenseInfo1.LoadDriverLicenseInfo(LicenseID);
        }
        private void txtLicenseIDValue_KeyPress(object sender, KeyPressEventArgs e)
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

        private void btnFindNow_Click(object sender, EventArgs e)
        {
            ctrlLicenseInfo1.LoadDriverLicenseInfo(int.Parse(txtLicenseIDValue.Text.ToString()));
            if(OnLicenseSelected!=null)
            {
                OnLicenseSelected(ctrlLicenseInfo1.LicenseID);
            }
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
