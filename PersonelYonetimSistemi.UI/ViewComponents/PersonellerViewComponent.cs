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
    public class PersonellerViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PersonellerViewComponent(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var veri = _unitOfWork.personelRepository.GetAll(p => p.IsActive == true && p.IsAdmin == false).ToList();

            if (veri != null)
            {
                var mappingVeri = _mapper.Map<List<Personels>, List<PersonelVM>>(veri);
                ViewBag.personeller = mappingVeri;
                return View(mappingVeri);
            }
            return View();
        }
    }
}
