using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonelYonetimSistemi.Data.Models
{
    public class PersonelIzin:BaseEntity
    {
        public int GunSayisi { get; set; }

        public DateTime OlusturulmaTarihi { get; set; }

        public int Period { get; set; }

        public string PersonelId { get; set; }
        [ForeignKey("PersonelId")]
        public Personels Personels { get; set; }

        public int personelIzinTipiId { get; set; }
        [ForeignKey("personelIzinTipiId")]
        public PersonelIzinTipi PersonelIzinTipi { get; set; }
    }
}
