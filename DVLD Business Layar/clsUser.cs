using DVLD_DataAccess_Layar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Layar
{
    public class clsUser
    {
        public int UserID { set; get; }
        public int PersonID { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }
        public bool isActive { set; get; }

        public clsPerson PersonInfo { set; get; }
        enum enMode
        {
            AddNew = 1,
            Update = 2
        }

        private enMode _Mode = enMode.Update;
        public clsUser()
        {
            UserID = -1;
            PersonID = -1;
            UserName = "";
            Password = "";
            isActive = false;
            PersonInfo = new clsPerson();
            _Mode = enMode.AddNew;

        }

        private clsUser(int UserID, int PersonID, string UserName, string Password, bool isActive)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.UserName = UserName;
            this.Password = Password;
            this.isActive = isActive;
            PersonInfo = clsPerson.FindPersonByID(PersonID);

            _Mode = enMode.Update;

        }

        public static clsUser FindUserByUserID(int UserID)
        {

            int PersonID = -1;
            string UserName = "";
            string Password = "";
            bool isActive = false;

            if (clsUserData.FindUserByUserID(UserID, ref PersonID, ref UserName, ref Password, ref isActive))
            {
                return new clsUser(UserID, PersonID, UserName, Password, isActive);
            }
            else { return null; }
        }

        public static clsUser FindUserByUserName(string UserName)
        {
            int UserID = -1;
            int PersonID = -1;

            string Password = "";
            bool isActive = false;

            if (clsUserData.FindUserByUserName(ref UserID, ref PersonID, UserName, ref Password, ref isActive))
            {
                return new clsUser(UserID, PersonID, UserName, Password, isActive);
            }
            else { return null; }
        }
        public static clsUser FindUserByPersonID(int PersonID)
        {

            int UserID = -1;
            string UserName = "";
            string Password = "";
            bool isActive = false;

            if (clsUserData.FindUserByPersonID(ref UserID,  PersonID, ref UserName, ref Password, ref isActive))
            { 
                return new clsUser(PersonID, PersonID, UserName, Password, isActive);
            }
            else { return null; }
        }

        public static bool IsUserExists(int UserID)
        {


            return clsUserData.IsUserExists(UserID);


        }
        public static bool IsUserExists(string UserName)
        {


            return clsUserData.IsUserExists(UserName);


        }
        public static bool IsUserExistsForPersonID(int PersonID)
        {


            return clsUserData.IsUserExistsForPersonID(PersonID);


        }


        public static DataTable UsersList()
        {

            return clsUserData.GetUsersList();
        }



        bool _AddNew()
        {
            this.UserID = clsUserData.AddNewUser(this.PersonID, this.UserName, this.Password, this.isActive);

            return this.UserID != -1;


        }
        bool _Update()
        {
            return clsUserData.UpdateUser(this.UserID, this.PersonID, this.UserName, this.Password, this.isActive);
        }

        public bool ChangePassword(string NewPassword)
        {
            return clsUserData.ChangePassword(this.UserName,NewPassword);
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

        public static bool DeleteUser(int UserID)
        {
            if (clsUserData.DeleteUser(UserID))
            {
                return true;
            }
            return false;

        }
    }
}
