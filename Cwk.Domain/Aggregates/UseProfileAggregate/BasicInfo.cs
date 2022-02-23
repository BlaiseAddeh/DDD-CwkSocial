using System;
namespace Cwk.Domain.Aggregates.UseProfileAggregate
{
    public class BasicInfo
    {
        private BasicInfo()
        {
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string EmailAddress { get; private set; }
        public string Phone { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public string CurrentCity { get; private set; }

        // Factory design pattern

        public static BasicInfo CreateBasicInfo(
          string firstName,
          string lastName,
          string emailAddress,
          string phone,
          DateTime dateOfBirth,
          string currentCity)
        {
            // TODO: implement validation, error handling strategis, error notification strategis

            return new BasicInfo
            {
                FirstName = firstName,
                LastName = lastName,
                EmailAddress = emailAddress,
                Phone = phone,
                DateOfBirth = dateOfBirth,
                CurrentCity = currentCity
            };

        }
    }
}

