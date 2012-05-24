using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.IO;
using System.IO.Ports;
using MM.Common;
using MM.Databasae;
using MM.Bussiness;

namespace MMService
{
    public partial class MMServices : ServiceBase
    {
        #region Members
        private List<SerialPort> _ports = new List<SerialPort>();
        private Hashtable _htLastResult = new Hashtable();
        #endregion

        #region Constructor
        public MMServices()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        protected override void OnStart(string[] args)
        {
            LoadConfig();
            OpenCOMPort();
        }

        protected override void OnStop()
        {
            CloseAllCOMPort();
        }

        private SerialPort GetPort(string portName)
        {
            foreach (SerialPort p in _ports)
            {
                if (p.PortName == portName)
                    return p;
            }

            return null;
        }

        private void CloseAllCOMPort()
        {
            foreach (SerialPort p in _ports)
            {
                p.DataReceived -= new System.IO.Ports.SerialDataReceivedEventHandler(port_DataReceived);
                p.ErrorReceived -= new System.IO.Ports.SerialErrorReceivedEventHandler(port_ErrorReceived);
                p.Close();
            }
        }

        private void OpenCOMPort()
        {
            if (File.Exists(Global.PortConfigPath))
            {
                Global.PortConfigCollection.Deserialize(Global.PortConfigPath);

                List<SerialPort> removePorts = new List<SerialPort>();
                foreach (SerialPort p in _ports)
                {
                    if (!Global.PortConfigCollection.CheckPortNameTonTai(p.PortName, string.Empty))
                    {
                        try
                        {
                            p.DataReceived -= new System.IO.Ports.SerialDataReceivedEventHandler(port_DataReceived);
                            p.ErrorReceived -= new System.IO.Ports.SerialErrorReceivedEventHandler(port_ErrorReceived);
                            p.Close();
                            removePorts.Add(p);
                        }
                        catch (Exception ex)
                        {
                            Utility.WriteToTraceLog(ex.Message);
                        }
                    }
                }

                foreach (SerialPort p in removePorts)
                {
                    _ports.Remove(p);
                }

                foreach (PortConfig p in Global.PortConfigCollection.PortConfigList)
                {
                    try
                    {
                        SerialPort port = GetPort(p.PortName);
                        if (port == null)
                        {
                            port = new SerialPort();
                            port.BaudRate = 9600;
                            port.DataBits = 8;
                            port.DiscardNull = false;
                            port.DtrEnable = true;
                            port.Handshake = Handshake.XOnXOff;
                            port.Parity = Parity.None;
                            port.ParityReplace = 63;
                            port.PortName = p.PortName;
                            port.ReadBufferSize = 4096;
                            port.ReadTimeout = -1;
                            port.ReceivedBytesThreshold = 1;
                            port.RtsEnable = true;
                            port.StopBits = StopBits.One;
                            port.WriteBufferSize = 2048;
                            port.WriteTimeout = -1;
                            port.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(port_DataReceived);
                            port.ErrorReceived += new System.IO.Ports.SerialErrorReceivedEventHandler(port_ErrorReceived);
                            port.Open();
                            _ports.Add(port);
                        }
                    }
                    catch (Exception ex)
                    {
                        Utility.WriteToTraceLog(ex.Message);
                    }
                }
            }
        }

        private void LoadConfig()
        {
            Configuration.LoadData(Global.AppConfig);

            object obj = Configuration.GetValues(Const.ServerNameKey);
            if (obj != null) Global.ConnectionInfo.ServerName = Convert.ToString(obj);

            obj = Configuration.GetValues(Const.DatabaseNameKey);
            if (obj != null) Global.ConnectionInfo.DatabaseName = Convert.ToString(obj);

            obj = Configuration.GetValues(Const.AuthenticationKey);
            if (obj != null) Global.ConnectionInfo.Authentication = Convert.ToString(obj);

            obj = Configuration.GetValues(Const.UserNameKey);
            if (obj != null) Global.ConnectionInfo.UserName = Convert.ToString(obj);

            obj = Configuration.GetValues(Const.PasswordKey);
            if (obj != null)
            {
                string password = Convert.ToString(obj);
                RijndaelCrypto crypto = new RijndaelCrypto();
                Global.ConnectionInfo.Password = crypto.Decrypt(password);
            }
        }

        private void port_ErrorReceived(object sender, System.IO.Ports.SerialErrorReceivedEventArgs e)
        {
            Utility.WriteToTraceLog(string.Format("SerialPort: Error received data '{0}'", e.EventType.ToString()));
        }

        private void port_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                SerialPort port = (SerialPort)sender;
                PortConfig portConfig = Global.PortConfigCollection.GetPortConfigByPortName(port.PortName);
                if (portConfig == null) return;

                string data = port.ReadExisting();

                if (portConfig.LoaiMay == LoaiMayXN.Hitachi917)
                {
                    List<TestResult_Hitachi917> testResults = ParseTestResult_Hitachi917(data, port.PortName);
                    Result result = XetNghiem_Hitachi917Bus.InsertKQXN(testResults);
                    if (!result.IsOK)
                        Utility.WriteToTraceLog(result.GetErrorAsString("XetNghiem_Hitachi917Bus.InsertKQXN"));
                }
                else if (portConfig.LoaiMay == LoaiMayXN.CellDyn3200)
                {
                    List<TestResult_CellDyn3200> testResults = ParseTestResult_CellDyn3200(data, port.PortName);
                    Result result = XetNghiem_CellDyn3200Bus.InsertKQXN(testResults);
                    if (!result.IsOK)
                        Utility.WriteToTraceLog(result.GetErrorAsString("XetNghiem_CellDyn3200Bus.InsertKQXN"));
                }
            }
            catch (Exception ex)
            {
                Utility.WriteToTraceLog(string.Format("Serial Port: {0}", ex.Message));
            }
        }

        private List<TestResult_Hitachi917> ParseTestResult_Hitachi917(string result, string portName)
        {
            //result = "217:n1    1    1  11DINH ALCAM      3 0319121056XN      9  3  62.2  10   4.8  13  75.7  16  18.2  17   4.1  18  25.8  19  18.0  34   2.9  36   5.2 A1\r218:n1    2    1  21LOC ACE         3 0319121058XN      6 10   6.1  16  27.2  17   5.2  18  18.6  19  17.8  21   1.3 34\r";

            //while (result[0] == '\x02')
            //{
            //    result = result.Substring(1, result.Length - 1);
            //}

            //result = '\x02' + result;

            string[] strArr = result.Split('\r');

            List<TestResult_Hitachi917> testResults = new List<TestResult_Hitachi917>();
            if (strArr != null && strArr.Length > 0)
            {
                string lastResult = string.Empty;
                if (_htLastResult.ContainsKey(portName))
                    lastResult = _htLastResult[portName].ToString();
                else
                    _htLastResult.Add(portName, string.Empty);

                strArr[0] = string.Format("{0}{1}", lastResult, strArr[0]);
                _htLastResult[portName] = strArr[strArr.Length - 1];

                for (int i = 0; i < strArr.Length - 1; i++)
                {
                    result = strArr[i];
                    int istart = result.IndexOf('\x02');
                    int iEnd = result.IndexOf('\x03');
                    if (istart != 0) continue;
                    if (istart < 0 || iEnd < 0) continue;

                    TestResult_Hitachi917 testResult = new TestResult_Hitachi917();
                    testResult.STX = result[0];
                    testResult.Receiver = result[1];
                    testResult.Sender = result[2];
                    testResult.PakageNum = result[3];
                    testResult.Frame = result[4];
                    testResult.FunctionCode = result[5];
                    testResult.SampleClass = result[6];
                    testResult.SampleNum = result.Substring(7, 5);
                    testResult.DiskNum = result.Substring(12, 5);
                    testResult.PositionNum = result.Substring(17, 3);
                    testResult.SampleCup = result.Substring(20, 1);
                    testResult.IDNum = result.Substring(21, 13);
                    testResult.Age = result.Substring(34, 3);
                    testResult.AgeUnit = result.Substring(37, 1);
                    testResult.Sex = result.Substring(38, 1);
                    testResult.CollectionDate = result.Substring(39, 6);
                    testResult.CollectionTime = result.Substring(45, 4);
                    testResult.OperatorID = result.Substring(49, 6);
                    int resultCount = Convert.ToInt32(result.Substring(55, 3));

                    for (int j = 0; j < resultCount; j++)
                    {
                        Result_Hitachi917 r = new Result_Hitachi917();
                        r.TestNum = Convert.ToInt32(result.Substring((58 + j * 10), 3));
                        r.Result = result.Substring(61 + j * 10, 6);
                        r.AlarmCode = result.Substring(67 + j * 10, 1);
                        testResult.Results.Add(r);
                    }

                    testResults.Add(testResult);
                }
            }

            return testResults;
        }

        private List<TestResult_CellDyn3200> ParseTestResult_CellDyn3200(string result, string portName)
        {
            List<TestResult_CellDyn3200> testResults = new List<TestResult_CellDyn3200>();
            //result = "\"   \",\"CD3200C\",\"------------\",3280,0,0,\"AVER124     \",\"BUI THI NGHIA TD            \",\"----------------\",\"F\",\"--/--/----\",\"----------------------\",\".  \",\"04/14/2012\",\"17:38\",\"--/--/----\",\"--:--\",\"----------------\",06.34,04.12,01.59,0.307,0.218,0.096,04.36,012.0,038.0,087.0,027.5,031.6,012.7,00254,06.53,0.166,016.7,065.1,025.1,04.85,03.45,01.51,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,00000,00000,00000,00000,00000,00000,00000,00000,-----,06.34,1,0,0,0,0,0,0,70\"   \",\"CD3200C\",\"------------\",3280,0,0,\"AVER124     \",\"BUI THI NGHIA TD            \",\"----------------\",\"F\",\"--/--/----\",\"----------------------\",\".  \",\"04/14/2012\",\"17:38\",\"--/--/----\",\"--:--\",\"----------------\",06.34,04.12,01.59,0.307,0.218,0.096,04.36,012.0,038.0,087.0,027.5,031.6,012.7,00254,06.53,0.166,016.7,065.1,025.1,04.85,03.45,01.51,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,00000,00000,00000,00000,00000,00000,00000,00000,-----,06.34,1,0,0,0,0,0,0,70";
            result = result.Replace("\"", "");

            string[] strArr = result.Split("".ToCharArray());

            if (strArr != null && strArr.Length > 0)
            {
                string lastResult = string.Empty;
                if (_htLastResult.ContainsKey(portName))
                    lastResult = _htLastResult[portName].ToString();
                else
                    _htLastResult.Add(portName, string.Empty);

                strArr[0] = string.Format("{0}{1}", lastResult, strArr[0]);
                _htLastResult[portName] = strArr[strArr.Length - 1];

                for (int i = 0; i < strArr.Length - 1; i++)
                {
                    TestResult_CellDyn3200 testResult = new TestResult_CellDyn3200();
                    result = strArr[i];
                    string[] arrResult = result.Split(",".ToCharArray(), StringSplitOptions.None);
                    if (arrResult == null || arrResult.Count() == 0) continue;

                    testResult.KetQuaXetNghiem.MessageType = arrResult[0].Trim().Substring(1);
                    testResult.KetQuaXetNghiem.InstrumentType = arrResult[1].Trim();
                    testResult.KetQuaXetNghiem.SerialNum = arrResult[2].Trim();
                    testResult.KetQuaXetNghiem.SequenceNum = Convert.ToInt32(arrResult[3].Trim());
                    testResult.KetQuaXetNghiem.SpareField = arrResult[4].Trim();
                    testResult.KetQuaXetNghiem.SpecimenType = Convert.ToInt32(arrResult[5].Trim());
                    if (testResult.KetQuaXetNghiem.SpecimenType != 0) continue;

                    testResult.KetQuaXetNghiem.SpecimenID = arrResult[6].Trim();
                    testResult.KetQuaXetNghiem.SpecimenName = arrResult[7].Trim();
                    testResult.KetQuaXetNghiem.PatientID = arrResult[8].Trim();
                    testResult.KetQuaXetNghiem.SpecimenSex = arrResult[9].Trim();
                    if (testResult.KetQuaXetNghiem.SpecimenSex == "-") testResult.KetQuaXetNghiem.SpecimenSex = string.Empty;

                    testResult.KetQuaXetNghiem.SpecimenDOB = arrResult[10].Trim();
                    if (testResult.KetQuaXetNghiem.SpecimenDOB == "--/--/----") testResult.KetQuaXetNghiem.SpecimenDOB = string.Empty;

                    testResult.KetQuaXetNghiem.DrName = arrResult[11].Trim();
                    testResult.KetQuaXetNghiem.OperatorID = arrResult[12].Trim();

                    testResult.KetQuaXetNghiem.NgayXN = DateTime.ParseExact(string.Format("{0} {1}", arrResult[13].Trim(), arrResult[14].Trim()),
                            "MM/dd/yyyy HH:mm", null);

                    testResult.KetQuaXetNghiem.CollectionDate = arrResult[15].Trim();
                    testResult.KetQuaXetNghiem.CollectionTime = arrResult[16].Trim();
                    testResult.KetQuaXetNghiem.Comment = arrResult[17].Trim();

                    //WBC
                    ChiTietKetQuaXetNghiem_CellDyn3200 ctkqxn = new ChiTietKetQuaXetNghiem_CellDyn3200();
                    ctkqxn.TenXetNghiem = "WBC";
                    ctkqxn.TestResult = Convert.ToDouble(arrResult[18].Trim());
                    testResult.ChiTietKetQuaXetNghiem.Add(ctkqxn);

                    //NEU
                    ctkqxn = new ChiTietKetQuaXetNghiem_CellDyn3200();
                    ctkqxn.TenXetNghiem = "NEU";
                    ctkqxn.TestResult = Convert.ToDouble(arrResult[19].Trim());
                    ctkqxn.TestPercent = Convert.ToDouble(arrResult[35].Trim());
                    testResult.ChiTietKetQuaXetNghiem.Add(ctkqxn);

                    //LYM
                    ctkqxn = new ChiTietKetQuaXetNghiem_CellDyn3200();
                    ctkqxn.TenXetNghiem = "LYM";
                    ctkqxn.TestResult = Convert.ToDouble(arrResult[20].Trim());
                    ctkqxn.TestPercent = Convert.ToDouble(arrResult[36].Trim());
                    testResult.ChiTietKetQuaXetNghiem.Add(ctkqxn);

                    //MONO
                    ctkqxn = new ChiTietKetQuaXetNghiem_CellDyn3200();
                    ctkqxn.TenXetNghiem = "MONO";
                    ctkqxn.TestResult = Convert.ToDouble(arrResult[21].Trim());
                    ctkqxn.TestPercent = Convert.ToDouble(arrResult[37].Trim());
                    testResult.ChiTietKetQuaXetNghiem.Add(ctkqxn);

                    //EOS
                    ctkqxn = new ChiTietKetQuaXetNghiem_CellDyn3200();
                    ctkqxn.TenXetNghiem = "EOS";
                    ctkqxn.TestResult = Convert.ToDouble(arrResult[22].Trim());
                    ctkqxn.TestPercent = Convert.ToDouble(arrResult[38].Trim());
                    testResult.ChiTietKetQuaXetNghiem.Add(ctkqxn);

                    //BASO
                    ctkqxn = new ChiTietKetQuaXetNghiem_CellDyn3200();
                    ctkqxn.TenXetNghiem = "BASO";
                    ctkqxn.TestResult = Convert.ToDouble(arrResult[23].Trim());
                    ctkqxn.TestPercent = Convert.ToDouble(arrResult[39].Trim());
                    testResult.ChiTietKetQuaXetNghiem.Add(ctkqxn);

                    //RBC
                    ctkqxn = new ChiTietKetQuaXetNghiem_CellDyn3200();
                    ctkqxn.TenXetNghiem = "RBC";
                    ctkqxn.TestResult = Convert.ToDouble(arrResult[24].Trim());
                    testResult.ChiTietKetQuaXetNghiem.Add(ctkqxn);

                    //HGB
                    ctkqxn = new ChiTietKetQuaXetNghiem_CellDyn3200();
                    ctkqxn.TenXetNghiem = "HGB";
                    ctkqxn.TestResult = Convert.ToDouble(arrResult[25].Trim());
                    testResult.ChiTietKetQuaXetNghiem.Add(ctkqxn);

                    //HCT
                    ctkqxn = new ChiTietKetQuaXetNghiem_CellDyn3200();
                    ctkqxn.TenXetNghiem = "HCT";
                    ctkqxn.TestResult = Convert.ToDouble(arrResult[26].Trim());
                    testResult.ChiTietKetQuaXetNghiem.Add(ctkqxn);

                    //MCV
                    ctkqxn = new ChiTietKetQuaXetNghiem_CellDyn3200();
                    ctkqxn.TenXetNghiem = "MCV";
                    ctkqxn.TestResult = Convert.ToDouble(arrResult[27].Trim());
                    testResult.ChiTietKetQuaXetNghiem.Add(ctkqxn);

                    //MCH
                    ctkqxn = new ChiTietKetQuaXetNghiem_CellDyn3200();
                    ctkqxn.TenXetNghiem = "MCH";
                    ctkqxn.TestResult = Convert.ToDouble(arrResult[28].Trim());
                    testResult.ChiTietKetQuaXetNghiem.Add(ctkqxn);

                    //MCHC
                    ctkqxn = new ChiTietKetQuaXetNghiem_CellDyn3200();
                    ctkqxn.TenXetNghiem = "MCHC";
                    ctkqxn.TestResult = Convert.ToDouble(arrResult[29].Trim());
                    testResult.ChiTietKetQuaXetNghiem.Add(ctkqxn);

                    //RDW
                    ctkqxn = new ChiTietKetQuaXetNghiem_CellDyn3200();
                    ctkqxn.TenXetNghiem = "RDW";
                    ctkqxn.TestResult = Convert.ToDouble(arrResult[30].Trim());
                    testResult.ChiTietKetQuaXetNghiem.Add(ctkqxn);

                    //PLT
                    ctkqxn = new ChiTietKetQuaXetNghiem_CellDyn3200();
                    ctkqxn.TenXetNghiem = "PLT";
                    ctkqxn.TestResult = Convert.ToDouble(arrResult[31].Trim());
                    testResult.ChiTietKetQuaXetNghiem.Add(ctkqxn);

                    //MPV
                    ctkqxn = new ChiTietKetQuaXetNghiem_CellDyn3200();
                    ctkqxn.TenXetNghiem = "MPV";
                    ctkqxn.TestResult = Convert.ToDouble(arrResult[32].Trim());
                    testResult.ChiTietKetQuaXetNghiem.Add(ctkqxn);

                    //PCT
                    ctkqxn = new ChiTietKetQuaXetNghiem_CellDyn3200();
                    ctkqxn.TenXetNghiem = "PCT";
                    ctkqxn.TestResult = Convert.ToDouble(arrResult[33].Trim());
                    testResult.ChiTietKetQuaXetNghiem.Add(ctkqxn);

                    //PDW
                    ctkqxn = new ChiTietKetQuaXetNghiem_CellDyn3200();
                    ctkqxn.TenXetNghiem = "PDW";
                    ctkqxn.TestResult = Convert.ToDouble(arrResult[34].Trim());
                    testResult.ChiTietKetQuaXetNghiem.Add(ctkqxn);

                    testResults.Add(testResult);
                }
            }

            return testResults;
        }
        #endregion
    }
}
