using PersonelYonetimSistemi.Data.Contracts;
using PersonelYonetimSistemi.Data.DataContext;
using PersonelYonetimSistemi.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonelYonetimSistemi.Data.Implementation
{
    public class PersonelIsEmirleriRepository : Repository<PersonelIsEmirleri>, IPersonelIsEmirleriRepository
    {
        public PersonelIsEmirleriRepository(PersonelYonetimSistemiContext context) : base(context)
        {

        }
    }
}
