using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonelYonetimSistemi.Business.Contracts;
using PersonelYonetimSistemi.Common.ResultMesajlari;
using PersonelYonetimSistemi.Common.VModels;

namespace PersonelYonetimSistemi.UI.Controllers
{
    [Authorize(Roles =ResultMesajlari.AdminRole)]
    public class PersonelIzinTipiController : Controller
    {
        private readonly IPersonelIzinTipiBusiness _personelIzinTipiBusiness;

        public PersonelIzinTipiController(IPersonelIzinTipiBusiness personelIzinTipiBusiness)
        {
            _personelIzinTipiBusiness = personelIzinTipiBusiness;
        }
        public IActionResult Index()
        {
            var data = _personelIzinTipiBusiness.GetAllPersonelIzinTipi();
            if (data.BasariliMi)
            {
                var result = data.Veri;
                return View(result);
            }
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(PersonelIzinTipiVM personelIzinTipiModel)
        {
            if (ModelState.IsValid)
            {
                var veri = _personelIzinTipiBusiness.CreatePersonelIzinTipi(personelIzinTipiModel);
                if (veri.BasariliMi)
                {
                    Thread.Sleep(3000);
                    return RedirectToAction("Index");
                }
                return View(personelIzinTipiModel);
            }
            else
            {
                return View(personelIzinTipiModel);
            }
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if(id < 0)
            {
                return View();
            }

            var veri = _personelIzinTipiBusiness.GetByIdPersonelIzinTipi(id);
            if(veri.BasariliMi)
            {
                return View(veri.Veri);
            }
            return View();
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(PersonelIzinTipiVM personelIzinTipiModel)
        {
            if (ModelState.IsValid)
            {
                var veri = _personelIzinTipiBusiness.EditPersonelIzinTipi(personelIzinTipiModel);
                if (veri.BasariliMi)
                {
                    return RedirectToAction("Index");
                }
                return View(personelIzinTipiModel);
            }
            else
            {
                return View(personelIzinTipiModel);
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if(id <= 0)
            {
                return Json(new { success = false, message = "Silmek İstediğiniz Veri Bulunamadı" });
            }
            var veri = _personelIzinTipiBusiness.RemovePersonelIzinTipi(id);
            if (veri.BasariliMi)
            {
                return Json(new { success = veri.BasariliMi, message = veri.Mesaj });
            }
            else
            {
                return Json(new { success = veri.BasariliMi, message = veri.Mesaj });
            }
        }
    }
}