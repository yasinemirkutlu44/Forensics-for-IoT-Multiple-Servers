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
    /// <summary>
    /// TransactionPage.xaml etkileşim mantığı
    /// </summary>
    public partial class TransactionPage : Window
    {
        public TransactionPage()
        {
            InitializeComponent();
        }
       
        private void btnSortDevices_Click(object sender, RoutedEventArgs e)
        {
            DeviceHistoryWindow mainWindow = new DeviceHistoryWindow();
            mainWindow.ShowDialog();

        }

        private void btnDevicesMap_Click(object sender, RoutedEventArgs e)
        {
            MapWindow googleMapwindow = new MapWindow();
            googleMapwindow.ShowDialog();
        }
        private void btnInvestigateDevice_Click(object sender, RoutedEventArgs e)
        {
            DeviceInvestigation deviceInvestigation = new DeviceInvestigation();
            deviceInvestigation.ShowDialog();

        }
    }
}
