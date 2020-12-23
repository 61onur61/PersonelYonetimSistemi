using System.ComponentModel.DataAnnotations;

namespace PersonelYonetimSistemi.Common.VModels
{
    public class BaseVM
    {
        [Key]
        public int Id { get; set; }
    }
}
