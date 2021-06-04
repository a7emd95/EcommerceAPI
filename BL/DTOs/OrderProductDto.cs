using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs
{
    public class OrderProductDto
    {
        public int ID { get; set; }


        [Range(minimum: 1, maximum: 100)]
        public int Quantity { get; set; }

        public int OrderID { get; set; }

        public int ProductID { get; set; }
    }
}
