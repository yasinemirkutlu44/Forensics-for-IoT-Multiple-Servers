using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;
using System;
using System.Windows.Controls;
using System.Device.Location;
using System.Net;
using System.Net.Sockets;
using Google.Cloud.Firestore;
using System.Globalization;

namespace Forensic
{
    public partial class DeviceRegistrationPage : Window
    {
        private GeoCoordinateWatcher Watcher = null; // The coordinate watcher.
        string deviceCode;
        public DeviceRegistrationPage()
        {
            InitializeComponent();
            btnApply.IsEnabled = false;
            txtLatitude.IsEnabled = false;
            txtLongitude.IsEnabled = false;
            txtIPAddress.IsEnabled = false;
            txtCountryCode.IsEnabled = false;
            txtCityCode.IsEnabled = false;
        }

        IFirebaseConfig config = new FirebaseConfig()
        {
            AuthSecret = "kXL5Grc0d1ieUW7TElhTcqooUaE0Jc8oYQSpUIxz",
            BasePath = "https://iotservers.firebaseio.com/"

        };

        IFirebaseClient firebaseClient;
        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                firebaseClient = new FireSharp.FirebaseClient(config);
                if(String.IsNullOrWhiteSpace(txtOwner.Text) || String.IsNullOrWhiteSpace(txtUser.Text) || String.IsNullOrWhiteSpace(txtSerialNumber.Text) ||
                String.IsNullOrWhiteSpace(txtIPAddress.Text) || String.IsNullOrWhiteSpace(txtIMEI.Text) || String.IsNullOrWhiteSpace(txtDeviceID.Text)|| String.IsNullOrWhiteSpace(cmbIssueCountry.Text) || String.IsNullOrWhiteSpace(cmbIssueCity.Text) ||
                String.IsNullOrWhiteSpace(cmbDevices.Text) || String.IsNullOrWhiteSpace(cmbDeviceType.Text))
                {
                    MessageBox.Show("A required field was not filled", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else // inserting a device
                {
                    Device device = new Device()
                    {
                        Owner = txtOwner.Text,
                        User = txtUser.Text,
                        SerialNumber = txtSerialNumber.Text,
                        Latitude = double.Parse(txtLatitude.Text.Trim(), CultureInfo.InvariantCulture.NumberFormat),
                        Longitude= double.Parse(txtLongitude.Text.Trim(), CultureInfo.InvariantCulture.NumberFormat),
                        IPAddress = txtIPAddress.Text,
                        IMEI = txtIMEI.Text,
                        DeviceType = cmbDeviceType.SelectedItem.ToString(),
                        DeviceName = cmbDevices.SelectedItem.ToString()
                    };
                    deviceCode = txtCountryCode.Text + txtCityCode.Text + txtDeviceID.Text;
                    SetResponse setResponse = firebaseClient.Set(cmbIssueCountry.SelectedItem.ToString() + "/" + cmbIssueCity.SelectedItem.ToString() + "/" + deviceCode, device);
                    MessageBox.Show("You have registered a new device into registration system", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                }
            }
            catch(FormatException)
            {
                MessageBox.Show("You are not able to register a device in to registration system", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Watcher_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e) // The watcher's status has change. See if it is ready
        {
            if (e.Status == GeoPositionStatus.Ready)
            {
                // Display the latitude and longitude.
                if (Watcher.Position.Location.IsUnknown)
                {
                    txtLatitude.Text = "Cannot find location data";
                }
                else
                {
                    GeoCoordinate location = Watcher.Position.Location;
                    txtLatitude.Text = location.Latitude.ToString().Replace(",",".");
                    txtLongitude.Text = location.Longitude.ToString().Replace(",", ".");
                }
            }
        }

        private void btnCurrentLocationIP_Click(object sender, RoutedEventArgs e)
        {
            Watcher = new GeoCoordinateWatcher(); // Create the watcher.
            Watcher.StatusChanged += Watcher_StatusChanged; // Catch the StatusChanged event.
            Watcher.Start(); // Start the watcher.
            txtLatitude.Text = Watcher.Position.Location.Latitude.ToString();
            txtLongitude.Text = Watcher.Position.Location.Longitude.ToString();
            txtIPAddress.Text = GetLocalIPAddress().ToString();
            btnCurrentLocationIP.IsEnabled = false;
            btnApply.IsEnabled = true;

        }

        public static IPAddress GetLocalIPAddress()
        {
            IPAddress[] ipv4Addresses = null;
            try
            {
                ipv4Addresses = Array.FindAll(
                Dns.GetHostEntry(string.Empty).AddressList,
                a => a.AddressFamily == AddressFamily.InterNetwork);
            }
            catch (FormatException)
            {
                MessageBox.Show("No network adapters with an IPv4 address in the system", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return ipv4Addresses[1];//IPv4 Address
        }

        private void cmbIssueCountry_Loaded(object sender, RoutedEventArgs e)
        {
            cmbIssueCountry.Items.Add("Turkey");
            cmbIssueCountry.Items.Add("United Kingdom");
            cmbIssueCountry.Items.Add("Germany");
            cmbIssueCountry.Items.Add("France");
            cmbIssueCountry.Items.Add("Spain");
            cmbIssueCountry.Items.Add("Italy");
        }

        private void cmbDeviceType_Loaded(object sender, RoutedEventArgs e)
        {
            cmbDeviceType.Items.Add("Laptop or Desktop Computer");
            cmbDeviceType.Items.Add("Tablets");
            cmbDeviceType.Items.Add("Smartphones");
        }

        private void cmbIssueCountry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbIssueCity.Items.Clear();
            txtCountryCode.Clear();
            if (cmbIssueCountry.SelectedItem.ToString() == "Turkey")
            {
                cmbIssueCity.Items.Add("Ankara");
                cmbIssueCity.Items.Add("Istanbul");
                txtCountryCode.Text = "TR";
            }
            else if(cmbIssueCountry.SelectedItem.ToString() == "United Kingdom")
            {
                cmbIssueCity.Items.Add("London");
                cmbIssueCity.Items.Add("Portsmouth");
                txtCountryCode.Text = "UK";
            }
            else if(cmbIssueCountry.SelectedItem.ToString() == "Germany")
            {
                cmbIssueCity.Items.Add("Berlin");
                cmbIssueCity.Items.Add("Cologne");
                txtCountryCode.Text = "GR";
            }
            else if(cmbIssueCountry.SelectedItem.ToString() == "France")
            {
                cmbIssueCity.Items.Add("Paris");
                cmbIssueCity.Items.Add("Lyon");
                txtCountryCode.Text = "FR";
            }
            else if(cmbIssueCountry.SelectedItem.ToString() == "Spain")
            {
                cmbIssueCity.Items.Add("Madrid");
                cmbIssueCity.Items.Add("Barcelona");
                txtCountryCode.Text = "SP";
            }
            else if (cmbIssueCountry.SelectedItem.ToString() == "Italy")
            {
                cmbIssueCity.Items.Add("Rome");
                cmbIssueCity.Items.Add("Naples");
                txtCountryCode.Text = "IT";
            }
        }

        private void cmbIssueCity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtCityCode.Clear();
            if (cmbIssueCity.SelectedItem != null)
            {
                if (cmbIssueCity.SelectedItem.ToString() == "Ankara")
                {
                    txtCityCode.Text = "AN";
                }
                else if (cmbIssueCity.SelectedItem.ToString() == "Istanbul")
                {
                    txtCityCode.Text = "IS";
                }
                else if (cmbIssueCity.SelectedItem.ToString() == "London")
                {
                    txtCityCode.Text = "LD";
                }
                else if (cmbIssueCity.SelectedItem.ToString() == "Portsmouth")
                {
                    txtCityCode.Text = "PT";
                }
                else if (cmbIssueCity.SelectedItem.ToString() == "Berlin")
                {
                    txtCityCode.Text = "BE";
                }
                else if (cmbIssueCity.SelectedItem.ToString() == "Cologne")
                {
                    txtCityCode.Text = "CG";
                }
                else if (cmbIssueCity.SelectedItem.ToString() == "Paris")
                {
                    txtCityCode.Text = "PR";
                }
                else if(cmbIssueCity.SelectedItem.ToString() == "Lyon")
                {
                    txtCityCode.Text = "LY";
                }
                else if (cmbIssueCity.SelectedItem.ToString() == "Madrid")
                {
                    txtCityCode.Text = "MD";
                }
                else if (cmbIssueCity.SelectedItem.ToString() == "Barcelona")
                {
                    txtCityCode.Text = "BC";
                }
                else if (cmbIssueCity.SelectedItem.ToString() == "Rome")
                {
                    txtCityCode.Text = "RM";
                }
                else if (cmbIssueCity.SelectedItem.ToString() == "Naples")
                {
                    txtCityCode.Text = "NP";
                }
            }
        }

        private void cmbDeviceType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbDevices.Items.Clear();
            if(cmbDeviceType.SelectedItem.ToString() == "Laptop or Desktop Computer")
            {
                cmbDevices.Items.Add("Asus FX505GD");
                cmbDevices.Items.Add("Lenovo V330");
                cmbDevices.Items.Add("Acer A315");
                cmbDevices.Items.Add("Casper Nirvana");
                cmbDevices.Items.Add("HP 17");
                cmbDevices.Items.Add("HP Pavilion Gaming 15");
                cmbDevices.Items.Add("Monster Tulpar");
                cmbDevices.Items.Add("MSI GT75");
                cmbDevices.Items.Add("ASUS VivoBook");
                cmbDevices.Items.Add("DELL G315");
                cmbDevices.Items.Add("Apple MacBook Pro");
            }
            else if (cmbDeviceType.SelectedItem.ToString() == "Tablets")
            {
                cmbDevices.Items.Add("Apple Ipad Pro 12.9");
                cmbDevices.Items.Add("Apple Ipad Air");
                cmbDevices.Items.Add("Apple Ipad Mini");
                cmbDevices.Items.Add("Apple Ipad Pro 11");
                cmbDevices.Items.Add("Apple Ipad Pro 10.5");
                cmbDevices.Items.Add("Apple Ipad 9.7");
                cmbDevices.Items.Add("Samsung Galaxy Tab S4");
                cmbDevices.Items.Add("Samsung Galaxy Tab S5E");
                cmbDevices.Items.Add("Microsoft Surface Go");
                cmbDevices.Items.Add("Microsoft Surface Pro 6");
                cmbDevices.Items.Add("Huawei MediaPad T3");
                cmbDevices.Items.Add("Google Pixel Slate");
            }
            else
            {
                cmbDevices.Items.Add("Apple Iphone XS");
                cmbDevices.Items.Add("Apple Iphone XR");
                cmbDevices.Items.Add("Samsung Galaxy S10 Plus");
                cmbDevices.Items.Add("Samsung Galaxy Note 9");
                cmbDevices.Items.Add("Huawei P30 Pro");
                cmbDevices.Items.Add("Huawei Mate 20 Pro");
                cmbDevices.Items.Add("Google Pixel 3 XL");
                cmbDevices.Items.Add("Xiamoi MI 9");
                cmbDevices.Items.Add("Nokia 9 Pureview");
                cmbDevices.Items.Add("Sony Xperia 10 Plus");
                cmbDevices.Items.Add("HTC U12+");
                cmbDevices.Items.Add("Microsoft Surface Pro 6");
            }
        }

        private void txtOwner_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex(@"[^\p{L}]+").IsMatch(e.Text);
        }

        private void txtUser_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex(@"[^\p{L}]+").IsMatch(e.Text);
        }

        private void txtSerialNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex(@"[^0-9.]+").IsMatch(e.Text);
        }

        private void txtIMEI_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex(@"[^0-9.]+").IsMatch(e.Text);
        }

        private void txtDeviceID_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex(@"[^0-9.]+").IsMatch(e.Text);
        }
    }
}
