using Android.App;
using Android.Widget;
using Android.OS;

namespace CogAuth.droid
{
    [Activity(Label = "CogAuth.droid", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.SignIn);
        }
    }
}

