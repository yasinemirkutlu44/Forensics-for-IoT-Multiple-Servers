using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Device.Location;
using System.Text.RegularExpressions;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System.IO;
using System.Collections.Generic;
using GMap.NET;
using GoogleMaps.LocationServices;
using Two10.CountryLookup;
using System.Globalization;

namespace Forensic
{
    public partial class UpdateLocationPage : Window
    {
        private GeoCoordinateWatcher Watcher = null; // The coordinate watcher..
        IFirebaseClient firebaseClient;
        string concatLocation;
        string deviceCode,countryCode,cityCode;
        static List<string> devicepoints = new List<string>();
        SetResponse setResponse = null;

        public UpdateLocationPage()
        {
            InitializeComponent();
        }

        IFirebaseConfig config = new FirebaseConfig()
        {
            AuthSecret = "jnANrKs9KTRWCvwhgkgvMQIJVAWNBi4uVHFlG1dY",
            BasePath = "https://iotcountries.firebaseio.com/"
        };

        private async void btnUpdateLocation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                firebaseClient = new FireSharp.FirebaseClient(config);
                if (String.IsNullOrWhiteSpace(txtLatitude.Text) || String.IsNullOrWhiteSpace(txtLongitude.Text) || String.IsNullOrWhiteSpace(txtDeviceID.Text) ||
                    String.IsNullOrWhiteSpace(cmbIssueCountry.Text) || String.IsNullOrWhiteSpace(cmbIssueCity.Text))
                {
                    MessageBox.Show("A required field was not filled", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else // Updating Latitude and Longitude 
                {
                    var tempLatLong = new LatLong
                    {
                        Latitude = double.Parse(txtLatitude.Text.Trim(), CultureInfo.InvariantCulture.NumberFormat),
                        Longitude = double.Parse(txtLongitude.Text.Trim(), CultureInfo.InvariantCulture.NumberFormat)
                    };
                    deviceCode = countryCode + cityCode + txtDeviceID.Text;//Generating device code using concatenation of countryCode, cityCode and deviceID.
                    FirebaseResponse firebaseResponse = firebaseClient.Get(cmbIssueCountry.SelectedItem.ToString() + "/" + cmbIssueCity.SelectedItem.ToString() + "/" + deviceCode);
                    Device device = firebaseResponse.ResultAs<Device>();
                    LatLong deviceLocation = firebaseResponse.ResultAs<LatLong>();
                    var locationService = new GoogleLocationService();
                    concatLocation = "Previous Latitude:" + deviceLocation.Latitude.ToString().Replace(",",".") + " " + "Previous Longitude:" + deviceLocation.Longitude.ToString().Replace(",",".") + " " + "Unique Device Code: " + deviceCode + " " + " Country of Issue:" + cmbIssueCountry.Text + " " + " City of Issue:" + cmbIssueCity.Text + " " + " Owner:" + device.Owner + " " + " User:" + device.User + " " + 
                    "Device Type:" + device.DeviceType + " " + "Device Name:" + device.DeviceName;
                    devicepoints.Add(concatLocation);// Inserted point[0] getting latitude and longitude values from firebase and adding point list
                    FirebaseResponse updateResponse = await firebaseClient.UpdateTaskAsync(cmbIssueCountry.Text + "/" + cmbIssueCity.Text + "/" + deviceCode, tempLatLong);
                    concatLocation = "Current Latitude:" + txtLatitude.Text + " " + "Current Longitude:" + txtLongitude.Text + " " + "Unique Device Code: " + deviceCode + " " + " Country of Issue:" + cmbIssueCountry.Text + " " + " City of Issue:" + cmbIssueCity.Text + " " + " Owner:" + device.Owner + " " + " User:" + device.User + " " +
                    "Device Type:" + device.DeviceType + " " + "Device Name:" + device.DeviceName;
                    devicepoints.Add(concatLocation);
                    var currentCountry = Handler.MethodHandler.GetCountryName(double.Parse(txtLatitude.Text.Trim(), CultureInfo.InvariantCulture.NumberFormat), double.Parse(txtLongitude.Text.Trim(), CultureInfo.InvariantCulture.NumberFormat));
                    if (cmbIssueCountry.SelectedItem.ToString() == "Turkey")
                    {
                        File.AppendAllLines(@"C:\Users\YasinEmir\source\repos\Forensic\Forensic\VisitedPointsTurkishDevices.txt", devicepoints);
                        if (!String.Equals(cmbIssueCountry.SelectedItem.ToString(), currentCountry))
                        {
                            setResponse = await firebaseClient.SetTaskAsync(currentCountry + "/" + "Visitors" + "/" + deviceCode, device);
                            updateResponse = await firebaseClient.UpdateTaskAsync(currentCountry + "/" + "Visitors" + "/" + deviceCode, tempLatLong);
                        }
                    }
                    else if (cmbIssueCountry.SelectedItem.ToString() == "United Kingdom")
                    {
                        File.AppendAllLines(@"C:\Users\YasinEmir\source\repos\Forensic\Forensic\VisitedPointsBritishDevices.txt", devicepoints);
                        if (!String.Equals(cmbIssueCountry.SelectedItem.ToString(), currentCountry))
                        {
                            setResponse = await firebaseClient.SetTaskAsync(currentCountry + "/" + "Visitors" + "/" + deviceCode, device);
                            updateResponse = await firebaseClient.UpdateTaskAsync(currentCountry + "/" + "Visitors" + "/" + deviceCode, tempLatLong);
                        }
                    }
                    else if (cmbIssueCountry.SelectedItem.ToString() == "Germany")
                    {
                        File.AppendAllLines(@"C:\Users\YasinEmir\source\repos\Forensic\Forensic\VisitedPointsGermanDevices.txt", devicepoints);
                        if (!String.Equals(cmbIssueCountry.SelectedItem.ToString(), currentCountry))
                        {
                            setResponse = await firebaseClient.SetTaskAsync(currentCountry + "/" + "Visitors" + "/" + deviceCode, device);
                            updateResponse = await firebaseClient.UpdateTaskAsync(currentCountry + "/" + "Visitors" + "/" + deviceCode, tempLatLong);
                        }
                    }
                    else if (cmbIssueCountry.SelectedItem.ToString() == "France")
                    {
                        File.AppendAllLines(@"C:\Users\YasinEmir\source\repos\Forensic\Forensic\VisitedPointsFrenchDevices.txt", devicepoints);
                        if (!String.Equals(cmbIssueCountry.SelectedItem.ToString(), currentCountry))
                        {
                            setResponse = await firebaseClient.SetTaskAsync(currentCountry + "/" + "Visitors" + "/" + deviceCode, device);
                            updateResponse = await firebaseClient.UpdateTaskAsync(currentCountry + "/" + "Visitors" + "/" + deviceCode, tempLatLong);
                        }
                    }
                    else if(cmbIssueCountry.SelectedItem.ToString() == "Spain")
                    {
                        File.AppendAllLines(@"C:\Users\YasinEmir\source\repos\Forensic\Forensic\VisitedPointsSpanishDevices.txt", devicepoints);
                        if (!String.Equals(cmbIssueCountry.SelectedItem.ToString(), currentCountry))
                        {
                            setResponse = await firebaseClient.SetTaskAsync(currentCountry + "/" + "Visitors" + "/" + deviceCode, device);
                            updateResponse = await firebaseClient.UpdateTaskAsync(currentCountry + "/" + "Visitors" + "/" + deviceCode, tempLatLong);
                        }
                    }
                    else if (cmbIssueCountry.SelectedItem.ToString() == "Italy")
                    {
                        File.AppendAllLines(@"C:\Users\YasinEmir\source\repos\Forensic\Forensic\VisitedPointsItalianDevices.txt", devicepoints);
                        if (!String.Equals(cmbIssueCountry.SelectedItem.ToString(), currentCountry))
                        {
                            setResponse = await firebaseClient.SetTaskAsync(currentCountry + "/" + "Visitors" + "/" + deviceCode, device);
                            updateResponse = await firebaseClient.UpdateTaskAsync(currentCountry + "/" + "Visitors" + "/" + deviceCode, tempLatLong);
                        }
                    }

                    MessageBox.Show("You have updated device's location succesfully ", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    MessageBox.Show("Device's location changes saved into specified file", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    devicepoints.Clear();
                }
            }
            catch
            {
                MessageBox.Show("Sorry,You cannot update a location with unknown ID number, please make sure your ID is correct", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnGetCurrentLocation_Click(object sender, RoutedEventArgs e)
        {
            Watcher = new GeoCoordinateWatcher(); // Create the watcher.
            Watcher.StatusChanged += Watcher_StatusChanged; // Catch the StatusChanged event.
            Watcher.Start(); // Start the watcher.
            txtLatitude.IsEnabled = false;
            txtLongitude.IsEnabled = false;
            btnGetCurrentLocation.IsEnabled = false;
        }

        private void Watcher_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e) // The watcher's status has change. See if it is ready
        {
            if (e.Status == GeoPositionStatus.Ready)
            {
                // Display the latitude and longitude.
                if (Watcher.Position.Location.IsUnknown)
                {
                    txtLatitude.Text="Cannot find location data";
                    txtLongitude.Text = "Cannot find location data";
                }
                else
                {
                    GeoCoordinate location = Watcher.Position.Location;
                    txtLatitude.Text = location.Latitude.ToString().Replace(",",".");
                    txtLongitude.Text = location.Longitude.ToString().Replace(",",".");
                }
            }
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

        private void cmbIssueCountry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbIssueCity.Items.Clear();
            txtDeviceID.Clear();
            if (cmbIssueCountry.SelectedItem.ToString() == "Turkey")
            {
                cmbIssueCity.Items.Add("Ankara");
                cmbIssueCity.Items.Add("Istanbul");
                countryCode = "TR";
            }
            else if (cmbIssueCountry.SelectedItem.ToString() == "United Kingdom")
            {
                cmbIssueCity.Items.Add("London");
                cmbIssueCity.Items.Add("Portsmouth");
                countryCode = "UK";
            }
            else if (cmbIssueCountry.SelectedItem.ToString() == "Germany")
            {
                cmbIssueCity.Items.Add("Berlin");
                cmbIssueCity.Items.Add("Cologne");
                countryCode = "GR";
            }
            else if (cmbIssueCountry.SelectedItem.ToString() == "France")
            {
                cmbIssueCity.Items.Add("Paris");
                cmbIssueCity.Items.Add("Lyon");
                countryCode = "FR";
            }
            else if (cmbIssueCountry.SelectedItem.ToString() == "Spain")
            {
                cmbIssueCity.Items.Add("Madrid");
                cmbIssueCity.Items.Add("Barcelona");
                countryCode = "SP";
            }
            else if (cmbIssueCountry.SelectedItem.ToString() == "Italy")
            {
                cmbIssueCity.Items.Add("Rome");
                cmbIssueCity.Items.Add("Naples");
                countryCode = "IT";
            }
        }

        private void cmbIssueCity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtDeviceID.Clear();
            if(cmbIssueCity.SelectedItem != null)
            {
                if (cmbIssueCity.SelectedItem.ToString() == "Ankara")
                {
                    cityCode = "AN";
                }
                else if (cmbIssueCity.SelectedItem.ToString() == "Istanbul")
                {
                    cityCode = "IS";
                }
                else if (cmbIssueCity.SelectedItem.ToString() == "London")
                {
                    cityCode = "LD";
                }
                else if (cmbIssueCity.SelectedItem.ToString() == "Portsmouth")
                {
                    cityCode = "PT";
                }
                else if (cmbIssueCity.SelectedItem.ToString() == "Berlin")
                {
                    cityCode = "BE";
                }
                else if (cmbIssueCity.SelectedItem.ToString() == "Cologne")
                {
                    cityCode = "CG";
                }
                else if (cmbIssueCity.SelectedItem.ToString() == "Paris")
                {
                    cityCode = "PR";
                }
                else if(cmbIssueCity.SelectedItem.ToString() == "Lyon")
                {
                    cityCode = "LY";
                }
                else if (cmbIssueCity.SelectedItem.ToString() == "Madrid")
                {
                    cityCode = "MD";
                }
                else if (cmbIssueCity.SelectedItem.ToString() == "Barcelona")
                {
                    cityCode = "BC";
                }
                else if (cmbIssueCity.SelectedItem.ToString() == "Rome")
                {
                    cityCode = "RM";
                }
                else if (cmbIssueCity.SelectedItem.ToString() == "Naples")
                {
                    cityCode = "NP";
                }
            }
        }

        private void txtDeviceID_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex(@"[^0-9.]+").IsMatch(e.Text);
        }

        private void txtLatitude_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex(@"[^0-9.]+").IsMatch(e.Text);
        }

        private void txtLongitude_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex(@"[^0-9.]+").IsMatch(e.Text);
        }

    }
}
