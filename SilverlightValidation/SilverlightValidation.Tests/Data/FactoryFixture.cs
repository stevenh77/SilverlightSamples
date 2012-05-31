using Microsoft.VisualStudio.TestTools.UnitTesting;
using SilverlightValidation.Data;
using FluentAssertions;

namespace SilverlightValidation.Tests.Data
{
    [TestClass]
    public class FactoryFixture
    {
        [TestMethod]
        public void WhenCreateUserModels_ThenFiveModelsWithUsernameAndPasswordAndEmailSet()
        {
            const int expectedCount = 5;
            var userModels = Factory.CreateUserModels();

            foreach (var userModel in userModels)
            {
                userModel.Username.Should().NotBeNullOrEmpty();
                userModel.Password.Should().NotBeNullOrEmpty();
                userModel.Email.Should().NotBeNullOrEmpty();
                userModel.DateOfBirth.Should().HaveValue();
            }

            userModels.Should().HaveCount(expectedCount);
        }

    }
}
