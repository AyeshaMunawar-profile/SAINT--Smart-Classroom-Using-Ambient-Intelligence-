﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SAINTClicker.Droid
{
    [Activity(Label = "SAINTClicker",
        Theme = "@style/splashscreen",
        MainLauncher = true,
        NoHistory = true)]
    public class splashscreen : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            StartActivity(typeof(MainActivity));
        }
    }
}