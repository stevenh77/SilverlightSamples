using GalaSoft.MvvmLight.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SilverlightValidation.Messages;
using SilverlightValidation.Tests.Helpers;

namespace SilverlightValidation.Tests.ViewModels
{
    [TestClass]
    public class UserViewModel_Commands
    {
        [TestMethod]
        public void WhenNewViewModel_ThenOkCommandIsExecutable()
        {
            // arrange
            var vm = Helper.CreateDefaultUserViewModel();

            // assert
            Assert.IsTrue(vm.OkCommand.CanExecute(null));
        }

        [TestMethod]
        public void WhenNewViewModel_WithOkExecuted_ThenShouldNotRaiseEvent()
        {
            // arrange
            var vm = Helper.CreateDefaultUserViewModel();
            bool hasMessage = false;
            Messenger.Default.Register<UserViewResponseMessage>(this, message => { hasMessage = true; });

            // act
            vm.OkCommand.Execute(null);

            // assert
            Assert.IsFalse(hasMessage);
        }

        [TestMethod]
        public void WhenNewViewModel_WithOkExecutedAndUsernameUpdatedWithInvalidData_ThenShouldNotRaiseEvent()
        {
            // arrange
            var vm = Helper.CreateDefaultUserViewModel();
            bool hasMessage = false;
            Messenger.Default.Register<UserViewResponseMessage>(this, message => { hasMessage = true; });

            // act
            var shortData = "a";
            vm.Username = shortData;
            vm.OkCommand.Execute(null);

            // assert
            Assert.IsFalse(hasMessage);
        }

        [TestMethod]
        public void WhenNewViewModel_WithOkExecutedAndUsernameUpdated_ThenShouldRaiseEvent()
        {
            // arrange
            var vm = Helper.CreateValidUserViewModel();
            bool hasMessage = false;
            Messenger.Default.Register<UserViewResponseMessage>(this, message => { hasMessage = true; });

            // act
            vm.Username = "updated";
            vm.OkCommand.Execute(null);

            // assert
            Assert.IsTrue(vm.IsChanged);
            Assert.IsTrue(hasMessage);
        }

        [TestMethod]
        public void WhenNewViewModel_ThenCancelCommandIsExecutable()
        {
            // arrange
            var vm = Helper.CreateDefaultUserViewModel();

            // assert
            Assert.IsTrue(vm.CancelCommand.CanExecute(null));
        }

        [TestMethod]
        public void WhenNewViewModel_WithCancelExecuted_ThenShouldRaiseEvent()
        {
            // arrange
            var vm = Helper.CreateDefaultUserViewModel();
            bool hasMessage = false;
            Messenger.Default.Register<UserViewResponseMessage>(this, message => { hasMessage = true; });

            // act
            vm.CancelCommand.Execute(null);

            // assert
            Assert.IsTrue(hasMessage);
        }
        
    }
}
