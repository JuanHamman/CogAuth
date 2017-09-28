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

    [Activity(Label = "SignInActivity")]
    public class SignInActivity : Activity
    {
        private Button btnRegister;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SignIn);

            btnRegister = FindViewById<Button>(Resource.Id.btnRegister);

            Console.WriteLine("FK 0");
            btnRegister.Click += BtnRegister_Click;
        }

        void BtnRegister_Click(object sender, EventArgs e)
        {
            Console.WriteLine("FK 1");
            StartActivity(new Intent(this, typeof(RegistrationFaceActivity)));
        }
    }
}