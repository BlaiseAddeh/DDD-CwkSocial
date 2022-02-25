using System;
using AutoMapper;
using Cwk.Domain.Aggregates.UseProfileAggregate;
using CwkSocial.Api.Contracts.UserProfile.Requests;
using CwkSocial.Api.Contracts.UserProfile.Responses;
using CwkSocial.Application.UserProfiles.Commands;

namespace CwkSocial.Api.MappingProfiles
{
    public class UserProfileMapping : Profile
    {
        public UserProfileMapping()
        {
            // Syntax CreateMap<Source, Destination>()
            CreateMap<UserProfileCreateUpdate, CreateUserCommand>(); // Create
            CreateMap<UserProfile, UserProfileResponse>();  // Get
            CreateMap<BasicInfo, BasicInformation>();       // Get
            CreateMap<UserProfileCreateUpdate, UpdateUserProfileBasicInfo>();  // Update
        }
    }
}

