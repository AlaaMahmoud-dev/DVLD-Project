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
    public partial class ctrlFindPersonByFilter : UserControl
    {

        public event Action<int> OnPersonSelected;
        protected virtual void PersonSelected(int PersonID)
        {
            Action<int>hundler = OnPersonSelected;
            if (hundler != null)
            {
                hundler(PersonID);
            }
        }
        bool _FilterEnabled = true;

        public bool FilterEnabled 
        {
            get 
            { 
                return _FilterEnabled;
            }
            set
            {
                _FilterEnabled=value;
                gbFilter.Enabled = _FilterEnabled;
            } 
        }
        string _cbFilterSelectedItem = "";
        public string cbFilterSelectedItem 
        { 
            get
            { 
                return _cbFilterSelectedItem;
            }
            set
            { 
                _cbFilterSelectedItem=value;
                cbFilterBy.SelectedItem = _cbFilterSelectedItem;
            }
        }
        string _txtFilterValue = "";
        public string txtFilterValue
        {
            get 
            {
                return _txtFilterValue;
            } 
            set
            { 
                _txtFilterValue=value;
                txtFilterBy.Text= _txtFilterValue; 
            } 
        }

        public  void FillPersonInfo(int PersonID)
        {
            ctrlPersonDetails1.FillPersonInfo(PersonID);
            
        }
        public ctrlFindPersonByFilter()
        {
            InitializeComponent();
            
        }

        private void gbFilter_Enter(object sender, EventArgs e)
        {

        }
        void _LoadData()
        {
            gbFilter.Enabled = true;
            cbFilterBy.SelectedIndex = 0;
            txtFilterBy.Focus();
           // btnSearch.Enabled = true;

        }
        private void ctrlFindPersonByFilter_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void _FindNow()
        {
            switch(cbFilterBy.SelectedItem.ToString())
            {
                case "PersonID":
                    ctrlPersonDetails1.FillPersonInfo(int.Parse(txtFilterBy.Text.ToString()));
                    break;
                case "National No":
                    ctrlPersonDetails1.FillPersonInfo(txtFilterBy.Text.ToString());
                    break;

                default:
                    break;

            }

            if (OnPersonSelected != null && gbFilter.Enabled == true)
            {
                OnPersonSelected(ctrlPersonDetails1.PersonID);
               
            }
        }
        public void FindNow()
        {
            _FindNow();
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
          
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
           
        }

        private void ctrlFindPersonByFilter_Load_1(object sender, EventArgs e)
        {
           btnSearch.Enabled = false;
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.SelectedItem == null)
            {
                txtFilterBy.Enabled = false;
            }
            txtFilterBy.Text = "";
            txtFilterBy.Focus();

        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {
          
        }

        private void btnAddNewPerson_Click_1(object sender, EventArgs e)
        {
            
        }
        private void NewPersonAdded(object sender, int PersonID)
        {
            // Handle the data received from AddNewForm
            cbFilterBy.SelectedItem = "PersonID";
            txtFilterBy.Text = PersonID.ToString();
            _FindNow();

        }
        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
           
          
        }

      

        private void ctrlPersonDetails1_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ctrlPersonDetails1_Load_1(object sender, EventArgs e)
        {

        }

        private void txtFilterBy_TextChanged_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFilterBy.Text.ToString()))
            {
                btnSearch.Enabled = false;
            }
            else
            {
                btnSearch.Enabled = true;
            }
        }

        private void txtFilterBy_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (
            cbFilterBy.SelectedItem != null && (cbFilterBy.SelectedItem.ToString() == "PersonID")
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
                    if (e.KeyChar == (char)Keys.Enter)
                    {
                        _FindNow();
                        return;
                    }
                    e.Handled = true;
                }
            }
        }

        private void btnAddNewPerson_Click_2(object sender, EventArgs e)
        {
            frmAddEdit addEdit = new frmAddEdit(-1);
            addEdit.DataBack += NewPersonAdded;
            addEdit.ShowDialog();
        }

        private void btnSearch_Click_2(object sender, EventArgs e)
        {
            _FindNow();
        }
    }
}
