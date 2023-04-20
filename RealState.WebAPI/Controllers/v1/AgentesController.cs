using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealState.Core.Application.Features.Ads.Queries.GetAllAds;
using RealState.Core.Application.Features.Agent.Commands;
using RealState.Core.Application.Features.Agent.Queries.GetAllAgent;
using RealState.Core.Application.Features.Agent.Queries.GetById;
using RealState.Core.Application.ViewModels.Agents;
using RealState.Core.Application.ViewModels.Api;
using Swashbuckle.AspNetCore.Annotations;

using System.Net.Mime;

namespace RealState.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [SwaggerTag("Manejo de Agentes")]

    public class AgentesController : BaseApiController
    {
        [HttpGet]
        [SwaggerOperation(
            Summary = "Listado de Agente",
            Description = "Devuelve una lista de todos los agentes registrados"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AgentApiViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Consumes(MediaTypeNames.Application.Json)]

        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin, Developer")]

        public async Task<IActionResult> List()
        {

            try
            {
                return Ok(await Mediator.Send(new GetAllAgentsQuery()));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Agente filtrado por ID",
            Description = "Devuelve un agente en especifico."
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AgentApiViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]

        [Authorize(Roles = "Admin, Developer")]

        public async Task<IActionResult> GetAgentID(string id)
        {

            try
            {
                return Ok(await Mediator.Send(new GetAgentByIdQuery { Id = id}));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }





      



        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Activar / Desactivar Agente",
            Description = "Activa y desactiva un agente especificado."
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]

        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> ChangeStatus(string id, [FromBody] ChangeStatusCommand command)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                if (id != command.Id)
                {
                    return BadRequest();
                }
                return Ok(await Mediator.Send(command));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


    }
}
