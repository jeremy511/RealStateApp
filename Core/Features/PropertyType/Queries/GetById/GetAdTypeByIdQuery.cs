using AutoMapper;
using RealState.Core.Application.Interfaces.Repositories;
using MediatR;
using RealState.Core.Application.Interfaces.Services;
using RealState.Core.Application.ViewModels.Api;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealState.Core.Application.Features.PropertyType.Queries.GetById
{
    public class GetAdTypeByIdQuery : IRequest<TypeViewModel>
    {
        /// <example>1 </example>
        [SwaggerParameter(Description = "Id del tipo de propiedad")]
        public int Id { get; set; }

    }

    public class GetAdTypeByIdQueryHandler : IRequestHandler<GetAdTypeByIdQuery, TypeViewModel>
    {
        private readonly IAdsTypeRepository _adsRepository;
        private readonly IMapper _mapper;

        public GetAdTypeByIdQueryHandler(IAdsTypeRepository adsRepository, IMapper mapper)
        {
            _adsRepository = adsRepository;
            _mapper = mapper;

        }

        public async Task<TypeViewModel> Handle(GetAdTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var propertyTypeViewModel = await GetByIdTypeViewModel(request.Id);
            return propertyTypeViewModel;
        }

        private async Task<TypeViewModel> GetByIdTypeViewModel(int id)
        {
            var ad = await _adsRepository.GetViewModelById(id);


            TypeViewModel ads = new()
            {
                Id = ad.Id,
                Description = ad.Description,
                Name = ad.Name,
            };

            return ads;
        }
    }
}
