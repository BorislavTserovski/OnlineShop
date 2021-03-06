﻿using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace MVCBlog.Controllers
{
    public class HomeController : Controller

    {
        private ShopDbContext db = new
            ShopDbContext();
        //Get Recipes
        public ActionResult Index(string Searchby, string search)
        {
            if (Searchby == "Make")
            {
                return View(db.Cars.Where(x => x.Make.Contains(search)).ToList());
            }
            else if (Searchby == "Model")
            {
                return View(db.Cars.Where(x => x.Model.Contains(search)).ToList());
            }
            else if (Searchby == "Category")
            {
                var category = db.Categories.First(c => c.Name.Contains(search));
                return View(db.Cars.Where(c => c.CategoryId==category.Id).ToList());
            }
            else if (Searchby == "Year")
            {
                return View(db.Cars.Where(x => x.Year.ToString()==search).ToList());
            }
            var recipes = db.Cars
              .OrderByDescending(p => p.DateAdded).Take(3);

             return View(recipes.ToList());
           
        }
    }


}