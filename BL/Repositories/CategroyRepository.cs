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
    public class CategroyRepository : BaseRepository<Category>
    {
        private DbContext dbContext;

        public CategroyRepository(DbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool CheckIfCategroyExist(Category category)
        {

            return GetAny(cat => cat.ID == category.ID);
        }

        public bool CkeckIfCategroyExistByName(Category category)
        {
            return GetAny(cat => cat.Name == category.Name);
        }

        public Category GetCategoryWithProducts(int id)
        {
            var categroy = GetFirstOrDefault("Products", c => c.ID == id);
            return categroy;
        }


    }
}
