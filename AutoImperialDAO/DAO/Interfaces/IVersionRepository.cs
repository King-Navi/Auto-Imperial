using AutoImperialDAO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Version = AutoImperialDAO.Models.Version;

namespace AutoImperialDAO.DAO.Interfaces
{
    public interface IVersionRepository : IBaseRepository<Version>
    {
        List<Version> GetAllVersionsWithModelAndBrand();
        /// <summary>
        /// Retrieves the full name of a vehicle based on its <c>idVersion</c>.
        /// The full name includes: Brand, Model, Version, and Engine.
        /// </summary>
        /// <param name="idVersion">The ID of the vehicle version.</param>
        /// <returns>
        /// A string containing the full vehicle name (e.g., "Toyota Corolla XLE 2.0L").
        /// Returns "Vehicle not found" if the version does not exist.
        /// Returns <c>null</c> in case of an error.
        /// </returns>
        string GetFullVehicleName(int idVersion);
        Version? GetVersionById(int id);
    }
}
