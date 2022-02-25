using System;
using Cwk.Domain.Aggregates.UseProfileAggregate;
using MediatR;

namespace CwkSocial.Application.UserProfiles.Queries
{
    public class GetAllUserProfiles : IRequest<IEnumerable<UserProfile>>
    {

    }
}

