using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using MM.Common;

namespace MM
{
    #region Deletegate Events
    public delegate void CaptureCompletedHandler(Image img);
    #endregion
    public class PlayCapFactory
    {
        #region Members
        private static PlayCapProxy _callBack = null;
        private static InstanceContext _instanceContext = null;
        private static PlayCapService.PlayCapClient _client = null;
        public static event CaptureCompletedHandler OnCaptureCompletedEvent = null;
        #endregion

        #region Methods
        public static void InitClient()
        {
            try
            {
                _callBack = new PlayCapProxy();
                _instanceContext = new InstanceContext(_callBack);
                _client = new PlayCapService.PlayCapClient(_instanceContext);
                _client.RegisterEvent();
                _callBack.OnBitmapEvent += new BitmapHandler(_callBack_OnBitmapEvent);
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, string.Format("Init Client :{0}", ex.Message), Common.IconType.Error); 
            }
        }

        public static void RunPlayCapProcess()
        {
            if (!Utility.CheckPlayCapProcessExist())
            {
                Utility.RunPlayCapProcess();
                while (!Utility.CheckPlayCapProcessExist())
                {
                    
                }

                InitClient();
            }
        }

        public static void KillPlayCapProcess()
        {
            Utility.KillPlayCapProcess();
        }

        public static void Capture()
        {
            _client.Capture();
        }

        private static void _callBack_OnBitmapEvent(byte[] bmp)
        {
            if (OnCaptureCompletedEvent != null)
            {
                string fileName = string.Format("{0}\\ImageCapture.png", AppDomain.CurrentDomain.BaseDirectory);
                Bitmap img = new Bitmap(fileName);
                OnCaptureCompletedEvent(img);
                img.Dispose();
                img = null;
            }
        }
        #endregion
    }
}
