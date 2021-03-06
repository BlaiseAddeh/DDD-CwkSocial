using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CwkSocial.Api.Contracts.Common;
using CwkSocial.Api.Contracts.UserProfile.Requests;
using CwkSocial.Api.Contracts.UserProfile.Responses;
using CwkSocial.Application.Enums;
using CwkSocial.Application.UserProfiles.Commands;
using CwkSocial.Application.UserProfiles.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CwkSocial.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    [ApiController]
    public class UserProfilesController : BaseController
    {
        private readonly IMediator _mediator;

        private readonly IMapper _mapper;

        public UserProfilesController(IMediator mediator,
            IMapper mapper)
        {
            this._mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProfiles()
        {
            var query = new GetAllUserProfiles();
            var response = await _mediator.Send(query);
            var profiles = _mapper.Map<List<UserProfileResponse>>(response);
            return Ok(profiles);
        }


        [HttpPost]
        public async Task<IActionResult> CreateUserProfile([FromBody] UserProfileCreateUpdate profile)
        {
            // 1- Convertir profile en command
            var command = _mapper.Map<CreateUserCommand>(profile);
            // 2-Envoyer la commande au Médiator
            var response = await _mediator.Send(command); // qui est de type UserProfile
            //3- On veut renvoyer un UserProfileResponse
            var userProfile = _mapper.Map<UserProfileResponse>(response);

            return CreatedAtAction(nameof(GetUserProfileById), new { id = response.UserProfileId }, userProfile);
        }


        [HttpGet]
        [Route(ApiRoutes.UserProfiles.IdRoute)]
        public async Task<IActionResult> GetUserProfileById(string id)
        {
            // Create a command query
            var query = new GetUserProfileById { UserProfileId = Guid.Parse(id) };

            var response = await _mediator.Send(query);

            if (response is null) return NotFound($"No User with profile ID {id} found");

            var userProfile = _mapper.Map<UserProfileResponse>(response);

            return Ok(userProfile);
        }

        [HttpPatch]
        [Route(ApiRoutes.UserProfiles.IdRoute)]
        public async Task<IActionResult> UpdateUserProfile(string id, UserProfileCreateUpdate updatedProfile)
        {
            var command = _mapper.Map<UpdateUserProfileBasicInfo>(updatedProfile);
            command.UserProfileId = Guid.Parse(id);
            var response = await _mediator.Send(command);

            if (response.IsError)
            {
                return HandleErrorResponse(response.Errors);
            }

            return NoContent();
        }

        [HttpDelete]
        [Route(ApiRoutes.UserProfiles.IdRoute)]
        public async Task<IActionResult> DeleteUserProfile(string id)
        {
            var command = new DeleteUserProfile() { UserProfileId = Guid.Parse(id) };
            var response = await _mediator.Send(command);

            return NoContent();
        }

    }
}

