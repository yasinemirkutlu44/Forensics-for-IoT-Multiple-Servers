using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;
using System.Text.RegularExpressions;
using Forensic.Handler;

namespace Forensic
{
    /// <summary>
    /// CreateAccountWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class CreateAccountWindow : Window
    {
        IFirebaseClient firebaseClient;
        HashCode hashCode = new HashCode();

        public CreateAccountWindow()
        {
            InitializeComponent();
        }

        IFirebaseConfig config = new FirebaseConfig()
        {
            AuthSecret = "jnANrKs9KTRWCvwhgkgvMQIJVAWNBi4uVHFlG1dY",
            BasePath = "https://iotcountries.firebaseio.com/"
        };


        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                firebaseClient = new FireSharp.FirebaseClient(config);
                if (String.IsNullOrWhiteSpace(txtName.Text) || String.IsNullOrWhiteSpace(txtSurname.Text)
                    || String.IsNullOrWhiteSpace(txtPass.Password) || String.IsNullOrWhiteSpace(txtUserID.Text) || String.IsNullOrWhiteSpace(dtDateofBirth.Text))
                {
                    MessageBox.Show("A required field was not filled", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    User user = new User()
                    {

                        UserID = txtUserID.Text,
                        Name = txtName.Text,
                        Surname = txtSurname.Text,
                        Password = hashCode.PasswordHass(txtPass.Password),
                        Country = cmbIssueCountry.SelectedItem.ToString(),
                        City = cmbIssueCity.SelectedItem.ToString(),
                        DOB = dtDateofBirth.Text
                    };

                    SetResponse set = firebaseClient.Set("Users/"+ Int32.Parse(txtUserID.Text), user);
                    MessageBox.Show("You have successfully created a new account", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Connection error","Error",MessageBoxButton.OK,MessageBoxImage.Error);
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
            if(cmbIssueCountry.SelectedItem.ToString() == "Turkey")
            {
                cmbIssueCity.Items.Add("Ankara");
                cmbIssueCity.Items.Add("Istanbul");
            }
            else if(cmbIssueCountry.SelectedItem.ToString() == "United Kingdom")
            {
                cmbIssueCity.Items.Add("London");
                cmbIssueCity.Items.Add("Portsmouth");
            }
            else if(cmbIssueCountry.SelectedItem.ToString() == "Germany")
            {
                cmbIssueCity.Items.Add("Berlin");
                cmbIssueCity.Items.Add("Cologne");
            }
            else if(cmbIssueCountry.SelectedItem.ToString() == "France")
            {
                cmbIssueCity.Items.Add("Paris");
                cmbIssueCity.Items.Add("Lyon");
            }
            else if (cmbIssueCountry.SelectedItem.ToString() == "Spain")
            {
                cmbIssueCity.Items.Add("Madrid");
                cmbIssueCity.Items.Add("Barcelona");
            }
            else if (cmbIssueCountry.SelectedItem.ToString() == "Italy")
            {
                cmbIssueCity.Items.Add("Rome");
                cmbIssueCity.Items.Add("Naples");
            }
        }

        private void txtUserID_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex(@"[^2-9]+").IsMatch(e.Text);
        }

        private void txtName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex(@"[^\p{L}]+").IsMatch(e.Text);
        }

        private void txtSurname_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex(@"[^\p{L}]+").IsMatch(e.Text);
        }
    }
}




