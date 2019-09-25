using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;
using Forensic.Classes;
using System.Text.RegularExpressions;
using Forensic.Handler;

namespace Forensic
{
    public partial class AuthorisedPersonLoginPage : Window
    {
        IFirebaseClient firebaseClient;
        string investigatorCode, countryCode, cityCode, rankCode;
        HashCode hashCode = new HashCode();

        public AuthorisedPersonLoginPage()
        {
            InitializeComponent();
            txtAuthorisedAuthKey.IsEnabled = false;
        }

        IFirebaseConfig investigatorConfiguration = new FirebaseConfig()
        {
            AuthSecret = "B28WujliT5DQT9SBQ2JXiwqw7mGRyAA8p9YVSxhq",
            BasePath = "https://investigators-fdcd3.firebaseio.com/"
        };

        private void btnAuthorisedLogin_Click(object sender, RoutedEventArgs e)
        {
            TransactionPage transactionPage = new TransactionPage();
            investigatorCode = countryCode + cityCode + rankCode + txtInvestigatorSequence.Text;
            txtAuthorisedAuthKey.Text = investigatorCode;
            try
            {
                if (String.IsNullOrWhiteSpace(cmbInvestigatorCountry.SelectedItem.ToString()) ||String.IsNullOrWhiteSpace(cmbInvestigatorCity.SelectedItem.ToString()) || String.IsNullOrWhiteSpace(cmbInvestigatorRank.SelectedItem.ToString())
                    ||String.IsNullOrWhiteSpace(txtAuthorisedAuthKey.Text) || String.IsNullOrWhiteSpace(txtAuthorisedPassword.Password) || String.IsNullOrWhiteSpace(cmbInvestigatorCountry.SelectedItem.ToString())
                    || String.IsNullOrWhiteSpace(cmbInvestigatorCity.SelectedItem.ToString()) || String.IsNullOrWhiteSpace(txtInvestigatorSequence.Text))
                {
                    MessageBox.Show("A required field was not filled", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    firebaseClient = new FireSharp.FirebaseClient(investigatorConfiguration);
                    FirebaseResponse firebaseResponse = firebaseClient.Get(cmbInvestigatorCountry.SelectedItem.ToString() + "/" + cmbInvestigatorCity.SelectedItem.ToString() + "/" + investigatorCode);
                    Investigator responseInvestigator = firebaseResponse.ResultAs<Investigator>(); // DB Result
                    Investigator currentInvestigator = new Investigator() // User gives information
                    {
                        InvestigatorID = txtAuthorisedAuthKey.Text,
                        Password = hashCode.PasswordHass(txtAuthorisedPassword.Password),
                    };
                    if (Handler.MethodHandler.InvestigatorVerification(responseInvestigator, currentInvestigator))
                    {
                        MessageBox.Show("Welcome, You have directed to Transaction Page for Investigator", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        transactionPage.ShowDialog();
                    }
                    else
                    {
                        Handler.MethodHandler.ShowMessage();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Sorry, We could not find an account with this Login Auth Key", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void cmbInvestigatorCountry_Loaded(object sender, RoutedEventArgs e)
        {
            cmbInvestigatorCountry.Items.Add("Turkey");
            cmbInvestigatorCountry.Items.Add("United Kingdom");
            cmbInvestigatorCountry.Items.Add("Germany");
            cmbInvestigatorCountry.Items.Add("France");
            cmbInvestigatorCountry.Items.Add("Spain");
            cmbInvestigatorCountry.Items.Add("Italy");
        }

        private void cmbInvestigatorRank_Loaded(object sender, RoutedEventArgs e)
        {
            cmbInvestigatorRank.Items.Add("Junior"); // Junior Digital Forensic Investigator // Rank Code : JR
            cmbInvestigatorRank.Items.Add("Consultant"); //Digital Forensic Consultant // Rank Code : CS
            cmbInvestigatorRank.Items.Add("Senior"); // Senior Digital Forensic Investigator // Rank Code : SR
            cmbInvestigatorRank.Items.Add("Manager"); // Digital Forensic Manager // Rank Code : MG
        }

        private void cmbInvestigatorCountry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtAuthorisedAuthKey.Clear();
            cmbInvestigatorCity.Items.Clear();
            if (cmbInvestigatorCountry.SelectedItem.ToString() == "Turkey")
            {
                cmbInvestigatorCity.Items.Add("Ankara");
                cmbInvestigatorCity.Items.Add("Istanbul");
                countryCode = "TR";
            }
            else if (cmbInvestigatorCountry.SelectedItem.ToString() == "United Kingdom")
            {
                cmbInvestigatorCity.Items.Add("London");
                cmbInvestigatorCity.Items.Add("Portsmouth");
                countryCode = "UK";
            }
            else if (cmbInvestigatorCountry.SelectedItem.ToString() == "Germany")
            {
                cmbInvestigatorCity.Items.Add("Berlin");
                cmbInvestigatorCity.Items.Add("Cologne");
                countryCode = "GR";
            }
            else if (cmbInvestigatorCountry.SelectedItem.ToString() == "France")
            {
                cmbInvestigatorCity.Items.Add("Paris");
                cmbInvestigatorCity.Items.Add("Lyon");
                countryCode = "FR";
            }
            else if (cmbInvestigatorCountry.SelectedItem.ToString() == "Spain")
            {
                cmbInvestigatorCity.Items.Add("Madrid");
                cmbInvestigatorCity.Items.Add("Barcelona");
                countryCode = "SP";
            }
            else if (cmbInvestigatorCountry.SelectedItem.ToString() == "Italy")
            {
                cmbInvestigatorCity.Items.Add("Rome");
                cmbInvestigatorCity.Items.Add("Naples");
                countryCode = "IT";
            }
        }

        private void cmbInvestigatorCity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtAuthorisedAuthKey.Clear();
            txtInvestigatorSequence.Clear();
            if (cmbInvestigatorCity.SelectedItem != null)
            {
                if (cmbInvestigatorCity.SelectedItem.ToString() == "Ankara")
                {
                    cityCode = "AN";
                }
                else if (cmbInvestigatorCity.SelectedItem.ToString() == "Istanbul")
                {
                    cityCode = "IS";
                }
                else if (cmbInvestigatorCity.SelectedItem.ToString() == "London")
                {
                    cityCode = "LD";
                }
                else if (cmbInvestigatorCity.SelectedItem.ToString() == "Portsmouth")
                {
                    cityCode = "PT";
                }
                else if (cmbInvestigatorCity.SelectedItem.ToString() == "Berlin")
                {
                    cityCode = "BR";
                }
                else if (cmbInvestigatorCity.SelectedItem.ToString() == "Cologne")
                {
                    cityCode = "CG";
                }
                else if (cmbInvestigatorCity.SelectedItem.ToString() == "Paris")
                {
                    cityCode = "PR";
                }
                else if (cmbInvestigatorCity.SelectedItem.ToString() == "Lyon")
                {
                    cityCode = "LY";
                }
                else if (cmbInvestigatorCity.SelectedItem.ToString() == "Madrid")
                {
                    cityCode = "MD";
                }
                else if (cmbInvestigatorCity.SelectedItem.ToString() == "Barcelona")
                {
                    cityCode = "BC";
                }
                else if (cmbInvestigatorCity.SelectedItem.ToString() == "Rome")
                {
                    cityCode = "RM";
                }
                else if (cmbInvestigatorCity.SelectedItem.ToString() == "Naples")
                {
                    cityCode = "NP";
                }
            }
        }

        private void cmbInvestigatorRank_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbInvestigatorRank.SelectedItem.ToString() == "Junior")
            {
                rankCode = "JR";
            }
            else if (cmbInvestigatorRank.SelectedItem.ToString() == "Consultant")
            {
                rankCode = "CS";
            }
            else if (cmbInvestigatorRank.SelectedItem.ToString() == "Senior")
            {
                rankCode = "SR";
            }
            else if (cmbInvestigatorRank.SelectedItem.ToString() == "Manager")
            {
                rankCode = "MG";
            }
        }

        private void txtInvestigatorSequence_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex(@"[^0-9]+").IsMatch(e.Text);
        }
    }
}
