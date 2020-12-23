using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PersonelYonetimSistemi.Business.Contracts;
using PersonelYonetimSistemi.Common.PagingListModels;
using PersonelYonetimSistemi.Common.ResultMesajlari;
using PersonelYonetimSistemi.Common.VModels;
using PersonelYonetimSistemi.Data.Contracts;

namespace PersonelYonetimSistemi.UI.Controllers
{
    [Authorize(Roles = ResultMesajlari.AdminRole)]
    public class PersonelIsEmirleriController : Controller
    {
        #region Variables
        private readonly IPersonelIsEmirleriRepository _personelIsEmirleriRepository;
        private readonly IPersonelIsEmirleriBusiness _personelIsEmirleriBusiness;
        private readonly IPersonelBusiness _personelBusiness;
        private readonly IHostingEnvironment _hostingEnvironment;
        #endregion

        #region Constructor
        [Obsolete]
        public PersonelIsEmirleriController(IPersonelIsEmirleriRepository personelIsEmirleriRepository, IPersonelIsEmirleriBusiness personelIsEmirleriBusiness, IPersonelBusiness personelBusiness, IHostingEnvironment hostingEnvironment)
        {
            _personelIsEmirleriRepository = personelIsEmirleriRepository;
            _personelIsEmirleriBusiness = personelIsEmirleriBusiness;
            _personelBusiness = personelBusiness;
            _hostingEnvironment = hostingEnvironment;
        } 
        #endregion
        public IActionResult Index(string personelId)
        {
            if(!String.IsNullOrWhiteSpace(personelId))
            {
                var veriWithPersonel = _personelIsEmirleriBusiness.GetIsEmriByPersonelId(personelId);
                return View(veriWithPersonel.Veri);
            }
            var veri = _personelIsEmirleriBusiness.GetAllIsEmirleri();
            if (veri.BasariliMi)
            {
                return View(veri.Veri);
            }
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(PersonelIsEmirleriVm personelIsEmirleriVm)
        {
            string uniqueDosyaAdi = null;
            if (personelIsEmirleriVm.ResimPath != null)
            {
                string uploadsDosya = Path.Combine(_hostingEnvironment.WebRootPath, "CustomResim");
                uniqueDosyaAdi = Guid.NewGuid().ToString() + "_" + personelIsEmirleriVm.ResimPath.FileName;
                string dosyaPath = Path.Combine(uploadsDosya, uniqueDosyaAdi);
                personelIsEmirleriVm.ResimPath.CopyTo(new FileStream(dosyaPath, FileMode.Create));
            }
            var result = _personelIsEmirleriBusiness.CreateIsEmirleri(personelIsEmirleriVm,uniqueDosyaAdi);
            if (result.BasariliMi)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.PersonelList = _personelBusiness.GetAllPersonel().Veri;
            var veri = _personelIsEmirleriBusiness.GetIsEmri(id).Veri;
            return View(veri);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(PersonelIsEmirleriVm personelIsEmirleriVm)
        {
            var veri = _personelIsEmirleriBusiness.EditIsEmri(personelIsEmirleriVm);
            if (veri.BasariliMi)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return Json(new { success = false, message = "Silmek İstediğiniz Veri Bulunamadı" });
            }
            var veri = _personelIsEmirleriBusiness.RemoveIsEmri(id);
            if (veri.BasariliMi)
            {
                return Json(new { success = veri.BasariliMi, message = veri.Mesaj });
            }
            else
            {
                return Json(new { success = veri.BasariliMi, message = veri.Mesaj });
            }
        }

        public ActionResult GetIsEmriByPersonelId(string Id)
        {
            var veri = _personelIsEmirleriBusiness.GetIsEmriByPersonelId(Id);
            if (veri.BasariliMi)
            {
                return Json(new { basariliMi = veri.BasariliMi, mesaj = veri.Mesaj , veri = veri.Veri });
            }
            else
            {
                return RedirectToAction("Index", new { personelId = Id });
            }
        }
    }
}