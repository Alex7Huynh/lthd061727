using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoogleMapApp
{
    public class DiaDiemDTO
    {
        private string tenDiaDiem;
        private float viDo;
        private float kinhDo;
        private string ghiChu;
        private DanhMucDTO danhMuc;

        public string TenDiaDiem
        {
            get { return tenDiaDiem; }
            set { tenDiaDiem = value; }
        }
        public float ViDo
        {
            get { return viDo; }
            set { viDo = value; }
        }
        public float KinhDo
        {
            get { return kinhDo; }
            set { kinhDo = value; }
        }
        public string GhiChu
        {
            get { return ghiChu; }
            set { ghiChu = value; }
        }
        public DanhMucDTO DanhMuc
        {
            get { return danhMuc; }
            set { danhMuc = value; }
        }

        public DiaDiemDTO()
        {
            tenDiaDiem = String.Empty;
        }
        public DiaDiemDTO(string name, float lat, float lgn, string note, DanhMucDTO dm)
        {
            tenDiaDiem = name;
            ViDo = lat;
            kinhDo = lgn;
            ghiChu = note;
            danhMuc = new DanhMucDTO(dm);
        }
    }
}