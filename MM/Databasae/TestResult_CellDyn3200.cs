using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MM.Databasae
{
    public class TestResult_CellDyn3200
    {
        #region Members
        private KetQuaXetNghiem_CellDyn3200 _ketQuaXetNghiem = new KetQuaXetNghiem_CellDyn3200();
        private List<ChiTietKetQuaXetNghiem_CellDyn3200> _chiTietKetQuaXetNghiem = new List<ChiTietKetQuaXetNghiem_CellDyn3200>();
        #endregion

        #region Constructor
        public TestResult_CellDyn3200()
        {

        }
        #endregion

        #region Properties
        public KetQuaXetNghiem_CellDyn3200 KetQuaXetNghiem
        {
            get { return _ketQuaXetNghiem; }
            set { _ketQuaXetNghiem = value; }
        }

        public List<ChiTietKetQuaXetNghiem_CellDyn3200> ChiTietKetQuaXetNghiem
        {
            get { return _chiTietKetQuaXetNghiem; }
            set { _chiTietKetQuaXetNghiem = value; }
        }
        #endregion
    }
}
