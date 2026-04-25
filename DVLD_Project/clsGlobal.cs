using DVLD_Business_Layar;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DVLD_Project
{
    internal class clsGlobal
    {
        public static clsUser CurrentUser;

        [Obsolete("This method will be deleted in the next version use RememberUsernameAndPasswordNew instead")]
        public static bool RememberUsernameAndPassword(string Username, string Password)
        {
            try
            {
                string CurrentDirectory = System.IO.Directory.GetCurrentDirectory();

                string FilePath = CurrentDirectory + "\\Login.txt";

                if (Username == "")
                {
                    if (File.Exists(FilePath))
                    {
                        File.Delete(FilePath);

                    }
                    return true;
                }


                using (StreamWriter writer = new StreamWriter(FilePath, false))
                {
                    if (!File.Exists(FilePath))
                    {
                        File.Create(FilePath);
                    }
                    writer.WriteLine(Username + "#//#" + Password);
                    return true;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;

            }

        }
        public static bool RememberUsernameAndPasswordNew(string Username, string Password)
        {
            try
            {
                string keyPath = @"HKey_CURRENT_USER\Software\DVLD";

                Registry.SetValue(keyPath, "Username", Username, RegistryValueKind.String);
                Registry.SetValue(keyPath, "Password", Password, RegistryValueKind.String);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred { ex.Message}");
                return false;

            }

        }
        [Obsolete("This method will be deleted in the next version use GetStoredCredentialNew instead")]

        public static bool GetStoredCredential(ref string UserName, ref string Password)
        {
            try
            {

                string CurrentDirectory = System.IO.Directory.GetCurrentDirectory();

                string FilePath = CurrentDirectory + "\\Login.txt";


                if (File.Exists(FilePath))
                {

                    using (StreamReader reader = new StreamReader(FilePath))
                    {
                        string Line;
                        while ((Line = reader.ReadLine()) != null)
                        {
                            string[] StoredCredential;
                            StoredCredential = Line.Split(new string[] { "#//#" }, StringSplitOptions.None);
                            UserName = StoredCredential[0];
                            Password = StoredCredential[1];
                        }
                        return true;

                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public static bool GetStoredCredentialNew(ref string UserName, ref string Password)
        {
            try
            {
                string keyPath = @"HKey_CURRENT_USER\Software\DVLD";

                UserName = Registry.GetValue(keyPath, "Username","") as string;
                Password = Registry.GetValue(keyPath, "Password","") as string;
                return UserName != "";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

    }
}
