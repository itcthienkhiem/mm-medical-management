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
using System.IO;

namespace MM.Common
{
    #region Delegate Events
    public delegate void CreatedFileEventHandler(System.IO.FileSystemEventArgs e);
    #endregion

    public class WatchingFolder
    {
        #region Members
        private FileSystemWatcher _watchFolder = new FileSystemWatcher();
        public event CreatedFileEventHandler OnCreatedFileEvent = null;
        #endregion

        #region Constructor
        public WatchingFolder()
        {

        }
        #endregion

        #region Public Methods
        public void StartMoritoring(string path)
        {
            try
            {
                _watchFolder.Path = path;
                _watchFolder.NotifyFilter = System.IO.NotifyFilters.FileName | System.IO.NotifyFilters.Attributes;

                _watchFolder.Created += new FileSystemEventHandler(EventRaised);
                _watchFolder.EnableRaisingEvents = true;
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        public void StopMoritoring()
        {
            _watchFolder.EnableRaisingEvents = false;
        }

        private void EventRaised(object sender, System.IO.FileSystemEventArgs e)
        {
            switch (e.ChangeType)
            {
                case WatcherChangeTypes.Created:
                    if (OnCreatedFileEvent != null) 
                        OnCreatedFileEvent(e);
                    break;
            }
        }
        #endregion
    }
}
