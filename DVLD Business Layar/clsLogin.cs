using DVLD_DataAccess_Layar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Layar
{
    public class clsLogin
    {
        
        public int LoginID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool isActive { get; set; }

        public bool isRememberMeChecked { get; set; }

        public clsLogin()
        {
            LoginID = -1;
            UserID = -1;
            UserName = "";
            Password = "";
            isActive = false;
            isRememberMeChecked = false;
        }
        clsLogin(int LoginID, int UserID, string UserName, string Password, bool isActive, bool isRememberMeChecked)
        {
            this.LoginID = LoginID;
            this.UserID = UserID;
            this.UserName = UserName;
            this.Password = Password;
            this.isRememberMeChecked = isRememberMeChecked;
            this.isActive = isActive;
        }
     

    
    }
}
