using PersonelYonetimSistemi.Common.ResultMesajModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PersonelYonetimSistemi.Common.VModels
{
    public class PersonelIzinOnayVM : BaseVM
    {
        [Required]
        public DateTime IzinBaslangicTarihi { get; set; }
        [Required]
        public DateTime IzinBitisTarihi { get; set; }

        public DateTime IzinBasvuruTarihi { get; set; }

        [Display(Name ="İzin Talep Notu")]
        [MaxLength(500,ErrorMessage ="İzin Talep Notu 500 Karaterden Fazla Olamaz")]
        public string IzinNotu { get; set; }

        public EnumPersonelIzinOnayDurum OnayDurumu { get; set; }
        public string OnayText { get; set; }

        public bool TamamlandiMi { get; set; }

        //İzin Talebinde bulunan kullanıcı bilgileri.
        public string PersonelTalepId { get; set; }

        public string IzinTalepEdenPersonel { get; set; }

        public PersonelVM PersonelTalep { get; set; }

        //İzin Talebini Onaylayan kullanıcı bilgileri.
        public string PersonelOnayId { get; set; }

        public PersonelVM PersonelOnay { get; set; }

        [Required]
        public int PersonelIzinTipiId { get; set; }
        public string IzinTipiMetin { get; set; }
        public PersonelIzinTipiVM personelIzinTipi { get; set; }


    }
}
