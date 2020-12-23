using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PersonelYonetimSistemi.Common.ResultMesajModels
{
    public enum EnumIsEmriDurumlari
    {
        [Display(Name = "İş Emri Oluşturuldu")]
        IsEmriOlusturuldu = 1,
        [Display(Name = "İş Atandı")]
        IsAtandı = 2,
        [Display(Name = "İş Üstlenildi")]
        IsUstlenildi = 3,
        [Display(Name = "İş Kapatıldı")]
        IsKapatildi = 4
    }
}
