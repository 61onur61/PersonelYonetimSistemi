using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PersonelYonetimSistemi.Common.ResultMesajModels
{
    public enum EnumPersonelIzinOnayDurum
    {
        [Display(Name ="Onaya Göderildi")]
        OnayaGonderildi = 1,
        [Display(Name = "Onaylandı")]
        Onaylandi = 2,
        [Display(Name = "Reddedildi")]
        Reddedildi = 3
    }
}
