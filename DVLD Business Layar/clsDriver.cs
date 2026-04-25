using DVLD_DataAccess_Layar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Layar
{
    public class clsDriver
    {
        public int DriverID { get; set; }

        public int PersonID { get; set; }

        public clsPerson PersonInfo { get; set; }
        public int CreatedByUserID { get; set; }

        public DateTime CreatedDate { get; set; }

        enum enMode
        {
            AddNew = 1,
            Update = 2
        }

        private enMode _Mode = enMode.Update;
        public clsDriver()
        {

            this.DriverID = -1;
            this.PersonID = -1;
            this.CreatedByUserID = -1;
            this.CreatedDate = DateTime.Now;
            PersonInfo = new clsPerson();
            _Mode = enMode.AddNew;
        }

        public clsDriver(int DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {

            this.DriverID = DriverID;
            this.PersonID = PersonID;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedDate = CreatedDate;
            PersonInfo = clsPerson.FindPersonByID(PersonID);
            _Mode = enMode.Update;
        }

        public static clsDriver GetDriverInfoByID(int DriverID)
        {

            int PersonID = -1; int CreatedByUserID = -1; DateTime CreatedDate = DateTime.Now;

            if (clsDriverData.GetDriverInfoByID(DriverID, ref PersonID, ref CreatedByUserID, ref CreatedDate))

                return new clsDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);
            else
                return null;

        }

        public static clsDriver GetDriverInfoByPersonID(int PersonID)
        {

            int DriverID = -1; int CreatedByUserID = -1; DateTime CreatedDate = DateTime.Now;

            if (clsDriverData.GetDriverInfoByPersonID(ref DriverID,  PersonID, ref CreatedByUserID, ref CreatedDate))

                return new clsDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);
            else
                return null;



        }

        public static int GetDriverID(int PersonID)
        {

            return clsPersonData.GetDriverID(PersonID);


        }
        public static DataTable GetAllDriversList()
        {

            return clsDriverData.GetAllDrivers();


        }
        bool _AddNewDriver()
        {
            this.DriverID = clsDriverData.AddNewDriver(this.PersonID, this.CreatedByUserID);
            return DriverID != -1;
        }
        bool _UpdateDriver()
        {
            return clsDriverData.UpdateDriverInfo(this.DriverID, this.PersonID, this.CreatedByUserID);
           
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDriver())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateDriver();


            }
            return false;
        }

    }
}
