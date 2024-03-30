using System;

namespace LegacyApp
{
    public class UserService
    {
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (IsFirstNameValid(firstName) || IsLastNameValid(lastName))
            {
                return false;
            }

            if (IsEmailValid(email))
            {
                return false;
            }

            var age = CalculateAgeUsingBirthdate(dateOfBirth);

            if (IsAdult(age))
            {
                return false;
            }

            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };

            if (IsVeryImportantClient(client))
            {
                user.HasCreditLimit = false;
            }
            else if (IsImportantClient(client))
            {
                using (var userCreditService = new UserCreditService())
                {
                    DoubleCreditLimit(userCreditService, user);
                }
            }
            else
            {
                user.HasCreditLimit = true;
                using (var userCreditService = new UserCreditService())
                {
                    int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    user.CreditLimit = creditLimit;
                }
            }

            if (VerifyCreditLimitBelow500(user))
            {
                return false;
            }

            UserDataAccess.AddUser(user);
            return true;
        }

        private static bool VerifyCreditLimitBelow500(User user)
        {
            return user.HasCreditLimit && user.CreditLimit < 500;
        }

        private static bool IsImportantClient(Client client)
        {
            return client.Type == "ImportantClient";
        }

        private static bool IsVeryImportantClient(Client client)
        {
            return client.Type == "VeryImportantClient";
        }

        private static void DoubleCreditLimit(UserCreditService userCreditService, User user)
        {
            var creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
            creditLimit = creditLimit * 2;
            user.CreditLimit = creditLimit;
        }

        private static bool IsAdult(int age)
        {
            return age < 21;
        }

        private static int CalculateAgeUsingBirthdate(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;
            return age;
        }

        private static bool IsEmailValid(string email)
        {
            return !email.Contains('@') && !email.Contains('.');
        }

        private static bool IsLastNameValid(string lastName)
        {
            return string.IsNullOrEmpty(lastName);
        }

        private static bool IsFirstNameValid(string firstName)
        {
            return string.IsNullOrEmpty(firstName);
        }
    }
}
