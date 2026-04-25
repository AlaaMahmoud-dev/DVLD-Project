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
    public partial class frmManageTestTypes : Form
    {
        DataTable _dtTestTypes;
        public frmManageTestTypes()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _dgvFillTestTypes()
        {
            
            _dtTestTypes = clsTestType.TestTypesList();

            if (dgvTestTypes.RowCount>0)
            {
                dgvTestTypes.Columns[0].HeaderText = "ID";
                dgvTestTypes.Columns[0].Width = 100;
                dgvTestTypes.Columns[1].HeaderText = "Title";
                dgvTestTypes.Columns[1].Width = 200;
                dgvTestTypes.Columns[2].HeaderText = "Description";
                dgvTestTypes.Columns[2].Width = 380;
                dgvTestTypes.Columns[3].HeaderText = "Fees";
                dgvTestTypes.Columns[3].Width = 120;
            }
            lblRecords.Text=dgvTestTypes.Rows.Count.ToString();


        }
        private void frmManageTestTypes_Load(object sender, EventArgs e)
        {
            _dgvFillTestTypes();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsTestType TestTypeInfo=new clsTestType();
            int ID = int.Parse(dgvTestTypes.CurrentRow.Cells[0].Value.ToString());
           

            frmUpdateTestTypeInfo UpdateTestTypeInfo=new frmUpdateTestTypeInfo(ID);
            UpdateTestTypeInfo.ShowDialog();
            _dgvFillTestTypes();
        }
    }
}
