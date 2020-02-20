using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly CityEntity _cityEntity;
        private readonly ICityRepository _cityRepository;
        private readonly IRegion _region;

        public CityModel()
        {
            _cityRepository = new CityRepository();
            _cityEntity = new CityEntity();
            _region = new RegionModel();
        }

        public int CityID { get; set; }
        public int RegionID { get; set; }
        public string CityName { get; set; }
        public string RegionName { get; set; }

        public EntityState State { set => _state = value; }

        /*  public IEnumerable<CityViewModel> GetCities()
          {
              List<CityViewModel> cities = new List<CityViewModel>();

              var cityViews = (
                  from getCities in _city.GetAll()
                  join getRegions in _region.GetAll()
                      on getCities.RegionID equals getRegions.RegionID
                  select new
                  {
                      getRegions.RegionID,
                      getRegions.RegionName,
                      getCities.CityID,
                      getCities.CityName
                  });

              foreach (var city in cityViews)
              {
                  cities.Add(new CityViewModel()
                  {
                      RegionID = city.RegionID,
                      RegionName = city.RegionName,
                      CityID = city.CityID,
                      CityName = city.CityName
                  });
              }

              return cities;
          }*/

        public IEnumerable<CityModel> GetAll()
        {
            List<CityModel> cities = new List<CityModel>();

            var cityModel = _cityRepository.GetAll();

            var cityViews = (
                from getCities in cityModel
                join getRegions in _region.GetAll() 
                    on getCities.RegionID equals getRegions.RegionID
                select new
                {
                    getRegions.RegionID,
                    getRegions.RegionName,
                    getCities.CityID,
                    getCities.CityName
                });

            foreach (var city in cityViews)
            {
                cities.Add(new CityModel()
                {
                    RegionID = city.RegionID,
                    RegionName = city.RegionName,
                    CityID = city.CityID,
                    CityName = city.CityName
                });
            }

            return cities;
        }

        public string SaveChanges()
        {
            string message;

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

        //private IEnumerable<CityModel> GetCityModels()
        //{
        //    List<CityModel> cityModels = new List<CityModel>();

        //    var models = _cityRepository.GetAll();

        //    foreach (CityEntity city in models)
        //    {
        //        var model = new CityModel()
        //        {
        //            CityID = city.CityID,
        //            RegionID = city.RegionID,
        //            CityName = city.CityName
        //        };

        //        cityModels.Add(model);
        //    }

        //    return cityModels;
        //}


    }
}
