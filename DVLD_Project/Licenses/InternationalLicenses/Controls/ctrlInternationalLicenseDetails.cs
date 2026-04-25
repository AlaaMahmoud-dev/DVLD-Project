using DVLD_Business_Layar;
using DVLD_Project.Properties;
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
    public partial class ctrlInternationalLicenseDetails : UserControl
    {
        int _InternationalLicenseID;
        clsInternationalDrivingLicense _InternationalLicenseInfo;
        public ctrlInternationalLicenseDetails()
        {
            InitializeComponent();
        }

        private void _LoadPersonImage()
        {
            if (_InternationalLicenseInfo.ApplicantPersonInfo.Gendor == (byte)clsPerson.enGender.Male)
                pbDriverPicture.Image = Resources.Male_512;
            else
                pbDriverPicture.Image = Resources.Female_512;
            if (!string.IsNullOrWhiteSpace(_InternationalLicenseInfo.ApplicantPersonInfo.ImagePath))
            {
                if (File.Exists(_InternationalLicenseInfo.ApplicantPersonInfo.ImagePath))
                    pbDriverPicture.ImageLocation = _InternationalLicenseInfo.ApplicantPersonInfo.ImagePath;
                else
                    MessageBox.Show("Can't find person's image.");
            }
            
        }
        public void LoadInternationalLicenseInfo(int InternationalLicenseID)
        {

            _InternationalLicenseInfo = clsInternationalDrivingLicense.FindInfoByID(InternationalLicenseID);
            if (_InternationalLicenseInfo == null)
            {
                MessageBox.Show($"International License With ID[{InternationalLicenseID}] Wasn't Found.",
                    "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblName.Text = _InternationalLicenseInfo.ApplicantPersonInfo.FullName;
            lblInternationalLicenseID.Text=InternationalLicenseID.ToString();
            lblLicenseID.Text= _InternationalLicenseInfo.LocalLicenseID.ToString();
            lblGendor.Text = _InternationalLicenseInfo.ApplicantPersonInfo.Gendor == (byte)clsPerson.enGender.Male ? "Male" : "Female";
            lblExpirationDate.Text= _InternationalLicenseInfo.ExpirationDate.ToString("dd/MMM/yyyy");
            lblDriverID.Text= _InternationalLicenseInfo.DriverID.ToString();
            lblDateOfBirth.Text= _InternationalLicenseInfo.ApplicantPersonInfo.DateOfBirth.ToString("dd/MMM/yyyy");
            lblApplicationID.Text= _InternationalLicenseInfo.ApplicationID.ToString();
            lblActiveOrNot.Text = _InternationalLicenseInfo.IsActive ? "Yes" : "No";
            lblIssueDate.Text= _InternationalLicenseInfo.IssueDate.ToString("dd/MMM/yyyy");
            lblNationalNo.Text = _InternationalLicenseInfo.ApplicantPersonInfo.NationalNo;
            _LoadPersonImage();
            




        }

        private void label22_Click(object sender, EventArgs e)
        {

        }
    }
}
