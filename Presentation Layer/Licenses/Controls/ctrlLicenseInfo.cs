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
    public partial class ctrlLicenseInfo : UserControl
    {
        int _LicenseID=-1;

        public int LicenseID
        {
            get
            {
                return _LicenseID;
            }
        }

        public ctrlLicenseInfo()
        {
            InitializeComponent();
          
        }

        public void ResetInfo()
        {
            _LicenseID = -1;
            lblClass.Text = "[????]";
            lblActiveOrNot.Text = lblClass.Text;
            lblDateOfBirth.Text = lblClass.Text;
            lblDetainedOrNot.Text = lblClass.Text;
            lblDriverID.Text = lblClass.Text;
            lblExpirationDate.Text = lblClass.Text;
            lblGendor.Text = lblClass.Text;
            lblName.Text = lblClass.Text;
            lblIssueReasine.Text = lblClass.Text;
            lblIssueDate.Text = lblClass.Text;
            lblLicenseID.Text = lblClass.Text;
            lblNationalNo.Text = lblClass.Text;
            lblNote.Text = lblClass.Text;

            pbDriverPicture.Image = Properties.Resources.Male_512;

        }
        public void LoadDriverLicenseInfo(int DriverLicenseID)
        {

           

            clsDrivingLicense DriverLicense = clsDrivingLicense.FindDriverLicense(DriverLicenseID);
            if (DriverLicense == null)
            {
                ResetInfo();
                MessageBox.Show($"Error Occured, Driver License With ID[{DriverLicenseID}] Was Not Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            _LicenseID= DriverLicenseID;
            lblLicenseID.Text = DriverLicenseID.ToString();
            
            lblActiveOrNot.Text = DriverLicense.isActive?"Yes":"No";
            lblDriverID.Text=DriverLicense.DriverID.ToString();
            lblIssueDate.Text=DriverLicense.IssueDate.ToString();
            lblExpirationDate.Text=DriverLicense.ExpirationDate.ToString();
            lblClass.Text=DriverLicense.LicenseClassName.ToString();
            switch (DriverLicense.IssueReasone)
            {
                case 1:
                    lblIssueReasine.Text = "First Time";
                    break;
                case 2:
                    lblIssueReasine.Text = "Renew License";
                    break;
                case 3:
                    lblIssueReasine.Text = "Replacement For Lost";
                    break;
                case 4:
                    lblIssueReasine.Text = "Replacement For Damaged";
                    break;

                default:
                    break;


            }
        
            lblNote.Text=DriverLicense.Notes.ToString();

            

            clsPerson personInfo = clsPerson.FindPerson(clsApplications.Find(DriverLicense.ApplicationID).ApplicantPersonID);

            lblName.Text = personInfo.FullName;
            lblNationalNo.Text = personInfo.NationalNo;
            lblDateOfBirth.Text = personInfo.DateOfBirth.ToString();
            lblGendor.Text=personInfo.Gendor.ToString().ToLower()=="m"?"Male":"Female";
            if (string.IsNullOrWhiteSpace(personInfo.ImagePath))
            {
                pbDriverPicture.Image = Properties.Resources.Male_512; ;
              
            }
            else
            {
                pbDriverPicture.ImageLocation=personInfo.ImagePath;
               

            }
            lblDetainedOrNot.Text = clsDrivingLicense.isLicenseDetained(DriverLicenseID) ? "Yes" : "No";


        }
        private void ctrlLicenseInfo_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
