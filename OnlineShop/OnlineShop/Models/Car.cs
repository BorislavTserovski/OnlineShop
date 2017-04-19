using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineShop.Models
{
    public class Car
    {
        public Car()
        {
            this.Categories = new HashSet<Category>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Year { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Date Added")]
        [DisplayFormat(DataFormatString ="{0:d}")]
        [Column(TypeName = "datetime2")]
        public DateTime DateAdded { get; set; }

        public byte[] Image { get; set; }

        [ForeignKey("Owner")]
        public string OwnerId { get; set; }

        public ApplicationUser Owner { get; set; }

        [DisplayName("Category")]
        public int CategoryId { get; set; }

        public virtual ICollection<Category> Categories { get; set; }




    }
}