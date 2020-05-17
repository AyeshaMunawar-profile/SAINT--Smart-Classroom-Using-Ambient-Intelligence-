using SAINTClicker.Models;
using SAINTClicker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SkiaSharp;
using Entry = Microcharts.Entry;


namespace SAINTClicker.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class VisualizeGraph : ContentPage
	{

        string myclass, mycourse, mylectid;
        int myind;

        List<Responses> myresponses = new List<Responses>();
       List<Entry> myenteries = new List<Entry>();
        IDictionary<int, string> dict = new Dictionary<int, string>();

        AzureServiceResponse azr_resp = new AzureServiceResponse();

        public VisualizeGraph(List<Quiz> quiz)
        {
            InitializeComponent();
            Title = "Student Performance Graphs";
            SetData(quiz);
            // testc(quiz);
            // test1.Text = myresp.ToList().Count().ToString();
            

        }

        private async void SetData(List<Quiz> quiz)
        {
            foreach (var k in quiz)
            {
                myclass = k.myClass;
                mycourse = k.myCourse;
                mylectid = k.myLectureNo;
            }


            Responses resp = new Responses
            {
                myClass = myclass,
                myCourse = mycourse,
                myLectureNo = mylectid

            };


            IsBusy = true;
            try { 
                var myresp = (await azr_resp.GetResponses(resp)).ToList();
           // var random = new Random();
                var ind0list = myresp.Where(s => s.answerIndex == "0").Count();
                var ind1list = myresp.Where(s => s.answerIndex == "1").Count();
                var ind2list = myresp.Where(s => s.answerIndex == "2").Count();
                var ind3list = myresp.Where(s => s.answerIndex == "3").Count();

                string color0 = "31B2BF";
                string color1 = "54DCA9";
                string color2 = "736FCB";
                string color3 = "CB6FB5";


                List<Entry> enteries = new List<Entry>
                {

                new Entry(float.Parse(ind0list.ToString()))
                 {
                     Color = SKColor.Parse(color0),
                     Label ="Option1",
                     ValueLabel = ind0list.ToString()
                 },
                 new Entry(float.Parse(ind1list.ToString()))
                 {
                     Color = SKColor.Parse(color1),
                     Label ="Option02" ,
                     ValueLabel = ind1list.ToString()
                 },
                 new Entry(float.Parse(ind2list.ToString()))
                 {
                     Color = SKColor.Parse(color2),
                     Label = "Option03",
                     ValueLabel = ind2list.ToString()
                 },
                 new Entry(float.Parse(ind0list.ToString()))
                 {
                     Color = SKColor.Parse(color3),
                     Label = "Option04",
                     ValueLabel = ind3list.ToString()
                 }

                 };
                myenteries = enteries;


                /*   foreach (var k in myresp)
                   {


                       var color = String.Format("#{0:X6}", random.Next(0x1000000));

                       enteries.Add(

                    new Entry(float.Parse(k.answerIndex))
                    {
                        Color = SKColor.Parse(color),
                        Label = k.answerOption,
                        ValueLabel = k.answerIndex.ToString()
                    }
                    );


                   }*/

            }

            catch
            {
                await DisplayAlert("ERROR", "NO RESPONSES FOUND ", "OK");
            }
            finally
            {
                IsBusy = false;
            }

           
             displayData();




        }

        private void displayData()
        {
            MyChart1.Chart = new Microcharts.DonutChart { Entries = myenteries };
            MyChart2.Chart = new Microcharts.BarChart { Entries = myenteries };
            MyChart3.Chart = new Microcharts.LineChart { Entries = myenteries };
            MyChart4.Chart = new Microcharts.PointChart { Entries = myenteries };
        }
    }
}