using PersonelYonetimSistemi.Common.ResultModels;
using PersonelYonetimSistemi.Common.SessionOperations;
using PersonelYonetimSistemi.Common.VModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonelYonetimSistemi.Business.Contracts
{
    public interface IPersonelIsEmirleriBusiness
    {
        Result<List<PersonelIsEmirleriVm>> GetAllIsEmirleri();

        Result<PersonelIsEmirleriVm> CreateIsEmirleri(PersonelIsEmirleriVm personelIsEmirleriVm, string uniqueDosyaAdi);

        Result<PersonelIsEmirleriVm> GetIsEmri(int id);

        Result<PersonelIsEmirleriVm> EditIsEmri(PersonelIsEmirleriVm personelIsEmirleriVm);

        Result<bool> RemoveIsEmri(int id);

        Result<List<PersonelIsEmirleriVm>> GetIsEmriByPersonelId(string personelId);

    }
}
