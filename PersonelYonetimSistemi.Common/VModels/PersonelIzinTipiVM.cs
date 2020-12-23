using System;
using System.Collections.Generic;
using System.Text;

namespace PersonelYonetimSistemi.Common.VModels
{
    public class PersonelIzinTipiVM : BaseVM
    {
        public string Ad { get; set; }

        public int GunSayisi { get; set; }

        public DateTime OlusturulmaTarihi { get; set; }

        public bool AktifMi { get; set; }

        //MVVM Create PersonelIzinTipi
        public void SetPersonelIzinTipi(string ad)
        {
            Ad = ad;
        }
    }
}
