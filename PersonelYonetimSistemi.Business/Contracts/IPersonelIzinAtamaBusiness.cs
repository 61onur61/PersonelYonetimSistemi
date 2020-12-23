using PersonelYonetimSistemi.Common.ResultModels;

namespace PersonelYonetimSistemi.Business.Contracts
{
    public interface IPersonelIzinAtamaBusiness
    {
        Result<bool> OnaylaPersonelIzin(int id);
    }
}
