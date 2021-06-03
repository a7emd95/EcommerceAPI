using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    [Table("ProductCart")]
   public class ProductCart
    {
        public int ID { get; set; }

        [Range(minimum:1 ,maximum:100)]
        public int  Quantity { get; set; }

       
        public string CartID{ get; set; }
        [ForeignKey("CartID")]
        public virtual Cart Cart{ get; set; }


        public int ProductID { get; set; }
        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }

    }
}
