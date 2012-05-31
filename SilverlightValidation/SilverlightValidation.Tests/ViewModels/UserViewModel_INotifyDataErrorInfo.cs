using System;
using FluentAssertions;
using SilverlightValidation.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SilverlightValidation.Tests.ViewModels
{
    [TestClass]
    public class UserViewModel_INotifyDataErrorInfo
    {
        [TestMethod]
        public void WhenValidVM_WithUpdatedUsername_WithValidData_ThenErrorsForUsernameEmpty()
        {
            // arrange
            var vm = Helper.CreateValidUserViewModel();

            // act
            var validData = "valid";
            vm.Username = validData;

            // assert
            Assert.IsFalse(vm.HasErrors);
            vm.GetErrors("Username").Should().BeNull();
        }

        [TestMethod]
        public void WhenValidVM_WithUpdatedUsername_WithInvalidEmptyData_ThenErrorsForUsernameEqualsOne()
        {
            // arrange
            var vm = Helper.CreateValidUserViewModel();

            // act
            vm.Username = string.Empty;

            // assert
            Assert.IsTrue(vm.HasErrors);
            vm.GetErrors("Username").Should().HaveCount(1);
        }

        [TestMethod]
        public void WhenValidVM_WithUpdatedUsername_WithInvalidShortData_ThenErrorsForUsernameEqualsOne()
        {
            // arrange
            var vm = Helper.CreateValidUserViewModel();

            // act
            var shortData = "a";
            vm.Username = shortData;

            // assert
            Assert.IsTrue(vm.HasErrors);
            vm.GetErrors("Username").Should().HaveCount(1);
        }

        [TestMethod]
        public void WhenValidVM_WithUpdatedUsername_WithInvalidLongData_ThenErrorsForUsernameEqualsOne()
        {
            // arrange
            var vm = Helper.CreateValidUserViewModel();

            // act
            vm.Username = "thisdatashouldbetoolong";

            // assert
            Assert.IsTrue(vm.HasErrors);
            vm.GetErrors("Username").Should().HaveCount(1);
        }

        [TestMethod]
        public void WhenValidVM_WithUpdatedPassword_WithValidData_ThenErrorsForPasswordEmpty()
        {
            // arrange
            var vm = Helper.CreateValidUserViewModel();

            // act
            var validPassword = "Pa33word";
            vm.Password = validPassword;

            // assert
            Assert.IsFalse(vm.HasErrors);
            vm.GetErrors("Password").Should().BeNull();
        }

        [TestMethod]
        public void WhenValidVM_WithUpdatedPassword_WithInvalidEmptyData_ThenErrorsForPasswordEqualsOne()
        {
            // arrange
            var vm = Helper.CreateValidUserViewModel();

            // act
            vm.Password = string.Empty;

            // assert
            Assert.IsTrue(vm.HasErrors);
            vm.GetErrors("Password").Should().HaveCount(1);
        }

        [TestMethod]
        public void WhenValidVM_WithUpdatedPassword_WithInvalidDataNotContainingANumber_ThenErrorsForPasswordEqualsOne()
        {
            // arrange
            var vm = Helper.CreateValidUserViewModel();

            // act
            vm.Password = "Password";

            // assert
            Assert.IsTrue(vm.HasErrors);
            vm.GetErrors("Password").Should().HaveCount(1);
        }

        [TestMethod]
        public void WhenValidVM_WithUpdatedPassword_WithInvalidDataNotContainingAnUpperCaseLetter_ThenErrorsForPasswordEqualsOne()
        {
            // arrange
            var vm = Helper.CreateValidUserViewModel();

            // act
            vm.Password = "pa33word";

            // assert
            Assert.IsTrue(vm.HasErrors);
            vm.GetErrors("Password").Should().HaveCount(1);
        }

        [TestMethod]
        public void WhenValidVM_WithUpdatedPassword_WithInvalidDataNotContainingALowerCaseLetter_ThenErrorsForPasswordEqualsOne()
        {
            // arrange
            var vm = Helper.CreateValidUserViewModel();

            // act
            vm.Password = "PA33WORD";

            // assert
            Assert.IsTrue(vm.HasErrors);
            vm.GetErrors("Password").Should().HaveCount(1);
        }

        [TestMethod]
        public void WhenValidVM_WithUpdatedEmail_WithValidData_ThenErrorsForEmailEmpty()
        {
            // arrange
            var vm = Helper.CreateValidUserViewModel();

            // act
            var validData = "valid@email.com";
            vm.Email = validData;

            // assert
            Assert.IsFalse(vm.HasErrors);
            vm.GetErrors("Email").Should().BeNull();
        }

        [TestMethod]
        public void WhenValidVM_WithUpdatedEmail_WithInvalidEmptyData_ThenErrorsForEmailEqualsOne()
        {
            // arrange
            var vm = Helper.CreateValidUserViewModel();

            // act
            vm.Email = string.Empty;

            // assert
            Assert.IsTrue(vm.HasErrors);
            vm.GetErrors("Email").Should().HaveCount(1);
        }

        [TestMethod]
        public void WhenValidVM_WithUpdatedEmail_WithInvalidDataNoAtSign_ThenErrorsForEmailEqualsOne()
        {
            // arrange
            var vm = Helper.CreateValidUserViewModel();

            // act
            var noAtSign = "invalidemail.com";
            vm.Email = noAtSign;

            // assert
            Assert.IsTrue(vm.HasErrors);
            vm.GetErrors("Email").Should().HaveCount(1);
        }

        [TestMethod]
        public void WhenValidVM_WithUpdatedEmail_WithInvalidDataNothingBeforeAtSign_ThenErrorsForEmailEqualsOne()
        {
            // arrange
            var vm = Helper.CreateValidUserViewModel();

            // act
            var nothingBeforeAtSign = "@email.com";
            vm.Email = nothingBeforeAtSign;

            // assert
            Assert.IsTrue(vm.HasErrors);
            vm.GetErrors("Email").Should().HaveCount(1);
        }

        [TestMethod]
        public void WhenValidVM_WithUpdatedEmail_WithInvalidDataNoDotAfterAtSign_ThenErrorsForEmailEqualsOne()
        {
            // arrange
            var vm = Helper.CreateValidUserViewModel();

            // act
            vm.Email = "invalid@email";

            // assert
            Assert.IsTrue(vm.HasErrors);
            vm.GetErrors("Email").Should().HaveCount(1);
        }

        [TestMethod]
        public void WhenValidVM_WithUpdatedDateOfBirth_WithValidData_ThenErrorsForDateOfBirthEmpty()
        {
            // arrange
            var vm = Helper.CreateValidUserViewModel();

            // act
            var validData = new DateTime(1977, 01, 01);
            vm.DateOfBirth = validData;

            // assert
            Assert.IsFalse(vm.HasErrors);
            vm.GetErrors("DateOfBirth").Should().BeNull();

            vm.GetErrors("Username").Should().BeNull();
            vm.GetErrors("Password").Should().BeNull();
            vm.GetErrors("Email").Should().BeNull();
        }

        [TestMethod]
        public void WhenValidVM_WithUpdatedDateOfBirth_WithInvalidEarlyData_ThenErrorsForDateOfBirthEqualsOne()
        {
            // arrange
            var vm = Helper.CreateValidUserViewModel();

            // act
            vm.DateOfBirth = DateTime.MinValue;

            // assert
            Assert.IsTrue(vm.HasErrors);
            vm.GetErrors("DateOfBirth").Should().HaveCount(1);

            vm.GetErrors("Username").Should().BeNull();
            vm.GetErrors("Password").Should().BeNull();
            vm.GetErrors("Email").Should().BeNull();
        }

        [TestMethod]
        public void WhenValidVM_WithUpdatedDateOfBirth_WithInvalidDataMaxDate_ThenErrorsForDateOfBirthEqualsOne()
        {
            // arrange
            var vm = Helper.CreateValidUserViewModel();

            // act
            vm.DateOfBirth = DateTime.MaxValue;

            // assert
            Assert.IsTrue(vm.HasErrors);
            vm.GetErrors("DateOfBirth").Should().HaveCount(1);

            vm.GetErrors("Username").Should().BeNull();
            vm.GetErrors("Password").Should().BeNull();
            vm.GetErrors("Email").Should().BeNull();
        }

        [TestMethod]
        public void WhenValidVM_WithUpdatedDateOfBirth_WithInvalidDataTomorrow_ThenErrorsForDateOfBirthEqualsOne()
        {
            // arrange
            var vm = Helper.CreateValidUserViewModel();

            // act
            vm.DateOfBirth = DateTime.Now.AddDays(1);

            // assert
            Assert.IsTrue(vm.HasErrors);
            vm.GetErrors("DateOfBirth").Should().HaveCount(1);

            vm.GetErrors("Username").Should().BeNull();
            vm.GetErrors("Password").Should().BeNull();
            vm.GetErrors("Email").Should().BeNull();
        }

        [TestMethod]
        public void WhenInvalidVM_ThenErrorsForUsernameAndPasswordAndEmailAndDateOfBirthEqualOneEach()
        {
            // arrange
            var vm = Helper.CreateInvalidUserViewModel();

            // act
            vm.OkCommand.Execute(null);
            
            // assert
            Assert.IsTrue(vm.HasErrors);
            vm.GetErrors("Username").Should().HaveCount(1);
            vm.GetErrors("Password").Should().HaveCount(1);
            vm.GetErrors("Email").Should().HaveCount(1);
            vm.GetErrors("DateOfBirth").Should().HaveCount(1);
            vm.GetErrors("Description").Should().BeNull();
        }

    }
}