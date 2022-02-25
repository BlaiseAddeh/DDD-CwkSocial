using System;
using AutoMapper;
using Cwk.Domain.Aggregates.UseProfileAggregate;
using CwkSocial.Application.UserProfiles.Commands;

namespace CwkSocial.Application.MappingProfiles
{
    internal class UserProfileMap : Profile
    {
        public UserProfileMap()
        {
            CreateMap<CreateUserCommand, BasicInfo>();
        }
    }
}

