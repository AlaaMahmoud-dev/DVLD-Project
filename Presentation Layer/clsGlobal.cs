using DVLD_Business_Layar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Project
{
    internal class clsGlobal
    {
        public static clsUsers CurrentUser;

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


    }
}
