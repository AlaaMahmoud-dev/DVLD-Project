using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Business_Layar;
namespace DVLD_Project
{
    public partial class frmManagePeople : Form
    {
        

        static DataTable _dtPeopleList=clsPerson.PeopleList();
        DataTable _dtPeople = _dtPeopleList.DefaultView.ToTable(false,"PersonID",
            "NationalNo","FirstName","SecondName","ThirdName","LastName",
            "GendorCaption","DateOfBirth","CountryName","Phone","Email");
        public frmManagePeople()
        {
            
            InitializeComponent();
           
        }

        private void _RefreshPeopleList()
        {
             _dtPeopleList = clsPerson.PeopleList();

            _dtPeople.DefaultView.ToTable(false, "PersonID", "NationalNo",
                "FirstName", "SecondName", "ThirdName", "LastName",
                "GendorCaption", "DateOfBirth", "CountryName",
                "Phone", "Email");

            dgvPeopleList.DataSource = _dtPeople;

            lblPeopleCount.Text = (dgvPeopleList.RowCount).ToString();



        }

        private void _LoadData()
        {
           dgvPeopleList.DataSource= _dtPeople;
            lblPeopleCount.Text = dgvPeopleList.Rows.Count.ToString();

            cbFilterBy.Items.Add("None");
            cbFilterBy.Items.Add("Person ID");
            cbFilterBy.Items.Add("National No");
            cbFilterBy.Items.Add("First Name");
            cbFilterBy.Items.Add("Second Name");
            cbFilterBy.Items.Add("Third Name");
            cbFilterBy.Items.Add("Last Name");
            cbFilterBy.Items.Add("Nationality");
            cbFilterBy.Items.Add("Gendor");
            cbFilterBy.Items.Add("Phone");
            cbFilterBy.Items.Add("Email");

            if(dgvPeopleList.Rows.Count>0)
            {
                dgvPeopleList.Columns[0].HeaderText = "Person ID";
                dgvPeopleList.Columns[0].Width = 100;

                dgvPeopleList.Columns[1].HeaderText = "National No";
                dgvPeopleList.Columns[1].Width = 120;

                dgvPeopleList.Columns[2].HeaderText = "First Name";
                dgvPeopleList.Columns[2].Width = 120;


                dgvPeopleList.Columns[3].HeaderText = "Second Name";
                dgvPeopleList.Columns[3].Width = 120;


                dgvPeopleList.Columns[4].HeaderText = "Third Name";
                dgvPeopleList.Columns[4].Width = 120;


                dgvPeopleList.Columns[5].HeaderText = "Last Name";
                dgvPeopleList.Columns[5].Width = 140;


                dgvPeopleList.Columns[6].HeaderText = "Gendor";
                dgvPeopleList.Columns[6].Width = 100;

                dgvPeopleList.Columns[7].HeaderText = "Date Of Birth";
                dgvPeopleList.Columns[7].Width = 140;


                dgvPeopleList.Columns[8].HeaderText = "Nationality";
                dgvPeopleList.Columns[8].Width = 120;

                dgvPeopleList.Columns[9].HeaderText = "Phone";
                dgvPeopleList.Columns[9].Width = 120;


                dgvPeopleList.Columns[10].HeaderText = "Email";
                dgvPeopleList.Columns[10].Width = 160;



            }

            cbFilterBy.SelectedItem = "None";
            
        }
        private void frmManagePeople_Load(object sender, EventArgs e)
        {
          _LoadData();
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddEdit AddPerson = new frmAddEdit(-1);
            AddPerson.ShowDialog();
            _RefreshPeopleList();
        }

        private void lblPersonsCount_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmAddEdit AddPerson = new frmAddEdit(1028);
            AddPerson.ShowDialog();
            _RefreshPeopleList();
        }




        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEdit addEdit = new frmAddEdit(-1);
            addEdit.ShowDialog();
            _RefreshPeopleList();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEdit addEdit = new frmAddEdit(Convert.ToInt32( dgvPeopleList.CurrentRow.Cells[0].Value));
            addEdit.ShowDialog();
            _RefreshPeopleList();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = Convert.ToInt16(dgvPeopleList.CurrentRow.Cells[0].Value);
            if (MessageBox.Show("Are you sure you want to delete person with ID ["+PersonID+"]??",
                "Confirmation Message",MessageBoxButtons.YesNo,MessageBoxIcon.Question)
                ==DialogResult.Yes)
            {

                if (clsPerson.DeletePerson(PersonID))
                {
                    MessageBox.Show("Person with Id[" + PersonID.ToString() + "] was deleted successfully",
                        "Deleted successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshPeopleList();

                }
                else
                {
                    MessageBox.Show("Person was not deleted because it has data linked to it",
                        "Terminated!!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
            }

        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature will be available soon");
        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature will be available soon");
        }

    

        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {
            string ColoumnFilter = "None";

            switch (cbFilterBy.SelectedItem)
            {
                case "Person ID":
                    ColoumnFilter = "PersonID";
                    break;
                case "National No":
                    ColoumnFilter = "NationalNo";
                    break;
                case "First Name":
                    ColoumnFilter = "FirstName";
                    break;
                case "Second Name":
                    ColoumnFilter = "SecondName";
                    break;
                case "Third Name":
                    ColoumnFilter = "ThirdName";
                    break;
                case "Last Name":
                    ColoumnFilter = "LastName";
                    break;
                case "Nationality":
                    ColoumnFilter = "Nationality";
                    break;
                case "Gendor":
                    ColoumnFilter = "GendorCaption";
                    break;
                case "Phone":
                    ColoumnFilter = "Phone";
                    break;
                case "Email":
                    ColoumnFilter = "Email";
                    break;
                default:
                    ColoumnFilter = "None";
                    break;
            }

            if (txtFilterValue.Text.Trim()==""||ColoumnFilter=="None")
            {
                _dtPeople.DefaultView.RowFilter = "";
                lblPeopleCount.Text = dgvPeopleList.Rows.Count.ToString();
                return;

            }
            if (ColoumnFilter == "PersonID")
            {
                _dtPeople.DefaultView.RowFilter = string.Format("{0}={1}", ColoumnFilter, txtFilterValue.Text);
            }
            else
            {
                _dtPeople.DefaultView.RowFilter = string.Format("{0} LIKE '{1}%'", ColoumnFilter, txtFilterValue.Text);


            }
            lblPeopleCount.Text = dgvPeopleList.Rows.Count.ToString();


          



        }

        private void cbFilterBy_SelectedValueChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = cbFilterBy.SelectedItem.ToString() != "None";
            txtFilterValue.Text = "";
            txtFilterValue.Focus();
        }

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Person ID")
                e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar);

          
        }

      
        private void ShowDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmPersonDetails personDetails = new frmPersonDetails(Convert.ToInt32(dgvPeopleList.CurrentRow.Cells[0].Value.ToString()));
            personDetails.ShowDialog();
           
            _RefreshPeopleList();
        }
    }
}
