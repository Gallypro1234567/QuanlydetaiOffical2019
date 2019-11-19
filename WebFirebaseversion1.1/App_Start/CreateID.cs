using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMVCFirebaseOffical.App_Start
{
    public class CreateID
    {

        public static string createID_byDocument(string doc)
        {
            string idstring = doc.ToUpper() + "" + DateTime.Now.ToString("yyyyMMddHHmmss");
            return idstring;
        }
    }
}