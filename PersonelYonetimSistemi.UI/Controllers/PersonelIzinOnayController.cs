using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PersonelYonetimSistemi.Business.Contracts;
using PersonelYonetimSistemi.Common.ResultMesajlari;
using PersonelYonetimSistemi.Common.SessionOperations;
using PersonelYonetimSistemi.Common.VModels;

namespace PersonelYonetimSistemi.UI.Controllers
{
    public class PersonelIzinOnayController : Controller
    {
        private readonly IPersonelIzinOnayBusiness _personelIzinOnayBusiness;
        private readonly IPersonelIzinTipiBusiness _personelIzinTipiBusiness;
        public PersonelIzinOnayController(IPersonelIzinOnayBusiness personelIzinOnayBusiness, IPersonelIzinTipiBusiness personelIzinTipiBusiness)
        {
            _personelIzinOnayBusiness = personelIzinOnayBusiness;
            _personelIzinTipiBusiness = personelIzinTipiBusiness;
        }
        public IActionResult Index()
        {

            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultMesajlari.LoginUserInfo));

            var onayModel = _personelIzinOnayBusiness.GetAllIzinOnayByUserId(user.LoginId);
            ViewBag.personelIzinTipis = _personelIzinTipiBusiness.GetAllPersonelIzinTipi();
            if (onayModel.BasariliMi)
            {
                var result = onayModel.Veri;
                return View(result);
            }
            return View(user);
        }

        public IActionResult Create()
        {
            ViewBag.PersonelIzinTipleri = _personelIzinTipiBusiness.GetAllPersonelIzinTipi().Veri;
            return View();
        }

        public ActionResult Edit(int? id)
        {
            ViewBag.PersonelIzinTipleri = _personelIzinTipiBusiness.GetAllPersonelIzinTipi().Veri;
            if (id > 0)
            {
                var veri = _personelIzinOnayBusiness.GetAllIzinOnayById((int)id);
                return View(veri.Veri);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public IActionResult Create(PersonelIzinOnayVM personelIzinOnayVM,int? id)
        {
            var user = JsonConvert.DeserializeObject<SessionContext>(HttpContext.Session.GetString(ResultMesajlari.LoginUserInfo));
            #region CreateOrEdit
            if (id > 0)
            {
                var veri = _personelIzinOnayBusiness.EditPersonelIzinOnay(personelIzinOnayVM,user);
                if (veri.BasariliMi)
                {
                    return RedirectToAction("Index");
                }
                return View(personelIzinOnayVM);
            }
            else
            {
                var veri = _personelIzinOnayBusiness.CreatePersonelIzinOnay(personelIzinOnayVM,user);
                if (veri.BasariliMi)
                {
                    return RedirectToAction("Index");
                }
                return View(personelIzinOnayVM);
            }
            #endregion
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return Json(new { success = false, message = "Silmek İstediğiniz Veri Bulunamadı" });
            }
            var veri = _personelIzinOnayBusiness.RemovePersonelIzinOnay(id);
            if (veri.BasariliMi)
            {
                return Json(new { success = veri.BasariliMi, message = veri.Mesaj });
            }
            else
            {
                return Json(new { success = veri.BasariliMi, message = veri.Mesaj });
            }
        }

        [HttpPost]
        public IActionResult Reddet(int id)
        {
            if (id <= 0)
            {
                return Json(new { success = false, message = "Onaylamak İçin Kayıt Seçiniz" });
            }
            var veri = _personelIzinOnayBusiness.ReddetPersonelIzinTalebi(id);
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