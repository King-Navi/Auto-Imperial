using AutoImperialDAO.Models;

namespace AutoImperialDAO.DAO.Interfaces
{
    public interface IPhotoRepository : IBaseRepository<Foto>
    {
        /// <summary>
        /// Retrieves the first available photo (as a byte array) for a vehicle based on its version ID.
        /// </summary>
        /// <param name="idVersion">The ID of the vehicle's version.</param>
        /// <returns>
        /// A byte array representing the image (photo) of the vehicle.
        /// Returns <c>null</c> if no photo is found or if an exception occurs.
        /// </returns>
        /// <remarks>
        /// This method navigates from the <c>Fotos</c> table to <c>Vehiculo</c> using the navigation property
        /// <c>idVehiculoNavigation</c> and filters by the vehicle's <c>idVersion</c>.
        /// </remarks>
        byte[] GetPhotoByIdVehicle(int idVersion);
    }
}
