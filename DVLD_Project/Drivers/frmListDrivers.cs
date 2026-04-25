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

namespace DVLD_Project.Drivers
{
    public partial class frmListDrivers: Form
    {
        static DataTable _dtDriversList = clsDriver.GetAllDriversList();
       
        private void _RefreshPeopleList()
        {
            _dtDriversList = clsDriver.GetAllDriversList();


            dgvDriversList.DataSource = _dtDriversList;

            lblRecordsCount.Text = (dgvDriversList.RowCount).ToString();



        }

        private void _LoadData()
        {
            dgvDriversList.DataSource = _dtDriversList;
            lblRecordsCount.Text = dgvDriversList.Rows.Count.ToString();

            cbFilterBy.Items.Add("None");
            cbFilterBy.Items.Add("Driver ID");
            cbFilterBy.Items.Add("Person ID");
            cbFilterBy.Items.Add("National No");
            cbFilterBy.Items.Add("First Name");
            cbFilterBy.Items.Add("Second Name");
            cbFilterBy.Items.Add("Third Name");
            cbFilterBy.Items.Add("Last Name");
            //cbFilterBy.Items.Add("Created Date");
            cbFilterBy.Items.Add("# Licenses");
          

            if (dgvDriversList.Rows.Count > 0)
            {
                dgvDriversList.Columns[0].HeaderText = "Driver ID";
                dgvDriversList.Columns[0].Width = 100;

                dgvDriversList.Columns[1].HeaderText = "Person ID";
                dgvDriversList.Columns[1].Width = 100;

                dgvDriversList.Columns[2].HeaderText = "National No";
                dgvDriversList.Columns[2].Width = 120;

                dgvDriversList.Columns[3].HeaderText = "First Name";
                dgvDriversList.Columns[3].Width = 120;


                dgvDriversList.Columns[4].HeaderText = "Second Name";
                dgvDriversList.Columns[4].Width = 120;


                dgvDriversList.Columns[5].HeaderText = "Third Name";
                dgvDriversList.Columns[5].Width = 120;


                dgvDriversList.Columns[6].HeaderText = "Last Name";
                dgvDriversList.Columns[6].Width = 140;



                dgvDriversList.Columns[7].HeaderText = "Creation Date";
                dgvDriversList.Columns[7].Width = 140;


                dgvDriversList.Columns[8].HeaderText = "# Licenses";
                dgvDriversList.Columns[8].Width = 150;

            }

            cbFilterBy.SelectedItem = "None";

        }
        public frmListDrivers()
        {
            InitializeComponent();
        }

        private void frmListDrivers_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string ColoumnFilter = "None";

            switch (cbFilterBy.SelectedItem)
            {
                case "Driver ID":
                    ColoumnFilter = "DriverID";
                    break;
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
                //case "Creation Date":
                //    ColoumnFilter = "CreatedDate";
                //    break;
                case "# Licenses":
                    ColoumnFilter = "NumberOfActiveLicenses";
                    break;
             
                default:
                    ColoumnFilter = "None";
                    break;
            }

            if (string.IsNullOrWhiteSpace(txtFilterValue.Text.Trim()) || ColoumnFilter == "None")
            {
                _dtDriversList.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvDriversList.Rows.Count.ToString();
                return;

            }
            if (ColoumnFilter == "PersonID" || ColoumnFilter == "DriverID" || ColoumnFilter == "NumberOfActiveLicenses")
            {
                _dtDriversList.DefaultView.RowFilter = string.Format("{0}={1}", ColoumnFilter, txtFilterValue.Text);
            }
            else
            {
                _dtDriversList.DefaultView.RowFilter = string.Format("{0} LIKE '{1}%'", ColoumnFilter, txtFilterValue.Text);


            }
            lblRecordsCount.Text = dgvDriversList.Rows.Count.ToString();

        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {

            txtFilterValue.Visible = cbFilterBy.SelectedItem.ToString() != "None";
            txtFilterValue.Text = "";
            txtFilterValue.Focus();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Person ID" || cbFilterBy.Text == "Driver ID")
                e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar);
        }

        private void showPersonInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPersonDetails personDetails = new frmPersonDetails
                (int.Parse(dgvDriversList.CurrentRow.Cells[1].Value.ToString()));
            personDetails.Show();
        }

        private void showLicensesHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowLicensesHistory showLicensesHistory = new frmShowLicensesHistory(
                int.Parse(dgvDriversList.CurrentRow.Cells[1].Value.ToString()));
            showLicensesHistory.Show();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
