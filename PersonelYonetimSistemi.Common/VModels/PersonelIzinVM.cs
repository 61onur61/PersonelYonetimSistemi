using System;
using System.Collections.Generic;
using System.Text;

namespace PersonelYonetimSistemi.Common.VModels
{
    public class PersonelIzinVM : BaseVM
    {
        public int GunSayisi { get; set; }

        public DateTime OlusturulmaTarihi { get; set; }

        public int Period { get; set; }

        public string PersonelId { get; set; }
        
        public PersonelVM PersonelVM { get; set; }

        public int personelIzinTipiId { get; set; }
       
        public PersonelIzinTipiVM PersonelIzinTipiVM { get; set; }

    }
}
