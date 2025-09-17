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
        DataTable dtUsers=new DataTable();
        string _Filtering_Item="None";
        public frmManageUsers()
        {
            InitializeComponent();
            dtUsers = clsUsers.UsersList();

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void _dgvFillUsers()
        {
            dtUsers=clsUsers.UsersList();

            

            int RowIndex = 0;
            dgvUsersList.Rows.Clear();

            for (int i = 0; i < dtUsers.Rows.Count; i++)
            {
                dgvUsersList.Rows.Add();
            }

            foreach (DataRow row in dtUsers.Rows)
            {

                // Ensure DataGridView has enough rows
                //if (!(RowIndex < dgvPeopleList.Rows.Count))
                //{
                //    dgvPeopleList.Rows.Add();
                //}
                for (int i = 0; i < dtUsers.Columns.Count; i++)
                {
                    //if (dtUsers.Columns[i].ToString() == "IsActive")
                    //{
                    //    if (row[i].ToString() == "0")
                    //    {
                    //        dgvUsersList.Rows[RowIndex].Cells[i].Value = "Male";
                    //    }
                    //    else
                    //    {
                    //        dgvUsersList.Rows[RowIndex].Cells[i].Value = "Female";
                    //    }
                    //    continue;
                    //}
                    // Check if the cell exists before setting its value
                    if (i < dgvUsersList.Rows[RowIndex].Cells.Count)
                    {
                        dgvUsersList.Rows[RowIndex].Cells[i].Value = row[i].ToString();
                    }
                }


                ++RowIndex;
            }

            //int RowIndex = 0;
            //foreach (DataRow Rows in dtPeopleList.Rows)
            //{
            //    for (int i=0;i<dtPeopleList.Columns.Count;i++)
            //    {
            //        dgvPeopleList.Rows[RowIndex].Cells[i].Value=Rows[i].ToString();
            //    }
            //    ++RowIndex;
            //}
            //dgvPeopleList.DataSource =dtPeopleList ;
            lblUsersCount.Text = (dgvUsersList.RowCount).ToString();


        }

        private void _LoadData()
        {
            _dgvFillUsers();
            cbFilterBy.Items.Add("None");
            cbFilterBy.Items.Add("UserID");
            cbFilterBy.Items.Add("PersonID");
            cbFilterBy.Items.Add("UserName");
            
            cbFilterBy.Items.Add("isActive");

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
        private bool _Find(string Value, ref DataView dataView)
        {
            foreach (DataRow Record in dtUsers.Rows)
            {
                if (Record[_Filtering_Item].ToString().ToLower() == Value.ToLower())
                {
                    dataView.RowFilter = ($"{_Filtering_Item} = '{Record[_Filtering_Item]}'");
                    return true;
                }
            }

            return false;
        }
        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {

          

            if (string.IsNullOrWhiteSpace(txtFilterBy.Text.ToString()))
            {
                _dgvFillUsers();
                //DataView dv = null;
                // dv = new DataView();
                // dv.

                //dgvPeopleList.Rows[0].Cells[0].Value = txtFilterBy.Text;
                //dgvPeopleList.Rows.Count = 0;
            }

            else
            {
                DataView dataView = dtUsers.DefaultView;
                if (_Find(txtFilterBy.Text.ToString(), ref dataView))
                {


                    dgvUsersList.Rows.Clear();
                    for (int i = 0; i < dataView.Count; i++)
                    {
                        dgvUsersList.Rows.Add();
                    }

                    for (int i = 0; i < dataView.Count; i++)
                    {
                        for (int j = 0; j < dgvUsersList.Columns.Count; j++)
                        {
                            dgvUsersList.Rows[i].Cells[j].Value = dataView[i][j].ToString();
                        }
                    }



                    lblUsersCount.Text = dataView.Count.ToString();
                    //DataRow[]Result= dtPeopleList.Select(_Filtering_Item + "=" + txtFilterBy.Text.ToString());
                    //foreach (DataRow Record in Result)
                    //{

                    //    Result
                    //}

                    //dgvPeopleList.DataSource = dtPeopleList.Select(_Filtering_Item + "=" + txtFilterBy.Text.ToString());
                }
                else
                {
                    dgvUsersList.Rows.Clear();
                    lblUsersCount.Text = "0";
                    //int RowsCount=dgvPeopleList.Rows.Count;
                    // for (int Index=0;Index< RowsCount;Index++)
                    // {
                    //     dgvPeopleList.Rows.RemoveAt(Index);
                    // }

                    //dgvPeopleList.DataBindings.Clear();
                    //dgvPeopleList.Rows.
                    //dgvPeopleList.Rows.Clear();
                }


            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            _Filtering_Item = cbFilterBy.SelectedItem.ToString();

            if (cbFilterBy.SelectedItem.ToString() == "None")
            {
                _dgvFillUsers();
                txtFilterBy.Visible = false;
                cbIsActive.Visible = false;
            }
            else
            {

                if (cbFilterBy.SelectedItem.ToString() == "isActive")
                {
                  
                    cbIsActive.Visible = true;
                    txtFilterBy.Visible = false;
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
                int Letter_Unicode_Value = Convert.ToInt32(e.KeyChar);

                int BackSpace_Unicode_Value = 8;

                if (!(Letter_Unicode_Value >= 48 && Letter_Unicode_Value <= 57))
                {
                    if (Letter_Unicode_Value == BackSpace_Unicode_Value)
                    {
                        return;
                    }
                    e.Handled = true;
                }
            }
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataView dvUsers = dtUsers.DefaultView;
            
            if (cbIsActive.SelectedItem.ToString() == "All")
            {
                
                _dgvFillUsers();
                return;
            }
            else if(cbIsActive.SelectedItem.ToString()=="Yes")
            {
                dvUsers.RowFilter = "IsActive=1";
            }
            else
            {
                dvUsers.RowFilter = "IsActive=0";
            }
            dgvUsersList.Rows.Clear();
            for (int i = 0;  i < dvUsers.Count;  i++)
                {
                    dgvUsersList.Rows.Add();
                }


            for (int i = 0; i < dvUsers.Count; i++)
            {
                for (int j = 0; j < dgvUsersList.Columns.Count; j++)
                {
                    dgvUsersList.Rows[i].Cells[j].Value = dvUsers[i][j].ToString();
                }
            }


           
            


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

            if (clsUsers.DeleteUser(UserID))
            {
                MessageBox.Show("User With ID [" + UserID.ToString() + "] Deleted Successfully");
                return;
            }
            MessageBox.Show("Deleting User With ID [" + UserID.ToString() + "]was not Completed Successfully");


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
