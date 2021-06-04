using BL.Bases;
using BL.DTOs;
using BL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.AppServices
{
    public class OrderAppService : BaseAppService
    {
        private readonly CartAppServices CartAppServices;

        public OrderAppService(IUnitOfWork unitOfWork, CartAppServices cartAppServices) : base(unitOfWork)
        {
            this.CartAppServices = cartAppServices;
        }

        public List<OrderDto> GetAllOrderForUser(string userId)
        {
            return Mapper.Map<List<OrderDto>>(TheUnitOfWork.OrderRepositroy.GetWhere(o => o.UserID == userId));
        }

        public OrderDto GetOrderForuserById(string userId, int orderId)
        {
            return Mapper.Map<OrderDto>(TheUnitOfWork.OrderRepositroy.GetFirstOrDefault(o => o.UserID == userId && o.ID == orderId));
        }

        public OrderDto PurchaseOrderForUser(string userId)
        {

            var cart = TheUnitOfWork.CartRepository.GetFirstOrDefault("ProductCarts", c => c.UserID == userId);

            var cartproducts = CartAppServices.GetAllProductsInCart(userId);
            var orderTotalPrice = 0m;
            foreach (var item in cartproducts)
            {
                orderTotalPrice += item.TotalPrice;
            }

            var order = new Order { UserID = userId, DateTime = DateTime.Now, TotalPrice = orderTotalPrice };
            order = TheUnitOfWork.OrderRepositroy.Insert(order);

            if (TheUnitOfWork.SaveChanges() > new int())
            {
                var orderProductList = new List<OrderProduct>();

                foreach (var item in cart.ProductCarts)
                {
                    orderProductList.Add(new OrderProduct { OrderID = order.ID, ProductID = item.ProductID, Quantity = item.Quantity });
                }

                TheUnitOfWork.OrderProductRepositroy.InsertList(orderProductList);
                if (TheUnitOfWork.SaveChanges() > new int())
                {
                    TheUnitOfWork.ProductCartRepository.DeleteList(cart.ProductCarts.ToList());
                    TheUnitOfWork.SaveChanges();
                }
                else
                {
                    return null;
                }

                return Mapper.Map<OrderDto>(order);
            }
            return null;
        }

        public bool DeleteOrder(int orderId)
        {
            var result = false;
            TheUnitOfWork.OrderRepositroy.Delete(orderId);

            result = TheUnitOfWork.SaveChanges() > new int();
            return result;
        }


    }
}
