using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonelYonetimSistemi.Data.Models
{
    public class PersonelIzinOnay:BaseEntity
    {
        public DateTime IzinBaslangicTarihi { get; set; }

        public DateTime IzinBitisTarihi { get; set; }

        public DateTime IzinBasvuruTarihi { get; set; }

        public string IzinNotu { get; set; }

        public int? OnaylandiMi { get; set; }

        public bool TamamlandiMi { get; set; }

        //İzin Talebinde bulunan kullanıcı bilgileri.
        public string PersonelTalepId { get; set; }
        [ForeignKey("PersonelTalepId")]
        public Personels PersonelTalep { get; set; }

        //İzin Talebini Onaylayan kullanıcı bilgileri.
        public string PersonelOnayId { get; set; }
        [ForeignKey("PersonelOnayId")]
        public Personels PersonelOnay { get; set; }

        //İzin Tipi İlişkisi
        public int PersonelIzinTipiId { get; set; }
        [ForeignKey("PersonelIzinTipiId")]
        public PersonelIzinTipi personelIzinTipi { get; set; }
    }
}
