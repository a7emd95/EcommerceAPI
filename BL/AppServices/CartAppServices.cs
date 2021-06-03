using BL.Bases;
using BL.DTOs;
using BL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.AppServices
{
    public class CartAppServices : BaseAppService
    {
     
        public CartAppServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
           
        }

        public ProductForCartDto AddnewProductToCart(string userId, int productID, int quantity)
        {
            var cart = TheUnitOfWork.CartRepository.GetFirstOrDefault(c => c.UserID == userId);
            var product = TheUnitOfWork.ProductRepository.GetFirstOrDefault(p => p.ID == productID);

            if (cart == null)
            {
                cart = new Cart { UserID = userId };
                TheUnitOfWork.CartRepository.Insert(cart);
                var result = TheUnitOfWork.SaveChanges();
                if (result < new int())
                    return null;
            }


            var productCart = new ProductCart { CartID = cart.UserID, ProductID = productID, Quantity = quantity };
            TheUnitOfWork.ProductCartRepository.Insert(productCart);

            if (TheUnitOfWork.SaveChanges() > new int())
            {
                decimal totalprice = ((quantity * product.Price) - ((product.Price * (product.DisscountRate / 100)) * quantity)).Value;

                return new ProductForCartDto
                {
                    ID = productCart.ID,
                    ProductID = product.ID,
                    Name = product.Name,
                    Quantity = quantity,
                    TotalPrice = totalprice,
                    Image = product.Image
                };
            }
            return null;
        }

        public bool DeleteProducatFromCart(string userId, int productID)
        {
            var productCart = TheUnitOfWork.ProductCartRepository.GetFirstOrDefault(c => c.CartID == userId && c.ProductID == productID);

            if (productCart != null)
            {
                TheUnitOfWork.ProductCartRepository.Delete(productCart);
                if (TheUnitOfWork.SaveChanges() > new int())
                    return true;
            }
            return false;
        }

        public bool UpdateProducatFromCart(string userId, int productID, int quantity)
        {

            var productCart = TheUnitOfWork.ProductCartRepository.GetFirstOrDefault(c => c.CartID == userId && c.ProductID == productID);

            if (productCart != null)
            {
                productCart.Quantity = quantity;
                TheUnitOfWork.ProductCartRepository.Update(productCart);
                if (TheUnitOfWork.SaveChanges() > new int())
                    return true;
            }

            return false;
        }

        public List<ProductForCartDto> GetAllProductsInCart(string userId)
        {
            var cart = TheUnitOfWork.CartRepository.GetFirstOrDefault(c => c.UserID == userId);
            List<ProductForCartDto> products = new List<ProductForCartDto>();

            foreach (var item in cart.ProductCarts)
            {
                var product = item.Product;
                decimal totalprice = ((item.Quantity * product.Price) - ((product.Price * (product.DisscountRate / 100)) * item.Quantity)).Value;

                products.Add(
                 new ProductForCartDto
                 {
                     ID = item.ID,
                     ProductID = product.ID,
                     Name = product.Name,
                     Quantity = item.Quantity,
                     TotalPrice = totalprice,
                     Image = product.Image
                 }
                 );
            }
            return products;
        }

        public bool DeleteCart(string userId)
        {
            var cart = TheUnitOfWork.CartRepository.GetFirstOrDefault(c => c.UserID == userId);
            TheUnitOfWork.CartRepository.Delete(cart);
            if (TheUnitOfWork.SaveChanges() > new int())
                return true;
            return false;
        }

        public Cart CreateCart(string userId)
        {
            var cart = new Cart { UserID = userId };
            cart = TheUnitOfWork.CartRepository.Insert(cart);
            if (TheUnitOfWork.SaveChanges() > new int())
                return cart;
            return null;
        }


    }
}
