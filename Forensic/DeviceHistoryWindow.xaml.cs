using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using FireSharp.Response;
using FireSharp.Interfaces;
using FireSharp.Config;
using System.IO;

namespace Forensic
{
    public partial class DeviceHistoryWindow : Window
    {
        public DeviceHistoryWindow()
        {
            InitializeComponent();
        }
        IFirebaseClient firebaseClient;
        DataTable dt = new DataTable();
        StreamReader file = null;

        IFirebaseConfig config = new FirebaseConfig()
        {
            AuthSecret = "kXL5Grc0d1ieUW7TElhTcqooUaE0Jc8oYQSpUIxz",
            BasePath = "https://iotservers.firebaseio.com/"

        };


        private void ReadShowData()
        {
            //StreamReader file = new StreamReader(@"C:\Users\YasinEmir\source\repos\Forensic\Forensic\VisitedPointsTurkishDevices.txt");
            //string[] columnnames = file.ReadLine().Split(' ');
            //DataTable dt = new DataTable();
            //foreach (string c in columnnames)
            //{
            //    dt.Columns.Add(c);
            //}
            //string newline;
            //while ((newline = file.ReadLine()) != null)
            //{
            //    DataRow dr = dt.NewRow();
            //    string[] values = newline.Split(' ');
            //    for (int i = 0; i < values.Length; i++)
            //    {
            //        dr[i] = values[i];
            //    }
            //    dt.Rows.Add(dr);
            //}
            //file.Close();
            //DataGridView.ItemsSource = dt;


            MessageBox.Show("All data are sorted successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            ReadShowData();
        }

        private void cmbCountries_Loaded(object sender, RoutedEventArgs e)
        {
            cmbCountries.Items.Add("Turkey");
            cmbCountries.Items.Add("United Kingdom");
            cmbCountries.Items.Add("Germany");
            cmbCountries.Items.Add("France");
        }

        private void cmbCountries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbCities.Items.Clear();
            if (cmbCountries.SelectedItem.ToString() == "Turkey")
            {
                cmbCities.Items.Add("Ankara");
                cmbCities.Items.Add("Istanbul");
            }
            else if (cmbCountries.SelectedItem.ToString() == "United Kingdom")
            {
                cmbCities.Items.Add("London");
                cmbCities.Items.Add("Portsmouth");
            }
            else if (cmbCountries.SelectedItem.ToString() == "Germany")
            {
                cmbCities.Items.Add("Berlin");
                cmbCities.Items.Add("Cologne");
            }
            else
            {
                cmbCities.Items.Add("Paris");
                cmbCities.Items.Add("Lyon");
            }
        }
    }
}
