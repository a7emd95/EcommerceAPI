using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    [Table("Product")]
    public class Product
    {
        public int ID { get; set; }


        [Required(ErrorMessage = "Name Is Required")
        , MinLength(2, ErrorMessage = "Product Name Must Be At Least 2 Charcahter")]
        public string Name { get; set; }


        [Range(typeof(int), "1", "10000", ErrorMessage = "Quantity of product must between 1 and 10000")]
        public int Quantity { get; set; }


        [Required(ErrorMessage = "Description Is Required")
        , MinLength(10, ErrorMessage = "Product Description Must Be At Least 10 Charcahter")]
        public string Description { get; set; }


        [ Column(TypeName="decimal(18,4)"), Range(typeof(decimal), "1", "10000000", ErrorMessage = "Price Shouldn't be zero")]
        public decimal Price { get; set; }


        public string Color { get; set; }


        [Required(ErrorMessage = "Image Is Required")]
        public string Image { get; set; }


        public int? DisscountRate { get; set; }



        public int? CategroyId { get; set; }
        [ForeignKey("CategroyId ")]
        public virtual Category Category { get; set; }


        public  ICollection<ProductCart> ProductCarts { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
