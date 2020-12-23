using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PersonelYonetimSistemi.Common.VModels;
using PersonelYonetimSistemi.Data.Contracts;
using PersonelYonetimSistemi.Data.Implementation;
using PersonelYonetimSistemi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PersonelYonetimSistemi.UI.ViewComponents
{
    public class UserNameViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserNameViewComponent(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IViewComponentResult Invoke()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var personelFromDb = _unitOfWork.personelRepository.GetFirstOrDefault(p => p.Id == claims.Value);

            var personelToDb = _mapper.Map<Personels, PersonelVM>(personelFromDb);

            return View(personelToDb);
        }
    }
}
