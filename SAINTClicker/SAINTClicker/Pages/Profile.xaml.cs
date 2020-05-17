using SAINTClicker.Models;
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
	public partial class Profile : ContentPage
	{
        public string id,fname,uname, pw, regno, myrole,myclass,mycourse;

        public Profile (List<User> data)
		{
			InitializeComponent ();
            Title = "My Profile";
            foreach (var q in data)
            {
                id = q.id;
                fname = q.firstName;
                uname = q.username;
                pw = q.password;
                regno = q.registrationNo;
                myrole = q.role;
                myclass = q.myclass;
                mycourse = q.mycourse;

            }
            //name.Text = fname;
            //usname.Text = uname;
            //string size = (data.Count).ToString();
            Myid.Text = id;
            Myfname.Text = fname;
            Myuname.Text = uname;
            Mypw.Text = pw;
            Myregno.Text = regno;
            Myrole.Text = myrole;
            Myclass.Text = myclass;
            Mycourse.Text = mycourse;
            // mySize.Text = size;
            // myuserlist.Add(fname);
            // myuserlist.Items.Add(fname);
            //myuserlist.ItemsSource = data;
            // name.Text = fname;
            //usname.Text = uname;
            //  myname.Text = fname;


            //mylistofusers.bindingContext = data;

            //userlist.BindingContext = data;
            // userlist.ItemsSource = data;

            //i want to access the elements of this data list i-e username, password, regno,firstname etc here

            /* try
             {
                 // Add the questions
               //  foreach (var q in data)
                 //    userlist.ItemsSource = data;
                    // questionPicker.Items.Add(q.question);

             }
             catch (Exception ex)
             {
                 await this.DisplayAlert("Error", "Failed to download user details: "
                                             + ex.Message, "OK");
             }*/
        }









    }
}