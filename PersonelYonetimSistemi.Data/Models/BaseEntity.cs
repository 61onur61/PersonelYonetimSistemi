using System.ComponentModel.DataAnnotations;

namespace PersonelYonetimSistemi.Data.Models
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
