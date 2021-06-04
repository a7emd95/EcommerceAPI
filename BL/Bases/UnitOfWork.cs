using BL.Interfaces;
using BL.Repositories;
using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        private readonly UserManager<ApplicationUser> UserManger;
        private readonly RoleManager<IdentityRole> RoleManager;
        private readonly IConfiguration Configuration;

        public UnitOfWork(ApiContext dbContext, UserManager<ApplicationUser> userManger, RoleManager<IdentityRole> roleManager
            , IConfiguration configuration)
        {
            DbContext = dbContext;
            UserManger = userManger;
            RoleManager = roleManager;
            Configuration = configuration;
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

        private ProductRepository productRepo;
        public ProductRepository ProductRepository
        {
            get
            {
                if (productRepo == null)
                    productRepo = new ProductRepository(DbContext);
                return productRepo;
            }
        }

        private CartRepository cartRepo;
        public CartRepository CartRepository
        {
            get
            {
                if (cartRepo == null)
                    cartRepo = new CartRepository(DbContext);
                return cartRepo;
            }
        }

        private ProductCartRepository productCartRepo;
        public ProductCartRepository ProductCartRepository
        {
            get
            {
                if (productCartRepo == null)
                    productCartRepo = new ProductCartRepository(DbContext);
                return productCartRepo;
            }
        }

        private AccountRepositroy accountRepo;
        public AccountRepositroy AccountRepositroy
        {
            get
            {
                if (accountRepo == null)
                    accountRepo = new AccountRepositroy(UserManger, RoleManager, Configuration);
                return accountRepo;
            }
        }

        private OrderRepositroy orderRepo;
        public OrderRepositroy OrderRepositroy
        {
            get
            {
                if (orderRepo == null)
                    orderRepo = new OrderRepositroy(DbContext);
                return orderRepo;                   
            }
        }

        private OrderProductRepositroy orderProductRepo;
        public OrderProductRepositroy OrderProductRepositroy
        {
            get
            {
                if (orderProductRepo == null)
                    orderProductRepo = new OrderProductRepositroy(DbContext);
                return orderProductRepo;
            }
        }



    }
}
