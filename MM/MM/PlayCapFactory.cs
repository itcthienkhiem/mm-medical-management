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
        public static bool InitClient()
        {
            try
            {
                _callBack = new PlayCapProxy();
                _instanceContext = new InstanceContext(_callBack);
                _client = new PlayCapService.PlayCapClient(_instanceContext);
                _client.RegisterEvent();
                _callBack.OnBitmapEvent += new BitmapHandler(_callBack_OnBitmapEvent);
                return true;
            }
            catch (Exception ex)
            {
                //MsgBox.Show(Application.ProductName, string.Format("Init Client :{0}", ex.Message), Common.IconType.Error); 
            }

            return false;
        }

        public static void RunPlayCapProcess(bool isShowCapture)
        {
            if (!Utility.CheckPlayCapProcessExist())
            {
                Utility.RunPlayCapProcess(isShowCapture);
                while (!Utility.CheckPlayCapProcessExist())
                {
                    System.Threading.Thread.Sleep(500);
                }

                //
                while (!InitClient())
                {
                    System.Threading.Thread.Sleep(500);
                }
            }
        }

        public static void KillPlayCapProcess()
        {
            Utility.KillPlayCapProcess();
        }

        public static void Capture()
        {
            try
            {
                _client.Capture();
            }
            catch (Exception ex)
            {
                //MsgBox.Show(Application.ProductName, string.Format("Capture :{0}", ex.Message), Common.IconType.Error); 
            }
        }

        private static void _callBack_OnBitmapEvent(byte[] bmp)
        {
            if (OnCaptureCompletedEvent != null)
            {
                string fileName = string.Format("{0}\\ImageCapture.png", AppDomain.CurrentDomain.BaseDirectory);
                Bitmap img = new Bitmap(fileName);
                MemoryStream mem = new MemoryStream();
                img.Save(mem, System.Drawing.Imaging.ImageFormat.Png);
                Bitmap b = new Bitmap(mem);
                OnCaptureCompletedEvent(b);
                img.Dispose();
                img = null;
            }
        }
        #endregion
    }
}
