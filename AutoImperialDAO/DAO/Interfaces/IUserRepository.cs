﻿using AutoImperialDAO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoImperialDAO.DAO.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        User Authenticate(string username, string password);
    }
}
