
using AutoMapper;
using RealState.Core.Application.Interfaces.Repositories;
using RealState.Core.Application.Interfaces.Services;
using RealState.Core.Application.ViewModels.Properties;
using RealState.Core.Domain.Entities;

namespace RealState.Core.Application.Services
{
    public class AdsUpgrateService : GenericService<SaveUpgradeViewModel, MantUpgradesViewModel, AdsUpgrates>, IAdsUpgrateService
    {
        private readonly IAdsUpgrateRepository _repository;
        private readonly IMapper _mapper;

        public AdsUpgrateService(IAdsUpgrateRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        

    }
}
