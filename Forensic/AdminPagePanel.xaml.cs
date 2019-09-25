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
    /// AdminPagePanel.xaml etkileşim mantığı
    /// </summary>
    public partial class AdminPagePanel : Window
    {
        public AdminPagePanel()
        {
            InitializeComponent();
        }

        private void btnAddInvestigator_Click(object sender, RoutedEventArgs e)
        {
            InvestigatorCreateAccountPage investigatorCreateAccountPage = new InvestigatorCreateAccountPage();
            investigatorCreateAccountPage.ShowDialog();
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            CreateAccountWindow createAccountWindow = new CreateAccountWindow();
            createAccountWindow.ShowDialog();
        }
    }
}
