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
	public partial class Attempt_Quiz : ContentPage
	{

        string quesId,
            quesName,mylectid,mycourse,myclass, op1, op2, op3, op4,ans_index,ans_option;
            Responses myResp = new Responses();

        int indexno;
        string name;
        AzureService az_serv = new AzureService();
        AzureServiceQuiz az_serv_quiz = new AzureServiceQuiz();
        AzureServiceResponse az_serv_resp = new AzureServiceResponse();


        public Attempt_Quiz (List<Quiz> myquizlist)
		{
            

            InitializeComponent ();
            Title = "Attempt Quiz";

            foreach (var temp in myquizlist)
            {
                quesId = temp.questionid;
                quesName = temp.questionName;
                mylectid = temp.myLectureNo;
                mycourse = temp.myCourse;
                myclass = temp.myClass;

                op1 = temp.option1;
                op2 = temp.option2;
                op3 = temp.option3;
                op4 = temp.option4;
              
                

            }

            myquesid.Text = quesId;
            myquesname.Text = quesName;
            //  AnswerPicker.Items.Add("Bismillah");

            AnswerPicker.Items.Add(op1);
            AnswerPicker.Items.Add(op2);
            AnswerPicker.Items.Add(op3);
            AnswerPicker.Items.Add(op4);


        }

      
        private async void  AnswerPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
             indexno = (AnswerPicker.SelectedIndex);
             name = AnswerPicker.Items[indexno];
           await DisplayAlert(indexno.ToString(), "selected index", "OK");

            Responses responses = new Responses()
            {
                questionid = quesId,
                questionName = quesName,
                myLectureNo=mylectid,
                myCourse=mycourse,
                myClass=myclass,
                answerIndex=indexno.ToString(),
                answerOption=name
            };

            myResp = responses;
        }


        private async void Submit_Clicked(object sender, EventArgs e)
        {

            submit.BackgroundColor = Color.LightCyan;

            IsBusy = true;

            try
            {
                await az_serv_resp.AddResponses(myResp);
                await DisplayAlert("SUCCESS", "Responses have been sent to cloud", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("FAILURE", "Responses could not be saved. Please try again", "OK");
            }
            finally
            {
                IsBusy = false;

               

            }
        }

    }
    
}