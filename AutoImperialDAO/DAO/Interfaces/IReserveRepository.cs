using AutoImperialDAO.Enums;
using AutoImperialDAO.Models;

namespace AutoImperialDAO.DAO.Interfaces
{
    public interface IReserveRepository : IBaseRepository<Reserva>
    {
        int CreateReserve(Reserva reserva);
        List<Reserva> GetReservesByIdSeller(int id, ReserveStatusEnum status);
    }
}
