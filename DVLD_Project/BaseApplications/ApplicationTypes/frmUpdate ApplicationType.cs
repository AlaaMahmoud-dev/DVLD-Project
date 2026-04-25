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
    public partial class frmUpdate_ApplicationType : Form
    {
       clsApplicationType _ApplicationTypeInfo;
        int _ApplicationTypeID;
        public frmUpdate_ApplicationType(int applicationTypeID)
        {
            InitializeComponent();

           this._ApplicationTypeID=applicationTypeID;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmUpdate_ApplicationType_Load(object sender, EventArgs e)
        {
            _ApplicationTypeInfo = clsApplicationType.FindApplicationTypeInfoByID(_ApplicationTypeID);
           
            if (_ApplicationTypeInfo == null)
            {
                MessageBox.Show("Application Type with ID[" + _ApplicationTypeID + "] not found", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            else
            {

                lblID.Text = _ApplicationTypeInfo.ID.ToString();
                txtTitle.Text = _ApplicationTypeInfo.Title;
                txtFees.Text = _ApplicationTypeInfo.Fees.ToString();
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("There is Missing Data, Please check for red icons.",
                    "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            _ApplicationTypeInfo.Title=txtTitle.Text.Trim();
            _ApplicationTypeInfo.Fees=float.Parse (txtFees.Text.Trim());

            if(_ApplicationTypeInfo.Save())
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
