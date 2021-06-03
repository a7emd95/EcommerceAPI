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
    public class CartRepository : BaseRepository<Cart>
    {
        public CartRepository(DbContext dbContext) : base(dbContext)
        {
        }


        public bool CheckIfCartExist(Cart cart)
        {
            return GetAny(c => c.UserID == cart.UserID);
        }


    }
}
