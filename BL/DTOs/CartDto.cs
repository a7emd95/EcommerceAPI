using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs
{
    public class CartDto
    {
      
        public string UserID { get; set; }

        public List<ProductCartDto> ProductCarts { get; set; }
    }
}
