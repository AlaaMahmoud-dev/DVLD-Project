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
    public partial class frmScheduleTest : Form
    {

        int _AppointmentID;
        int _LDLApplicationID;
        clsTestType.enTestType _TestTypeID;
      
      
        public frmScheduleTest(int LDLApplicationID, clsTestType.enTestType TestTypeID, int AppointmentID = -1)
        {
            InitializeComponent();
         
            _AppointmentID = AppointmentID;
            _LDLApplicationID = LDLApplicationID;
            _TestTypeID = TestTypeID;

        }

        private void frmScheduleTest_Load(object sender, EventArgs e)
        {
            ctrlScheduleTest1.TestType = _TestTypeID;
            ctrlScheduleTest1.LoadData(_LDLApplicationID, _AppointmentID);

        }

       
    }
}
