using AutoImperialDAO.Enums;
using AutoImperialDAO.Models;

namespace AutoImperialDAO.DAO.Interfaces
{
    public interface IReserveRepository : IBaseRepository<Reserva>
    {
        Reserva? GetReserveById(int id);
        int CreateReserve(Reserva reserva);
        List<Reserva> GetReservesByIdSeller(int id, ReserveStatusEnum status);
        int DeleteReserve(int idReserva);
    }
}
