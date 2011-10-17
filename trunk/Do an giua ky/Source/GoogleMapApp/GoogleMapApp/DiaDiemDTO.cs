using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoogleMapApp
{
    public class DiaDiemDTO
    {        
        public string tenDiaDiem;
        public float kinhDo;
        public float viDo;

        public DiaDiemDTO()
        { tenDiaDiem = string.Empty; }

        public DiaDiemDTO(string ten, float x, float y)
        {
            tenDiaDiem = ten;
            viDo = x;
            kinhDo = y;
        }
    }

    public static class MyClass
    {
        public static List<DiaDiemDTO> ds;
    }
}