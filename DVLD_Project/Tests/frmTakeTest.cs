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
    public partial class frmTakeTest : Form
    {
        
        clsTestType.enTestType _TestTypeID;
        int _TestID;
        clsTest _TestInfo;
        int _AppointmentID = -1;
        public frmTakeTest(int AppointmentID,clsTestType.enTestType TestTypeID)
        {
            InitializeComponent();
            _TestTypeID = TestTypeID;
            _AppointmentID = AppointmentID;
            _TestID = -1;
            _TestInfo = new clsTest();
        }

        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            ctrlScheduledTest1.TestTypeID = _TestTypeID;
            ctrlScheduledTest1.LoadData(_AppointmentID);
            if (ctrlScheduledTest1.AppointmentID == -1)
                btnSave.Enabled = false;
            else
                btnSave.Enabled = true;
            if (ctrlScheduledTest1.TestID != -1)
            {
                _TestInfo = clsTest.FindTestInfoByID(ctrlScheduledTest1.TestID);
                rbPass.Checked = _TestInfo.TestResult;
                rbFail.Checked = !_TestInfo.TestResult;
                rbPass.Enabled = false;
                rbFail.Enabled = false;
                txtNotes.Text = _TestInfo.Notes;
            }
            else
                _TestInfo = new clsTest();
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {


            _TestInfo.TestAppointmentID = _AppointmentID;
            _TestInfo.TestResult = rbPass.Checked;
            _TestInfo.Notes=txtNotes.Text;
            _TestInfo.CreatedByUserID=DVLDSettings.CurrentUser.UserID;
            
            if (_TestInfo.Save()) 
            {
                _TestID = _TestInfo.TestID;
                MessageBox.Show("Data Saved Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 
            }
            else
            {
                MessageBox.Show("Saving Data Failed", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            rbPass.Enabled = false;
            rbFail.Enabled = false;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
