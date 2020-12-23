using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonelYonetimSistemi.Business.Contracts;
using PersonelYonetimSistemi.Common.ResultMesajlari;

namespace PersonelYonetimSistemi.UI.Controllers
{
    [Authorize(Roles = ResultMesajlari.AdminRole)]
    public class PersonelIzinAtamaController : Controller
    {
        #region Variables
        private readonly IPersonelIzinAtamaBusiness _personelIzinAtamaBusiness;
        private readonly IPersonelIzinOnayBusiness _personelIzinOnayBusiness;
        #endregion

        #region Constructor
        public PersonelIzinAtamaController(IPersonelIzinAtamaBusiness personelIzinAtamaBusiness, IPersonelIzinOnayBusiness personelIzinOnayBusiness)
        {
            _personelIzinAtamaBusiness = personelIzinAtamaBusiness;
            _personelIzinOnayBusiness = personelIzinOnayBusiness;
        } 
        #endregion
        public IActionResult Index()
        {
            var veri = _personelIzinOnayBusiness.GetOnayaGonderilmisTalepler();
            if (veri.BasariliMi)
            {
                return View(veri.Veri);
            }
            return View();
        }

        public IActionResult Onayla(int id)
        {
            if (id <= 0)
            {
                return Json(new { success = false, message = "Onaylamak İçin Kayıt Seçiniz" });
            }
            var veri = _personelIzinAtamaBusiness.OnaylaPersonelIzin(id);
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