using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Forensic
{
    public partial class DeviceInvestigation : Window
    {
        public DeviceInvestigation()
        {
            InitializeComponent();
            txtFinalMessage.IsEnabled = false;
            txtOwnerName.IsEnabled = false;
            txtUserName.IsEnabled = false;
            txtDeviceType.IsEnabled = false;
            txtIMEINumber.IsEnabled = false;
            txtIPAddress.IsEnabled = false;
            txtSerialNumber.IsEnabled = false;
            txtLatitude.IsEnabled = false;
            txtLongitude.IsEnabled = false;
        }

        IFirebaseConfig config = new FirebaseConfig()
        {
            AuthSecret = "jnANrKs9KTRWCvwhgkgvMQIJVAWNBi4uVHFlG1dY",
            BasePath = "https://iotcountries.firebaseio.com/"
        };

        IFirebaseClient firebaseClient;

        string countryCode,cityCode;

        private void cmbCountries_Loaded(object sender, RoutedEventArgs e)
        {
            cmbCountries.Items.Add("Turkey");
            cmbCountries.Items.Add("United Kingdom");
            cmbCountries.Items.Add("Germany");
            cmbCountries.Items.Add("France");
            cmbCountries.Items.Add("Spain");
            cmbCountries.Items.Add("Italy");
        }

        private void cmbCountries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbCities.Items.Clear();
            if (cmbCountries.SelectedItem.ToString() == "Turkey")
            {
                cmbCities.Items.Add("Ankara");
                cmbCities.Items.Add("Istanbul");
                countryCode = "TR";
            }
            else if (cmbCountries.SelectedItem.ToString() == "United Kingdom")
            {
                cmbCities.Items.Add("London");
                cmbCities.Items.Add("Portsmouth");
                countryCode = "UK";
            }
            else if (cmbCountries.SelectedItem.ToString() == "Germany")
            {
                cmbCities.Items.Add("Berlin");
                cmbCities.Items.Add("Cologne");
                countryCode = "GR";
            }
            else if (cmbCountries.SelectedItem.ToString() == "France")
            {
                cmbCities.Items.Add("Paris");
                cmbCities.Items.Add("Lyon");
                countryCode = "FR";
            }
            else if (cmbCountries.SelectedItem.ToString() == "Italy")
            {
                cmbCities.Items.Add("Rome");
                cmbCities.Items.Add("Naples");
                countryCode = "IT";
            }
            else
            {
                cmbCities.Items.Add("Madrid");
                cmbCities.Items.Add("Barcelona");
                countryCode = "SP";
            }
        }

        private void txtDeviceID_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex(@"[^0-9.]+").IsMatch(e.Text);
        }

        private void cmbCities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtDeviceID.Clear();
            if (cmbCities.SelectedItem != null)
            {
                if (cmbCities.SelectedItem.ToString() == "Ankara")
                {
                    cityCode = "AN";
                }
                else if (cmbCities.SelectedItem.ToString() == "Istanbul")
                {
                    cityCode = "IS";
                }
                else if (cmbCities.SelectedItem.ToString() == "London")
                {
                    cityCode = "LD";
                }
                else if (cmbCities.SelectedItem.ToString() == "Portsmouth")
                {
                    cityCode = "PT";
                }
                else if (cmbCities.SelectedItem.ToString() == "Berlin")
                {
                    cityCode = "BE";
                }
                else if (cmbCities.SelectedItem.ToString() == "Cologne")
                {
                    cityCode = "CG";
                }
                else if (cmbCities.SelectedItem.ToString() == "Paris")
                {
                    cityCode = "PR";
                }
                else if (cmbCities.SelectedItem.ToString() == "Lyon")
                {
                    cityCode = "LY";
                }
                else if (cmbCities.SelectedItem.ToString() == "Madrid")
                {
                    cityCode = "MD";
                }
                else if (cmbCities.SelectedItem.ToString() == "Barcelona")
                {
                    cityCode = "BC";
                }
                else if (cmbCities.SelectedItem.ToString() == "Rome")
                {
                    cityCode = "RM";
                }
                else if (cmbCities.SelectedItem.ToString() == "Naples")
                {
                    cityCode = "NP";
                }
            }
        }

        private void btnTraceDevices_Click(object sender, RoutedEventArgs e)
        {
            string concat = countryCode + cityCode+txtDeviceID.Text;
            string[] result = System.IO.File.ReadAllLines(@"C:\Users\YasinEmir\IoTDevices\" + concat + ".txt");
            firebaseClient = new FireSharp.FirebaseClient(config);
            FirebaseResponse firebaseResponse = firebaseClient.Get(cmbCountries.SelectedItem.ToString() + "/" + cmbCities.SelectedItem.ToString() + "/" + concat);
            Device responseDevice = firebaseResponse.ResultAs<Device>();
            if (String.IsNullOrWhiteSpace(cmbCountries.Text) || String.IsNullOrWhiteSpace(cmbCities.Text) || String.IsNullOrWhiteSpace(txtDeviceID.Text))
            {
                MessageBox.Show("A required field was not filled", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                for (int i = 0; i < result.Length; i++)
                {
                    if (result[i] == "1.0 0.0")
                    {
                        txtFinalMessage.Text = "The picture which may pose a thread for crime! This IoT device should be investigated urgently!";
                        txtOwnerName.Text = responseDevice.Owner;
                        txtUserName.Text = responseDevice.User;
                        txtIMEINumber.Text = responseDevice.IMEI;
                        txtIPAddress.Text = responseDevice.IPAddress;
                        txtSerialNumber.Text = responseDevice.SerialNumber;
                        txtDeviceType.Text = responseDevice.DeviceType;
                        txtLatitude.Text = responseDevice.Latitude.ToString();
                        txtLongitude.Text = responseDevice.Longitude.ToString();
                        break;
                    }
                    else
                    { 
                        txtFinalMessage.Text = "There is nothing illegal";
                        txtOwnerName.Clear();
                        txtUserName.Clear();
                        txtIMEINumber.Clear();
                        txtSerialNumber.Clear();
                        txtLatitude.Clear();
                        txtLongitude.Clear();
                        txtIPAddress.Clear();
                        txtSerialNumber.Clear();
                        txtDeviceType.Clear();
                    }
                }
            }
        }
    }
}
