using System;
using FluentAssertions.EventMonitoring;
using SilverlightValidation.Models;
using SilverlightValidation.Tests.Fakes;
using SilverlightValidation.ViewModels;

namespace SilverlightValidation.Tests.Helpers
{
    public class Helper
    {
        public static UserViewModel CreateDefaultUserViewModel()
        {
            var vm = new UserViewModel(UserModel.Create(), UserModelValidatorFake.Create());
            vm.MonitorEvents();
            return vm;
        }

        public static UserViewModel CreateInvalidUserViewModel()
        {
            var vm = new UserViewModel(UserModel.Create(), UserModelValidatorFake.Create());
            vm.MonitorEvents();
            return vm;
        }

        public static UserViewModel CreateValidUserViewModel()
        {
            var vm = new UserViewModel(UserModel.Create("dummy", "Dummy1", "dummy@dummy.com", new DateTime(1977, 01, 01)), UserModelValidatorFake.Create());
            vm.MonitorEvents();
            return vm;
        }
    }
}
