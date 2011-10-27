using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.IO;

namespace GoogleMapApp
{
    public class NguoiDungDAO
    {
        public static NguoiDungDTO DangNhap(string ten, string matKhau)
        {
            NguoiDungDTO nguoiDung = null;
            XmlDocument doc = new XmlDocument();
            doc.Load(Util.FileName);
            XmlElement root = doc.DocumentElement;
            foreach (XmlNode n in root.ChildNodes)
            {
                if (n.Attributes["username"].Value == ten && n.Attributes["password"].Value == matKhau)
                {
                    nguoiDung = new NguoiDungDTO(ten, matKhau);
                    return nguoiDung;
                }
            }
            
            return nguoiDung;
        }
        public static NguoiDungDTO DangKy(string ten, string matKhau)
        {
            NguoiDungDTO nguoiDung = null;
            XmlDocument doc = new XmlDocument();
            doc.Load(Util.FileName);
            XmlElement root = doc.DocumentElement;
            //Kiem tra trung ten
            foreach (XmlNode n in root.ChildNodes)
            {
                if (n.Attributes["username"].Value == ten)
                {
                    return nguoiDung;
                }
            }
            //Them nguoi dung
            XmlElement child = doc.CreateElement("NGUOIDUNG");
            child.SetAttribute("username", ten);
            child.SetAttribute("password", matKhau);            
            root.AppendChild(child);
            doc.Save(Util.FileName);

            nguoiDung = new NguoiDungDTO(ten, matKhau);
            return nguoiDung;
        }
    }
}