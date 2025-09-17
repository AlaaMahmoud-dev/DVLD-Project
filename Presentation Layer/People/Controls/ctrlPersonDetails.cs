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
    public partial class ctrlPersonDetails : UserControl
    {
        public int PersonID=-1;
        public ctrlPersonDetails()
        {
            InitializeComponent();
        }

        public void FillPersonInfo(int PersonID)
        {

            if (string.IsNullOrWhiteSpace(PersonID.ToString()))
            { return; }
            clsPerson person = clsPerson.FindPerson(PersonID);

            if(person == null)
            {
                llEditInfo.Visible = false;
                MessageBox.Show("Person With ID ["+PersonID+"] is not Found","Not Found",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            else
            {
                this.PersonID = PersonID;
                lblPersonID.Text=person.ID.ToString();

                lblName.Text = person.FullName;
                lblNationalNo.Text = person.NationalNo.ToString();
                lblPhone.Text = person.Phone.ToString();
                lblCountry.Text = clsPerson.GetCountryNameByCountryID(person.CountryID);
                lblEmail.Text = person.Email.ToString();
                lblGendor.Text = person.Gendor.ToString();
                lblDateOfBirth.Text = person.DateOfBirth.ToString();
                lblAddress.Text = person.Address.ToString();

                if (!string.IsNullOrWhiteSpace(person.ImagePath.ToString()))
                {
                    pbPersonPicture.ImageLocation = person.ImagePath.ToString();
                }
               else
                    pbPersonPicture.Image = Properties.Resources.Male_512;




            }

        }
        public  void FillPersonInfo(string NationalNo)
        {
           if(string.IsNullOrWhiteSpace(NationalNo))
            { return; }
            clsPerson person = clsPerson.FindPerson(NationalNo);

            if (person == null)
            {
                llEditInfo.Visible = false;
                
               
                

                MessageBox.Show("Person With NationalNo [" + NationalNo + "] is not Found", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblAddress.Text = "[????]";
                lblCountry.Text = "[????]";
                lblDateOfBirth.Text = "[????]";
                lblEmail.Text = "[????]";
                lblPhone.Text = "[????]";
                lblGendor.Text = "[????]";
                lblName.Text = "[????]";
                lblNationalNo.Text = "[????]";
                lblPersonID.Text = "[????]";
                pbPersonPicture.Image = Properties.Resources.Male_512;
                return;
            }
            else
            {

                PersonID =person.ID;

                lblPersonID.Text = person.ID.ToString();

                lblName.Text = person.FullName;
                lblNationalNo.Text = person.NationalNo.ToString();
                lblPhone.Text = person.Phone.ToString();
                lblCountry.Text = clsPerson.GetCountryNameByCountryID(person.CountryID);
                lblEmail.Text = person.Email.ToString();
                lblGendor.Text = person.Gendor.ToString();
                lblDateOfBirth.Text = person.DateOfBirth.ToString();
                lblAddress.Text = person.Address.ToString();

                if (!string.IsNullOrWhiteSpace(person.ImagePath.ToString()))
                {
                    pbPersonPicture.ImageLocation = person.ImagePath.ToString();
                }
                else
                    pbPersonPicture.Image = Properties.Resources.Male_512;


            }

        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void PersonDetails_Load(object sender, EventArgs e)
        {

        }
        void PersonInfoUpdated(object sender,int PersonID)
        {
            FillPersonInfo(PersonID);
        }
        private void llEditInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
          
        }

        private void llEditInfo_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddEdit addEdit = new frmAddEdit(PersonID);
            addEdit.DataBack += PersonInfoUpdated;
            addEdit.ShowDialog();
        }
    }
}
