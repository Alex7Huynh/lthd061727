using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoogleMapApp
{
    public class DanhMucDTO
    {
        private int maDanhMuc;
        private string tenDanhMuc;
        NguoiDungDTO nguoiDung;

        public int MaDanhMuc
        {
            get { return maDanhMuc; }
            set { maDanhMuc = value; }
        }
        public string TenDanhMuc
        {
            get { return tenDanhMuc; }
            set { tenDanhMuc = value; }
        }
        public NguoiDungDTO NguoiDung
        {
            get { return nguoiDung; }
            set { nguoiDung = value; }
        }

        public DanhMucDTO()
        {
            tenDanhMuc = "";
            nguoiDung = new NguoiDungDTO();
        }
        public DanhMucDTO(int id, string name, NguoiDungDTO user)
        {
            maDanhMuc = id;
            tenDanhMuc = name;
            nguoiDung = user;
        }
        public DanhMucDTO(DanhMucDTO dm)
        {
            maDanhMuc = dm.MaDanhMuc;
            tenDanhMuc = dm.TenDanhMuc;
            nguoiDung = new NguoiDungDTO(dm.NguoiDung);
        }

    }
}