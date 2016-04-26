/* Copyright (c) 2016, Cocosoft Inc.
 All rights reserved.
 http://www.Cocosofttech.com

 This file is part of the LIS open source project.

 The LIS  open source project is free software: you can
 redistribute it and/or modify it under the terms of the GNU General Public
 License as published by the Free Software Foundation, either version 3 of the
 License, or (at your option) any later version.

 The ClearCanvas LIS open source project is distributed in the hope that it
 will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
 Public License for more details.

 You should have received a copy of the GNU General Public License along with
 the LIS open source project.  If not, see
 <http://www.gnu.org/licenses/>.
*/
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
