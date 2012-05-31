using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SilverlightValidation.Models;
using SilverlightValidation.Validators;
using SilverlightValidation.ViewModels;

namespace SilverlightValidation.Tests.ViewModels
{
    [TestClass]
    public class UserViewModel_Constructor
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenConstructed_WithTwoNulls_ThenThrowsArgumentNullException()
        {
            new UserViewModel(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenConstructed_WithNullFirstParam_ThenThrowsArgumentNullException()
        {
            new UserViewModel(null, UserModelValidator.Create());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenConstructed_WithNullSecondParam_ThenThrowsArgumentNullException()
        {
            new UserViewModel(UserModel.Create(), null);
        }

        [TestMethod]
        public void WhenConstructed_WithGenericParams_ThenInstantiatesViewModel()
        {
            var vm = new UserViewModel(UserModel.Create(), UserModelValidator.Create());
            Assert.IsNotNull(vm);
        }
    }
}
