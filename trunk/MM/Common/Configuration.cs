using System;
using System.Globalization;
using System.IO;
using System.Collections;

/*
 * HOW TO USE
			+ Load data : 
				CommonData.Configuration.LoadData(@"D:\abcd.cfig");
			+ Set data
				CommonData.Configuration.SetValues("a1",123);
				CommonData.Configuration.SetValues("a2","aaaa");
				or
				CommonData.Configuration aa=new CommonData.Configuration();
				aa["a1"]=123;
				
			+ Get data
				x=CommonData.Configuration.GetValues("a1");
				x=CommonData.Configuration.GetValues("a2");
				or 
				CommonData.Configuration aa=new CommonData.Configuration();
				x=aa["a1"];
			
			+ Save data
				CommonData.Configuration.SaveData(@"D:\abcd.cfig");
			
*/

namespace MM.Common
{
	public class Configuration
    {
        #region Members
        public static SortedList _data = new SortedList();
        public static string _version = "Inner Data 1.0.0";
        #endregion

        #region Properties
        public Object this[string key]
        {
            get { return GetValues(key); }
            set { SetValues(key, value); }
        }

        public static CultureInfo DefaultCulture
        {
            get { return new CultureInfo("en-US", true); }
        }
        #endregion

        #region Static Methods
        public static bool LoadData(string filepath)
        {
            if (!File.Exists(filepath))
            {
                //Load into PhysicalAngle varible
                return false;
            }

            FileStream fs = null;
            BinaryReader br = null;
            try
            {
                fs = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                br = new BinaryReader(fs);

                //Read version 
                _version = br.ReadString();

                //Check version
                //if (_version == "Inner Data 1.0.0")
                {
                    //Get number of key
                    int maxkey = br.ReadInt32();

                    _data.Clear();

                    for (int i = 0; i < maxkey; i++)
                    {
                        //read key
                        string skey = br.ReadString();

                        if (skey == null || skey.Trim() == "") continue;

                        //read type
                        string stype = br.ReadString();

                        //read data
                        Type type = Type.GetType(stype);
                        Object values = null;

                        if (type == typeof(String))
                        {
                            values = br.ReadString();
                        }
                        else if (type == typeof(bool))
                        {
                            values = br.ReadBoolean();
                        }
                        else if (type == typeof(short))
                        {
                            values = br.ReadInt16();
                        }
                        else if (type == typeof(int))
                        {
                            values = br.ReadInt32();
                        }
                        else if (type == typeof(long))
                        {
                            values = br.ReadInt64();
                        }
                        else if (type == typeof(float))
                        {
                            values = br.ReadSingle();
                        }
                        else if (type == typeof(double))
                        {
                            values = br.ReadDouble();
                        }
                        else if (type == typeof(Decimal))
                        {
                            values = br.ReadDecimal();
                        }
                        else
                            continue;
                        //....
                        _data.Add(skey, values);

                        skey = null;
                        stype = null;
                    }
                }
                //Load into PhysicalAngle varible
                return true;

            }
            catch (Exception ex)
            {
                Utility.WriteToTraceLog(string.Format("Load config: {0}"), ex.Message);
                return false;
            }
            finally
            {
                if (br != null)
                {
                    br.Close();
                    br = null;
                }

                if (fs != null)
                {
                    fs.Close();
                    fs = null;
                }
            }
        }

        public static bool SaveData(string filepath)
        {
            if (_data == null) return false;

            //pre save
            try
            {
                if (File.Exists(filepath))
                {
                    File.SetAttributes(filepath, FileAttributes.Normal);
                    File.Delete(filepath);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message != null ? ex.Message : "Access denied.";
                Utility.WriteToTraceLog(string.Format("Save config: {0}"), errorMessage);
                return false;
            }

            FileStream fs = null;
            BinaryWriter bw = null;
            try
            {
                fs = new FileStream(filepath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                bw = new BinaryWriter(fs);

                //Write version
                bw.Write(_version);

                //Write number of key
                int maxkey = _data.Count;
                bw.Write(maxkey);

                //Write data
                for (int i = 0; i < maxkey; i++)
                {
                    //get key
                    string skey = _data.GetKey(i).ToString();

                    if (skey == null || skey.Trim() == "") continue;

                    //get values
                    if (_data.ContainsKey(skey) == false) continue;

                    Object values = _data[skey];

                    //write key
                    bw.Write(skey);

                    //write type
                    Type type = values.GetType();
                    bw.Write(type.ToString());

                    if (type == typeof(String))
                    {
                        bw.Write((String)values);
                    }
                    else if (type == typeof(bool))
                    {
                        bw.Write((bool)values);
                    }
                    else if (type == typeof(short))
                    {
                        bw.Write((short)values);
                    }
                    else if (type == typeof(int))
                    {
                        bw.Write((int)values);
                    }
                    else if (type == typeof(long))
                    {
                        bw.Write((long)values);
                    }
                    else if (type == typeof(float))
                    {
                        bw.Write((float)values);
                    }
                    else if (type == typeof(double))
                    {
                        bw.Write((double)values);
                    }
                    else if (type == typeof(Decimal))
                    {
                        bw.Write((Decimal)values);
                    }
                    else
                        continue;

                    skey = null;
                }


                return true;
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message != null ? ex.Message : "Access denied.";
                Utility.WriteToTraceLog(string.Format("Save config: {0}"), errorMessage);
                return false;
            }
            finally
            {
                if (bw != null)
                {
                    bw.Close();
                    bw = null;
                }

                if (fs != null)
                {
                    fs.Close();
                    fs = null;
                }
            }
        }

        public static Object GetValues(string key)
        {
            if (_data.ContainsKey(key))
                return _data[key];
            else
                return null;
        }

        public static bool ContainsKey(string key)
        {
            return _data.ContainsKey(key);
        }

        public static Object GetValues(string key, Object defaultValue)
        {
            Object val = GetValues(key);
            if (val == null)
                val = defaultValue;
            return val;
        }

        public static void SetValues(string key, Object values)
        {
            if (_data.ContainsKey(key))
                _data[key] = values;
            else
                _data.Add(key, values);
        }
        #endregion
	}
}
