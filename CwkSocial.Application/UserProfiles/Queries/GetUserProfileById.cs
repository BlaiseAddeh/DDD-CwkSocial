﻿using System;
using Cwk.Domain.Aggregates.UseProfileAggregate;
using MediatR;

namespace CwkSocial.Application.UserProfiles.Queries
{
    public class GetUserProfileById : IRequest<UserProfile>
    {
        public Guid UserProfileId { get; set; }
    }
}

