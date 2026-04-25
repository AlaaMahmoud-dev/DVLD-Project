using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DVLD_DataAccess_Layar;
namespace DVLD_Business_Layar
{
    public class clsPerson
    {

        public int ID { get; set; }

      
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public byte Gendor {  get; set; }

        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }


        public int CountryID { get; set; }

        public clsCountry CountryInfo { set; get; }
        
        public string ImagePath { get; set; }

        public string FullName
        {
            get
            {

                return FirstName + " " + SecondName + " " + ThirdName + " " + LastName;
            }
        }
        public string GenderCaption
        {
            get
            {
                switch((enGender)Gendor)
                {
                    case enGender.Male:
                        return "Male";
                    default:
                        return "Femail";
                }
            }
        }
        public enum enGender
        {
            Male=0,
            Female=1
        }
        enum enMode
        {
            AddNew = 1,
            Update = 2
        }

        private enMode _Mode = enMode.Update;
        public clsPerson()
        {
            ID = -1;
            NationalNo = string.Empty;
            FirstName = string.Empty;
            SecondName = string.Empty;
            ThirdName = string.Empty;
            LastName = string.Empty;
            DateOfBirth = DateTime.Now;
            Gendor = 1;
            Address = string.Empty;
            Phone = string.Empty;
            Email = string.Empty;
            CountryID = -1;
            CountryInfo = new clsCountry();
            ImagePath = string.Empty;



            _Mode = enMode.AddNew;

        }

        private clsPerson(int ID, string NationalNo, string FirstName, string SecondName, 
            string ThirdName, string LastName, DateTime DateOfBirth, byte Gendor, 
            string Address, string Phone, string Email,
            int CountryID, string ImagePath)
        {
            this.ID = ID;
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Gendor = Gendor;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.CountryID = CountryID;
            CountryInfo = clsCountry.Find(CountryID);
            this.ImagePath = ImagePath;

            _Mode = enMode.Update;

        }

        public static clsPerson FindPersonByID(int PersonID)
        {

            string NationalNo = "";
            string FirstName = "";
            string SecondName = "";
            string ThirdName = "";
            string LastName = "";
            DateTime DateOfBirth = DateTime.Now;
            byte Gendor = 1;
            string Address = "";
            string Phone = "";
            string Email = "";
            int CountryID = 90;
            string ImagePath = "";

            if (clsPersonData.FindPersonByID(PersonID, ref NationalNo, ref FirstName,
                ref SecondName, ref ThirdName, ref LastName, ref DateOfBirth,
                ref Gendor, ref Address, ref Phone, ref Email, ref CountryID,
                ref ImagePath))
            {
                return new clsPerson(PersonID, NationalNo, FirstName, SecondName, 
                    ThirdName, LastName, DateOfBirth, Gendor, Address, Phone,
                    Email, CountryID, ImagePath);
            }
            else { return null; }
        }

        public static clsPerson FindPersonByNationalNo(string NationalNo)
        {
            

            int PersonID = -1;
            string FirstName = "";
            string SecondName = "";
            string ThirdName = "";
            string LastName = "";
            DateTime DateOfBirth = DateTime.Now;
            byte Gendor = 1;
            string Address = "";
            string Phone = "";
            string Email = "";
            int CountryID = 90;
            string ImagePath = "";

            if (clsPersonData.FindPersonByNationalNo(ref PersonID,  NationalNo,
                ref FirstName, ref SecondName, ref ThirdName, ref LastName,
                ref DateOfBirth, ref Gendor, ref Address, ref Phone,
                ref Email, ref CountryID, ref ImagePath))
            {
                return new clsPerson(PersonID, NationalNo, FirstName, SecondName,
                    ThirdName, LastName, DateOfBirth, Gendor, Address, Phone,
                    Email, CountryID, ImagePath);
            }
            else { return null; }
        }

        public static DataTable PeopleList()
        {

            return clsPersonData.GetPeopleList();
        }

      
        bool _AddNew()
        {
            this.ID = clsPersonData.AddNewPerson(this.NationalNo,this.FirstName,
                this.SecondName, this.ThirdName, this.LastName, this.DateOfBirth,
                this.Gendor, this.Address, this.Phone, this.Email
                , this.CountryID, this.ImagePath);

            return this.ID != -1;
            

        }
        bool _Update()
        {
            return clsPersonData.UpdatePerson(this.ID,this.NationalNo, this.FirstName,
                this.SecondName, this.ThirdName, this.LastName, this.DateOfBirth, 
                this.Gendor, this.Address, this.Phone, this.Email
                , this.CountryID, this.ImagePath);
        }
        public bool Save()
        {
            switch(_Mode)
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
        public static DataTable NationalNoList()
        {
            return clsPersonData.GetAllNationalNumbers();
        }
        public static bool DeletePerson(int PersonID)
        {
           return clsPersonData.DeletePerson(PersonID);
           
        }

        public static int GetDriverID(int PersonID)
        {
            return clsPersonData.GetDriverID(PersonID);
        }

        public static bool IsPersonExistsByPersonID(int PersonID)
        {
            return clsPersonData.IsPersonExistsByPersonID(PersonID);
        }
        public static bool IsPersonExistsByNationalNo(string NationalNo)
        {
            return clsPersonData.IsPersonExistsByNationalNo(NationalNo);
        }

    }
    

   

   
   

   

  

  

   

  

   

   
    
}
