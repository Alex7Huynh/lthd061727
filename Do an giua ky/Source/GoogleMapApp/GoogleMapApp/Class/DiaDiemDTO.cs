using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoogleMapApp
{
    public class DiaDiemDTO
    {
        private string tenDiaDiem;
        private string diaChi;
        private float viDo;
        private float kinhDo;
        private bool deleted;
        private string ghiChu;
        private DanhMucDTO danhMuc;

        public string TenDiaDiem
        {
            get { return tenDiaDiem; }
            set { tenDiaDiem = value; }
        }
        public string DiaChi
        {
            get { return diaChi; }
            set { diaChi = value; }
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
        public bool Deleted
        {
            get { return deleted; }
            set { deleted = value; }
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
        public DiaDiemDTO(string name, string address, float lat, float lgn, string note, DanhMucDTO dm)
        {
            tenDiaDiem = name;
            diaChi = address;
            ViDo = lat;
            kinhDo = lgn;
            ghiChu = note;
            danhMuc = dm;
        }
    }
}