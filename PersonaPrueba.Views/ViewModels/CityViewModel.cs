using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonaPrueba.Domain.Contracts;
using PersonaPrueba.Domain.Models;
using PersonaPrueba.Domain.ObjectValues;

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

        [Required(ErrorMessage = "The field Region is requerid")]
        public int CityID { get; set; }

        [Required(ErrorMessage = "The field Region is requerid")]
        public int RegionID { get; set; }

        [Required(ErrorMessage = "The field city is requerid")]
        [RegularExpression("^[a-z A-Z]+$")]
        [StringLength(maximumLength:100,MinimumLength =3)]
        public string CityName { get; set; }

        public string RegionName { get; set; }

        public EntityState State { get; set; }

        public IEnumerable<CityViewModel> GetCities()
        {
            List<CityViewModel> cities = new List<CityViewModel>();

            var cityModel = _city.GetAll();

            foreach (var city in cityModel)
            {
                var item = new CityViewModel
                {
                    CityID = city.CityID,
                    RegionID = city.RegionID,
                    CityName = city.CityName,
                    RegionName = city.RegionName
                };
                cities.Add(item);
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

        private void SetDataToPropierties()
        {
            _city.State = State;
            _city.CityID = CityID;
            _city.RegionID = RegionID;
            _city.CityName = CityName;
        }
    }
}
