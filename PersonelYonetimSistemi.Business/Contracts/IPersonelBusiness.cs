using PersonelYonetimSistemi.Common.ResultModels;
using PersonelYonetimSistemi.Common.VModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonelYonetimSistemi.Business.Contracts
{
    public interface IPersonelBusiness
    {
        Result<List<PersonelVM>> GetAllPersonel();
    }
}
