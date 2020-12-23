using AutoMapper;
using PersonelYonetimSistemi.Business.Contracts;
using PersonelYonetimSistemi.Common.Extension;
using PersonelYonetimSistemi.Common.ResultMesajlari;
using PersonelYonetimSistemi.Common.ResultMesajModels;
using PersonelYonetimSistemi.Common.ResultModels;
using PersonelYonetimSistemi.Common.SessionOperations;
using PersonelYonetimSistemi.Common.VModels;
using PersonelYonetimSistemi.Data.Contracts;
using PersonelYonetimSistemi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonelYonetimSistemi.Business.Implementation
{
    public class PersonelIzinOnayBusiness : IPersonelIzinOnayBusiness
    {
        #region Variables
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public PersonelIzinOnayBusiness(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region CustomMethods
        public Result<List<PersonelIzinOnayVM>> GetAllIzinOnayByUserId(string userId)
        {
            var veri = _unitOfWork.personelIzinOnayRepository.GetAll(u => u.PersonelTalepId == userId && u.TamamlandiMi == false, includeProperties: "PersonelTalep,personelIzinTipi").ToList();

            if (veri != null)
            {
                List<PersonelIzinOnayVM> returnData = new List<PersonelIzinOnayVM>();
                foreach (var item in veri)
                {
                    returnData.Add(new PersonelIzinOnayVM()
                    {
                        Id = item.Id,
                        OnayDurumu = (EnumPersonelIzinOnayDurum)item.OnaylandiMi,
                        OnayText = EnumExtension<EnumPersonelIzinOnayDurum>.GetDisplayValue((EnumPersonelIzinOnayDurum)item.OnaylandiMi),
                        PersonelOnayId = item.PersonelOnayId,
                        TamamlandiMi = item.TamamlandiMi,
                        IzinBasvuruTarihi = item.IzinBasvuruTarihi,
                        PersonelIzinTipiId = item.PersonelIzinTipiId,
                        IzinTipiMetin = item.personelIzinTipi.Ad,
                        IzinBitisTarihi = item.IzinBitisTarihi,
                        IzinBaslangicTarihi = item.IzinBaslangicTarihi,
                        IzinNotu = item.IzinNotu,
                        PersonelTalepId = item.PersonelTalepId
                    });
                }
                return new Result<List<PersonelIzinOnayVM>>(true, ResultMesajlari.KayitBulundu, returnData);
            }
            else
                return new Result<List<PersonelIzinOnayVM>>(true, ResultMesajlari.KayitBulunamadi);
            //var IzinTipleri = _mapper.Map<List<PersonelIzinOnay>, List<PersonelIzinOnayVM>>(veri);
            //return new Result<List<PersonelIzinOnayVM>>(true, ResultMesajlari.KayitBulundu, IzinTipleri);


        }
        public Result<PersonelIzinOnayVM> CreatePersonelIzinOnay(PersonelIzinOnayVM personelIzinOnayVM, SessionContext user)
        {
            if (personelIzinOnayVM != null)
            {
                try
                {
                    var IzinTalebi = _mapper.Map<PersonelIzinOnayVM, PersonelIzinOnay>(personelIzinOnayVM);
                    IzinTalebi.PersonelTalepId = user.LoginId;
                    IzinTalebi.TamamlandiMi = false;
                    IzinTalebi.IzinBasvuruTarihi = DateTime.Now;
                    IzinTalebi.OnaylandiMi = (int)EnumPersonelIzinOnayDurum.OnayaGonderildi;
                    _unitOfWork.personelIzinOnayRepository.Add(IzinTalebi);
                    _unitOfWork.Save();
                    return new Result<PersonelIzinOnayVM>(true, ResultMesajlari.KayitBasariylaOlustruldu);
                }
                catch (Exception ex)
                {
                    return new Result<PersonelIzinOnayVM>(false, ResultMesajlari.KayitOlusturulurkenHataOlustu + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<PersonelIzinOnayVM>(false, "Kayıt Girilirken Boş Alan Bırakılamaz.");
            }
        }

        /// <summary>
        /// Personel İzin Talebi Güncelleme Metodu.
        /// </summary>
        /// <param name="personelIzinOnayVM"></param>
        /// <returns></returns>
        public Result<PersonelIzinOnayVM> EditPersonelIzinOnay(PersonelIzinOnayVM personelIzinOnayVM, SessionContext user)
        {
            if (personelIzinOnayVM != null)
            {
                try
                {
                    var izinTalebi = _mapper.Map<PersonelIzinOnayVM, PersonelIzinOnay>(personelIzinOnayVM);
                    izinTalebi.OnaylandiMi = (int)personelIzinOnayVM.OnayDurumu;
                    izinTalebi.PersonelTalepId = user.LoginId;
                    izinTalebi.IzinBasvuruTarihi = DateTime.Now;
                    izinTalebi.OnaylandiMi = (int)EnumPersonelIzinOnayDurum.OnayaGonderildi;
                    _unitOfWork.personelIzinOnayRepository.Update(izinTalebi);
                    _unitOfWork.Save();
                    return new Result<PersonelIzinOnayVM>(true, ResultMesajlari.KayitBasariylaGuncellendi);

                }
                catch (Exception ex)
                {
                    return new Result<PersonelIzinOnayVM>(false, ResultMesajlari.KayitGuncellenemedi + " " + ex.Message.ToString());
                }
            }
            else
                return new Result<PersonelIzinOnayVM>(false, "Kayıt Güncelleme Sayfasında Hiç Bir Alan Boş Bırakılamaz!");
        }

        public Result<PersonelIzinOnayVM> GetAllIzinOnayById(int id)
        {
            var veri = _unitOfWork.personelIzinOnayRepository.Get(id);
            if (veri != null)
            {
                var izinTalebi = _mapper.Map<PersonelIzinOnay, PersonelIzinOnayVM>(veri);
                izinTalebi.OnayDurumu = (EnumPersonelIzinOnayDurum)veri.OnaylandiMi;
                izinTalebi.OnayText = EnumExtension<EnumPersonelIzinOnayDurum>.GetDisplayValue((EnumPersonelIzinOnayDurum)veri.OnaylandiMi);
                return new Result<PersonelIzinOnayVM>(true, ResultMesajlari.KayitBulundu, izinTalebi);
            }
            else
            {
                return new Result<PersonelIzinOnayVM>(false, ResultMesajlari.KayitBulunamadi);
            }

        }

        public Result<PersonelIzinOnayVM> RemovePersonelIzinOnay(int id)
        {
            var veri = _unitOfWork.personelIzinOnayRepository.Get(id);
            if (veri != null)
            {
                veri.TamamlandiMi = true;
                _unitOfWork.personelIzinOnayRepository.Update(veri);
                _unitOfWork.Save();
                return new Result<PersonelIzinOnayVM>(true, ResultMesajlari.KayitBasariylaSilindi);
            }
            else
            {
                return new Result<PersonelIzinOnayVM>(true, ResultMesajlari.KayitSilinirkenBirHataOluştu);
            }
        }

        public Result<List<PersonelIzinOnayVM>> GetOnayaGonderilmisTalepler()
        {
            var veri = _unitOfWork.personelIzinOnayRepository.GetAll(u => u.OnaylandiMi == (int)EnumPersonelIzinOnayDurum.OnayaGonderildi && u.TamamlandiMi == false, includeProperties: "PersonelTalep,personelIzinTipi").ToList();

            if (veri != null)
            {
                List<PersonelIzinOnayVM> returnData = new List<PersonelIzinOnayVM>();
                foreach (var item in veri)
                {
                    returnData.Add(new PersonelIzinOnayVM()
                    {
                        Id = item.Id,
                        OnayDurumu = (EnumPersonelIzinOnayDurum)item.OnaylandiMi,
                        OnayText = EnumExtension<EnumPersonelIzinOnayDurum>.GetDisplayValue((EnumPersonelIzinOnayDurum)item.OnaylandiMi),
                        PersonelOnayId = item.PersonelOnayId,
                        TamamlandiMi = item.TamamlandiMi,
                        IzinBasvuruTarihi = item.IzinBasvuruTarihi,
                        PersonelIzinTipiId = item.PersonelIzinTipiId,
                        IzinTipiMetin = item.personelIzinTipi.Ad,
                        IzinBitisTarihi = item.IzinBitisTarihi,
                        IzinBaslangicTarihi = item.IzinBaslangicTarihi,
                        IzinNotu = item.IzinNotu,
                        PersonelTalepId = item.PersonelTalepId,
                        IzinTalepEdenPersonel = item.PersonelTalep.Email
                    });
                }
                return new Result<List<PersonelIzinOnayVM>>(true, ResultMesajlari.KayitBulundu, returnData);
            }
            else
                return new Result<List<PersonelIzinOnayVM>>(true, ResultMesajlari.KayitBulunamadi);
        }

        public Result<bool> ReddetPersonelIzinTalebi(int id)
        {
            var veri = _unitOfWork.personelIzinOnayRepository.Get(id);
            if (veri != null)
            {
                try
                {
                    veri.OnaylandiMi = (int)EnumPersonelIzinOnayDurum.Reddedildi;
                    _unitOfWork.personelIzinOnayRepository.Update(veri);
                    _unitOfWork.Save();
                    return new Result<bool>(true, ResultMesajlari.IzinReddedildi);
                }
                catch (Exception ex)
                {
                    return new Result<bool>(true, ResultMesajlari.IzinReddedildiHata + "=>" + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<bool>(false,"Parametreler Boş Geçilemez.");
            }
        }
        #endregion

    }
}
