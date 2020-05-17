using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SAINTClicker.Pages;

namespace SAINTClicker
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();   
		}

        private async void Login_Clicked(object sender, EventArgs e)
        {

            
            await Navigation.PushModalAsync(new NavigationPage(new LoginPage()));
        }

        private async void Signup_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new SignupPage()));
        }

        

    }
}
