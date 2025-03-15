using AutoImperialDAO.Models;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfClient.MVVM.Model;
using WpfClient.Utilities.Validation;

namespace TestProject.ClienteTest
{
    [TestCategory("ValidatorTest")]
    [TestClass]
    public class ValidatorTest
    {
        private ClientValidator _validator;

        [TestInitialize]
        public void Setup()
        {
            _validator = new ClientValidator();
        }
        [TestMethod]
        public void ValidateClient_WhenValid_ShouldNotHaveErrors()
        {
            var client = new Client
            {
                Name = "Juan",
                PaternalSurname = "Pérez",
                MaternalSurname = "Gómez",
                Phone = "1234567890",
                Street = "Av. Reforma",
                CP = "12345",
                City = "CDMX",
                Email = "juan@example.com",
                RFC = "GOMJ850123MNE",
                CURP = "GOMJ850123HDFRLR05"
            };

            var result = _validator.TestValidate(client);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [TestMethod]
        public void ValidateClient_WhenCurpAndRfcAreInvalid_ShouldReturnErrors()
        {
            var client = new Client
            {
                Name = "Juan",
                PaternalSurname = "Pérez",
                MaternalSurname = "Gómez",
                Phone = "1234567890",
                Street = "Av. Reforma",
                CP = "12345",
                City = "CDMX",
                Email = "juan@example.com",
                RFC = "123456789012",  
                CURP = "ABC123",
            };

            var result = _validator.TestValidate(client);

            result.ShouldHaveValidationErrorFor(c => c.RFC)
                .WithErrorMessage("El RFC no tiene un formato válido.");
            result.ShouldHaveValidationErrorFor(c => c.CURP)
                .WithErrorMessage("El CURP no tiene un formato válido.");
        }

        [TestMethod]
        public void ValidateClient_WhenNameIsInvalid_ShouldReturnError()
        {
            var client = new Client
            {
                Name = "Juan123!",
                PaternalSurname = "Pérez",
                MaternalSurname = "Gómez",
                Phone = "1234567890",
                Street = "Av. Reforma",
                CP = "12345",
                City = "CDMX",
                Email = "juan@example.com",
                RFC = "GOMJ850123MNE",
                CURP = "GOMJ850123HDFRLR05"
            };

            var result = _validator.TestValidate(client);

            result.ShouldHaveValidationErrorFor(c => c.Name)
                .WithErrorMessage("El nombre no puede contener números ni caracteres especiales.");
        }
    }
}
