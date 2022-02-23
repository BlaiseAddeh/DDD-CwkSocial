using System;
namespace Cwk.Domain.Aggregates.UseProfileAggregate
{
    public class UserProfile
    {
        private UserProfile()
        {
        }
        public Guid UserProfileId { get; private set; }

        public string IdentityId { get; private set; } // Foreign key from Identity database

        public BasicInfo BasicInfo { get; private set; }

        public DateTime DateCreated { get; private set; }
        public DateTime LastModified { get; private set; }


        // Factory design patern

        public static UserProfile CreateUserProfile(string identityId, BasicInfo basicInfo)
        {
            //TODO: add validation, error handling strategies, error notification strategies


            return new UserProfile
            {
                IdentityId = identityId,
                BasicInfo = basicInfo,
                DateCreated = DateTime.UtcNow,
                LastModified = DateTime.UtcNow
            };
        }

        // Public methods

        public void UpdateBasicInfo(BasicInfo newInfo)
        {
            BasicInfo = newInfo;
            LastModified = DateTime.UtcNow;
        }

    }
}

