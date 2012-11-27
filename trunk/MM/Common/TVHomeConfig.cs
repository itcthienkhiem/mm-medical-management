using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MM.Common
{
    public class TVHomeConfig
    {
        #region Members
        private string _path = string.Empty;
        private bool _suDungSoiCTC = false;
        private bool _suDungSieuAm = false;
        #endregion

        #region Constructor
        public TVHomeConfig()
        {

        }
        #endregion

        #region Properties
        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }

        public bool SuDungSoiCTC
        {
            get { return _suDungSoiCTC; }
            set { _suDungSoiCTC = value; }
        }

        public bool SuDungSieuAm
        {
            get { return _suDungSieuAm; }
            set { _suDungSieuAm = value; }
        }
        #endregion
    }
}
