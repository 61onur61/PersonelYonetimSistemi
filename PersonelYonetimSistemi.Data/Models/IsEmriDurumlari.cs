using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PersonelYonetimSistemi.Data.Models
{
    public class IsEmriDurumlari : BaseEntity
    {
        [Required]
        public string IsEmriDurumuAdi { get; set; }

    }
}
