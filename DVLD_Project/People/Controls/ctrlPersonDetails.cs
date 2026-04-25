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
        private int _PersonID;
        public int PersonID
        {
            get
            {
                return _PersonID;
            }
        }
        private clsPerson _PersonInfo;

        public clsPerson SelectedPersonInfo
        {
            get
            {
                return _PersonInfo;
            }
        }
        public ctrlPersonDetails()
        {
            InitializeComponent();
        }
        private void _ResetDefaultValues()
        {
            _PersonID = -1;
            _PersonInfo = new clsPerson();
            lblAddress.Text = "[????]";
            lblCountry.Text = "[????]";
            lblDateOfBirth.Text = "[????]";
            lblEmail.Text = "[????]";
            lblPhone.Text = "[????]";
            lblGendor.Text = "[????]";
            lblName.Text = "[????]";
            lblNationalNo.Text = "[????]";
            lblPersonID.Text = "[????]";
            pbPersonPicture.ImageLocation = "";
            pbPersonPicture.Image = Properties.Resources.Male_512;
            llEditInfo.Visible = false;
        }

        private void _FillPersonInfo()
        {
            llEditInfo.Visible = true;
            this._PersonID = _PersonInfo.ID;
            lblPersonID.Text = _PersonInfo.ID.ToString();
            lblName.Text = _PersonInfo.FullName;
            lblNationalNo.Text = _PersonInfo.NationalNo.ToString();
            lblPhone.Text = _PersonInfo.Phone.ToString();
            lblCountry.Text = clsCountry.GetCountryNameByCountryID(_PersonInfo.CountryID);
            lblEmail.Text = _PersonInfo.Email.ToString();
            lblGendor.Text = _PersonInfo.GenderCaption;
            lblDateOfBirth.Text = _PersonInfo.DateOfBirth.ToString("yyyy/MMM/dd");
            lblAddress.Text = _PersonInfo.Address.ToString();

            if (!string.IsNullOrWhiteSpace(_PersonInfo.ImagePath.ToString()))
            {
                pbPersonPicture.ImageLocation = _PersonInfo.ImagePath.ToString();
            }
        }

        public void FillPersonInfo(int PersonID)
        {

            _PersonInfo = clsPerson.FindPersonByID(PersonID);

            if(_PersonInfo == null)
            {
                _ResetDefaultValues();
                MessageBox.Show("Person With ID ["+PersonID+"] is not Found","Not Found",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            else
            {
                _FillPersonInfo();

            }

        }
        public  void FillPersonInfo(string NationalNo)
        {
         
             _PersonInfo = clsPerson.FindPersonByNationalNo(NationalNo);

            if (_PersonInfo == null)
            {
                _ResetDefaultValues();
                MessageBox.Show("Person With NationalNo [" + NationalNo + "] is not Found", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                _FillPersonInfo();

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
