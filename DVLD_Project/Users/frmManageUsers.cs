using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Business_Layar;

namespace DVLD_Project
{
    public partial class frmManageUsers : Form
    {
        DataTable dtUsers;
       
        public frmManageUsers()
        {
            InitializeComponent();
            

        }
        private void _dgvFillUsers()
        {

            dtUsers=clsUser.UsersList();
            dgvUsersList.DataSource = dtUsers.DefaultView;
            if (dgvUsersList.Rows.Count>0)
            {
                dgvUsersList.Columns[0].HeaderText = "User ID";
                dgvUsersList.Columns[0].Width = 120;
                dgvUsersList.Columns[1].HeaderText = "Person ID";
                dgvUsersList.Columns[1].Width = 120;
                dgvUsersList.Columns[2].HeaderText = "Full Name";
                dgvUsersList.Columns[2].Width = 450;
                dgvUsersList.Columns[3].HeaderText = "Username";
                dgvUsersList.Columns[3].Width = 150;
                dgvUsersList.Columns[4].HeaderText = "Is Active";
                dgvUsersList.Columns[4].Width = 100;
            }
            lblUsersCount.Text = (dgvUsersList.RowCount).ToString();


        }

        private void _LoadData()
        {
            _dgvFillUsers();
            cbFilterBy.Items.Add("None");
            cbFilterBy.Items.Add("User ID");
            cbFilterBy.Items.Add("Person ID");
            cbFilterBy.Items.Add("Username");
            cbFilterBy.Items.Add("Is Active");

            cbFilterBy.SelectedItem = "None";


            cbIsActive.Items.Add("All");
            cbIsActive.Items.Add("Yes");
            cbIsActive.Items.Add("No");

            cbIsActive.SelectedItem = "All";


        }

        private void frmManageUsers_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
     
        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {

            string Filtering_Item;
            switch(cbFilterBy.SelectedItem)
            {
                case "User ID":
                    Filtering_Item = "UserID";
                    break;
                case "Person ID":
                    Filtering_Item = "PersonID";
                    break;
                case "Full Name":
                    Filtering_Item = "FullName";
                    break;
                case "Username":
                    Filtering_Item = "UserName";
                    break;
                default:
                    Filtering_Item = "None";
                    break;
              
            }
            if (string.IsNullOrWhiteSpace(txtFilterBy.Text.Trim()))
            {
                dtUsers.DefaultView.RowFilter = "";
                return;
            }

            if (Filtering_Item=="UserID"|| Filtering_Item == "PersonID")
            {
                dtUsers.DefaultView.RowFilter = string.Format("{0}={1}", Filtering_Item, int.Parse(txtFilterBy.Text.Trim()));

            }
            else
            {
                dtUsers.DefaultView.RowFilter = string.Format("{0} LIKE '{1}%'", Filtering_Item, txtFilterBy.Text.Trim());

            }
            lblUsersCount.Text = dgvUsersList.RowCount.ToString();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            

            if (cbFilterBy.SelectedItem.ToString() == "None")
            {
                _dgvFillUsers();
                txtFilterBy.Visible = false;
                cbIsActive.Visible = false;
            }
            else
            {

                if (cbFilterBy.SelectedItem.ToString() == "Is Active")
                {
                  
                    cbIsActive.Visible = true;
                    txtFilterBy.Visible = false;
                    cbIsActive.SelectedIndex = 0;
                    return;
                }
               
              
                cbIsActive.Visible=false;
                txtFilterBy.Visible = true;

            }
        }

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
          
            if (
                cbFilterBy.SelectedItem != null && (cbFilterBy.SelectedItem.ToString() == "UserID" || cbFilterBy.SelectedItem.ToString() == "PersonID")
               )
            {
                e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));
            }
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            
            if (cbIsActive.SelectedItem.ToString() == "All")
            {

                dtUsers.DefaultView.RowFilter = "";
            }
            else if(cbIsActive.SelectedItem.ToString()=="Yes")
            {
                dtUsers.DefaultView.RowFilter = "IsActive=1";
            }
            else
            {
                dtUsers.DefaultView.RowFilter = "IsActive=0";
            }

            lblUsersCount.Text = dgvUsersList.RowCount.ToString();

           
            


        }

        private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            frmAddEditUser addEditUserscreen = new frmAddEditUser(-1);
            addEditUserscreen.ShowDialog();
            _dgvFillUsers();
        }

        private void AddNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditUser addEditUser=new frmAddEditUser(-1);
            addEditUser.ShowDialog();
            _dgvFillUsers();
        }

        private void editToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmAddEditUser addEditUser = new frmAddEditUser(int.Parse( dgvUsersList.CurrentRow.Cells[0].Value.ToString()));
            addEditUser.ShowDialog();
            _dgvFillUsers();
        }

        private void sendEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature will be avialable soon");
        }

        private void mailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature will be avialable soon");

        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int UserID = int.Parse(dgvUsersList.CurrentRow.Cells[0].Value.ToString());
            if (MessageBox.Show("Are you sure you want to delete user with ID [" + UserID + "]??",
               "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
               == DialogResult.Yes)
            {
                if (clsUser.DeleteUser(UserID))
                {
                    MessageBox.Show("User With ID [" + UserID.ToString() + "] Deleted Successfully");
                    _dgvFillUsers();
                }
                else
                {
                    MessageBox.Show("Deleting User With ID [" + UserID.ToString() + "]was not Completed Successfully");
                }
            }
            
        }

        private void editUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowUserDetails showUserDetails = new frmShowUserDetails(int.Parse(dgvUsersList.CurrentRow.Cells[0].Value.ToString()));
            showUserDetails.ShowDialog();
            _dgvFillUsers();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangeUserPassword frmChange = new frmChangeUserPassword(int.Parse(dgvUsersList.CurrentRow.Cells[0].Value.ToString()));
            frmChange.ShowDialog();
            
        }
    }
}
