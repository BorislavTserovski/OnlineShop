﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineShop.Models
{
    public class Category
    {
        private ICollection<Car> cars;

        public Category()
        {
            this.cars = new HashSet<Car>();
        }

        [Key]
        public int Id { get; set; }


        [Required]
        [Index(IsUnique =true)]
        [StringLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}