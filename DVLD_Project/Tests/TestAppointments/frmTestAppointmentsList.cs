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
    public partial class frmTestAppointmentsList : Form
    {
        DataTable _dtAppointmentsList;

        int _LDLApplicationID = -1;
        clsTestType.enTestType _TestTypeID;



        public frmTestAppointmentsList(int LDLApplicationID, clsTestType.enTestType TestTypeID)
        {
            InitializeComponent();

            _LDLApplicationID = LDLApplicationID;
            _TestTypeID = TestTypeID;

        }

        void _dgvFillAppointments()
        {

            _dtAppointmentsList = clsTestAppointment.GetAppointmentsListPerApplicationAndTestType(_LDLApplicationID, (int)_TestTypeID);
            dgvAppointmentsList.DataSource = _dtAppointmentsList;
            if (dgvAppointmentsList.RowCount>0)
            {
                dgvAppointmentsList.Columns[0].HeaderText = "Appointment ID";
                dgvAppointmentsList.Columns[0].Width = 150;
                dgvAppointmentsList.Columns[1].HeaderText = "Appointment Date";
                dgvAppointmentsList.Columns[1].Width = 150;
                dgvAppointmentsList.Columns[2].HeaderText = "Paid Fees";
                dgvAppointmentsList.Columns[2].Width = 125;
                dgvAppointmentsList.Columns[3].HeaderText = "is Locked";
                dgvAppointmentsList.Columns[3].Width = 100;

            }
        }
        private void _LoadTestTypeImageAndTitle()
        {
            if (_TestTypeID == clsTestType.enTestType.VisionTest)
            {
                this.Text = "Vision Test Appointment";
                lblAppointmentType.Text = "Vision Test Appointment";

                pbTestTypePic.BackgroundImage = Properties.Resources.Vision_512;
            }
            else if (_TestTypeID == clsTestType.enTestType.WrittenTest)
            {
                this.Text = "Written Test Appointment";
                lblAppointmentType.Text = "Written Test Appointment";
                pbTestTypePic.BackgroundImage = Properties.Resources.Written_Test_512;
            }
            else
            {
                this.Text = "Street Test Appointment";

                lblAppointmentType.Text = "Street Test Appointment";
                pbTestTypePic.BackgroundImage = Properties.Resources.Street_Test_32;

            }
        }
        void _LoadData()
        {
            _LoadTestTypeImageAndTitle();
             _dgvFillAppointments();
           
            ctrlLocalDrivingLicenseApplicationInfo1.LoadLocalDrivingLicenseApplicationInfo(_LDLApplicationID);
            


        }
        private void frmTakeAppointment_Load(object sender, EventArgs e)
        {

           _LoadData();


        }

        private void btnAddNewAppointment_Click(object sender, EventArgs e)
        {

            if (clsLDLApplication.IsThereAnActiveScheduledTest(_LDLApplicationID, (int)_TestTypeID))
            {
                MessageBox.Show("Person Already has an active Appointment for this test, you cannot add new Appointment", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (clsLDLApplication.DoesPassTest(_LDLApplicationID, _TestTypeID))
            {
                MessageBox.Show("Person Already Passed this test, you cannot add new Appointment", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frmScheduleTest ScheduleTest = new frmScheduleTest(_LDLApplicationID, (clsTestType.enTestType)_TestTypeID);
            ScheduleTest.ShowDialog();
            _dgvFillAppointments();


        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int AppointmentID = int.Parse(dgvAppointmentsList.CurrentRow.Cells[0].Value.ToString());
            frmScheduleTest EditScheduledTest = new frmScheduleTest(_LDLApplicationID, _TestTypeID, AppointmentID);
            EditScheduledTest.ShowDialog();
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTakeTest takeTest = new frmTakeTest(int.Parse(dgvAppointmentsList.CurrentRow.Cells[0].Value.ToString()),_TestTypeID);
            takeTest.ShowDialog();
            _dgvFillAppointments();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
           
            
        }

        private void contextMenuStrip1_Opened(object sender, EventArgs e)
        {
           
        }
    }
}
