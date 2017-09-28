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

namespace CogAuth.droid.Activities
{
    [Activity(Label = "RegistrationVoiceActivity")]
    public class RegistrationVoiceActivity : Activity
    {
        #region Private Members
        MediaRecorder _recorder;
        MediaPlayer _player;
        string path = "/sdcard/test.3gpp";
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
        }

        protected override void OnResume()
        {
            base.OnResume();

            _recorder = new MediaRecorder();
            _player = new MediaPlayer();

            _player.Completion += (sender, e) => {
                _player.Reset();
                btnStart.Enabled = !btnStart.Enabled;
            };

        }

        protected override void OnPause()
        {
            base.OnPause();

            _player.Release();
            _recorder.Release();
            _player.Dispose();
            _recorder.Dispose();
            _player = null;
            _recorder = null;
        }
        #endregion

        #region Methods

        #endregion

        #region Events
        private void BtnStop_Click(object sender, EventArgs e)
        {
            btnStop.Enabled = !btnStop.Enabled;

            _recorder.Stop();
            _recorder.Reset();

            _player.SetDataSource(path);
            _player.Prepare();
            _player.Start();
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            btnStop.Enabled = !btnStop.Enabled;
            btnStart.Enabled = !btnStart.Enabled;

            _recorder.SetAudioSource(AudioSource.Mic);
            _recorder.SetOutputFormat(OutputFormat.ThreeGpp);
            _recorder.SetAudioEncoder(AudioEncoder.AmrNb);
            _recorder.SetOutputFile(path);
            _recorder.Prepare();
            _recorder.Start();
        }
        #endregion
    }
}