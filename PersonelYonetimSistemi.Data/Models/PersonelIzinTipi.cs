using System;

namespace PersonelYonetimSistemi.Data.Models
{
    public class PersonelIzinTipi:BaseEntity
    {
        public string Ad { get; set; }

        public int GunSayisi { get; set; }

        public DateTime OlusturulmaTarihi { get; set; }

        public bool AktifMi { get; set; }

    }
}
