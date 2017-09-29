using System;
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

    [Activity(Label = "SignInActivity" , MainLauncher = true)]
    public class SignInActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.SignIn);

            Button btnSignIn = FindViewById<Button>(Resource.Id.btn_SignIn_SignIn);
            btnSignIn.Click += BtnNext_Click;

            Button btnRegister = FindViewById<Button>(Resource.Id.btn_SignIn_Register);
            btnRegister.Click += BtnRegister_Click;
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(RegistrationGuidelinesActivity));
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(SignInFaceActivity));
        }
    }
}