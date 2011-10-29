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
            child.SetAttribute("madiadiem", diaDiem.MaDiaDiem.ToString());
            child.SetAttribute("tendiadiem", diaDiem.TenDiaDiem);
            child.SetAttribute("vido", diaDiem.ViDo.ToString());
            child.SetAttribute("kinhdo", diaDiem.KinhDo.ToString());
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
                    //if (diaDiem.TenDiaDiem.Contains(n.Attributes["tendiadiem"].Value))
                    if (diaDiem.MaDiaDiem.ToString() == n.Attributes["madiadiem"].Value)
                    {
                        n.ParentNode.RemoveChild(n);
                    }
            }

            doc.Save(Util.FileName);

            return true;
        }
        public static bool CapNhatDiaDiem(DiaDiemDTO diaDiem)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Util.FileName);
            XmlNodeList list = doc.SelectNodes("//NGUOIDUNG[@username='" + diaDiem.DanhMuc.NguoiDung.Username + "']");
            if (list.Count == 0)
                return false;

            foreach (XmlNode node in list[0].ChildNodes)
            {
                foreach (XmlNode n in node.ChildNodes)
                    //if (diaDiem.TenDiaDiem.Contains(n.Attributes["tendiadiem"].Value))
                    if (diaDiem.MaDiaDiem.ToString() == n.Attributes["madiadiem"].Value)
                    {
                        n.Attributes["tendiadiem"].Value = diaDiem.TenDiaDiem;
                        n.Attributes["vido"].Value = diaDiem.ViDo.ToString();
                        n.Attributes["kinhdo"].Value = diaDiem.KinhDo.ToString();
                        n.Attributes["ghichu"].Value = diaDiem.GhiChu;

                        doc.Save(Util.FileName);
                        return true;
                    }
            }

            return false;
        }

        public static DiaDiemDTO TimDiaDiem(DiaDiemDTO diaDiem)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Util.FileName);
            XmlNodeList list = doc.SelectNodes("//NGUOIDUNG[@username='" + diaDiem.DanhMuc.NguoiDung.Username + "']");
            if (list.Count == 0)
                return null;

            foreach (XmlNode node in list[0].ChildNodes)
            {
                foreach (XmlNode n in node.ChildNodes)
                    if (diaDiem.TenDiaDiem.Contains(n.Attributes["tendiadiem"].Value))
                    {
                        diaDiem = new DiaDiemDTO();
                        diaDiem.MaDiaDiem = int.Parse(n.Attributes["madiadiem"].Value);
                        diaDiem.TenDiaDiem = n.Attributes["tendiadiem"].Value;
                        diaDiem.ViDo = float.Parse(n.Attributes["vido"].Value);
                        diaDiem.KinhDo = float.Parse(n.Attributes["kinhdo"].Value);
                        diaDiem.GhiChu = n.Attributes["ghichu"].Value;

                        return diaDiem;
                    }
            }

            return null;
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
        //public static bool CapNhatDiaDiem(DiaDiemDTO diaDiem)
        //{
        //    XmlDocument doc = new XmlDocument();
        //    doc.Load(Util.FileName);
        //    XmlNodeList list = doc.SelectNodes("//NGUOIDUNG[@username='" + diaDiem.DanhMuc.NguoiDung.Username + "']//DANHMUC[@tendanhmuc='"
        //        + diaDiem.DanhMuc.TenDanhMuc + "']//DIADIEM[@tendiadiem='" + diaDiem.TenDiaDiem + "']");
        //    if (list.Count == 0)
        //        return false;

        //    list[0].Attributes["ghichu"].Value = diaDiem.GhiChu;
        //    doc.Save(Util.FileName);

        //    return true;
        //}
    }
}