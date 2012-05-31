using System;
using FluentAssertions.EventMonitoring;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SilverlightValidation.Tests.Helpers;

namespace SilverlightValidation.Tests.ViewModels
{
    [TestClass]
    public class UserViewModel_INotifyPropertyChanged
    {
        [TestMethod]
        public void WhenPropertyChanged_WithValidDateOfBirthUpdated_ThenFiresChangeEventAndIsChangedEqualsTrue()
        {
            // arrange
            var vm = Helper.CreateDefaultUserViewModel();

            // act
            vm.DateOfBirth = new DateTime(1977, 01, 01);

            // assert
            vm.ShouldRaisePropertyChangeFor(x => x.DateOfBirth);
            Assert.IsTrue(vm.IsChanged);
        }

        [TestMethod]
        public void WhenPropertyChanged_WithInvalidDateOfBirthUpdated_ThenFiresChangeEventAndIsChangedEqualsTrue()
        {
            // arrange
            var vm = Helper.CreateDefaultUserViewModel();

            // act
            vm.DateOfBirth = new DateTime(1800, 01, 01);

            // assert
            vm.ShouldRaisePropertyChangeFor(x => x.DateOfBirth);
            Assert.IsTrue(vm.IsChanged);
        }

        [TestMethod]
        public void WhenPropertyChanged_WithDescriptionUpdated_ThenFiresChangeEventAndIsChangedEqualsTrue()
        {
            // arrange
            var vm = Helper.CreateDefaultUserViewModel();

            // act
            vm.Description = "New description";

            // assert
            vm.ShouldRaisePropertyChangeFor(x => x.Description);
            Assert.IsTrue(vm.IsChanged);
        }

        [TestMethod]
        public void WhenPropertyChanged_WithValidEmailUpdated_ThenFiresChangeEventAndIsChangedEqualsTrue()
        {
            // arrange
            var vm = Helper.CreateDefaultUserViewModel();

            // act
            vm.Email = "test@domain.com";

            // assert
            vm.ShouldRaisePropertyChangeFor(x => x.Email);
            Assert.IsTrue(vm.IsChanged);
        }

        [TestMethod]
        public void WhenPropertyChanged_WithInvalidEmailUpdated_ThenFiresChangeEventAndIsChangedEqualsTrue()
        {
            // arrange
            var vm = Helper.CreateDefaultUserViewModel();

            // act
            vm.Email = "invalid email";

            // assert
            vm.ShouldRaisePropertyChangeFor(x => x.Email);
            Assert.IsTrue(vm.IsChanged);
        }

        [TestMethod]
        public void WhenPropertyChanged_WithValidUsernameUpdated_ThenFiresChangeEventAndIsChangedEqualsTrue()
        {
            // arrange
            var vm = Helper.CreateDefaultUserViewModel();

            // act
            vm.Username = "dummy";

            // assert
            vm.ShouldRaisePropertyChangeFor(x => x.Username);
            Assert.IsTrue(vm.IsChanged);
        }

        [TestMethod]
        public void WhenPropertyChanged_WithInvalidUsernameUpdated_ThenFiresChangeEventAndIsChangedEqualsTrue()
        {
            // arrange
            var vm = Helper.CreateDefaultUserViewModel();

            // act
            vm.Username = "invalidusernametoolong";

            // assert
            vm.ShouldRaisePropertyChangeFor(x => x.Username);
            Assert.IsTrue(vm.IsChanged);
        }

        [TestMethod]
        public void WhenPropertyChanged_WithValidPasswordUpdated_ThenFiresChangeEventAndIsChangedEqualsTrue()
        {
            // arrange
            var vm = Helper.CreateDefaultUserViewModel();

            // act
            vm.Password = "dummy";

            // assert
            vm.ShouldRaisePropertyChangeFor(x => x.Password);
            Assert.IsTrue(vm.IsChanged);
        }

        [TestMethod]
        public void WhenPropertyChanged_WithInvalidPasswordUpdated_ThenFiresChangeEventAndIsChangedEqualsTrue()
        {
            // arrange
            var vm = Helper.CreateDefaultUserViewModel();

            // act
            vm.Password = "invalidpasswordtoolong";

            // assert
            vm.ShouldRaisePropertyChangeFor(x => x.Password);
            Assert.IsTrue(vm.IsChanged);
        }
    }
}
