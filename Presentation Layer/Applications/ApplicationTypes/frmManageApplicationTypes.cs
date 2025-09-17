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
        DataTable dtApplicationTypes=null;
        public frmManageApplicationTypes()
        {
            InitializeComponent();
          
        }
        private void _dgvFillApplicationTypes()
        {
            dtApplicationTypes = clsApplicationTypes.ApplicationTypesList();
            dgvApplicationTypes.Rows.Clear();
            
            DataView dvApplicationTypes=dtApplicationTypes.DefaultView;


            for (int i = 0; i < dtApplicationTypes.Rows.Count; i++)
            {
                dgvApplicationTypes.Rows.Add();
            }

            for (int i = 0;i < dtApplicationTypes.Rows.Count;i++)
            {
                for (int j = 0; j < dtApplicationTypes.Columns.Count; j++)
                {

                    dgvApplicationTypes.Rows[i].Cells[j].Value = dvApplicationTypes[i][j].ToString();
                }
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
            clsApplicationTypes SelectedApplicationType=new clsApplicationTypes();

            SelectedApplicationType.ID = int.Parse(dgvApplicationTypes.CurrentRow.Cells[0].Value.ToString());

            SelectedApplicationType.Title = dgvApplicationTypes.CurrentRow.Cells[1].Value.ToString();

            SelectedApplicationType.Fees = double.Parse(dgvApplicationTypes.CurrentRow.Cells[2].Value.ToString());
            frmUpdate_ApplicationType frmUpdate = new frmUpdate_ApplicationType(SelectedApplicationType);
            frmUpdate.ShowDialog();
            _dgvFillApplicationTypes();

        }
    }
}
