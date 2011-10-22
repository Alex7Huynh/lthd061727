using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace GoogleMapApp
{
    public class DiaDiemDAO
    {
        public static bool ThemDiaDiem(DiaDiemDTO diaDiem)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Util.FileName);
            XmlNodeList list = doc.SelectNodes("//NGUOIDUNG[@username='" + diaDiem.DanhMuc.NguoiDung.Username + "']//DANHMUC[@tendanhmuc='" 
                + diaDiem.DanhMuc.TenDanhMuc + "']");
            if (list.Count == 0)
                return false;

            XmlElement child = doc.CreateElement("DIADIEM");
            child.SetAttribute("tendiadiem", diaDiem.TenDiaDiem);
            child.SetAttribute("diachi", diaDiem.DiaChi);
            child.SetAttribute("vido", diaDiem.ViDo.ToString());
            child.SetAttribute("kinhdo", diaDiem.KinhDo.ToString());
            child.SetAttribute("deleted", "false");
            child.SetAttribute("ghichu", diaDiem.GhiChu);
            list[0].AppendChild(child);
            doc.Save(Util.FileName);

            return true;
        }
        public static bool XoaDiaDiem(DiaDiemDTO diaDiem)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Util.FileName);
            XmlNodeList list = doc.SelectNodes("//NGUOIDUNG[@username='" + diaDiem.DanhMuc.NguoiDung.Username + "']");
            if (list.Count == 0)
                return false;

            foreach (XmlNode node in list[0].ChildNodes)
            {
                foreach (XmlNode n in node.ChildNodes)
                    if (n.Attributes["tendiadiem"].Value == diaDiem.TenDiaDiem)
                        n.ParentNode.RemoveChild(n);
            }
            
            doc.Save(Util.FileName);

            return true;
        }
        public static bool CapNhatDiaDiem(DiaDiemDTO diaDiem)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Util.FileName);
            XmlNodeList list = doc.SelectNodes("//NGUOIDUNG[@username='" + diaDiem.DanhMuc.NguoiDung.Username + "']//DANHMUC[@tendanhmuc='"
                + diaDiem.DanhMuc.TenDanhMuc + "']//DIADIEM[@tendiadiem='" + diaDiem.TenDiaDiem + "']");
            if (list.Count == 0)
                return false;

            list[0].Attributes["ghichu"].Value = diaDiem.GhiChu;
            doc.Save(Util.FileName);

            return true;
        }
        //public static bool XoaDiaDiem(DiaDiemDTO diaDiem)
        //{
        //    XmlDocument doc = new XmlDocument();
        //    doc.Load(Util.FileName);
        //    XmlNodeList list = doc.SelectNodes("//NGUOIDUNG[@username='" + diaDiem.DanhMuc.NguoiDung.Username + "']//DANHMUC[@tendanhmuc='"
        //        + diaDiem.DanhMuc.TenDanhMuc + "']//DIADIEM[@tendiadiem='" + diaDiem.TenDiaDiem + "']");

        //    if (list.Count == 0)
        //        return false;

        //    list[0].ParentNode.RemoveChild(list[0]);
        //    doc.Save(Util.FileName);

        //    return true;
        //}
    }
}