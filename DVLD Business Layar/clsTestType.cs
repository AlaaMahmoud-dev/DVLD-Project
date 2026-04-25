using DVLD_DataAccess_Layar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Layar
{
    public class clsTestType
    {
        public enTestType ID { get; set; }
        public string Title { get; set; }
        public double Fees { get; set; }
        public string Description { get; set; }
        public enum enTestType
        {
            VisionTest=1,
            WrittenTest=2,
            PracticalTest=3
        }
        public enum enMode
        {
            AddNew,
            Update
        }

        private enMode _Mode = enMode.Update;
        public clsTestType()
        {
            ID = enTestType.VisionTest;
            Title = "";
            Fees = 0;
            Description = "";
            _Mode = enMode.AddNew;
        }
        private clsTestType(int ID, string Title, float Fees,string Description)
        {
            this.ID = (enTestType)ID;
            this.Title = Title;
            this.Fees = Fees;
            this.Description = Description;
            _Mode = enMode.Update;
        }

        public static clsTestType FindTestTypeInfoByID(int TestTypeID)
        {
            string Title = "";
            float Fees = 0;
            string Description = "";

            bool isFound = clsTestTypeData.GetTestTypeInfoByID(
                TestTypeID, ref Title, ref Fees,ref Description);

            if (isFound)
            {
                return new clsTestType(TestTypeID, Title, Fees,Description);
            }
            return null;
        }
        public static string GetTestTypeTitle(enTestType TestType)
        {

            switch (TestType)
            {
                case enTestType.VisionTest:
                    return "Vision Test";
                case enTestType.WrittenTest:
                    return "Written Test";
                case enTestType.PracticalTest:
                    return "Practical Test";
                default:
                    return "";
            }

        }

        public static DataTable TestTypesList()
        {
            return clsTestTypeData.GetTestTypesList();
        }
        //bool _AddNew()
        //{

        //    this.ID = clsTestTypeData.AddNewTestType(this.Title, this.Fees,this.Description);
        //    return (this.ID != -1);
        //}
        bool _Update()
        {
            return clsTestTypeData.UpdateTestTypeInfo((int)this.ID, this.Title, this.Description, this.Fees);
        }
        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                //    if (_AddNew())
                //    {
                        _Mode = enMode.Update;
                    break;
                //        return true;
                //    }
                //    else
                //    {
                //        return false;
                //    }
                case enMode.Update:
                    return _Update();


            }
            return false;
        }

        public static enTestType GetPreviousTestTypeID(clsTestType.enTestType CurrentTestType)
        {
            switch (CurrentTestType)
            {

                case clsTestType.enTestType.WrittenTest:
                    return enTestType.VisionTest;
                case clsTestType.enTestType.PracticalTest:
                    return enTestType.WrittenTest;
                default:
                    return enTestType.VisionTest;
            }
        }
        public static float TestFees(int TestTypeID)
        {
            return clsTestTypeData.GetTestFees(TestTypeID);
        }
    }
}
