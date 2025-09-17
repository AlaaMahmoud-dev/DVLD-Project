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
    public partial class frmTakeAppointment : Form
    {
        DataTable dtAppointmentsList=null;

        int _LDLApplicationID = -1;
        int _TestTypeID = 0;

        enum enTestType
        {
            Vision=1,
            Written=2,
            Street=3
        }

        enTestType testType = enTestType.Vision;
        public frmTakeAppointment(int LDLApplicationID,int TestTypeID)
        {
            InitializeComponent();
            
        _LDLApplicationID= LDLApplicationID;
            _TestTypeID= TestTypeID;

            if (TestTypeID==1)
            {
                testType = enTestType.Vision;
            }
            else if (TestTypeID==2)
            {
                testType = enTestType.Written;
            }
            else
            {
                testType = enTestType.Street;
            }
            dtAppointmentsList = clsTestAppointments.AppointmentsList(LDLApplicationID,TestTypeID);
        }

        void _dgvFillAppointments()
        {

            dtAppointmentsList = clsTestAppointments.AppointmentsList(_LDLApplicationID,_TestTypeID);
            dgvAppointmentsList.Rows.Clear();

            if (dtAppointmentsList.Rows.Count>0)
            {
                dgvAppointmentsList.Visible = true;
            }
            else
            {
                dgvAppointmentsList.Visible = false;
                return;
            }

            DataView dvTestTypes = dtAppointmentsList.DefaultView;


            for (int i = 0; i < dtAppointmentsList.Rows.Count; i++)
            {
                dgvAppointmentsList.Rows.Add();
            }

            for (int i = 0; i < dtAppointmentsList.Rows.Count; i++)
            {
                for (int j = 0; j < dtAppointmentsList.Columns.Count; j++)
                {
                    if(j==3)
                    {
                        dgvAppointmentsList.Rows[i].Cells[j].Value = (bool.Parse(dvTestTypes[i][j].ToString()));
                        continue;
                    }
                    dgvAppointmentsList.Rows[i].Cells[j].Value = dvTestTypes[i][j].ToString();
                }
            }
        }

        void _LoadData()
        {
           
             _dgvFillAppointments();
           
           
            if (testType == enTestType.Vision)
            {
                this.Text = "Vision Test Appointment";
                lblAppointmentType.Text = "Vision Test Appointment";
                
                pbTestTypePic.BackgroundImage = Properties.Resources.Vision_512;
            }
            else if (testType == enTestType.Written)
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

            ctrlLDLAndBasicApplicationsDetails1.LoadDLAndBasicApplicationsInfo(_LDLApplicationID);



        }
        private void frmTakeAppointment_Load(object sender, EventArgs e)
        {

           _LoadData();


        }

        private void btnAddNewAppointment_Click(object sender, EventArgs e)
        {
            if (clsTestAppointments.hasAppointment(_LDLApplicationID, _TestTypeID))
            {
                if (clsTestAppointments.hasAppointmentNotLocked(_LDLApplicationID, _TestTypeID))
                {
                    MessageBox.Show("Person Already has an active Appointment for this test,you cannot add new Appointment", "Adding Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    if (clsTestAppointments.isTestPassed(_LDLApplicationID, _TestTypeID))
                    {
                        MessageBox.Show("This Person Already Passed this test before,you can only Retake Faild Tests", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    frmScheduleTest ScheduleRetakeTest = new frmScheduleTest(-1,_LDLApplicationID, _TestTypeID,true);
                    ScheduleRetakeTest.ShowDialog();
                    _dgvFillAppointments();
                    return;
                }
            }
            frmScheduleTest ScheduleNewTest=new frmScheduleTest(-1,_LDLApplicationID,_TestTypeID);
            ScheduleNewTest.ShowDialog();
            _dgvFillAppointments();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int AppointmentID = int.Parse(dgvAppointmentsList.CurrentRow.Cells[0].Value.ToString());
            frmScheduleTest EditScheduledTest = new frmScheduleTest(AppointmentID, _LDLApplicationID, _TestTypeID);
            EditScheduledTest.ShowDialog();
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTakeTest takeTest = new frmTakeTest(int.Parse(dgvAppointmentsList.CurrentRow.Cells[0].Value.ToString()));
            takeTest.ShowDialog();
            _dgvFillAppointments();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
           
            
        }

        private void contextMenuStrip1_Opened(object sender, EventArgs e)
        {
            takeTestToolStripMenuItem.Enabled = true;
            if (bool.Parse(dgvAppointmentsList.CurrentRow.Cells[3].Value.ToString()))
            {
                takeTestToolStripMenuItem.Enabled = false;

            }
        }
    }
}
