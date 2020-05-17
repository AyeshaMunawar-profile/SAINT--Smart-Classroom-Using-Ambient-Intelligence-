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
	public partial class CreateQuiz : ContentPage
	{
        string myclass, mycourse, myregno;
        List<User> myuser = new List<User>();
        

        public CreateQuiz (List<User> data)
		{
			InitializeComponent ();
            Title = "Create Quiz";
            foreach(var q in data)
            {
                myclass = q.myclass;
                mycourse = q.mycourse;
                myregno = q.registrationNo;
                
                
            }
            RegNo.Text = myregno;
            MyClassName.Text = myclass;
            MyCourseName.Text = mycourse;

            myuser = data;

		}

        private async Task CreateMyQuiz_Clicked(object sender, EventArgs e)
        {
            create.BackgroundColor = Color.LightCyan;
            
            AzureServiceQuiz az_serv = new AzureServiceQuiz();
          



            var quiz = new Quiz
            {
                regNo = myregno,
                myClass=myclass,
                myCourse=mycourse,
                myLectureNo= MyLectId.Text,
                questionid = MyQuesId.Text,
                questionName= MyQuesName.Text,
                option1=Op1.Text,
                option2 = Op2.Text,
                option3 = Op3.Text,
                option4 = Op4.Text


            };
            IsBusy = true;
            try
            {
                await az_serv.AddQuiz(quiz);
                await DisplayAlert("Success", "Quiz has been created and uploaded to Cloud", "OK");
               
            }
            catch(Exception ex)
            {
                await DisplayAlert("Error", "Couldnot create quiz.", "Back");
            }
            finally
            {
                IsBusy = false;
                await Navigation.PushModalAsync(new NavigationPage(new SaintMainScreen(myuser)));
            }
        }
    }
}