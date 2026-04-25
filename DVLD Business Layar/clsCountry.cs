using DVLD_DataAccess_Layar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Layar
{
    public class clsCountry
    {
        public int CountryID { set; get; }
        public string CountryName { set; get; }

        public clsCountry()
        {
            CountryID = -1;
            CountryName = "";
        }
        private clsCountry(int countryID,string countryName)
        {
            CountryID = countryID;
            CountryName = countryName;
        }
        public static clsCountry Find(int CountryID)
        {
            string CountryName = "";

            if (clsCountryData.FindCountryByID(CountryID, ref CountryName))
            {
                return new clsCountry(CountryID, CountryName);
            }
            return null;
        }

        public static clsCountry Find(string  CountryName)
        {
            int CountryID = -1;

            if (clsCountryData.FindCountryByName(ref CountryID, CountryName))
            {
                return new clsCountry(CountryID, CountryName);
            }
            return null;
        }
        public static DataTable CountriesList()
        {
            return clsCountryData.GetCountriesList();
        }

        public static string GetCountryNameByCountryID(int CountryID)
        {
            return clsCountryData.GetCountryName(CountryID);
        }

        public static int GetCountryID(string CountryName)
        {
            return clsCountryData.GetCountryID(CountryName);
        }
    }
}
