using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealState.Core.Application.Features.Ads.Queries.GetAllAds;
using RealState.Core.Application.Features.Ads.Queries.GetByCode;
using RealState.Core.Application.Features.Ads.Queries.GetById;
using RealState.Core.Application.ViewModels.Api;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RealState.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize(Roles = "Admin, Developer")]
    [SwaggerTag("Manejo las Propiedades")]


    public class PropiedadesController : BaseApiController
    {
        [HttpGet]
        [SwaggerOperation(
            Summary = "Listado de Propiedades",
            Description = "Devuelve las propiedades registradas en el sistema"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropertyViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Consumes(MediaTypeNames.Application.Json)]

        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> List()
        {
           
            try
            {
                return Ok(await Mediator.Send(new GetAllAdsQuery()));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Propiedad",
            Description = "Devuelve una propiedad en especifica."
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropertyViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Consumes(MediaTypeNames.Application.Json)]

        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {

            try
            {
                return Ok(await Mediator.Send(new GetAdByIdQuery { Id= id}));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{code}")]
             [SwaggerOperation(
            Summary = "Propiedad",
            Description = "Devuelve una propiedad en especifica que tengo el identificador"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropertyViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Consumes(MediaTypeNames.Application.Json)]

        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByCode(string code)
        {

            try
            {
                return Ok(await Mediator.Send(new GetByCodeQuery { Code= code}));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
