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
using System.Xml.Serialization;

namespace MM.Common
{
    public class PortConfig
    {
        #region Members
        private string _id = string.Empty;
        private string _tenMayXetNghiem = string.Empty;
        private string _portName = string.Empty;
        private LoaiMayXN _loaiMay = LoaiMayXN.Hitachi917;
        #endregion

        #region Contructor
        public PortConfig()
        {

        }
        #endregion

        #region Properties
        [XmlElement("Id", typeof(string))]
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        [XmlElement("TenMayXetNghiem", typeof(string))]
        public string TenMayXetNghiem
        {
            get { return _tenMayXetNghiem; }
            set { _tenMayXetNghiem = value; }
        }

        [XmlElement("PortName", typeof(string))]
        public string PortName
        {
            get { return _portName; }
            set { _portName = value; }
        }

        [XmlElement("LoaiMay", typeof(LoaiMayXN))]
        public LoaiMayXN LoaiMay
        {
            get { return _loaiMay; }
            set { _loaiMay = value; }
        }
        #endregion
    }

    public class PortConfigCollection
    {
        #region Members
        private List<PortConfig> _portConfigList = new List<PortConfig>();
        #endregion

        #region Constructor
        public PortConfigCollection()
        {

        }
        #endregion

        #region Properties
        [XmlElement("PortConfigList", typeof(PortConfig))]
        public List<PortConfig> PortConfigList
        {
            get { return _portConfigList; }
            set { _portConfigList = value; }
        }
        #endregion

        #region Methods
        public void Clear()
        {
            _portConfigList.Clear();
        }

        public void Add(PortConfig portConfig)
        {
            _portConfigList.Add(portConfig);
        }

        public void Remove(PortConfig portConfig)
        {
            foreach (PortConfig p in _portConfigList)
            {
                if (p.Id == portConfig.Id)
                {
                    _portConfigList.Remove(p);
                    break;
                }
            }
        }

        public void Remove(string id)
        {
            foreach (PortConfig p in _portConfigList)
            {
                if (p.Id == id)
                {
                    _portConfigList.Remove(p);
                    break;
                }
            }
        }

        public PortConfig GetPortConfigById(string id)
        {
            foreach (PortConfig p in _portConfigList)
            {
                if (p.Id == id) return p;
            }

            return null;
        }

        public PortConfig GetPortConfigByPortName(string portName)
        {
            foreach (PortConfig p in _portConfigList)
            {
                if (p.PortName.ToLower() == portName.ToLower())
                    return p;
            }

            return null;
        }

        public bool CheckTenMayXetNghiemTonTai(string tenMayXetNghiem, string id)
        {
            if (id == string.Empty)
            {
                foreach (PortConfig p in _portConfigList)
                {
                    if (p.TenMayXetNghiem.ToLower() == tenMayXetNghiem.ToLower())
                        return true;
                }
            }
            else
            {
                foreach (PortConfig p in _portConfigList)
                {
                    if (p.TenMayXetNghiem.ToLower() == tenMayXetNghiem.ToLower() && p.Id != id)
                        return true;
                }
            }

            return false;
        }

        public bool CheckPortNameTonTai(string portName, string id)
        {
            if (id == string.Empty)
            {
                foreach (PortConfig p in _portConfigList)
                {
                    if (p.PortName.ToLower() == portName.ToLower())
                        return true;
                }
            }
            else
            {
                foreach (PortConfig p in _portConfigList)
                {
                    if (p.PortName.ToLower() == portName.ToLower() && p.Id != id)
                        return true;
                }
            }

            return false;
        }
        #endregion

        #region Serialize & Deserialize
        public bool Serialize(string fileName)
        {
            XmlSerializer s = null;
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(fileName);
                s = new XmlSerializer(typeof(PortConfigCollection));
                s.Serialize(sw, this);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                    sw = null;
                }

                if (s != null)
                    s = null;
            }
        }

        public bool Deserialize(string fileName)
        {
            XmlSerializer s = null;
            StreamReader sr = null;

            try
            {
                s = new XmlSerializer(typeof(PortConfigCollection));
                sr = new StreamReader(fileName);
                PortConfigCollection portConfigCollection = (PortConfigCollection)s.Deserialize(sr);
                _portConfigList = portConfigCollection.PortConfigList;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                    sr = null;
                }

                if (s != null)
                    s = null;
            }
        }
        #endregion
    }
}
