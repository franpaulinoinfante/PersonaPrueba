using System;
using System.Collections.Generic;
using PersonaPrueba.DataAccess.Repository.Contracts;
using PersonaPrueba.DataAccess.Repository.Entities;
using PersonaPrueba.DataAccess.Repository.Repositories;
using PersonaPrueba.Domain.Contracts;
using PersonaPrueba.Domain.ObjectValues;


namespace PersonaPrueba.Domain.Models
{
    public class RegionModel : IRegion
    {
        private EntityState _state;
        private readonly IRegionRepository _regionRepository;
        private readonly RegionEntity _entity;

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
            string message;

            try
            {
                SetDataToEntity();

                switch (_state)
                {
                    case EntityState.Added:
                        RegionID = _regionRepository.Add(_entity);
                        message = $"Successfully recorded. \nThe new Id is: {RegionID}";
                        break;
                    case EntityState.Deleted:
                        _regionRepository.Delete(_entity);
                        message = $"Successfuly Deleted. \nThe region delete is: {RegionName}";
                        break;
                    case EntityState.Edited:
                        _regionRepository.Edit(_entity);
                        message = $"Successfuly edited. \nThe region edit is: {RegionName}";
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
            _entity.RegionID = RegionID;
            _entity.RegionName = RegionName;
        }
    }
}
