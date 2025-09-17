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
    public partial class frmPersonDetails : Form
    {
        private int _PersonID = -1;
        public frmPersonDetails(int PersonID)
        {
            InitializeComponent();

            _PersonID = PersonID;
        }

        private void personDetails1_Load(object sender, EventArgs e)
        {
            personDetails1.FillPersonInfo(_PersonID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
