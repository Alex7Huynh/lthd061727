using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoogleMapApp
{
    public static class Util
    {
        public static string FileName = HttpContext.Current.ApplicationInstance.Server.MapPath("~/App_Data/GoogleAPI.xml");
    }
}