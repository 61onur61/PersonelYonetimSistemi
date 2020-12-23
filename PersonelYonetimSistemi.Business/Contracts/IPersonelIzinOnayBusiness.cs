using PersonelYonetimSistemi.Common.ResultModels;
using PersonelYonetimSistemi.Common.SessionOperations;
using PersonelYonetimSistemi.Common.VModels;
using System.Collections.Generic;

namespace PersonelYonetimSistemi.Business.Contracts
{
    public interface IPersonelIzinOnayBusiness
    {
        Result<List<PersonelIzinOnayVM>> GetAllIzinOnayByUserId(string userId);

        Result<PersonelIzinOnayVM> CreatePersonelIzinOnay(PersonelIzinOnayVM personelIzinOnayVM,SessionContext user);

        Result<PersonelIzinOnayVM> EditPersonelIzinOnay(PersonelIzinOnayVM personelIzinOnayVM, SessionContext user);

        Result<PersonelIzinOnayVM> GetAllIzinOnayById(int id);

        Result<PersonelIzinOnayVM> RemovePersonelIzinOnay(int id);

        Result<List<PersonelIzinOnayVM>> GetOnayaGonderilmisTalepler();

        Result<bool> ReddetPersonelIzinTalebi(int id);
    }
}
