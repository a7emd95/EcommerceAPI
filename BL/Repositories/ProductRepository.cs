using BL.Bases;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Repositories
{
    public class ProductRepository : BaseRepository<Product>
    {
        private DbContext DbContext;

        public ProductRepository(DbContext dbContext) : base(dbContext)
        {
            this.DbContext = dbContext;
        }

        public bool CheckIfProductExist(Product product)
        {

            return GetAny(prod => prod.ID == product.ID);
        }

        public bool CkeckIfCategroyExistByName(Product product)
        {
            return GetAny(prod => prod.Name == product.Name);
        }

       
    }
}
