using DVLD_DataAccess_Layar;
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;

namespace DVLD_Business_Layar
{
    public class clsLicenseClass
    {
        public int LicenseClassID { get; set; }

        public string ClassName { get; set; }

        public string ClassDescription { get; set; }

        public byte MinimumAllowedAge { get; set; }

        public byte DefaultValidityLength { get; set; }

        public float ClassFees { get; set; }

        public  enum enLicenseClass
        {
            SmallMotorcycle = 1,
            HeavyMotorcycleLicense,
            OrdinaryDrivingLicense,
            Commercial,
            Agricultural,
            SmallAndMediumBus,
            TruckAndHeavyVehicle,

        }
        public static string GetLicenseClassName(enLicenseClass licenseClass)
        {
            switch(licenseClass)
            {
                case enLicenseClass.SmallMotorcycle:
                    return "Class 1 - Small Motorcycle";
                case enLicenseClass.HeavyMotorcycleLicense:
                    return "Class 2 - Heavy Motorcycle License";
                case enLicenseClass.OrdinaryDrivingLicense:
                    return "Class 3 - Ordinary driving license";
                case enLicenseClass.Commercial:
                    return "Class 4 - Commercial";
                case enLicenseClass.Agricultural:
                    return "Class 5 - Agricultural";
                case enLicenseClass.SmallAndMediumBus:
                    return "Class 6 - Small and medium bus";
                default:
                    return "Class 7 - Truck and heavy vehicle";
            }
           

        


        } 
        enum enMode
        {
            AddNew = 1,
            Update = 2
        }

        private enMode _Mode = enMode.Update;
       
        

        public clsLicenseClass()
        {
            LicenseClassID = -1;
            ClassName = "";
            ClassDescription = "";
            MinimumAllowedAge = 18;
            DefaultValidityLength = 5;
            ClassFees = 0;
            _Mode = enMode.AddNew;

        }

        private clsLicenseClass(int LicenseClassID, string ClassName,
            string ClassDescription, byte MinimumAllowedAge,
            byte DefaultValidityLength, float ClassFees)
        {
            this.LicenseClassID = LicenseClassID;           
            this.MinimumAllowedAge = MinimumAllowedAge;
            this.DefaultValidityLength = DefaultValidityLength;
            this.ClassName = ClassName;
            this.ClassDescription = ClassDescription;
            this.ClassFees = ClassFees;
            _Mode = enMode.Update;

        }

        public static clsLicenseClass FindLicenseClassByID(int LicenseClassID)
        {

            string ClassName = "";
            string ClassDescription = "";
            byte MinimumAllowedAge = 18;
            byte DefaultValidityLength = 5;
            float ClassFees = 0;
           

            if (clsLicenseClassData.FindLicenseClassByID(LicenseClassID,
                ref ClassName, ref ClassDescription,
                ref MinimumAllowedAge, ref DefaultValidityLength, ref ClassFees))
            {
                return new clsLicenseClass(LicenseClassID, ClassName,
                    ClassDescription, MinimumAllowedAge,
                    DefaultValidityLength, ClassFees);
            }
            else { return null; }
        }
        public static clsLicenseClass FindLicenseClassByClassName(string ClassName)
        {
            
            int LicenseClassID = -1;
            string ClassDescription = "";
            byte MinimumAllowedAge = 18;
            byte DefaultValidityLength = 5;
            float ClassFees = 0;


            if (clsLicenseClassData.FindLicenseClassByClassName(ref LicenseClassID,
                 ClassName, ref ClassDescription,
                ref MinimumAllowedAge, ref DefaultValidityLength, ref ClassFees))
            {
                return new clsLicenseClass(LicenseClassID, ClassName,
                    ClassDescription, MinimumAllowedAge,
                    DefaultValidityLength, ClassFees);
            }
            else { return null; }
        }


        public static DataTable GetAllLicenseClasses()
        {
            return clsLicenseClassData.GetAllLicenseClasses();
        }

        public static float GetLicenseClassFees(int LicenseClassID)
        {
            return clsLicenseClassData.GetLicenseClassFees(LicenseClassID);
        }
        bool _Update()
        {
            return clsLicenseClassData.UpdateLicenseClass(this.LicenseClassID,
                this.ClassName, this.ClassDescription,
                this.MinimumAllowedAge, this.DefaultValidityLength,
                this.ClassFees);
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


        bool _AddNew()
        {
            this.LicenseClassID =
                clsLicenseClassData.AddNewLicenseClass(this.ClassName,
                this.ClassDescription, this.MinimumAllowedAge,
                this.DefaultValidityLength, this.ClassFees);

            return LicenseClassID != -1;
        }
    }
}
