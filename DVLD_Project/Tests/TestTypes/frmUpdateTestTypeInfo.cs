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
    public partial class frmUpdateTestTypeInfo : Form
    {
        int _TestTypeID;
        clsTestType _TestTypeInfo;
        public frmUpdateTestTypeInfo(int ID)
        {
            InitializeComponent();
            _TestTypeID = ID;
            this._TestTypeInfo = new clsTestType();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmUpdateTestTypeInfo_Load(object sender, EventArgs e)
        {
            _TestTypeInfo = clsTestType.FindTestTypeInfoByID(_TestTypeID);
            if (_TestTypeInfo == null)
            {
                MessageBox.Show("TestType with ID[" + _TestTypeID + "] not found", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            lblID.Text=_TestTypeInfo.ID.ToString();
            txtDescription.Text=_TestTypeInfo.Description.ToString();
            txtFees.Text=_TestTypeInfo.Fees.ToString();
            txtTitle.Text=_TestTypeInfo.Title.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("There is Missing Data, Please check for red icons.",
                    "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            _TestTypeInfo.Title = txtTitle.Text.Trim();
            _TestTypeInfo.Description = txtDescription.Text.Trim();
            _TestTypeInfo.Fees=double.Parse(txtFees.Text.Trim());

            if (_TestTypeInfo.Save())
            {
                MessageBox.Show("Data Saved Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Saving Data Failed", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        
        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text.Trim()))
            {
                errorProvider1.SetError(txtTitle, "This Field is required!!");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtTitle, "");
                e.Cancel = false;
            }
        }

        private void txtDescription_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDescription.Text.Trim()))
            {
                errorProvider1.SetError(txtDescription, "This Field is required!!");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtDescription, "");
                e.Cancel = false;
            }
        }

        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFees.Text.Trim()))
            {
                errorProvider1.SetError(txtFees, "This Field is required!!");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtFees, "");
                e.Cancel = false;
            }

        }

        private void txtFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));
        }
    }
}
