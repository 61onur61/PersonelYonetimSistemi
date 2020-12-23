using PersonelYonetimSistemi.Data.Contracts;
using PersonelYonetimSistemi.Data.DataContext;
using PersonelYonetimSistemi.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonelYonetimSistemi.Data.Implementation
{
    public class PersonelIzinTipiRepository : Repository<PersonelIzinTipi>, IPersonelIzinTipiRepository
    {
        public PersonelIzinTipiRepository(PersonelYonetimSistemiContext context) : base(context)
        {

        }
    }
}
