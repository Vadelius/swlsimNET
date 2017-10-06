using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using swlsimNET.Controllers;
using swlsimNET.Models;
using swlsimNET.ServerApp.Weapons;

namespace swlsimNET.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void Index()
        {
            var hc = new HomeController();
            var res = hc.Index() as ViewResult;
            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void ModelValidation()
        {
            // At least have to set both weapons and apl (3 validation errors)
            var set = new Settings();
            Assert.IsTrue(this.ValidateModel(set).Count >= 3);
        }

        [TestMethod]
        public void ModelValidation2()
        {
            var set = new Settings
            {
                PrimaryWeapon = WeaponType.Hammer,
                SecondaryWeapon = WeaponType.Fist,
                Apl = "Hammer.Smash"
            };

            Assert.IsTrue(this.ValidateModel(set).Count <= 0);
        }

        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }
    }
}