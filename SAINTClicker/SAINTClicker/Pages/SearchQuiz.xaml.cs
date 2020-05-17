using SAINTClicker.Models;
using SAINTClicker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SAINTClicker.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchQuiz : ContentPage
	{
        string quesId, quesName,lectid, op1, op2, op3, op4, ans_index, ans_option;
        int indexno;
        string name;
        AzureService az_serv = new AzureService();
        AzureServiceQuiz az_serv_quiz = new AzureServiceQuiz();
        AzureServiceResponse az_serv_resp = new AzureServiceResponse();


        public SearchQuiz (List<User> user)
		{
			InitializeComponent ();
            Title = "Search your Class's Quiz";
        }



        private async void SearchQuiz_Clicked(object sender, EventArgs e)
        {
            submit.BackgroundColor = Color.LightCyan;

            List<User> userlist=new List<User>();
            List<Quiz> myquizlist = new List<Quiz>();
            var myuser = new User
            {

                mycourse = CourseName.Text,
                myclass = ClassName.Text
            };

            var myquiz = new Quiz
            {
                myCourse = CourseName.Text,
                myClass = ClassName.Text,
                myLectureNo= LectId.Text
            };
            IsBusy = true;
            try
            {
                 userlist = (await az_serv.GetUsersProfile(myuser)).ToList();


            }
            catch(Exception ex)
            {
                await DisplayAlert("ERROR", "Could not get your details", "OK");
            }
            finally
            {
                IsBusy = false;
            }


            if (userlist.Count() != 0)//if user exists with that registration
            {


                IsBusy = true;
                try
                {
                     myquizlist = (await az_serv_quiz.GetQuiz(myquiz)).ToList();
                    await Navigation.PushModalAsync(new NavigationPage(new Attempt_Quiz(myquizlist)));
                }
                catch (Exception ex)
                {
                    await DisplayAlert("ERROR", "Could not fetch quiz from the server", "OK");
                }
                finally
                {
                    IsBusy = false;
                    
                }

            }
            else
            {
                await DisplayAlert("ERROR", "No Record for the entered Class exists", "Return");
            }
        }
    }
}