using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    [Table("Category")]
    public class Category
    {
        public int ID { get; set; }
        [Required(ErrorMessage ="Categroy Name is Must") , MinLength( 3,ErrorMessage ="Name Must be Three Character Minumin")]
        public string Name { get; set; }
    }
}
