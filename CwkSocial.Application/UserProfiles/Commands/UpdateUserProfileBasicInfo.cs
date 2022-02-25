using System;
using MediatR;

namespace CwkSocial.Application.UserProfiles.Commands
{
    public class UpdateUserProfileBasicInfo : IRequest // On ne veut rien retourner
    {
        public Guid UserProfileId { get; set; } // On en a besoin pour faire le Update efficacement
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string CurrentCity { get; set; }

    }
}

