using AutoMapper;
using PersonelYonetimSistemi.Business.Contracts;
using PersonelYonetimSistemi.Common.ResultMesajlari;
using PersonelYonetimSistemi.Common.ResultMesajModels;
using PersonelYonetimSistemi.Common.ResultModels;
using PersonelYonetimSistemi.Data.Contracts;
using PersonelYonetimSistemi.Data.Models;
using System;

namespace PersonelYonetimSistemi.Business.Implementation
{
    public class PersonelIzinAtamaBusiness : IPersonelIzinAtamaBusiness
    {
        #region Variables
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public PersonelIzinAtamaBusiness(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region CustomMethods
        public Result<bool> OnaylaPersonelIzin(int id)
        {
            if(id > 0)
            {
                try
                {
                    var veri = _unitOfWork.personelIzinOnayRepository.GetFirstOrDefault(p => p.Id == id);
                    if (veri != null)
                    {
                        PersonelIzin personelIzin = new PersonelIzin();
                        personelIzin.OlusturulmaTarihi = DateTime.Now;
                        personelIzin.PersonelId = veri.PersonelTalepId;
                        personelIzin.personelIzinTipiId = veri.PersonelIzinTipiId;
                        var gun = (veri.IzinBitisTarihi - veri.IzinBaslangicTarihi).Days;
                        personelIzin.GunSayisi = gun < 0 ? -gun : gun;
                        personelIzin.Period = 1;
                        _unitOfWork.personelIzinRepository.Add(personelIzin);
                    }
                    veri.OnaylandiMi = (int)EnumPersonelIzinOnayDurum.Onaylandi;
                    _unitOfWork.personelIzinOnayRepository.Update(veri);
                    _unitOfWork.Save();
                    return new Result<bool>(true, ResultMesajlari.IzinOnaylandı);
                }
                catch (Exception ex)
                {
                    return new Result<bool>(false, ResultMesajlari.IzinOnaylandıHata + "=>" + ex.Message.ToString());
                }
            }
            else
            {
                return new Result<bool>(false, "Parametre olarak verilen veri boş bırakılamaz.");
            }
        }
        #endregion

    }
}
