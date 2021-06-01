using BL.Interfaces;
using BL.Repositories;
using DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Bases
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext DbContext;

        public UnitOfWork(ApiContext dbContext)
        {
            DbContext = dbContext;
        }

        #region Method
        public void Dispose()
        {
            DbContext.Dispose();
        }

        public int SaveChanges()
        {
            return DbContext.SaveChanges();
        }
        #endregion



        private CategroyRepository categroyRepo;
        public CategroyRepository CategroyRepository
        {
            get
            {
                if (categroyRepo == null)
                    categroyRepo = new CategroyRepository(DbContext);
                return categroyRepo;
            }
        }





    }
}
