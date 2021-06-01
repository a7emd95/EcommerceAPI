using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs
{
    public class CategroyWithProductsDto
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public List<ProductDto> Products { get; set; }
    }
}
