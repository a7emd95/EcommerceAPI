using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs
{
    public class OrderDto
    {
        public int ID { get; set; }

        [Column(TypeName = "decimal(18,4)"), Range(typeof(decimal), "1", "10000000", ErrorMessage = "Price Shouldn't be zero")]
        public decimal TotalPrice { get; set; }

        public DateTime rDateTime { get; set; }

        public string UserID { get; set; }

        

    }
}
