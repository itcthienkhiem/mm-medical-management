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
