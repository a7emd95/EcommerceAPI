using BL.Repositories;
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

        public ProductRepository ProductRepository { get; }

        public CartRepository CartRepository { get; }

        public ProductCartRepository ProductCartRepository { get; }

        public AccountRepositroy AccountRepositroy { get; }

        public OrderRepositroy OrderRepositroy  { get; }

        public OrderProductRepositroy OrderProductRepositroy { get; }







    }
}
