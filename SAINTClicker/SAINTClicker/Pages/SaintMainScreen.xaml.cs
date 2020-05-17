using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SAINTClicker.Pages;
using SAINTClicker.Models;

namespace SAINTClicker.Pages
{
   
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SaintMainScreen : ContentPage
	{
        string my_role;
        List<User> myuser = new List<User>();
		public SaintMainScreen (List<User> data)
		{
			InitializeComponent ();
            Title = "Welcome to SAINT";
            myuser = data;

            foreach (var temp in myuser)
            {
                my_role = temp.role;
            }
        }
   
        private async Task CreateQuiz_Clicked(object sender, EventArgs e)
        {
            createquiz.BackgroundColor = Color.LightCyan;


            if (my_role == "Student")
            {
                await DisplayAlert("Error", "You are not allowed to create a quiz", "OK");
            }
            else
            {
                await Navigation.PushModalAsync(new NavigationPage(new CreateQuiz(myuser)));
            }
        }

        private void Attendance_Clicked(object sender, EventArgs e)
        {
            markattendance.BackgroundColor = Color.LightCyan;
        }

        private async Task AttemptQuiz_Clicked(object sender, EventArgs e)
        {
            attemptquiz.BackgroundColor = Color.LightCyan;

            if (my_role == "Instructor")
            {
                await DisplayAlert("ERROR", "You cannot attempt the quiz", "OK");
            }
            else
            {
                await Navigation.PushModalAsync(new NavigationPage(new SearchQuiz(myuser)));
            }
        }

        private async Task Graphs_Clicked(object sender, EventArgs e)
        {
            graphs.BackgroundColor = Color.LightCyan;
            await Navigation.PushModalAsync(new NavigationPage(new ViewGraphs(myuser)));






        }

        private async Task Profile_Clicked(object sender, EventArgs e)
        {
            profile.BackgroundColor = Color.LightCyan;
            await Navigation.PushModalAsync(new NavigationPage(new Profile(myuser)));
        }
    }
}