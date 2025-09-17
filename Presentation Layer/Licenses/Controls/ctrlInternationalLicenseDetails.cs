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
    public partial class ctrlInternationalLicenseDetails : UserControl
    {
        public ctrlInternationalLicenseDetails()
        {
            InitializeComponent();
        }

        public void LoadInternationLicenseInfo(int InternationaLicenseID)
        {

            clsInternationalDrivingLicenses internationalDrivingLicense = clsInternationalDrivingLicenses.Find(InternationaLicenseID);
            clsDrivingLicense LocalLicense = clsDrivingLicense.FindDriverLicense(internationalDrivingLicense.LocalLicenseID);
            clsApplications basicApplication=clsApplications.Find(internationalDrivingLicense.ApplicationID);
            clsPerson PersonInfo = clsPerson.FindPerson(basicApplication.ApplicantPersonID);
            int DriverID = clsDrivers.GetDriverID(PersonInfo.ID);
            if (internationalDrivingLicense==null||LocalLicense==null||basicApplication==null||PersonInfo==null)
            {
                return;
            }

            lblName.Text = PersonInfo.FullName;
            lblInternationalLicenseID.Text=InternationaLicenseID.ToString();
            lblLicenseID.Text=internationalDrivingLicense.LocalLicenseID.ToString();
            lblGendor.Text = PersonInfo.Gendor == 'M' ? "Male" : "Female";
            lblExpirationDate.Text=internationalDrivingLicense.ExpirationDate.ToString("dd/MMM/yyyy");
            lblDriverID.Text=DriverID.ToString();
            lblDateOfBirth.Text=PersonInfo.DateOfBirth.ToString("dd/MMM/yyyy");
            lblApplicationID.Text=internationalDrivingLicense.ApplicationID.ToString();
            lblActiveOrNot.Text = internationalDrivingLicense.IsActive ? "Yes" : "No";
            lblIssueDate.Text=internationalDrivingLicense.IssueDate.ToString("dd/MMM/yyyy");
            lblNationalNo.Text = PersonInfo.NationalNo;
            if(PersonInfo.ImagePath!="")
            {
                pbDriverPicture.ImageLocation=PersonInfo.ImagePath;
            }




        }

        private void label22_Click(object sender, EventArgs e)
        {

        }
    }
}
