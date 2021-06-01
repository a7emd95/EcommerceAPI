﻿using BL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        #region Method
        int SaveChanges();
        #endregion

        public CategroyRepository CategroyRepository { get; }





    }
}