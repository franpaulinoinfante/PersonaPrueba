using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonaPrueba.Domain.Contracts;
using PersonaPrueba.Domain.Models;

namespace PersonaPrueba.Views.ViewModels
{
    public class CityViewModel
    {
        private ICity _city;
        private IRegion _region;

        public CityViewModel()
        {
            _city = new CityModel();
            _region = new RegionModel();
        }

        public int CityID { get; set; }
        public int RegionID { get; set; }
        public string CityName { get; set; }
        public string RegionName { get; set; }

        public IEnumerable<CityViewModel> GetCities()
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
        }

        public IEnumerable<RegionModel> GetRegionModels()
        {
            return _region.GetAll();
        }

        public string SaveChanges()
        {
            SetDataToPropierties();

            return _city.SaveChanges();
        }

        public void SetDataToPropierties()
        {
            _city.CityID = CityID;
            _city.RegionID = RegionID;
            _city.CityName = CityName;
        }
    }
}
