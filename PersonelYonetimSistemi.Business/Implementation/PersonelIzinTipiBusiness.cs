using AutoMapper;
using PersonelYonetimSistemi.Business.Contracts;
using PersonelYonetimSistemi.Common.ResultMesajlari;
using PersonelYonetimSistemi.Common.ResultModels;
using PersonelYonetimSistemi.Common.VModels;
using PersonelYonetimSistemi.Data.Contracts;
using PersonelYonetimSistemi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonelYonetimSistemi.Business.Implementation
{
    public class PersonelIzinTipiBusiness : IPersonelIzinTipiBusiness
    {
        #region Variables
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public PersonelIzinTipiBusiness(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region CustomMethods
        public Result<List<PersonelIzinTipiVM>> GetAllPersonelIzinTipi()
        {
            var data = _unitOfWork.personelIzinTipiRepository.GetAll(e => e.AktifMi == true).ToList();

            #region 2.Yontem
            var IzinTipleri = _mapper.Map<List<PersonelIzinTipi>, List<PersonelIzinTipiVM>>(data);
            return new Result<List<PersonelIzinTipiVM>>(true,ResultMesajlari.KayitBulundu,IzinTipleri);
            #endregion

        }
        
        public Result<PersonelIzinTipiVM> CreatePersonelIzinTipi(PersonelIzinTipiVM personelIzinTipiModel)
        {
            if (personelIzinTipiModel != null)
            {
                try
                {
                    var IzinTipi = _mapper.Map<PersonelIzinTipiVM, PersonelIzinTipi>(personelIzinTipiModel);
                    IzinTipi.OlusturulmaTarihi = DateTime.Now;
                    IzinTipi.AktifMi = true;
                    _unitOfWork.personelIzinTipiRepository.Add(IzinTipi);
                    _unitOfWork.Save();
                    return new Result<PersonelIzinTipiVM>(true, ResultMesajlari.KayitBasariylaOlustruldu);
                }
                catch (Exception ex)
                {
                    return new Result<PersonelIzinTipiVM>(false, ResultMesajlari.KayitOlusturulurkenHataOlustu + " " + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<PersonelIzinTipiVM>(false, "Kayıt Girilirken Boş Alan Bırakılamaz.");
            }
        }

        public Result<PersonelIzinTipiVM> GetByIdPersonelIzinTipi(int id)
        {
            var veri = _unitOfWork.personelIzinTipiRepository.Get(id);
            if(veri != null)
            {
                var izinTipi = _mapper.Map<PersonelIzinTipi, PersonelIzinTipiVM>(veri);
                return new Result<PersonelIzinTipiVM>(true, ResultMesajlari.KayitBulundu, izinTipi);
            }
            else
            {
                return new Result<PersonelIzinTipiVM>(false, ResultMesajlari.KayitBulunamadi);
            }
        }

        public Result<PersonelIzinTipiVM> EditPersonelIzinTipi(PersonelIzinTipiVM personelIzinTipiModel)
        {
            if (personelIzinTipiModel != null)
            {
                try
                {
                    var izinTipi = _mapper.Map<PersonelIzinTipiVM, PersonelIzinTipi>(personelIzinTipiModel);
                    _unitOfWork.personelIzinTipiRepository.Update(izinTipi);
                    _unitOfWork.Save();
                    return new Result<PersonelIzinTipiVM>(true, ResultMesajlari.KayitBasariylaGuncellendi);

                }
                catch (Exception ex)
                {
                    return new Result<PersonelIzinTipiVM>(false, ResultMesajlari.KayitGuncellenemedi + " " + ex.Message.ToString());
                }
            }
            else
                return new Result<PersonelIzinTipiVM>(false, "Kayıt Güncelleme Sayfasında Hiç Bir Alan Boş Bırakılamaz!");
        }

        public Result<PersonelIzinTipiVM> RemovePersonelIzinTipi(int id)
        {
            var veri = _unitOfWork.personelIzinTipiRepository.Get(id);
            if (veri != null)
            {
                veri.AktifMi = false;
                _unitOfWork.personelIzinTipiRepository.Update(veri);
                _unitOfWork.Save();
                return new Result<PersonelIzinTipiVM>(true, ResultMesajlari.KayitBasariylaSilindi);
            }
            else
            {
                return new Result<PersonelIzinTipiVM>(true, ResultMesajlari.KayitSilinirkenBirHataOluştu);
            }
        }
        #endregion
    }
}
