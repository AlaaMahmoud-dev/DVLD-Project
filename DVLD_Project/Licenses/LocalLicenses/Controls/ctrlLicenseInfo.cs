using DVLD_Business_Layar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project
{
    public partial class ctrlLicenseInfo : UserControl
    {
        int _LicenseID=-1;
        clsLicense _LocalDrivingLicenseInfo;
        public int LicenseID
        {
            get
            {
                return _LicenseID;
            }
        }

        public clsLicense SelectedLicenseInfo
        {
            get
            {
                return _LocalDrivingLicenseInfo;
            }
        }

        public ctrlLicenseInfo()
        {
            InitializeComponent();
          
        }

        public void ResetInfo()
        {
            _LocalDrivingLicenseInfo = new clsLicense();
            _LicenseID = -1;
            lblClass.Text = "[????]";
            lblActiveOrNot.Text = lblClass.Text;
            lblDateOfBirth.Text = lblClass.Text;
            lblDetainedOrNot.Text = lblClass.Text;
            lblDriverID.Text = lblClass.Text;
            lblExpirationDate.Text = lblClass.Text;
            lblGendor.Text = lblClass.Text;
            lblName.Text = lblClass.Text;
            lblIssueReason.Text = lblClass.Text;
            lblIssueDate.Text = lblClass.Text;
            lblLicenseID.Text = lblClass.Text;
            lblNationalNo.Text = lblClass.Text;
            lblNote.Text = lblClass.Text;

            pbDriverPicture.Image = Properties.Resources.Male_512;

        }
        private void _LoadPersonImage()
        {
            pbDriverPicture.Image = (clsPerson.enGender)_LocalDrivingLicenseInfo.ApplicationInfo.ApplicantPersonInfo.Gendor
                   == clsPerson.enGender.Male ? Properties.Resources.Male_512 
                   : Properties.Resources.Female_512;

            string imagePath = _LocalDrivingLicenseInfo.ApplicationInfo.ApplicantPersonInfo.ImagePath;

            if (!string.IsNullOrWhiteSpace(imagePath))
            {
                if (File.Exists(imagePath))
                    pbDriverPicture.ImageLocation = imagePath;
                else
                    MessageBox.Show("Could not find the selected person's image");
            }
           
        }
        public void LoadDriverLicenseInfo(int DriverLicenseID)
        {

            ResetInfo();
            _LicenseID = DriverLicenseID;
            _LocalDrivingLicenseInfo = clsLicense.FindDriverLicense(DriverLicenseID);
            if (_LocalDrivingLicenseInfo == null)
            {
                ResetInfo();
                MessageBox.Show($"Error Occurred, Driver License With ID[{DriverLicenseID}] Was Not Found", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            lblLicenseID.Text = DriverLicenseID.ToString();
            lblActiveOrNot.Text = _LocalDrivingLicenseInfo.isActive?"Yes":"No";
            lblDriverID.Text=_LocalDrivingLicenseInfo.DriverID.ToString();
            lblIssueDate.Text=_LocalDrivingLicenseInfo.IssueDate.ToString();
            lblExpirationDate.Text=_LocalDrivingLicenseInfo.ExpirationDate.ToString();
            lblClass.Text=_LocalDrivingLicenseInfo.LicenseClassInfo.ClassName;
            lblIssueReason.Text = _LocalDrivingLicenseInfo.GetIssueReasonText();
            lblNote.Text=_LocalDrivingLicenseInfo.Notes.ToString();
            lblName.Text = _LocalDrivingLicenseInfo.ApplicationInfo.ApplicantPersonInfo.FullName;
            lblNationalNo.Text = _LocalDrivingLicenseInfo.ApplicationInfo.ApplicantPersonInfo.NationalNo;
            lblDateOfBirth.Text = _LocalDrivingLicenseInfo.ApplicationInfo.ApplicantPersonInfo.DateOfBirth.ToString("yyyy/MMM/dd");
            lblGendor.Text= _LocalDrivingLicenseInfo.ApplicationInfo.ApplicantPersonInfo.GenderCaption;
            lblDetainedOrNot.Text = _LocalDrivingLicenseInfo.IsDetained ? "Yes" : "No";
            _LoadPersonImage();
            
        }
        private void ctrlLicenseInfo_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
