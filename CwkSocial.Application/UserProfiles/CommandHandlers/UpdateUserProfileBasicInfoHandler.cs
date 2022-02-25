using System;
using Cwk.Domain.Aggregates.UseProfileAggregate;
using CwkSocial.Application.UserProfiles.Commands;
using CwkSocial.DAL;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CwkSocial.Application.UserProfiles.CommandHandlers
{
    public class UpdateUserProfileBasicInfoHandler : IRequestHandler<UpdateUserProfileBasicInfo>
    {
        private readonly DataContext _ctx;

        public UpdateUserProfileBasicInfoHandler(DataContext ctx)
        {
            this._ctx = ctx;
        }

        public async Task<Unit> Handle(UpdateUserProfileBasicInfo request,
            CancellationToken cancellationToken)
        {
            var userProfile = await _ctx.UserProfiles
                .FirstOrDefaultAsync(up => up.UserProfileId == request.UserProfileId);

            var basicInfo = BasicInfo.CreateBasicInfo(request.FirstName, request.LastName,
                request.EmailAddress, request.Phone, request.DateOfBirth, request.CurrentCity);

            userProfile.UpdateBasicInfo(basicInfo);

            _ctx.UserProfiles.Update(userProfile);
            await _ctx.SaveChangesAsync();

            return new Unit();
        }
    }
}

