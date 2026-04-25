using DVLD_DataAccess_Layar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Layar
{
    public class clsApplicationType
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public float Fees { get; set; }

        public enum enMode
        {
            AddNew,
            Update
        }

        private enMode _Mode = enMode.Update;
 


        public enum enApplicationType
        {
            NewLocalDrivingLicense= 1,
            RenewDrivingLicense= 2,
            ReplacementForLostDrivingLicense = 3,
            ReplacementForDamagedDrivingLicense = 4,
            ReleaseDetainedLicense=5,
            NewInternationalDrivingLicense=6,
            RetakeTest=7,

        }
        public clsApplicationType()
        {
            ID = -1;
            Title = "";
            Fees = 0;
            _Mode = enMode.AddNew;
        }
        private clsApplicationType(int ID,string Title,float Fees)
        {
            this.ID = ID;
            this.Title = Title;
            this.Fees = Fees;
            _Mode = enMode.Update;
        }

        public static clsApplicationType FindApplicationTypeInfoByID(int ApplicationTypeID)
        {
            string Title = "";
            float Fees = 0;

            bool isFound = clsApplicationTypeData.GetApplicationTypeInfoByID(
                ApplicationTypeID, ref Title, ref Fees);

            if (isFound)
            {
                return new clsApplicationType(ApplicationTypeID, Title, Fees);
            }
            return null;
        }
        public static DataTable GetApplicationTypesList()
        {
            return clsApplicationTypeData.GetApplicationTypesList();
        }

        public static float GetApplicationFees(int ApplicationTypeID)
        {
            return clsApplicationTypeData.GetApplicationFees(ApplicationTypeID);
        }
        bool _AddNew()
        {

            this.ID = clsApplicationTypeData.AddNewApplicationType(this.Title, this.Fees);
            return (this.ID != -1);
        }
        bool _Update()
        {
            return clsApplicationTypeData.UpdateApplicationTypeInfo(this.ID, this.Title, this.Fees);
        }
        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNew())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _Update();


            }
            return false;
        }
    }
}
