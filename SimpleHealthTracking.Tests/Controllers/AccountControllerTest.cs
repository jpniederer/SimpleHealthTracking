namespace SimpleHealthTracking.Tests.Controllers
{
    using System.Web.Mvc;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SimpleHealthTracking.Models;
    using SimpleHealthTracking.Controllers;

    [TestClass]
    public class AccountControllerTest
    {
        [TestMethod]
        public void RegisterNewUser()
        {
            // Need to call the constructor with a userManager and token.
            AccountController ac = new AccountController();
            RegisterBindingModel model = new RegisterBindingModel();
            model.Email = @"josh@jpniederer.com";
            model.Password = "password";
            model.ConfirmPassword = "password";

            var re = ac.Register(model);
        }
    }
}