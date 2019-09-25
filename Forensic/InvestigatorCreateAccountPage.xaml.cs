using System.Windows;
using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;
using System.Windows.Controls;
using System.Windows.Input;
using System.Text.RegularExpressions;
using System;
using Forensic.Classes;
using System.Globalization;
using Forensic.Handler;

namespace Forensic
{
    public partial class InvestigatorCreateAccountPage : Window
    {
        IFirebaseClient firebaseClient;
        HashCode hashCode = new HashCode();
        string investigatorCode, countryCode, cityCode, rankCode;

        public InvestigatorCreateAccountPage()
        {
            InitializeComponent();
        }

        IFirebaseConfig investigatorConfiguration = new FirebaseConfig()
        {
            AuthSecret = "B28WujliT5DQT9SBQ2JXiwqw7mGRyAA8p9YVSxhq",
            BasePath = "https://investigators-fdcd3.firebaseio.com/"

        };

        private void btnInvestigatorRegister_Click(object sender, RoutedEventArgs e)
        {
            investigatorCode = countryCode + cityCode + rankCode + txtSequenceNumber.Text;
            try
            {
                firebaseClient = new FireSharp.FirebaseClient(investigatorConfiguration);
                if (String.IsNullOrWhiteSpace(cmbInvestigatorCountry.SelectedItem.ToString()) || String.IsNullOrWhiteSpace(cmbInvestigatorCity.SelectedItem.ToString())
                    || String.IsNullOrWhiteSpace(txtSequenceNumber.Text) || String.IsNullOrWhiteSpace(txtPassword.Password) ||
                    String.IsNullOrWhiteSpace(txtInvestigatorName.Text) || String.IsNullOrWhiteSpace(txtInvestigatorSurname.Text) || String.IsNullOrWhiteSpace(cmbInvestigatorRank.SelectedItem.ToString()) || String.IsNullOrWhiteSpace(txtConfidenceValue.Text))
                {
                    MessageBox.Show("A required field was not filled", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    Investigator investigator = new Investigator()
                    {
                        InvestigatorID = investigatorCode,
                        InvestigatorName = txtInvestigatorName.Text,
                        InvestigatorSurname = txtInvestigatorSurname.Text,
                        InvestigatorRank = cmbInvestigatorRank.SelectedItem.ToString(),
                        Password = hashCode.PasswordHass(txtPassword.Password),
                        ConfidenceValue = double.Parse(txtConfidenceValue.Text.Trim(), CultureInfo.InvariantCulture.NumberFormat)
                    };

                    SetResponse set = firebaseClient.Set(cmbInvestigatorCountry.SelectedItem.ToString() + "/" + cmbInvestigatorCity.SelectedItem.ToString() + "/"  + investigatorCode, investigator);
                    MessageBox.Show("You have successfully created a new account", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Something went wrong, Please try again later!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cmbInvestigatorCity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
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

        private void cmbInvestigatorCountry_Loaded(object sender, RoutedEventArgs e)
        {
            cmbInvestigatorCountry.Items.Add("Turkey");
            cmbInvestigatorCountry.Items.Add("United Kingdom");
            cmbInvestigatorCountry.Items.Add("Germany");
            cmbInvestigatorCountry.Items.Add("France");
            cmbInvestigatorCountry.Items.Add("Spain");
            cmbInvestigatorCountry.Items.Add("Italy");
        }

        private void cmbInvestigatorCountry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbInvestigatorCity.Items.Clear();
            txtSequenceNumber.Clear();
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

        private void txtConfidenceValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            //convidenceValue = double.Parse(txtConfidenceValue.Text.Trim(), CultureInfo.InvariantCulture.NumberFormat);
            //if (convidenceValue < 0.0 || convidenceValue > 1.0)
            //{
            //    MessageBox.Show("Convidence Value must be between 0.0 and 1.0. Please check your information and sumbit again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
            //else
            //{
            //    if (convidenceValue >= 0.0 || convidenceValue <= 0.2)
            //    {
            //        cmbInvestigatorRank.SelectedItem = "Constable";
            //        rankCode = "DC";
            //    }
            //    else if (convidenceValue >= 0.21 || convidenceValue <= 0.4)
            //    {
            //        cmbInvestigatorRank.SelectedItem = "Sergeant";
            //        rankCode = "DS";
            //    }
            //    else if (convidenceValue >= 0.41 || convidenceValue <= 0.6)
            //    {
            //        cmbInvestigatorRank.SelectedItem = "Inspector";
            //        rankCode = "DI";
            //    }
            //    else if (convidenceValue >= 0.61 || convidenceValue <= 0.8)
            //    {
            //        cmbInvestigatorRank.SelectedItem = "Chief Inspetor";
            //        rankCode = "DCI";
            //    }
            //    else if (convidenceValue >= 0.81 || convidenceValue <= 1.0)
            //    {
            //        cmbInvestigatorRank.SelectedItem = "Superintendent";
            //        rankCode = "DSI";
            //    }
            //}

        }

        private void cmbInvestigatorRank_Loaded(object sender, RoutedEventArgs e)
        {
            cmbInvestigatorRank.Items.Add("Junior"); // Junior Digital Forensic Investigator // Rank Code : JR
            cmbInvestigatorRank.Items.Add("Consultant"); //Digital Forensic Consultant // Rank Code : CS
            cmbInvestigatorRank.Items.Add("Senior"); // Senior Digital Forensic Investigator // Rank Code : SR
            cmbInvestigatorRank.Items.Add("Manager"); // Digital Forensic Manager // Rank Code : MG
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
    }
}


