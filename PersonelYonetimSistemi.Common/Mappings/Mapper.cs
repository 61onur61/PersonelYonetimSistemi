using AutoMapper;
using PersonelYonetimSistemi.Common.VModels;
using PersonelYonetimSistemi.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonelYonetimSistemi.Common.Mappings
{
    public class Maps : Profile
    {
        public Maps()
        {
            CreateMap<PersonelIzin, PersonelIzinVM>().ReverseMap();

            CreateMap<PersonelIzinTipi, PersonelIzinTipiVM>().ReverseMap();

            CreateMap<PersonelIzinOnay, PersonelIzinOnayVM>().ReverseMap();

            CreateMap<Personels, PersonelVM>().ReverseMap();
        }
    }
}
