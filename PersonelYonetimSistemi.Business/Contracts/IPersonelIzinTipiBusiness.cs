using PersonelYonetimSistemi.Common.ResultModels;
using PersonelYonetimSistemi.Common.VModels;
using System.Collections.Generic;

namespace PersonelYonetimSistemi.Business.Contracts
{
    public interface IPersonelIzinTipiBusiness
    {
        Result<List<PersonelIzinTipiVM>> GetAllPersonelIzinTipi();
       
        Result<PersonelIzinTipiVM> CreatePersonelIzinTipi(PersonelIzinTipiVM personelIzinTipiModel);
       
        Result<PersonelIzinTipiVM> GetByIdPersonelIzinTipi(int id);

        Result<PersonelIzinTipiVM> EditPersonelIzinTipi(PersonelIzinTipiVM personelIzinTipiVM);

        Result<PersonelIzinTipiVM> RemovePersonelIzinTipi(int id);

    }
}
