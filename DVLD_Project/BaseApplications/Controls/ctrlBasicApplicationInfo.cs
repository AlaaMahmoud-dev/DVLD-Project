using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Business_Layar;

namespace DVLD_Project.BaseApplications.Controls
{
    public partial class ctrlBasicApplicationInfo: UserControl
    {
        private clsApplication _ApplicationInfo;
        private int _ApplicationID;
        public int ApplicationID
        {
            get
            {
                return _ApplicationID;
            }
        }
        public ctrlBasicApplicationInfo()
        {
            InitializeComponent();
        }
        private void _FillInfo()
        {
            _ApplicationID = _ApplicationInfo.ApplicationID;
            lblBasicAppID.Text = _ApplicationInfo.ApplicationID.ToString();
            lblStatus.Text = _ApplicationInfo.StatusText;
            lblAppFees.Text = _ApplicationInfo.PaidFees.ToString();
            lblAppType.Text = clsApplication.ApplicationTypeTitle((int)_ApplicationInfo.ApplicationTypeID);
            lblPersonName.Text = _ApplicationInfo.ApplicantPersonInfo.FullName;
            lblAppDate.Text = _ApplicationInfo.ApplicationDate.ToShortDateString();
            lblStatusDate.Text = _ApplicationInfo.LastStatusDate.ToShortDateString();
            lblCreatorUserName.Text = _ApplicationInfo.CreatedByUserInfo.UserName;
        }
        private void _ResetDefaultValues()
        {
            _ApplicationInfo = new clsApplication();
            _ApplicationID = -1;
            lblBasicAppID.Text = "[????]";
            lblStatus.Text = "[????]";
            lblAppFees.Text = "[$$$$]";
            lblAppType.Text = "[????]";
            lblPersonName.Text = "[????]";
            lblAppDate.Text = "[????]";
            lblStatusDate.Text = "[????]";
            lblCreatorUserName.Text = "[????]";
        }
        public void ResetDefaultValues()
        {
            _ResetDefaultValues();
        }
        public void FillBasicApplicationInfo(int ApplicationID)
        {
            _ApplicationInfo = clsApplication.Find(ApplicationID);
            if (_ApplicationInfo==null)
            {
                _ResetDefaultValues();
                MessageBox.Show("Application With ID [" + ApplicationID + "] is not Found", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillInfo();
        }
    }
}
