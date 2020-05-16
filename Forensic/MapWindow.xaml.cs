using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using GMap.NET;
using GMap.NET.MapProviders;
using System.Device.Location;

namespace Forensic
{
    public partial class MapWindow : Window
    {
        List<string> devicepoints = new List<string>();
        IFirebaseClient firebaseClient;
        string deviceCode,countryCode,cityCode;

        public MapWindow()
        {
            InitializeComponent();
            Gmap.ShowCenter = false;
            Gmap.SetPositionByKeywords("Europe");
        }

        IFirebaseConfig config = new FirebaseConfig()
        {
            AuthSecret = "jnANrKs9KTRWCvwhgkgvMQIJVAWNBi4uVHFlG1dY",
            BasePath = "https://iotcountries.firebaseio.com/"
        };

        private void Gmap_Loaded(object sender, RoutedEventArgs e)
        {
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            Gmap.MapProvider = GoogleMapProvider.Instance;
            Gmap.MinZoom = 1;
            Gmap.MaxZoom = 17;
            Gmap.Zoom = 4;
            Gmap.MouseWheelZoomType = MouseWheelZoomType.MousePositionAndCenter;
            Gmap.CanDragMap = true;
            Gmap.DragButton = MouseButton.Left;
        }

        private void btnTraceDevices_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                firebaseClient = new FireSharp.FirebaseClient(config);
                if (String.IsNullOrWhiteSpace(cmbIssueCountry.Text) || String.IsNullOrWhiteSpace(cmbIssueCity.Text) || String.IsNullOrWhiteSpace(txtDeviceID.Text))
                {
                    MessageBox.Show("A required field was not filled", "Error",MessageBoxButton.OK,MessageBoxImage.Error);
                }
                else
                {
                    deviceCode = countryCode + cityCode + txtDeviceID.Text; //Generating device code using concatenation of countryCode, cityCode and deviceID.
                    FirebaseResponse firebaseResponse = firebaseClient.Get(cmbIssueCountry.SelectedItem.ToString() + "/" + cmbIssueCity.SelectedItem.ToString() + "/" + deviceCode);
                    LatLong location = firebaseResponse.ResultAs<LatLong>(); // DB Result
                    PointLatLng loc = new PointLatLng(location.Latitude, location.Longitude);
                    System.Windows.Media.Imaging.BitmapImage bitmapImage = new System.Windows.Media.Imaging.BitmapImage();
                    bitmapImage.BeginInit();
                    if(cmbIssueCountry.SelectedItem.ToString() == "Turkey")
                    {
                        bitmapImage.UriSource = new Uri(@"C:\Users\YasinEmir\source\repos\Forensic\Forensic\TurkishFlag.png");
                    }
                    else if(cmbIssueCountry.SelectedItem.ToString() == "United Kingdom")
                    {
                        bitmapImage.UriSource = new Uri(@"C:\Users\YasinEmir\source\repos\Forensic\Forensic\BritishFlag.png");
                    }
                    else if(cmbIssueCountry.SelectedItem.ToString() == "Germany")
                    {
                        bitmapImage.UriSource = new Uri(@"C:\Users\YasinEmir\source\repos\Forensic\Forensic\GermanFlag.png");
                    }
                    else if(cmbIssueCountry.SelectedItem.ToString() == "France")
                    {
                        bitmapImage.UriSource = new Uri(@"C:\Users\YasinEmir\source\repos\Forensic\Forensic\FrenchFlag.png");
                    }
                    else if(cmbIssueCountry.SelectedItem.ToString() == "Spain")
                    {
                        bitmapImage.UriSource = new Uri(@"C:\Users\YasinEmir\source\repos\Forensic\Forensic\SpanishFlag.png");
                    }
                    else if(cmbIssueCountry.SelectedItem.ToString() == "Italy")
                    {
                        bitmapImage.UriSource = new Uri(@"C:\Users\YasinEmir\source\repos\Forensic\Forensic\ItalianFlag.png");
                    }
                    bitmapImage.EndInit();
                    GMap.NET.WindowsPresentation.GMapMarker marker2 = new GMap.NET.WindowsPresentation.GMapMarker(loc);
                    Image image = new Image();
                    image.Source = bitmapImage;
                    marker2.Shape = image;
                    Gmap.Markers.Add(marker2);
                }
            }
            catch
            {
                MessageBox.Show("Sorry, We could not find a device with this ID number", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            else if(cmbIssueCountry.SelectedItem.ToString() == "Italy")
            {
                cmbIssueCity.Items.Add("Rome");
                cmbIssueCity.Items.Add("Naples");
                countryCode = "IT";
            }
        }

        private void cmbIssueCity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtDeviceID.Clear();
            if (cmbIssueCity.SelectedItem != null)
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
                else if (cmbIssueCity.SelectedItem.ToString() == "Lyon")
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
                else if(cmbIssueCity.SelectedItem.ToString( )== "Naples")
                {
                    cityCode = "NP";
                }
            }
        }

        private void txtDeviceID_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex(@"[^0-9.]+").IsMatch(e.Text);
        }
    }
}
