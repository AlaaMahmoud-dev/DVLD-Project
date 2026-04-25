using DVLD_Business_Layar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using DVLD_Project.Properties;
using System.Text.RegularExpressions;
namespace DVLD_Project
{
    public partial class frmAddEdit : Form
    {

        public delegate void DataBackEventHandler(object sender,int PersonID);

       
        public event DataBackEventHandler DataBack;
        enum enMode
        {
            AddNew = 1,
            Update = 2
        }
        enMode Mode = enMode.Update;
    
        clsPerson _Person;
        int _ID;
        public frmAddEdit(int PersonID)
        {
            InitializeComponent();
            if (PersonID == -1)
            {
                Mode = enMode.AddNew;
            }
            else
                Mode = enMode.Update;

            _ID = PersonID;
            _Person = new clsPerson();


        }
        private void _FillCountriesInComboBox()
        {
            DataTable dt = clsCountry.CountriesList();


            foreach (DataRow RowItem in dt.Rows)
            {
                cbCountry.Items.Add(RowItem["CountryName"]);
            }

        }

        private void _ResetDefaultValues()
        {
            _FillCountriesInComboBox();
            if (Mode==enMode.AddNew)
            {
                lblMode.Text = "Add New Person";
            }
            else
            {
                lblMode.Text = "Update Person Info";
            }
            if (rbMale.Checked)
                pbPersonPicture.Image = Resources.Male_512;
            else
                pbPersonPicture.Image = Resources.Female_512;

            llRemoveImage.Visible = (!string.IsNullOrEmpty(pbPersonPicture.ImageLocation));

            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            dtpDateOfBirth.MinDate = DateTime.Now.AddYears(-100);
            dtpDateOfBirth.Value = dtpDateOfBirth.MaxDate;

            cbCountry.SelectedIndex = cbCountry.FindString("Jordan");

            lblPersonID.Text ="N/A";
            txtFirstName.Text = "";
            txtNationalNo.Text = "";
            txtSecondName.Text = "";
            txtThirdName.Text = "";
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";
            txtPhone.Text = "";
            rbMale.Checked = true;
        }
        private void _LoadData()
        {
            
           

            _Person = clsPerson.FindPersonByID(_ID);

            if (_Person == null)
            {
                MessageBox.Show("This Page Will Close Because There is No Person Exists With ID: " + _ID.ToString());
                this.Close();
                return;
            }

            lblPersonID.Text = _ID.ToString();
            txtFirstName.Text = _Person.FirstName;
            txtNationalNo.Text = _Person.NationalNo;
            txtSecondName.Text = _Person.SecondName;
            txtThirdName.Text = _Person.ThirdName;
            txtLastName.Text = _Person.LastName;
            dtpDateOfBirth.Value = _Person.DateOfBirth;
            txtEmail.Text = _Person.Email;
            txtAddress.Text = _Person.Address;
            txtPhone.Text = _Person.Phone;
            cbCountry.SelectedItem = _Person.CountryInfo.CountryName;
            if (_Person.Gendor == (byte)clsPerson.enGender.Male)
            {
                rbMale.Checked = true;
            }
            else { rbFemale.Checked = true; }

            if (!string.IsNullOrEmpty(_Person.ImagePath))
            {
                pbPersonPicture.ImageLocation = _Person.ImagePath;
                
            }
            llRemoveImage.Visible = !string.IsNullOrEmpty(_Person.ImagePath);


        }

        private void frmAddEdit_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();
            if (Mode == enMode.Update)
                _LoadData();
        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(pbPersonPicture.ImageLocation))
            {
                return;
            }
            if (rbMale.Checked)
            {
                pbPersonPicture.Image = Resources.Male_512;
            }
            else
            {
                pbPersonPicture.Image = Resources.Female_512;
            }

        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(pbPersonPicture.ImageLocation))
            {
                return;
            }
            if (rbMale.Checked)
            {
                pbPersonPicture.Image = Resources.Male_512;
            }
            else
            {
                pbPersonPicture.Image = Resources.Female_512;
            }
        }

        private void llRemoveOmage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbPersonPicture.ImageLocation = null;

            if (rbFemale.Checked)
            {
                pbPersonPicture.Image=Resources.Female_512;
            }
            else
            {
                pbPersonPicture.Image = Resources.Male_512;
            }
            llRemoveImage.Visible = false;

        }

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                
                string selectedFilePath = openFileDialog1.FileName;
                

                pbPersonPicture.Load(selectedFilePath);
                
                llRemoveImage.Visible = true;



            }
        }

        private bool _HandlePersonImage()
        {

            if (!string.IsNullOrEmpty(pbPersonPicture.ImageLocation))
            {
                if (pbPersonPicture.ImageLocation == _Person.ImagePath)
                    return true;

                string ImagesFolder = Directory.GetCurrentDirectory() + "\\Images\\";
                Guid ImageName = Guid.NewGuid();
                FileInfo fileInfo= new FileInfo(pbPersonPicture.ImageLocation);
                string NewImagePath = ImagesFolder + ImageName + fileInfo.Extension;
                try { File.Copy(pbPersonPicture.ImageLocation, NewImagePath); }
                catch { return false; }
                
                if (!string.IsNullOrEmpty(_Person.ImagePath))
                {
                    try { File.Delete(_Person.ImagePath); }
                    catch { return false; }
                }
                _Person.ImagePath = NewImagePath;
                return true;

            }
            if (!string.IsNullOrEmpty(_Person.ImagePath))
            {
                try { File.Delete(_Person.ImagePath); }
                catch { return false; }
                
            }
            _Person.ImagePath = "";
            return true;
           

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("There is Missing Data, Please check for red icons.",
                    "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _Person.NationalNo = txtNationalNo.Text;
            _Person.FirstName = txtFirstName.Text;
            _Person.SecondName = txtSecondName.Text;
            _Person.ThirdName = txtThirdName.Text;
            _Person.LastName = txtLastName.Text;
            _Person.DateOfBirth = dtpDateOfBirth.Value;
            _Person.Email = txtEmail.Text;
            _Person.Phone = txtPhone.Text;
            _Person.Gendor = rbMale.Checked ? (byte)clsPerson.enGender.Male
                : (byte)clsPerson.enGender.Female;
            _Person.Address = txtAddress.Text;
            _Person.CountryID = clsCountry.GetCountryID(cbCountry.SelectedItem.ToString());


            //if (!_HandlePersonImage())
            //{
            //    return;
            //}

            if (_Person.Save())
            {
                if (Mode == enMode.AddNew)
                {
                    lblMode.Text = "Update Person Info";
                    lblPersonID.Text = _Person.ID.ToString();
                    _ID = _Person.ID;
                }
                MessageBox.Show("Data Saved Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DataBack?.Invoke(this, _Person.ID);
            }
            else
            {
                MessageBox.Show("Data Saving not complete", "Error Occured", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

            

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
        void ShouldHaveValue_Conrols_Validating(Control control,string ControlName, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(control.Text))
            {
                errorProvider1.SetError(control,ControlName+ " Should Have a Value");
                control.Focus();

                e.Cancel = true; //it means you cant exit the control

            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(control, "");
            }
        }
      
        private void txtFirstName_Validating(object sender, CancelEventArgs e)
        {

            ShouldHaveValue_Conrols_Validating(txtFirstName, "FirstName", e);
       
        }

       

        private void txtSecondName_Validating(object sender, CancelEventArgs e)
        {
            ShouldHaveValue_Conrols_Validating(txtSecondName, "SecondName", e);
        }

       

        private void txtLastName_Validating(object sender, CancelEventArgs e)
        {
            ShouldHaveValue_Conrols_Validating(txtLastName, "LastName", e);
        }

        private void txtPhone_Validating(object sender, CancelEventArgs e)
        {
            ShouldHaveValue_Conrols_Validating(txtPhone, "PhoneNumber", e);
        }

        private void txtAddress_Validating(object sender, CancelEventArgs e)
        {
            ShouldHaveValue_Conrols_Validating(txtAddress, "Address", e);
        }
        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmail.Text))
                return;

            if (!clsValidation.ValidateEmail(txtEmail.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail, "Email format should be [Email@example.com]");
                txtEmail.Focus();
            }

            else
            {
               
                errorProvider1.SetError(txtEmail, "");
            }



        }
        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {

           
           
            ShouldHaveValue_Conrols_Validating(txtNationalNo, "NationalNo", e);

            if (!string.IsNullOrEmpty(txtNationalNo.Text))
            {
                
               
                bool isNationalNoFound = clsPerson.IsPersonExistsByNationalNo(txtNationalNo.Text.Trim());
               
                if (isNationalNoFound && (txtNationalNo.Text.Trim()!=_Person.NationalNo))
                {
                    txtNationalNo.Focus();
                    e.Cancel = true;
                    errorProvider1.SetError(txtNationalNo, "This NationalNo is used by another Person");
                }
                else
                {
                    e.Cancel = false;
                    errorProvider1.SetError(txtNationalNo, "");
                }
            }
            
        }

        private void txtLastName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
