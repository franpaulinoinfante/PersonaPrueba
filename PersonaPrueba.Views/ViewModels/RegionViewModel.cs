using PersonaPrueba.Domain.Contracts;
using PersonaPrueba.Domain.Models;
using PersonaPrueba.Domain.ObjectValues;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonaPrueba.Views.ViewModels
{
    public class RegionViewModel
    {
        private IRegion _region;
        private EntityState _state;

        public RegionViewModel()
        {
            _region = new RegionModel();
        }

        public int RegionID { get; set; }

        [Required(ErrorMessage = "The field region is requerid")]
        [RegularExpression("^[a-z A-Z]+$")]
        [StringLength(maximumLength: 100, MinimumLength = 3)]
        [Display(Name = "Ciudad", Order = -9, Prompt = "Enter the city", Description = "Emp Ciudad")]
        public string RegionName { get; set; }

        public EntityState State { set => _state = value; }

        public IEnumerable<RegionViewModel> GetRegions()
        {
            List<RegionViewModel> regions = new List<RegionViewModel>();

            var regionModels = _region.GetAll();

            foreach (RegionModel region in regionModels)
            {
                regions.Add(new RegionViewModel
                {
                    RegionID = region.RegionID,
                    RegionName = region.RegionName
                });

            }
            return regions;
        }

        public string SaveChanges()
        {
            SetDataToPropierties();

            return _region.SaveChanges();
        }

        private void SetDataToPropierties()
        {
            RegionModel regionModel = new RegionModel
            {
                RegionID = RegionID,
                RegionName = RegionName
            };
        }
    }
}
