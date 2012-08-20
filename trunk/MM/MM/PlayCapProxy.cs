using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MM
{
    public delegate void BitmapHandler(byte[] bmp);
    public class PlayCapProxy : PlayCapService.IPlayCapCallback
    {
        #region Events
        public event BitmapHandler OnBitmapEvent = null;
        #endregion

        #region IPlayCapCallback Members
        public void OnBitmap(byte[] bmp)
        {
            if (OnBitmapEvent != null)
                OnBitmapEvent(bmp);
        }
        #endregion
    }
}
