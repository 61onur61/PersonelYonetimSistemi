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
    public class PersonelBusiness : IPersonelBusiness
    {
        #region Variables
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public PersonelBusiness(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region CustomMethods
        public Result<List<PersonelVM>> GetAllPersonel()
        {
            var veri = _unitOfWork.personelRepository.GetAll();
            if (veri != null)
            {
                List<PersonelVM> ListData = new List<PersonelVM>();
                foreach (var item in veri)
                {
                    ListData.Add(new PersonelVM
                    {
                        Ad = item.Ad,
                        Soyad = item.Soyad,
                        Email = item.Email,
                        DogumGunu = item.DogumGunu,
                        UserName = item.UserName,
                        PhoneNumber = item.PhoneNumber,
                        Id = item.Id,
                        MaasId = item.MaasId
                    });
                }

                return new Result<List<PersonelVM>>(true, ResultMesajlari.KayitBulundu, ListData);
            }
            else
            {
                return new Result<List<PersonelVM>>(false, ResultMesajlari.KayitBulunamadi);
            }
        } 
        #endregion




    }
}
