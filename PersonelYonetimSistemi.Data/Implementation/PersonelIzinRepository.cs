using PersonelYonetimSistemi.Data.Contracts;
using PersonelYonetimSistemi.Data.DataContext;
using PersonelYonetimSistemi.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonelYonetimSistemi.Data.Implementation
{
    public class PersonelIzinRepository : Repository<PersonelIzin>, IPersonelIzinRepository
    {
        public PersonelIzinRepository(PersonelYonetimSistemiContext context) : base(context)
        {

        }
    }
}
