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
using Android.Media;
using System.IO;
using Plugin.AudioRecorder;
using System.Threading.Tasks;

namespace CogAuth.droid.Activities
{
    [Activity(Label = "RegistrationVoiceActivity")]
    public class RegistrationVoiceActivity : Activity
    {
        #region Private Members
        AudioRecorderService recorder;
        Button btnStart, btnStop;
        #endregion

        #region Overrides
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Registration_Voice);

            btnStart = FindViewById<Button>(Resource.Id.btn_RegVoice_Start);
            btnStart.Click += BtnStart_Click;

            btnStop = FindViewById<Button>(Resource.Id.btn_RegVoice_Stop);
            btnStop.Click += BtnStop_Click;

            InitController();
        }

        private void InitController()
        {
            recorder = new AudioRecorderService
            {
                StopRecordingOnSilence = true, //will stop recording after 2 seconds (default)
                StopRecordingAfterTimeout = true,  //stop recording after a max timeout (defined below)
                TotalAudioTimeout = TimeSpan.FromSeconds(10) //audio will stop recording after 15 seconds
            };
        }

        protected override void OnResume()
        {
            base.OnResume();

        }

        protected override void OnPause()
        {
            base.OnPause();
        }
        #endregion

        #region Methods

        #endregion

        #region Events
        private void BtnStop_Click(object sender, EventArgs e)
        {

        }

        private async void BtnStart_Click(object sender, EventArgs e)
        {
            await RecordAudio();
        }

        private async Task RecordAudio()
        {
            try
            {
                if (!recorder.IsRecording)
                {
                    await recorder.StartRecording();
                }
                else
                {
                    await recorder.StopRecording();
                }
            }
            catch (Exception ex)
            {

	        }
        }
        #endregion
    }
}