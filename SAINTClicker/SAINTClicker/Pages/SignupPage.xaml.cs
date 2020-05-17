using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SAINTClicker.Pages;
using SAINTClicker.Models;
using SAINTClicker.Services;

namespace SAINTClicker
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SignupPage : ContentPage
	{
        

        public SignupPage ()
		{
			InitializeComponent ();
            Title = "SignUp";
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async  void Signup_Clicked(object sender, EventArgs e)
        {
            signup.BackgroundColor = Color.LightCyan;
            AzureService az_serv= new AzureService();
            var user = new User
            {
                firstName = FirstName.Text,
                username = Username.Text,
                role = Role.Text,
                password = Password.Text,
                registrationNo=RegistrationNo.Text,
                myclass=ClassName.Text,
                mycourse=CourseName.Text
            };

            IsBusy = true;

            try
            {
                await az_serv.AddUser(user);
                await Navigation.PushModalAsync(new NavigationPage(new LoginPage()));

            }
            catch (Exception ex)
            {
                await this.DisplayAlert("Error", "Failed to Register: "
                                            + ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;

                

            }
        }

       
    }
}