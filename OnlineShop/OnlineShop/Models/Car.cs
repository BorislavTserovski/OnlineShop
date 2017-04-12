using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineShop.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Year   { get; set; }

        public DateTime DateAdded { get; set; }

        public byte[] Image { get; set; }

        [ForeignKey("Buyer")]
        public string BuyerId { get; set; }

        public ApplicationUser Buyer { get; set; }


    }
}