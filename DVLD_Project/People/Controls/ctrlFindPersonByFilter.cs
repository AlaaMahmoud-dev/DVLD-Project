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
        public int PersonID
        {
            get
            {
                return ctrlPersonDetails1.PersonID;
            }
        }

        public clsPerson SelectedPersonInfo
        {
            get
            {
                return ctrlPersonDetails1.SelectedPersonInfo;
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
                _FilterEnabled = value;
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
            FilterEnabled = false;
            cbFilterBy.SelectedItem = "PersonID";
            txtFilterBy.Text = PersonID.ToString();
            _FindNow();
            
        }
        public void FillPersonInfo(string NationalNo)
        {
            FilterEnabled = false;
            cbFilterBy.SelectedItem = "National No";
            txtFilterBy.Text = NationalNo;
            ctrlPersonDetails1.FillPersonInfo(NationalNo);
            _FindNow();
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
            cbFilterBy.SelectedIndex = 0;
            gbFilter.Enabled = true;
            cbFilterBy.SelectedIndex = 0;
            txtFilterBy.Focus();
            btnSearch.Enabled = false;

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
        
     
        private void ctrlFindPersonByFilter_Load_1(object sender, EventArgs e)
        {
           btnSearch.Enabled = false;
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            txtFilterBy.Focus();
            txtFilterBy.Enabled = (cbFilterBy.SelectedItem != null);
            txtFilterBy.Text = "";

        }

   
        private void NewPersonAdded(object sender, int PersonID)
        {
            
            cbFilterBy.SelectedItem = "PersonID";
            txtFilterBy.Text = PersonID.ToString();
            _FindNow();

        }
       

        private void txtFilterBy_TextChanged_1(object sender, EventArgs e)
        {
            btnSearch.Enabled = (txtFilterBy.Enabled && !string.IsNullOrEmpty(txtFilterBy.Text.ToString()));
           
        }

        private void txtFilterBy_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnSearch.PerformClick();
                return;
            }
            if (cbFilterBy.SelectedItem != null
                && (cbFilterBy.SelectedItem.ToString() == "PersonID"))
            {
                e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));
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
