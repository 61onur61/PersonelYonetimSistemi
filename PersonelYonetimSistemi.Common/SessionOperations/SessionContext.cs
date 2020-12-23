using System;
using System.Collections.Generic;
using System.Text;

namespace PersonelYonetimSistemi.Common.SessionOperations
{
    public class SessionContext
    {
        public string LoginId { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Email { get; set; }
        public bool? IsAdmin { get; set; }

    }
}
