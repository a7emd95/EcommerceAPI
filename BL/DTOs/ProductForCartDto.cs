using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs
{
    public class ProductForCartDto
    {
        public int ID { get; set; }

        public int ProductID { get; set; }

        public string Name { get; set; }

        public decimal TotalPrice { get; set; }

        public string Image { get; set; }

        public int Quantity { get; set; }


    }
}
