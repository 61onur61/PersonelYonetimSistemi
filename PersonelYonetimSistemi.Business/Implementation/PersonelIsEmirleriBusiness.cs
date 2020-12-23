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
using System.Linq.Expressions;
using System.Threading;

namespace PersonelYonetimSistemi.Business.Implementation
{
    public class PersonelIsEmirleriBusiness : IPersonelIsEmirleriBusiness
    {
        #region Variables
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public PersonelIsEmirleriBusiness(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region Customs Methods
        public Result<List<PersonelIsEmirleriVm>> GetAllIsEmirleri()
        {
            var veri = _unitOfWork.personelIsEmirleriRepository.GetAll(includeProperties: "PersonelIsAtama").ToList();

            if(veri != null)
            {
                List<PersonelIsEmirleriVm> returnData = new List<PersonelIsEmirleriVm>();
                foreach (var item in veri)
                {
                    returnData.Add(new PersonelIsEmirleriVm
                    {
                        Id = item.Id,
                        PersonelIsAtamaId = item.PersonelIsAtamaId,
                        PersonelIsAtamaAdi = item.PersonelIsAtama != null ? item.PersonelIsAtama.Email : string.Empty,
                        OlusturulmaTarihi = item.OlusturulmaTarihi,
                        DegistirilmeTarihi = item.DegistirilmeTarihi,
                        IsEmriNumara = item.IsEmriNumara,
                        IsEmriPuan = item.IsEmriPuan,
                        IsEmriAciklamasi = item.IsEmriAciklamasi,
                        IsEmriDurumu = (EnumIsEmriDurumlari)item.IsEmriDurumu,
                        IsEmriDurumuAciklamasi = EnumExtension<EnumIsEmriDurumlari>.GetDisplayValue((EnumIsEmriDurumlari)item.IsEmriDurumu)
                    }); 
                }
                return new Result<List<PersonelIsEmirleriVm>>(true, ResultMesajlari.İsEmriEklendi, returnData.OrderByDescending(x=> x.OlusturulmaTarihi).ToList());
            }
            else
            {
                return new Result<List<PersonelIsEmirleriVm>>(true, ResultMesajlari.İsEmriEklenemedi);
            }
        }

        public Result<PersonelIsEmirleriVm> CreateIsEmirleri(PersonelIsEmirleriVm personelIsEmirleriVm,string uniqueDosyaAdi)
        {
            if (personelIsEmirleriVm != null)
            {
                try
                {
                    PersonelIsEmirleri personelIsEmirleri = new PersonelIsEmirleri();
                    var personelId = SetPersonelAtamaId();
                    personelIsEmirleri.OlusturulmaTarihi = DateTime.Now;
                    personelIsEmirleri.IsEmriAciklamasi = personelIsEmirleriVm.IsEmriAciklamasi;
                    personelIsEmirleri.IsEmriNumara = DateTime.Now.ToShortDateString().Replace(".","") + DateTime.Now.ToShortTimeString().Replace(":","");
                    personelIsEmirleri.IsEmriPuan = personelIsEmirleriVm.IsEmriPuan;
                    personelIsEmirleri.ResimPath = uniqueDosyaAdi;
                    personelIsEmirleri.PersonelIsAtamaId = personelId;
                    personelIsEmirleri.IsEmriDurumu = String.IsNullOrWhiteSpace(personelId) == true ? (int)EnumIsEmriDurumlari.IsEmriOlusturuldu : (int)EnumIsEmriDurumlari.IsAtandı;


                    _unitOfWork.personelIsEmirleriRepository.Add(personelIsEmirleri);
                    _unitOfWork.Save();

                    return new Result<PersonelIsEmirleriVm>(true,ResultMesajlari.İsEmriEklendi,personelIsEmirleriVm);
                }
                catch (Exception ex)
                {
                    return new Result<PersonelIsEmirleriVm>(false, ResultMesajlari.İsEmriEklenemedi + "Hata Mesajı =>" + ex.Message);
                }
            }
            else
            {
                return new Result<PersonelIsEmirleriVm>(false,"Model NULL Gelemez");
            }
        }

        public Result<PersonelIsEmirleriVm> GetIsEmri(int id)
        {
            if (id > 0)
            {
                var isEmri = _unitOfWork.personelIsEmirleriRepository.GetFirstOrDefault(p => p.Id == id, includeProperties: "PersonelIsAtama");
                if(isEmri != null)
                {
                    PersonelIsEmirleriVm personelIsEmirleriVm = new PersonelIsEmirleriVm();
                    personelIsEmirleriVm.PersonelIsAtamaId = isEmri.PersonelIsAtamaId;
                    personelIsEmirleriVm.PersonelIsAtamaAdi = isEmri.PersonelIsAtama != null ? isEmri.PersonelIsAtama.Ad : string.Empty;
                    personelIsEmirleriVm.OlusturulmaTarihi = isEmri.OlusturulmaTarihi;
                    personelIsEmirleriVm.Id = isEmri.Id;
                    personelIsEmirleriVm.DegistirilmeTarihi = isEmri.DegistirilmeTarihi;
                    personelIsEmirleriVm.IsEmriAciklamasi = isEmri.IsEmriAciklamasi;
                    personelIsEmirleriVm.IsEmriPuan = isEmri.IsEmriPuan;
                    personelIsEmirleriVm.IsEmriNumara = isEmri.IsEmriNumara;
                    personelIsEmirleriVm.IsEmriDurumu = (EnumIsEmriDurumlari)isEmri.IsEmriDurumu;
                    personelIsEmirleriVm.IsEmriDurumuAciklamasi = EnumExtension<EnumIsEmriDurumlari>.GetDisplayValue((EnumIsEmriDurumlari)isEmri.IsEmriDurumu);
                    personelIsEmirleriVm.ResimPathText = isEmri.ResimPath;

                    return new Result<PersonelIsEmirleriVm>(true,ResultMesajlari.KayitBulundu,personelIsEmirleriVm);
                }
                else
                {
                    return new Result<PersonelIsEmirleriVm>(false, ResultMesajlari.KayitBulunamadi);
                }
            }
            else
            {
                return new Result<PersonelIsEmirleriVm>(false, ResultMesajlari.KayitBulunamadi);
            }
        }

        public Result<PersonelIsEmirleriVm> EditIsEmri(PersonelIsEmirleriVm personelIsEmirleriVm)
        {
            if (personelIsEmirleriVm.Id > 0)
            {
                var isEmri = _unitOfWork.personelIsEmirleriRepository.Get(personelIsEmirleriVm.Id);
                if (isEmri != null)
                {
                    isEmri.DegistirilmeTarihi = DateTime.Now;
                    isEmri.IsEmriAciklamasi = personelIsEmirleriVm.IsEmriAciklamasi;
                    isEmri.IsEmriPuan = personelIsEmirleriVm.IsEmriPuan;
                    isEmri.IsEmriDurumu = (int)personelIsEmirleriVm.IsEmriDurumu;
                    isEmri.PersonelIsAtamaId = personelIsEmirleriVm.PersonelIsAtamaId;

                    _unitOfWork.personelIsEmirleriRepository.Update(isEmri);
                    _unitOfWork.Save();
                    return new Result<PersonelIsEmirleriVm>(true, ResultMesajlari.KayitBasariylaGuncellendi);
                }
                else
                {
                    return new Result<PersonelIsEmirleriVm>(false, "Lütfen Güncelleme İşlemi İçin Veri Seçiniz");
                }
            }
            else
            {
                return new Result<PersonelIsEmirleriVm>(false, "Lütfen Güncelleme İşlemi İçin Veri Seçiniz");
            }
        }

        public Result<bool> RemoveIsEmri(int id)
        {
            var veri = _unitOfWork.personelIsEmirleriRepository.Get(id);
            if (veri != null)
            {
                _unitOfWork.personelIsEmirleriRepository.Remove(veri);
                _unitOfWork.Save();
                return new Result<bool>(true, ResultMesajlari.KayitBasariylaSilindi);
            }
            else
            {
                return new Result<bool>(true, ResultMesajlari.KayitSilinirkenBirHataOluştu);
            }
        }

        public Result<List<PersonelIsEmirleriVm>> GetIsEmriByPersonelId(string personelId)
        {
            var veri = _unitOfWork.personelIsEmirleriRepository.GetAll(i => i.PersonelIsAtamaId == personelId).ToList();
            if(veri != null)
            {
                List<PersonelIsEmirleriVm> returnData = new List<PersonelIsEmirleriVm>();
                foreach (var item in veri)
                {
                    returnData.Add(new PersonelIsEmirleriVm()
                    {
                        Id = item.Id,
                        PersonelIsAtamaId = item.PersonelIsAtamaId,
                        PersonelIsAtamaAdi = item.PersonelIsAtama != null ? item.PersonelIsAtama.Email : string.Empty,
                        OlusturulmaTarihi = item.OlusturulmaTarihi,
                        DegistirilmeTarihi = item.DegistirilmeTarihi,
                        IsEmriNumara = item.IsEmriNumara,
                        IsEmriPuan = item.IsEmriPuan,
                        IsEmriAciklamasi = item.IsEmriAciklamasi,
                        IsEmriDurumu = (EnumIsEmriDurumlari)item.IsEmriDurumu,
                        IsEmriDurumuAciklamasi = EnumExtension<EnumIsEmriDurumlari>.GetDisplayValue((EnumIsEmriDurumlari)item.IsEmriDurumu)
                    });
                }
                return new Result<List<PersonelIsEmirleriVm>>(true, ResultMesajlari.KayitBulundu, returnData);
            }
            return new Result<List<PersonelIsEmirleriVm>>(false, ResultMesajlari.KayitBulunamadi);
        }


        #endregion

        #region PrivateMethods
        public string SetPersonelAtamaId()
        {
            var getIsEmirleriListesi = _unitOfWork.personelIsEmirleriRepository.GetAll(p => p.PersonelIsAtama.IsAdmin != true && (p.IsEmriDurumu == (int)EnumIsEmriDurumlari.IsAtandı || p.IsEmriDurumu == (int)EnumIsEmriDurumlari.IsUstlenildi) && p.PersonelIsAtamaId != null, includeProperties: "PersonelIsAtama").ToList();

            var veri = getIsEmirleriListesi.GroupBy(p => p.PersonelIsAtamaId).ToList();

            //Kullanıcı İş Emri Toplam Puan
            Dictionary<string, double> personelValue = new Dictionary<string, double>();
            //Personel 
            foreach (var personel in veri)
            {
                double personelPuan = 0;
                foreach (var puan in personel)
                {
                    personelPuan += puan.IsEmriPuan;
                }
                personelValue.Add(personel.Key, personelPuan);
            }

            var puanaGoreSiraliPersoneller = personelValue.OrderBy(p => p.Value).First().Key;
            return puanaGoreSiraliPersoneller;
        }
        #endregion


    }
}
