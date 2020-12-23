using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PersonelYonetimSistemi.Data.Models
{
    public class PersonelIsEmirleri : BaseEntity
    {
        public DateTime OlusturulmaTarihi { get; set; }

        public DateTime? DegistirilmeTarihi { get; set; }

        [MaxLength(10000)]
        public string IsEmriAciklamasi { get; set; }

        public int IsEmriDurumu { get; set; }

        public double IsEmriPuan { get; set; }
        [MaxLength(500,ErrorMessage ="Resim Yolu 500 Karakterden Uzun Olamaz")]
        public string ResimPath { get; set; }

        [MaxLength(40)]
        public string IsEmriNumara { get; set; }

        public string PersonelIsAtamaId { get; set; }
        [ForeignKey("PersonelIsAtamaId")]
        public Personels PersonelIsAtama { get; set; }

    }
}
