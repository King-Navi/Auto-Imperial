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
    }
}
