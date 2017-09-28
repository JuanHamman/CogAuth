using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Content;
using CogAuth.droid.Activities;

namespace CogAuth.droid
{
    [Activity(Label = "CogAuth.droid", MainLauncher = true)]
    public class MainActivity : Activity
    {
        Button btnRegister;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.SignIn);

            btnRegister = FindViewById<Button>(Resource.Id.btnRegister);
            btnRegister.Click += BtnRegister_Click;
        }

		void BtnRegister_Click(object sender, EventArgs e)
		{
			StartActivity(new Intent(this, typeof(RegistrationFaceActivity)));
		}
    }
}

