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
    public class ProductAppService : BaseAppService
    {
        public ProductAppService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public List<ProductDto> GetAllProduct()
        {
            return Mapper.Map<List<ProductDto>>(TheUnitOfWork.ProductRepository.GetAll());
        }

        public ProductDto GetProduct(ProductDto productDto)
        {
            return Mapper.Map<ProductDto>(TheUnitOfWork.ProductRepository.GetFirstOrDefault(p => p.ID == productDto.ID));
        }

        public ProductDto GetProduct(int productDtoId)
        {
            return Mapper.Map<ProductDto>(TheUnitOfWork.ProductRepository.GetFirstOrDefault(p => p.ID == productDtoId));
        }

        public List<ProductDto> GetAllProductsPerCategroy(int categroyId)
        {
            return Mapper.Map<List<ProductDto>>(TheUnitOfWork.ProductRepository.GetWhere(p => p.CategroyId == categroyId));

        }

        public ProductDto CreateNewProduct(ProductDto productDto)
        {
            var product = Mapper.Map<Product>(productDto);

            product = TheUnitOfWork.ProductRepository.Insert(product);

            if (TheUnitOfWork.SaveChanges() > new int())
            {
                productDto.ID = product.ID;
                return productDto;
            }
            else
            {
                return null;
            }

        }

        public bool UpdateProduct(ProductDto productDto)
        {
            var product = Mapper.Map<Product>(productDto);
            TheUnitOfWork.ProductRepository.Update(product);

            if (TheUnitOfWork.SaveChanges() > new int())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteProduct(ProductDto productDto)
        {
            var product = Mapper.Map<Product>(productDto);
            TheUnitOfWork.ProductRepository.Delete(product);

            if (TheUnitOfWork.SaveChanges() > new int())
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool DeleteProduct(int productDtoId)
        {
            var result = false;
            TheUnitOfWork.ProductRepository.Delete(productDtoId);

            result = TheUnitOfWork.SaveChanges() > new int();
            return result;
        }

        public bool CheckProductIsExist(ProductDto productDto)
        {
            var product = Mapper.Map<Product>(productDto);
            return TheUnitOfWork.ProductRepository.CheckIfProductExist(product);
        }

        public bool CheckProductIsExistByName(ProductDto productDto)
        {
            var product = Mapper.Map<Product>(productDto);
            return TheUnitOfWork.ProductRepository.CkeckIfCategroyExistByName(product);
        }


        public List<ProductDto> GetProductByPage(int pageNumber, int pageSize)
        {
            return Mapper.Map<List<ProductDto>>(TheUnitOfWork.ProductRepository.GetByPage(pageNumber, pageSize));
        }

        public List<ProductDto> GetProductInCategroyByPage(int catgoryId , int pageNumber, int pageSize )
        {
            return Mapper.Map<List<ProductDto>>(TheUnitOfWork.ProductRepository.
                GetByPage(pageNumber, pageSize, c => c.CategroyId == catgoryId));
        }

    }
}
