using System;
using System.Collections;
using System.Collections.Generic;
using PersonaPrueba.DataAccess.Repository.Contracts;
using PersonaPrueba.DataAccess.Repository.Entities;
using PersonaPrueba.DataAccess.Repository.Repositories;
using PersonaPrueba.Domain.Contracts;
using PersonaPrueba.Domain.ObjectValues;

namespace PersonaPrueba.Domain.Models
{
    public class CityModel : ICity
    {
        private EntityState _state;
        private ICityRepository _cityRepository;
        CityEntity _cityEntity;

        public CityModel()
        {
            _cityRepository = new CityRepository();
            _cityEntity = new CityEntity();
        }

        public int CityID { get; set; }
        public int RegionID { get; set; }
        public string CityName { get; set; }
        public EntityState State { set => _state = value; }

        public IEnumerable<CityModel> GetAll()
        {
            List<CityModel> cityModels = new List<CityModel>();

            var models = _cityRepository.GetAll();

            foreach (CityEntity city in models)
            {
                var model = new CityModel()
                {
                    CityID = city.CityID,
                    RegionID = city.RegionID,
                    CityName = city.CityName
                };

                cityModels.Add(model);
            }

            return cityModels;
        }

        public string SaveChanges()
        {
            string message = string.Empty;

            SetDataToEntity();

            try
            {
                switch (_state)
                {
                    case EntityState.Added:
                        CityID = _cityRepository.Add(_cityEntity);
                        message = $"Successfully recorded. \nThe new CityID is : {CityID}";
                        break;
                    case EntityState.Edited:
                        _cityRepository.Edit(_cityEntity);
                        message = $"Successfuly edited. \nThe edited CityID is : {CityID}";
                        break;
                    case EntityState.Deleted:
                        _cityRepository.Delete(_cityEntity);
                        message = $"Successfuly deleted. \nThe deleted CityID is : {CityID}";
                        break;
                    default:
                        message = "Select a operation";
                        break;
                }

            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return message;
        }

        private void SetDataToEntity()
        {
            _cityEntity.CityID = CityID;
            _cityEntity.RegionID = RegionID;
            _cityEntity.CityName = CityName;
        }
    }
}
