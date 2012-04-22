using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MM.Common
{
    public class Result_Hitachi917
    {
        #region Members
        private int _testNum;
        private string _result;
        private string _alarmCode;
        #endregion

        #region Constructor
        public Result_Hitachi917()
        {

        }
        #endregion

        #region Properties
        public int TestNum
        {
            get { return _testNum; }
            set { _testNum = value; }
        }

        public string Result
        {
            get { return _result; }
            set { _result = value; }
        }

        public string AlarmCode
        {
            get { return _alarmCode; }
            set { _alarmCode = value; }
        }
        #endregion
    }

    public class TestResult_Hitachi917
    {
        #region Members
        private char _stx;
        private char _receiver;
        private char _sender;
        private char _packageNum;
        private char _frame;
        private char _functionCode;
        private char _sampleClass;
        private string _sampleNum;
        private string _diskNum;
        private string _positionNum;
        private string _sampleCup;
        private string _idNum;
        private string _age;
        private string _ageUnit;
        private string _sex;
        private string _collectionDate;
        private string _collectionTime;
        private string _operatorID;
        private List<Result_Hitachi917> _results = new List<Result_Hitachi917>();
        #endregion

        #region Constructor
        public TestResult_Hitachi917()
        {
        }
        #endregion

        #region Properties
        public char STX
        {
            get { return _stx; }
            set { _stx = value; }
        }

        public char Receiver
        {
            get { return _receiver; }
            set { _receiver = value; }
        }

        public char Sender
        {
            get { return _sender; }
            set { _sender = value; }
        }

        public char PakageNum
        {
            get { return _packageNum; }
            set { _packageNum = value; }
        }

        public char Frame
        {
            get { return _frame; }
            set { _frame = value; }
        }

        public char FunctionCode
        {
            get { return _functionCode; }
            set { _functionCode = value; }
        }

        public char SampleClass
        {
            get { return _sampleClass; }
            set { _sampleClass = value; }
        }

        public string SampleNum
        {
            get { return _sampleNum; }
            set { _sampleNum = value; }
        }

        public string DiskNum
        {
            get { return _diskNum; }
            set { _diskNum = value; }
        }

        public string PositionNum
        {
            get { return _positionNum; }
            set { _positionNum = value; }
        }

        public string SampleCup
        {
            get { return _sampleCup; }
            set { _sampleCup = value; }
        }

        public string IDNum
        {
            get { return _idNum; }
            set { _idNum = value; }
        }

        public string Age
        {
            get { return _age; }
            set { _age = value; }
        }

        public string AgeUnit
        {
            get { return _ageUnit; }
            set { _ageUnit = value; }
        }

        public string Sex
        {
            get { return _sex; }
            set { _sex = value; }
        }

        public string CollectionDate
        {
            get { return _collectionDate; }
            set { _collectionDate = value; }
        }

        public string CollectionTime
        {
            get { return _collectionTime; }
            set { _collectionTime = value; }
        }

        public string OperatorID
        {
            get { return _operatorID; }
            set { _operatorID = value; }
        }

        public List<Result_Hitachi917> Results
        {
            get { return _results; }
            set { _results = value; }
        }
        #endregion        
    }
  }
