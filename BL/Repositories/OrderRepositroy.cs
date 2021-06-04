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
    public class OrderRepositroy : BaseRepository<Order>
    {
        public OrderRepositroy(DbContext dbContext) : base(dbContext)
        {
        }

        public bool CheckIfProductExist(Product product)
        {

            return GetAny(prod => prod.ID == product.ID);
        }
    }


}



