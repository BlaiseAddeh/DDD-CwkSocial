using System;
using AutoMapper;
using Cwk.Domain.Aggregates.UseProfileAggregate;
using CwkSocial.Application.UserProfiles.Commands;
using CwkSocial.DAL;
using MediatR;

namespace CwkSocial.Application.UserProfiles.CommandHandlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserProfile>
    {
        private readonly DataContext _ctx;


        public CreateUserCommandHandler(DataContext ctx)
        {
            this._ctx = ctx;
        }

        public async Task<UserProfile> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var basicInfo = BasicInfo.CreateBasicInfo(request.FirstName, request.LastName,
                request.EmailAddress, request.Phone, request.DateOfBirth,
                request.CurrentCity);

            var userProfile = UserProfile.CreateUserProfile(Guid.NewGuid().ToString(), basicInfo);

            _ctx.UserProfiles.Add(userProfile);
            await _ctx.SaveChangesAsync();

            return userProfile;
        }
    }
}

