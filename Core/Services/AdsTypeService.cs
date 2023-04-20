
using AutoMapper;
using RealState.Core.Application.Interfaces.Repositories;
using RealState.Core.Application.Interfaces.Services;
using RealState.Core.Application.ViewModels.Properties;
using RealState.Core.Domain.Entities;

namespace RealState.Core.Application.Services
{
    public class AdsTypeService : GenericService<SavePropertyTypeViewModel, MantPropertyViewModel, AdsType>, IAdsTypeService
    {
        private readonly IAdsTypeRepository _repository;
        private readonly IMapper _mapper;

        public AdsTypeService(IAdsTypeRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        

    }
}
