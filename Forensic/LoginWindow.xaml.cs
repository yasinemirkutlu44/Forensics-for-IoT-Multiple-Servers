using System;
using System.Windows;
using System.Windows.Input;
using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;
using System.Text.RegularExpressions;
using Forensic.Handler;

namespace Forensic
{
    public partial class LoginWindow : Window
    {
        HashCode hashCode = new HashCode();

        public LoginWindow()
        {
            InitializeComponent();
            txtLoginAuth.Focus();
        }


        IFirebaseConfig config = new FirebaseConfig()
        {
            AuthSecret = "kXL5Grc0d1ieUW7TElhTcqooUaE0Jc8oYQSpUIxz",
            BasePath = "https://iotservers.firebaseio.com/"

        };

        IFirebaseClient firebaseClient;

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(txtLoginAuth.Text) || String.IsNullOrWhiteSpace(txtPass.Password))
                {
                    MessageBox.Show("A required field was not filled", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                        firebaseClient = new FireSharp.FirebaseClient(config);
                        FirebaseResponse firebaseResponse = firebaseClient.Get("Users/" + txtLoginAuth.Text);
                        User responseUser = firebaseResponse.ResultAs<User>(); // DB Result
                        User currentUser = new User() // User gives information
                        {
                            UserID = txtLoginAuth.Text,
                            Password = hashCode.PasswordHass(txtPass.Password),
                        };
                        if (Handler.MethodHandler.IsEqual(responseUser, currentUser))
                        {
                            if(responseUser.Admin == true ) // if it is true, that means an admin has logged in
                            {
                            MessageBox.Show("Welcome to Transaction Page Admin!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                            AdminPagePanel adminPagePanel = new AdminPagePanel();
                            adminPagePanel.ShowDialog();
                            }
                            else
                            {
                            MessageBox.Show("Welcome, You have directed to User Transaction Page", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                            UserTransactionPage userTransactionPage = new UserTransactionPage();
                            userTransactionPage.ShowDialog();
                            }
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

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            CreateAccountWindow createAccountWindow = new CreateAccountWindow();
            MessageBox.Show("You have successfully accessed the system", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            createAccountWindow.ShowDialog();
        }

        private void btnAuthorisedPerson_Click(object sender, RoutedEventArgs e)
        {
            AuthorisedPersonLoginPage authorisedPersonLoginPage = new AuthorisedPersonLoginPage();
            authorisedPersonLoginPage.ShowDialog();
        }

        private void txtLoginAuth_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex(@"[^0-9]+").IsMatch(e.Text);
        }
    }
 
}
