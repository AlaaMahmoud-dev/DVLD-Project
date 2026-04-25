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
    public partial class frmManageApplicationTypes : Form
    {
        DataTable _dtApplicationTypes;
        public frmManageApplicationTypes()
        {
            InitializeComponent();
            
        }
        private void _dgvFillApplicationTypes()
        {
            _dtApplicationTypes = clsApplicationType.GetApplicationTypesList();

            dgvApplicationTypes.DataSource = _dtApplicationTypes;

            if (dgvApplicationTypes.RowCount > 0)
            {
                dgvApplicationTypes.Columns[0].HeaderText = "ID";
                dgvApplicationTypes.Columns[0].Width = 100;
                dgvApplicationTypes.Columns[1].HeaderText = "Title";
                dgvApplicationTypes.Columns[1].Width = 400;
                dgvApplicationTypes.Columns[2].HeaderText = "Fees";
                dgvApplicationTypes.Columns[2].Width = 200;
               
            }
            lblRecords.Text = dgvApplicationTypes.Rows.Count.ToString();


        }
        private void frmManageApplicationTypes_Load(object sender, EventArgs e)
        {
            _dgvFillApplicationTypes();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ApplicationTypeID= int.Parse(dgvApplicationTypes.CurrentRow.Cells[0].Value.ToString());
            
            frmUpdate_ApplicationType frmUpdate = new frmUpdate_ApplicationType(ApplicationTypeID);
            frmUpdate.ShowDialog();
            _dgvFillApplicationTypes();

        }
    }
}
