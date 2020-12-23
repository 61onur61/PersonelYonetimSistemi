using Microsoft.AspNetCore.Http;
using PersonelYonetimSistemi.Common.ResultMesajModels;
using PersonelYonetimSistemi.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace PersonelYonetimSistemi.Common.VModels
{
    public class PersonelIsEmirleriVm : BaseVM
    {
        public DateTime OlusturulmaTarihi { get; set; }

        public DateTime? DegistirilmeTarihi { get; set; }

        [MaxLength(10000)]
        public string IsEmriAciklamasi { get; set; }

        public EnumIsEmriDurumlari IsEmriDurumu { get; set; }

        public string IsEmriDurumuAciklamasi { get; set; }

        [Required]
        public double IsEmriPuan { get; set; }

        [MaxLength(40)]
        public string IsEmriNumara { get; set; }

        public IFormFile ResimPath { get; set; }
        public string ResimPathText { get; set; }

        public string PersonelIsAtamaId { get; set; }
        public string PersonelIsAtamaAdi { get; set; }
        [ForeignKey("PersonelIsAtamaId")]
        public PersonelVM PersonelIsAtama { get; set; }
    }
}
