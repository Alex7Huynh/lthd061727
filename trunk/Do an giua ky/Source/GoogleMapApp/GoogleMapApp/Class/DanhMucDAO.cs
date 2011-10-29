using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace GoogleMapApp
{
    public class DanhMucDAO
    {
        public static bool ThemDanhMuc(DanhMucDTO danhMuc)
        {            
            XmlDocument doc = new XmlDocument();
            doc.Load(Util.FileName);
            XmlNodeList list = doc.SelectNodes("//NGUOIDUNG[@username='" + danhMuc.NguoiDung.Username + "']");
            if (list.Count == 0)
                return false;
  
            XmlElement child = doc.CreateElement("DANHMUC");
            child.SetAttribute("madanhmuc", danhMuc.MaDanhMuc.ToString());
            child.SetAttribute("tendanhmuc", danhMuc.TenDanhMuc);
            list[0].AppendChild(child);
            doc.Save(Util.FileName);

            return true;
        }
        public static bool XoaDanhMuc(DanhMucDTO danhMuc)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Util.FileName);
            XmlNodeList list = doc.SelectNodes("//NGUOIDUNG[@username='" + danhMuc.NguoiDung.Username + "']//DANHMUC[@tendanhmuc='" + danhMuc.TenDanhMuc + "']");
            if (list.Count == 0)
                return false;

            list[0].ParentNode.RemoveChild(list[0]);
            doc.Save(Util.FileName);

            return true;
        }
        public static DanhMucDTO TimDanhMuc(string tenDanhMuc, NguoiDungDTO nguoiDung)
        {
            DanhMucDTO danhMuc = null;
            XmlDocument doc = new XmlDocument();
            doc.Load(Util.FileName);
            XmlNodeList list = doc.SelectNodes("//NGUOIDUNG[@username='" + nguoiDung.Username + "']//DANHMUC[@tendanhmuc='" + tenDanhMuc + "']");
            if (list.Count == 0)
                return danhMuc;
            danhMuc = new DanhMucDTO();
            danhMuc.NguoiDung = new NguoiDungDTO();
            danhMuc.NguoiDung = nguoiDung;
            danhMuc.TenDanhMuc = tenDanhMuc;

            return danhMuc;
        }
    }
}