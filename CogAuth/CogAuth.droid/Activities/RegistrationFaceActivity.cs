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

namespace CogAuth.droid.Activities
{
    [Activity(Label = "RegistrationFaceActivity")]
    public class RegistrationFaceActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Button btnNext = FindViewById<Button>(Resource.Id.btn_RegFace_Next);
            btnNext.Click += BtnNext_Click;
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(RegistrationVoiceActivity));
        }
    }
}