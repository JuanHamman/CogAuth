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
using Plugin.AudioRecorder;
using Android.Media;
using System.IO;
using CogAuth.droid.Commons;
using System.Threading.Tasks;
using CogAuth.services.Implementation;

namespace CogAuth.droid.Activities
{
    [Activity(Label = "SignInVoice")]
    public class SignInVoiceActivity : Activity
    {
        #region Private Members
        AudioRecorderService recorder;
        MediaPlayer player;
        Button btnStart, btnPlay, btnNext;
        private string Path;
        AlertDialog SignInResultDialog;
        #endregion

        #region Overrides
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.SignIn_Voice);

            btnNext = FindViewById<Button>(Resource.Id.btn_SignInVoice_Next);
            btnNext.Click += BtnNext_Click;

            btnPlay = FindViewById<Button>(Resource.Id.btn_SignInVoice_Play);
            btnPlay.Click += BtnPlay_Click;

            btnStart = FindViewById<Button>(Resource.Id.btn_SignInVoice_Record);
            btnStart.Click += BtnStart_Click;

            InitController();
        }

        protected override void OnStart()
        {
            base.OnStart();

            recorder = new AudioRecorderService
            {
                StopRecordingOnSilence = false,
                StopRecordingAfterTimeout = false
            };

            //alternative event-based API can be used here in lieu of the returned recordTask used below
            //recorder.AudioInputReceived += Recorder_AudioInputReceived;

            player = new MediaPlayer();
            player.Completion += Player_Completion;

            btnStart.Enabled = true;
        }

        #endregion


        #region Methods
        async Task PlayRecordedAudio()
        {
            try
            {
                btnStart.Enabled = false;
                btnPlay.Enabled = false;

                var filePath = recorder.GetAudioFilePath();

                if (filePath != null)
                {
                    player.Reset();
                    await player.SetDataSourceAsync(filePath);
                    player.Prepare();
                    player.Start();
                }
            }
            catch (Exception ex)
            {
                //blow up the app!
                throw ex;
            }
        }

        async Task RecordAudio()
        {
            try
            {
                if (!recorder.IsRecording)
                {
                    // recorder.StopRecordingOnSilence = checkTimeout.Checked;

                    btnStart.Enabled = false;


                    //the returned Task here will complete once recording is finished
                    var recordTask = await recorder.StartRecording();

                    btnStart.Text = "Stop";
                    btnStart.Enabled = true;

                    var audioFile = await recordTask;

                    //audioFile will contain the path to the recorded audio file

                    btnStart.Text = "Record";

                    btnStart.Enabled = !string.IsNullOrEmpty(audioFile);
                }
                else
                {
                    btnStart.Enabled = false;

                    await recorder.StopRecording();

                    btnStart.Text = "Record";
                    btnStart.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                //blow up the app!
                throw;
            }
        }
        void Recorder_AudioInputReceived(object sender, string audioFile)
        {
            RunOnUiThread(() =>
            {
                btnStart.Text = "Record";

                btnStart.Enabled = !string.IsNullOrEmpty(audioFile);
            });
        }

        private void ConvertAudioToArray(string path)
        {
            using (var streamReader = new StreamReader(path))
            {
                var bytes = default(byte[]);
                using (var memstream = new MemoryStream())
                {
                    streamReader.BaseStream.CopyTo(memstream);
                    bytes = memstream.ToArray();

                    String encoded = Android.Util.Base64.EncodeToString(bytes, 0);

                    commons.Instance.Audio = encoded;
                }
            }
        }

        private void Player_Completion(object sender, EventArgs e)
        {
            player.Stop();

            this.Path = recorder.GetAudioFilePath();

            ConvertAudioToArray(this.Path);

            btnStart.Enabled = true;
            btnPlay.Enabled = true;
        }
        private void InitController()
        {
            recorder = new AudioRecorderService
            {
                StopRecordingOnSilence = true, //will stop recording after 2 seconds (default)
                StopRecordingAfterTimeout = false,  //stop recording after a max timeout (defined below)   
                PreferredSampleRate = 16,
                TotalAudioTimeout = TimeSpan.FromSeconds(10) //audio will stop recording after 15 seconds
            };
        }
        #endregion

        #region Events
        private async void BtnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            await RecordAudio();
        }

        private async void BtnPlay_Click(object sender, EventArgs e)
        {
            await PlayRecordedAudio();
        }

        private async void BtnNext_Click(object sender, EventArgs e)
        {
            try
            {
                ProgressDialog pd = new ProgressDialog(this);
                pd.SetMessage("Fazureing...");
                pd.Show();

                UserService us = new UserService();
                var result = await us.SignIn(commons.Instance.image, commons.Instance.Audio.ToString(), "");
                if (result)
                {
                    StartActivity(typeof(RegistrationResultActivity));
                    pd.Hide();
                    commons.Instance.Audio = "";
                    commons.Instance.image = "";
                    commons.Instance.UserName = "";
                }
                else
                {
                    pd.Hide();
                    SignInResultDialog = new AlertDialog.Builder(this)

            .SetCancelable(false)
            .SetTitle("Sign in failed")
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
            StartActivity(typeof(SignInActivity));
        }

        #endregion


    }
}