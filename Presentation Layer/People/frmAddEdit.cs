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
            DataTable dt = clsPerson.CountriesList();


            foreach (DataRow RowItem in dt.Rows)
            {
                cbCountry.Items.Add(RowItem["CountryName"]);
            }

        }


        private void _LoadData()
        {
            _FillCountriesInComboBox();

            pbPersonPicture.ImageLocation = @"C:\Users\96278.DESKTOP-URQBK3R\Downloads\Male.png";


            dtpDateOfBirth.MaxDate = new DateTime(DateTime.Now.Year - 18, DateTime.Now.Month, DateTime.Now.Day);

            rbMale.Checked = true;
            cbCountry.SelectedItem = "Jordan";

            if (Mode == enMode.AddNew)
            {
                lblMode.Text = "Add New Person";
                return;
            }

            _Person = clsPerson.FindPerson(_ID);

            if (_Person == null)
            {
                MessageBox.Show("This Page Will Close Because There is No Person Exists With ID: " + _ID.ToString());
                this.Close();
            }

            lblMode.Text = "Update Person Info";

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
            cbCountry.SelectedItem = clsPerson.GetCountryNameByCountryID(_Person.CountryID);
            if (_Person.Gendor == 'M')
            {
                rbMale.Checked = true;
            }
            else { rbFemale.Checked = true; }

            if (_Person.ImagePath != "")
            {
                pbPersonPicture.ImageLocation = _Person.ImagePath;
                llRemoveImage.Visible = true;
            }



        }

        private void frmAddEdit_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if (pbPersonPicture.ImageLocation != "C:\\Users\\96278.DESKTOP-URQBK3R\\Downloads\\Female.png"
                && pbPersonPicture.ImageLocation != "C:\\Users\\96278.DESKTOP-URQBK3R\\Downloads\\Male.png")
            {
                return;
            }

            if (rbFemale.Checked)
            {
                pbPersonPicture.ImageLocation = "C:\\Users\\96278.DESKTOP-URQBK3R\\Downloads\\Female.png";
            }
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {

            if (pbPersonPicture.ImageLocation != "C:\\Users\\96278.DESKTOP-URQBK3R\\Downloads\\Female.png"
                   && pbPersonPicture.ImageLocation != "C:\\Users\\96278.DESKTOP-URQBK3R\\Downloads\\Male.png")
            {
                return;
            }

            if (rbMale.Checked)
            {
                pbPersonPicture.ImageLocation = "C:\\Users\\96278.DESKTOP-URQBK3R\\Downloads\\Male.png";
            }
        }

        private void llRemoveOmage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbPersonPicture.ImageLocation = null;

            if (rbFemale.Checked)
            {
                pbPersonPicture.ImageLocation = "C:\\Users\\96278.DESKTOP-URQBK3R\\Downloads\\Female.png";
            }
            else
            {
                pbPersonPicture.ImageLocation = "C:\\Users\\96278.DESKTOP-URQBK3R\\Downloads\\Male.png";
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
                // Process the selected file
                string selectedFilePath = openFileDialog1.FileName;
                //MessageBox.Show("Selected Image is:" + selectedFilePath);

                pbPersonPicture.Load(selectedFilePath);
                //
                llRemoveImage.Visible = true;



            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

           
            _Person.NationalNo = txtNationalNo.Text;
            _Person.FirstName = txtFirstName.Text;
            _Person.SecondName = txtSecondName.Text;
            _Person.ThirdName = txtThirdName.Text;
            _Person.LastName = txtLastName.Text;
            _Person.DateOfBirth = dtpDateOfBirth.Value;
            _Person.Email = txtEmail.Text;
            _Person.Phone = txtPhone.Text;
            _Person.Gendor = rbMale.Checked ? 'M' : 'F';
            _Person.Address = txtAddress.Text;


            string MaleImageLocation = @"C:\Users\96278.DESKTOP-URQBK3R\Downloads\Male.png";
            string FemaleImageLocation = @"C:\Users\96278.DESKTOP-URQBK3R\Downloads\Female.png";

            if (pbPersonPicture.ImageLocation == MaleImageLocation || pbPersonPicture.ImageLocation == FemaleImageLocation)
            {
                _Person.ImagePath = "";
            }
            else
            {
                Guid ImageName = Guid.NewGuid();


                if (_Person.ImagePath == "")
                {
                    File.Copy(pbPersonPicture.ImageLocation, @"C:\DVLD Photos\" + ImageName + ".jpg");
                }
                else
                {
                    File.Copy(pbPersonPicture.ImageLocation, @"C:\DVLD Photos\" + ImageName + ".jpg");
                    File.Delete(_Person.ImagePath);
                }
                _Person.ImagePath = @"C:\DVLD Photos\" + ImageName + ".jpg";
               
            }

            _Person.CountryID = clsPerson.GetCountryID(cbCountry.SelectedItem.ToString());

            if (_Person.Save())
            {
                MessageBox.Show("Data Saved Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Data Saving not complete", "Error Occured", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

            if (Mode == enMode.AddNew)
            {
                lblMode.Text = "Update Person Info";
                lblPersonID.Text = _Person.ID.ToString();
            }
            DataBack?.Invoke(this,_Person.ID);

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
        //    if (string.IsNullOrEmpty(txtFirstName.Text))
        //    {
        //        errorProvider1.SetError(txtFirstName, "FirstName Should Have a Value");
        //        txtFirstName.Focus();

        //        e.Cancel= true; //it means you cant exit the control
                
        //    }
        //    else
        //    {
        //        e.Cancel=false;
        //        errorProvider1.SetError(txtFirstName, "");
        //    }
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

            string[] EmailFormat = { "@gmail.com", "@hotmail.com", "@yahoo.com" };


            if ((!string.IsNullOrWhiteSpace(txtEmail.Text)) && !(txtEmail.Text.EndsWith(EmailFormat[0]) || txtEmail.Text.EndsWith(EmailFormat[1]) || txtEmail.Text.EndsWith(EmailFormat[2])))
            {
                errorProvider1.SetError(txtEmail, "Email format should be [Email@example.com]");
                e.Cancel = true;
                txtEmail.Focus();


            }
            else
            {
                e.Cancel = false; errorProvider1.SetError(txtEmail, "");
            }



        }
        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {

            //if (string.IsNullOrEmpty(txtNationalNo.Text))
            //{
            //    errorProvider1.SetError(txtNationalNo, "Mustn't be empty");
            //    txtNationalNo.Focus();
            //    e.Cancel = true;
            //}
           
            ShouldHaveValue_Conrols_Validating(txtNationalNo, "NationalNo", e);

            if (!string.IsNullOrEmpty(txtNationalNo.Text))
            {
                DataTable dt = new DataTable();
                dt = clsPerson.NationalNoList();
                bool isNationalNoFound = false;
                foreach (DataRow dtNationalNo in dt.Rows)
                {
                    if (txtNationalNo.Text.ToString().ToLower() == dtNationalNo["NationalNo"].ToString().ToLower())
                    {
                        isNationalNoFound = true;
                        break;
                    }
                }
                if (isNationalNoFound)
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
    }
}
