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
    [Activity(Label = "SignInVoice", Theme = "@android:style/Theme.Holo.Light")]
    public class SignInVoiceActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.SignIn_Voice);

            // Create your application here
        }
    }
}