using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoogleMapApp
{
    public class NguoiDungDTO
    {
        private string username;        
        private string password;

        public string Username
        {
            get { return username; }
            set { username = value; }
        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public NguoiDungDTO()
        {
            username = String.Empty;
            password = String.Empty;
        }
        public NguoiDungDTO(string ten, string matKhau)
        {
            username = ten;
            password = matKhau;
        }
    }
}