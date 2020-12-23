using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PersonelYonetimSistemi.Common.ResultMesajModels;
using PersonelYonetimSistemi.Common.VModels;
using PersonelYonetimSistemi.Data.Contracts;
using PersonelYonetimSistemi.Data.Implementation;
using PersonelYonetimSistemi.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PersonelYonetimSistemi.UI.ViewComponents
{
    public class AtanmisIsEmirleriViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AtanmisIsEmirleriViewComponent(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var personelFromDb = _unitOfWork.personelRepository.GetFirstOrDefault(p => p.Id == claims.Value);
            var personelId = personelFromDb.Id;
            var isEmriDurumu = (int)EnumIsEmriDurumlari.IsAtandı;

            var veri = _unitOfWork.personelIsEmirleriRepository
                .GetAll(p => p.PersonelIsAtamaId == personelId && p.IsEmriDurumu == isEmriDurumu).ToList();

            if (veri != null)
            {
                List<PersonelIsEmirleriVm> returnData = new List<PersonelIsEmirleriVm>();

                foreach (var item in veri)
                {
                    returnData.Add(new PersonelIsEmirleriVm
                    {
                        PersonelIsAtamaAdi = item.PersonelIsAtama.Ad + " " + item.PersonelIsAtama.Soyad,
                        IsEmriNumara = item.IsEmriNumara,
                        IsEmriPuan = item.IsEmriPuan,
                        IsEmriDurumuAciklamasi = item.IsEmriAciklamasi,
                        OlusturulmaTarihi = item.OlusturulmaTarihi,
                        DegistirilmeTarihi = item.DegistirilmeTarihi,
                        Id = item.Id,
                        PersonelIsAtamaId = item.PersonelIsAtamaId
                    });
                }
                return View(returnData);
            }
            //var personelToDb = _mapper.Map<List<PersonelIsEmirleri>,List<PersonelIsEmirleriVm>>(veri);

            return View();
        }
    }
}
