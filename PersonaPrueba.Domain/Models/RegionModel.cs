using PersonaPrueba.Domain.ObjectValues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonaPrueba.DataAccess.Repository.Contracts;
using PersonaPrueba.DataAccess.Repository.Entities;
using PersonaPrueba.DataAccess.Repository.Repositories;
using PersonaPrueba.Domain.Contracts;

namespace PersonaPrueba.Domain.Models
{
    public class RegionModel : IRegion
    {
        private EntityState _state;
        private IRegionRepository _regionRepository;
        private RegionEntity _entity;

        public RegionModel()
        {
            _regionRepository = new RegionRepository();
            _entity = new RegionEntity();
        }

        public int RegionID { get; set; }
        public string RegionName { get; set; }
        public EntityState State { set => _state = value; }

        public IEnumerable<RegionModel> GetAll()
        {
            List<RegionModel> regionModels = new List<RegionModel>();

            var models = _regionRepository.GetAll();

            foreach (RegionEntity entity in models)
            {
                var model = new RegionModel
                {
                    RegionID = entity.RegionID,
                    RegionName = entity.RegionName
                };

                regionModels.Add(model);
            }

            return regionModels;
        }

        public string SaveChanges()
        {
            string message = string.Empty;

            try
            {
                SetDataToPropierties();

                switch (_state)
                {
                    case EntityState.Added:
                        RegionID = _regionRepository.Add(_entity);
                        message = $"Successfuly record. \nThe new Id is: {RegionID}";
                        break;
                    case EntityState.Deleted:
                        _regionRepository.Delete(RegionID);
                        message = $"Successfuly Deleted. \nThe region delete is: {RegionName}";
                        break;
                    case EntityState.Edited:
                        _regionRepository.Edit(_entity);
                        message = $"Successfuly edited. \nThe region edite is: {RegionName}";
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

        private void SetDataToPropierties()
        {
            _entity.RegionID = RegionID;
            _entity.RegionName = RegionName;
        }
    }
}
