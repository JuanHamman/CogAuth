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

namespace CogAuth.droid.Commons
{
   public class commons
    {
        #region Singleton
        private commons()
        { }

        private static commons instance;
        public static commons Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new commons();
                }
                return instance;
            }
        }
        #endregion


        public string image;

        public byte[] AudioPath;

    }
}