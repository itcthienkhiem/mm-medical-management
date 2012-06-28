using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MM.Common
{
    public class PageSetup
    {
        #region Members
        private string _template = string.Empty;
        private double _leftMargin = 0;
        private double _topMargin = 0;
        private double _rightMargin = 0;
        private double _bottomMargin = 0;
        private double _headerMargin = 0;
        private double _footerMargin = 0;
        #endregion

        #region Contructor
        public PageSetup()
        {

        }
        #endregion

        #region Properties
        public string Template
        {
            get { return _template; }
            set { _template = value; }
        }

        public double LeftMargin
        {
            get { return _leftMargin; }
            set { _leftMargin = value; }
        }

        public double ToptMargin
        {
            get { return _topMargin; }
            set { _topMargin = value; }
        }

        public double RighttMargin
        {
            get { return _rightMargin; }
            set { _rightMargin = value; }
        }

        public double BottomMargin
        {
            get { return _bottomMargin; }
            set { _bottomMargin = value; }
        }

        public double HeaderMargin
        {
            get { return _headerMargin; }
            set { _headerMargin = value; }
        }

        public double FooterMargin
        {
            get { return _footerMargin; }
            set { _footerMargin = value; }
        }
        #endregion
    }
}
