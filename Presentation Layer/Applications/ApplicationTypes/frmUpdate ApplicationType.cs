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
       clsApplicationTypes applicationType=null;
        public frmUpdate_ApplicationType(clsApplicationTypes applicationType)
        {
            InitializeComponent();

           this.applicationType = applicationType;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmUpdate_ApplicationType_Load(object sender, EventArgs e)
        {
            lblID.Text = applicationType.ID.ToString();
            txtTitle.Text = applicationType.Title;
            txtFees.Text= applicationType.Fees.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            applicationType.Title=txtTitle.Text.ToString();
            applicationType.Fees=double.Parse (txtFees.Text.ToString());

            if(applicationType.Save())
            {
                MessageBox.Show("Data Saved Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Saving Data Faild", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            
        }
    }
}
