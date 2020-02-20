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

        [Display(Name ="ID",Order = -9)]
        public int CityID { get; set; }

        [Required(ErrorMessage = "The field Region is requerid")]
        [RegularExpression("[0-9]+", ErrorMessage = "The RegionID can be only numbers")]
        public int RegionID { private get; set; }

        [Required(ErrorMessage = "The field city is requerid")]
        [RegularExpression("^[a-z A-Z]+$")]
        [StringLength(maximumLength: 100, MinimumLength = 3)]
        [Display(Name = "Ciudad", Order = -9,Prompt ="Enter the city" ,Description = "Emp Ciudad")]
        public string CityName { get; set; }

        [Display(Name = "Region")]
        public string RegionName { get; set; }

        public EntityState State { private get; set; }

        public IEnumerable<CityViewModel> GetCities()
        {
            var cities = new List<CityViewModel>();

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
            CityModel model = new CityModel
            {
                State = State,
                CityID = CityID,
                RegionID = RegionID,
                CityName = CityName
            };
        }
    }
}
