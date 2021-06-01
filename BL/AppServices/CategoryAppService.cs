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
    public class CategoryAppService : BaseAppService
    {
        public CategoryAppService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public List<CategroyDto> GetAllCategroies()
        {
            return Mapper.Map<List<CategroyDto>>(TheUnitOfWork.CategroyRepository.GetAll());
        }

        public CategroyDto GetCategroy(CategroyDto categroyDto)
        {
            return Mapper.Map<CategroyDto>(TheUnitOfWork.CategroyRepository.GetFirstOrDefault(c => c.ID == categroyDto.ID));
        }

        public CategroyDto GetCategroy(int id)
        {
            return Mapper.Map<CategroyDto>(TheUnitOfWork.CategroyRepository.GetFirstOrDefault(c => c.ID == id));
        }

        public CategroyWithProductsDto GetCategoryWithProducts(int id)
        {
            return  Mapper.Map<CategroyWithProductsDto>(TheUnitOfWork.CategroyRepository.GetCategoryWithProducts(id)) ;
        }

        public CategroyDto CreateNewCategroy(CategroyDto categroyDto)
        {
            var catrgroy = Mapper.Map<Category>(categroyDto);

            catrgroy = TheUnitOfWork.CategroyRepository.Insert(catrgroy);
            if (TheUnitOfWork.SaveChanges() > new int())
            {
                categroyDto.ID = catrgroy.ID;
                return categroyDto;
            }
            else
            {
                return null;
            }
        }

        public bool UpdateCategroy(CategroyDto categroyDto)
        {
            var categroy = Mapper.Map<Category>(categroyDto);
            TheUnitOfWork.CategroyRepository.Update(categroy);

            if (TheUnitOfWork.SaveChanges() > new int())
            {
                return true;
            }
            else
            {
                return false;
            }


        }

        public bool DeleteCategroy(CategroyDto categroyDto)
        {
            var categroy = Mapper.Map<Category>(categroyDto);
            TheUnitOfWork.CategroyRepository.Delete(categroy);

            if (TheUnitOfWork.SaveChanges() > new int())
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        public bool DeleteCategroy(int categroyDtoId)
        {
            var result = false;
            TheUnitOfWork.CategroyRepository.Delete(categroyDtoId);

            result = TheUnitOfWork.SaveChanges() > new int();
            return result;
        }

        public bool CheckCategroyIsExist(CategroyDto categroyDto)
        {

            var categroy = Mapper.Map<Category>(categroyDto);
            return TheUnitOfWork.CategroyRepository.CheckIfCategroyExist(categroy);
        }

        public bool CheckCategroyIsExistByName(CategroyDto categroyDto)
        {
            var categroy = Mapper.Map<Category>(categroyDto);
            return TheUnitOfWork.CategroyRepository.CkeckIfCategroyExistByName(categroy);
        }

        public List<CategroyDto> GetCategroysByPage(int pageNumber, int pageSize)
        {
            return Mapper.Map<List<CategroyDto>>(TheUnitOfWork.CategroyRepository.GetByPage(pageNumber, pageSize));
        }


    }

}
