
using AutoMapper;
using RealState.Core.Application.Interfaces.Repositories;
using RealState.Core.Application.Interfaces.Services;
using RealState.Core.Application.ViewModels.Properties;
using RealState.Core.Domain.Entities;

namespace RealState.Core.Application.Services
{
    public class SaleService : GenericService<SaveSalesViewModel, MantSalesViewModel, Sales>, ISaleService
    {
        private readonly ISaleRepository _repository;
        private readonly IMapper _mapper;

        public SaleService(ISaleRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        

    }
}
