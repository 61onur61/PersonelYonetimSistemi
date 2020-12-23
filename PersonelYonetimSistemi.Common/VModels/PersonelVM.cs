using System;
using System.ComponentModel.DataAnnotations;

namespace PersonelYonetimSistemi.Common.VModels
{
    public class PersonelVM
    {
        public string Id { get; set; }
        [Display(Name ="Kullanıcı Adı")]
        public string UserName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Ad { get; set; }

        public string Soyad { get; set; }

        public string MaasId { get; set; }

        public DateTime DogumGunu { get; set; }
    }
}
