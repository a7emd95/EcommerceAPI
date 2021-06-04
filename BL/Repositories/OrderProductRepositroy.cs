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
    public class OrderProductRepositroy : BaseRepository<OrderProduct>
    {
        public OrderProductRepositroy(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
