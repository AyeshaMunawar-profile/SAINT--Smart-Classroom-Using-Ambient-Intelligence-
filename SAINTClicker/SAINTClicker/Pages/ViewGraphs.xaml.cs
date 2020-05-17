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

	public partial class ViewGraphs : ContentPage
	{
        string myclass, mycourse, mylectid;
        AzureServiceQuiz az_serv_quiz = new AzureServiceQuiz();
        AzureServiceResponse az_serv_resp = new AzureServiceResponse();
        List<Quiz> myquizlist = new List<Quiz>();


        public ViewGraphs (List<User> data)
		{
			InitializeComponent ();
            Title = "View Graphs";

        }
        private async void submit_Clicked(object sender, EventArgs e)
        {
            Quiz quiz = new Quiz()
            {
                myClass = ClassName.Text,
                myCourse = CourseName.Text,
                myLectureNo = LectId.Text

            };
            IsBusy = true;
             try
            {

            myquizlist = (await az_serv_quiz.GetQuiz(quiz)).ToList();
            
            
               if (myquizlist.Count() == 0)
                {
                    await DisplayAlert("ERROR", "NO RECORD MATCHED", "OK");
                }
                else
                {
                   
                    await Navigation.PushModalAsync(new NavigationPage(new VisualizeGraph(myquizlist)));
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("ERROR", "Could not get your details", "OK");
            }
            finally
            {
                IsBusy = false;
            }

            

    
        }
        
    }
}