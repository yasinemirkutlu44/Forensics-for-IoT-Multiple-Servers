using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;
using Two10.CountryLookup;
using System.Windows;
using Forensic.Classes;

namespace Forensic.Handler
{
    public static class MethodHandler
    {
        public static void ShowMessage()
        {
            System.Windows.MessageBox.Show("Username and Password do not match each other, please check your personal information and try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static bool IsEqual(User user1, User user2)
        {
            if (user1 == null || user2 == null)
            {
                return false;
            }
            else if (user1.UserID != user2.UserID)
            {
                return false;
            }
            else if (user1.Password != user2.Password)
            {
                return false;
            }
            return true;
        }

        public static string GetCountryName(double latitude, double longitude) // Get country details based on lat/long
        {
            var lookup = new ReverseLookup();
            string countryName = null;
            try
            {
                var country = lookup.Lookup((float)latitude, (float)longitude);
                countryName = country.Name;
            }
            catch
            {
                MessageBox.Show("Location has not been founded with given lat and long", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return countryName;
        }

        public static bool InvestigatorVerification(Investigator investigator1, Investigator investigator2)
        {
            {
                if(investigator1 == null || investigator2 == null)
                {
                    return false;
                }
                else if(investigator1.InvestigatorID != investigator2.InvestigatorID)
                {
                    return false;
                }
                else if(investigator1.Password != investigator2.Password)
                {
                    return false;
                }
                return true;
            }
        }

    }
}
