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

namespace Forensic
{
    public partial class UserTransactionPage : Window
    {
        public UserTransactionPage()
        {
            InitializeComponent();
        }

        private void btnRegisterDevice_Click(object sender, RoutedEventArgs e)
        {
            DeviceRegistrationPage deviceRegistrationPage = new DeviceRegistrationPage();
            deviceRegistrationPage.ShowDialog();              
        }

        private void btnUpdateLocation_Click(object sender, RoutedEventArgs e)
        {
            UpdateLocationPage updateLocationPage = new UpdateLocationPage();
            updateLocationPage.ShowDialog();
        }
    }
}
