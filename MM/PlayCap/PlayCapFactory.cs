using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;
using System.ServiceModel;
using System.Windows.Forms;

namespace PlayCap
{
    #region Delegate Events
    public delegate void CaptureHandle();
    #endregion
    public class PlayCapFactory : IDisposable
    {
        #region Members
        public static PlayCapFactory PlayCapFact = new PlayCapFactory();
        public RegisteredEvent ClientEvent = new RegisteredEvent();
        public event CaptureHandle OnCaptureEvent = null;
        #endregion

        #region IDisposable
        ~PlayCapFactory()
		{
			Dispose(false);
		}

		public void Dispose()
		{
			GC.SuppressFinalize(this);
			Dispose(true);
		}

        protected virtual void Dispose(bool disposing)
        {
            // Only clean up managed resources if being called from IDisposable.Dispose
            if (disposing)
            {
            }
        }
        #endregion

        #region Methods
        public void Capture()
        {
            RaiseCaptureEvent();
        }
        #endregion

        #region Raise Events
        public void RaiseCaptureEvent()
        {
            if (OnCaptureEvent != null)
                OnCaptureEvent();
        }

        public void RaiseOnBitmap(byte[] bmp)
        {
            //raise event
            try
            {
                ClientEvent.RaiseOnBitmap(bmp);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Play Cap", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}
