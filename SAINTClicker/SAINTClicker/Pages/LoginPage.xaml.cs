using SAINTClicker.Models;
using SAINTClicker.Pages;
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
	public partial class LoginPage : ContentPage
	{
        public List<User> data;
       
        public LoginPage()
        {
            InitializeComponent();
            Title = "Login to SAINT";
           // NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void LoginButton_Clicked(object sender, EventArgs e)

        {
            loginbutton.BackgroundColor = Color.LightCyan;
            AzureService az_serv = new AzureService();
            var user = new User
            {
               
                username = Username.Text,
               
                password = Password.Text
            };

            IsBusy = true;

            try
            {
                 data = (await az_serv.GetUsers(user)).ToList();
                if(data.Count()==0)// no record found
                {
                    await DisplayAlert("ERROR", "No user exists with the given credentials", "OK");
                    return;
                    //await Navigation.PushModalAsync(new NavigationPage(new SignupPage()));

                }
                else
                await Navigation.PushModalAsync(new NavigationPage(new SaintMainScreen(data)));


            }
            catch (Exception ex)
            {
                await this.DisplayAlert("Error", "Failed to authenticate: "
                                            + ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
               
            }
        }

        
    }
}