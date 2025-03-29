using AutoImperialDAO.DAO.Interfaces;
using AutoImperialDAO.Enums;
using AutoImperialDAO.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfClient.MVVM.ViewModel;

namespace TestProject.ClienteTest.ViewModel
{
    [TestClass]
    public class ReserveViewModelTest
    {
        [TestMethod]
        public void ConvertToReserveCardModel_WithValidReserves_ReturnsMappedModels()
        {
            var versionRepoMock = new Mock<IVersionRepository>();
            var photoRepoMock = new Mock<IPhotoRepository>();
            var reserveRepoMock = new Mock<IReserveRepository>();

            versionRepoMock.Setup(v => v.GetNombreCompletoVehiculo(1)).Returns("Toyota Corolla 2020");
            photoRepoMock.Setup(p => p.GetPhotoByIdVehicle(1)).Returns(new byte[0]);

            var viewModel = new ReserveViewModel(null, null, null, null, reserveRepoMock.Object, versionRepoMock.Object, photoRepoMock.Object, null);

            var reserves = new List<Reserva>
            {
                new Reserva { idVersion = 1, estado = ReserveStatusEnum.Interesado.ToString() }
            };

            var result = viewModel.ConvertToReserveCardModel(reserves);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Toyota Corolla 2020", result[0].Vehicle);
            Assert.AreEqual(ReserveStatusEnum.Interesado, result[0].ReservationStatus);
        }

        [TestMethod]
        public void ConvertToReserveCardModel_WithNullList_ReturnsNull()
        {
            var viewModel = new ReserveViewModel(null, null, null, null, null, null, null, null);

            var result = viewModel.ConvertToReserveCardModel(null);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void ConvertToReserveCardModel_WithNullList_ReturnsNull()
        {
            var viewModel = new ReserveViewModel(null, null, null, null, null, null, null, null);

            var result = viewModel.ConvertToReserveCardModel(null);

            Assert.IsNull(result);
        }
    }
}
