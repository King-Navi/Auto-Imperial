﻿using AutoImperialDAO.DAO.Interfaces;
using AutoImperialDAO.Enums;
using AutoImperialDAO.Models;

namespace AutoImperialDAO.DAO.Repositories
{
    public class ReserveRepository : BaseRepository<Reserva>, IReserveRepository 
    {
        const int SUCCESS = 1;
        const int ERROR = -1;
        public ReserveRepository(AutoImperialContext context) : base(context)
        {
        }

        public int CreateReserve(Reserva reserva)
        {
            try
            {
                reserva.fechaReserva = DateTime.Now;
                _context.Reserva.Add(reserva);       
                _context.SaveChanges();             
                return SUCCESS;             
            }
            catch (Exception)
            {
                return ERROR;
            }
        }

        public List<Reserva> GetReservesByIdSeller(int id , ReserveStatusEnum status)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("El ID del vendedor no es válido.", nameof(id));

                return _context.Reserva
                    .Where(r => r.idVendedor == id && r.estado == status.ToString())
                    .ToList();
            }
            catch (Exception)
            {
                return new List<Reserva>() { new Reserva() {idReserva = -1 } };
            }
        }

        public Reserva? GetReserveById(int id)
        {
            try
            {
                return _context.Reserva.FirstOrDefault(r => r.idReserva == id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int DeleteReserve(int idReserva)
        {
            try
            {
                var reserva = _context.Reserva.Find(idReserva);
                if (reserva == null)
                    return ERROR;
                reserva.estado = ReserveStatusEnum.Cancelado.ToString();
                _context.SaveChanges();
                return SUCCESS;
            }
            catch (Exception)
            {
                return ERROR;
            }
        }
    }

}
