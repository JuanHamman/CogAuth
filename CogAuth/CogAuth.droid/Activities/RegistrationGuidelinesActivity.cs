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
using CogAuth.droid.Commons;
using Java.Lang;

namespace CogAuth.droid.Activities
{
    [Activity(Label = "RegistrationGuidelinesActivity")]
    public class RegistrationGuidelinesActivity : Activity
    {

        Button btnNext;
        EditText edtUserName;
        AlertDialog EmptyUserNameDialog;
        TextView txtGuidelines;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.RegistrationGuidelines);

            btnNext = FindViewById<Button>(Resource.Id.btn_RegGuidlines_Guidelines);
            btnNext.Click += BtnNext_Click;

            edtUserName = FindViewById<EditText>(Resource.Id.edt_RegGuidelines_Username);

            txtGuidelines = FindViewById<TextView>(Resource.Id.txt_RegGuidelines_GuideLines);
            txtGuidelines.Text = Guidlines();

        }

        private string Guidlines()
        {
            string g;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("Face");
            sb.AppendLine(System.Environment.NewLine);
            sb.Append("* Hold face in the middle of the screen");
            sb.Append(System.Environment.NewLine);
            sb.Append("* Be happy");
            sb.AppendLine(System.Environment.NewLine);
            sb.Append("Audio");
            sb.AppendLine(System.Environment.NewLine);
            sb.Append("* Speak load");
            sb.Append(System.Environment.NewLine);
            sb.Append("* Make sure there is no background noise.");

            return g = sb.ToString();
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(edtUserName.Text))
                showValidationDialog();
            else
                commons.Instance.UserName = edtUserName.Text;
            StartActivity(typeof(RegistrationFaceActivity));
        }

        private void showValidationDialog()
        {
            EmptyUserNameDialog = new AlertDialog.Builder(this)

            .SetCancelable(false)
            .SetTitle("Attention")
            .SetMessage("Username cant be empty.")
            .SetPositiveButton("Ok", EmptyUserNamePositive)
            .Create();

            EmptyUserNameDialog.Show();
        }

        private void EmptyUserNamePositive(object sender, DialogClickEventArgs e)
        {
          
        }
    }
}