using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Widget;
using CogAuth.droid.Commons;
using Java.IO;
using Environment = Android.OS.Environment;
using Uri = Android.Net.Uri;
using CogAuth.services.Implementation;

namespace CogAuth.droid.Activities
{
    [Activity(Label = "SignInFace", Theme = "@android:style/Theme.Holo.Light")]
    public class SignInFaceActivity : Activity
    {
		ImageView imgPhoto;
		Button btnNext;
		Button btnCapture;
        AlertDialog SignInResultDialog;

        protected override void OnCreate(Bundle savedInstanceState)
        {
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.SignIn_Face);



			if (IsThereAnAppToTakePictures())
			{
				CreateDirectoryForPictures();

				btnNext = FindViewById<Button>(Resource.Id.btnNext);
				btnCapture = FindViewById<Button>(Resource.Id.btnCapture);
				imgPhoto = FindViewById<ImageView>(Resource.Id.imgPhoto);
				btnCapture.Click += BtnCapture_Click;
				btnNext.Click += BtnNext_Click;
			}
        }

		private async void BtnNext_Click(object sender, EventArgs e)
		{
            try
            {
                ProgressDialog pd = new ProgressDialog(this);
                pd.SetMessage("Fazureing Sign in...");
                pd.Show();

                UserService us = new UserService();
                var result = await us.SignIn(commons.Instance.image, commons.Instance.Audio.ToString(), "");

                if (!string.IsNullOrEmpty(result.UserName))
                {
                    StartActivity(typeof(RegistrationVoiceActivity));
                    pd.Hide();
                    Toast.MakeText(this,"Welcome " + result.UserName, ToastLength.Long);
                }
                else
                {
                    pd.Hide();
                    SignInResultDialog = new AlertDialog.Builder(this)

            .SetCancelable(false)
            .SetTitle("Invalid user")
            .SetMessage("Sign in failed, please try again")
            .SetPositiveButton("Ok", SignInDialogPositive)
            .Create();

                    SignInResultDialog.Show();
                }
            }
            catch (Exception ex)
            {
                return;
            }

           
		}

        private void SignInDialogPositive(object sender, DialogClickEventArgs e)
        {
            throw new NotImplementedException();
        }

        void BtnCapture_Click(object sender, EventArgs e)
		{
			Intent intent = new Intent(MediaStore.ActionImageCapture);

			App._file = new File(App._dir, String.Format("myPhoto_{0}.jpg", Guid.NewGuid()));

			intent.PutExtra(MediaStore.ExtraOutput, Uri.FromFile(App._file));

			StartActivityForResult(intent, 0);
		}

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);

			// Make it available in the gallery

			Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
			Uri contentUri = Uri.FromFile(App._file);
			mediaScanIntent.SetData(contentUri);
			SendBroadcast(mediaScanIntent);

			// Display in ImageView. We will resize the bitmap to fit the display
			// Loading the full sized image will consume to much memory 
			// and cause the application to crash.

			int height = Resources.DisplayMetrics.HeightPixels;
			int width = imgPhoto.Width;
			App.bitmap = App._file.Path.LoadAndResizeBitmap(width, height);
			if (App.bitmap != null)
			{
				var rotatedImage = RotateImage(App.bitmap, -90);
				imgPhoto.SetImageBitmap(rotatedImage);
				var base64 = BitmapHelpers.ConvertBitMapToBase64String(rotatedImage);
                commons.Instance.image = base64;
				App.bitmap = null;
			}

			// Dispose of the Java side bitmap.
			GC.Collect();
		}

		private void CreateDirectoryForPictures()
		{
			App._dir = new File(
				Environment.GetExternalStoragePublicDirectory(
					Environment.DirectoryPictures), "CogAuthDemo");
			if (!App._dir.Exists())
			{
				App._dir.Mkdirs();
			}
		}

		private Bitmap RotateImage(Bitmap src, float degrees)
		{
			var matrix = new Matrix();

			matrix.PostRotate(degrees);

			return Bitmap.CreateBitmap(src, 0, 0, src.Width, src.Height, matrix, true);
		}

		private bool IsThereAnAppToTakePictures()
		{
			Intent intent = new Intent(MediaStore.ActionImageCapture);
			IList<ResolveInfo> availableActivities =
				PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
			return availableActivities != null && availableActivities.Count > 0;
		}
    }
}