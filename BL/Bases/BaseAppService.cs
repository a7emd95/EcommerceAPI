using AutoMapper;
using BL.Config;
using BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Bases
{
    public class BaseAppService : IDisposable
    {
        protected readonly IUnitOfWork TheUnitOfWork;
        protected readonly IMapper Mapper = AutoMapperConfig.Mapper;



        public BaseAppService(IUnitOfWork unitOfWork)
        {
            this.TheUnitOfWork = unitOfWork;
        }

        public void Dispose()
        {
            TheUnitOfWork.Dispose();
        }
    }
}
