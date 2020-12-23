using Microsoft.AspNetCore.Identity;
using System;

namespace PersonelYonetimSistemi.Data.Models
{
    public class Personels : IdentityUser
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }

        public string MaasId { get; set; }

        public DateTime DogumGunu { get; set; }

        public bool IsActive { get; set; }

        public bool IsAdmin { get; set; }
    }
}
