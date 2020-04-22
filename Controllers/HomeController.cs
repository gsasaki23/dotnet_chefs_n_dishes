using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using chefs_n_dishes.Models;
using Microsoft.EntityFrameworkCore;

namespace chefs_n_dishes.Controllers
{
    public class HomeController : Controller
    {
        private ChefContext db;
        public HomeController(ChefContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            ViewBag.AllChefs = db.Chefs
                .Include(chef => chef.ChefDishes)
                .ToList();
            ViewBag.AllDishes = db.Dishes
                .Include(dish => dish.OwnerChef)
                .ToList();
            return View();
        }

        // GET & POST for Chef form page
        [HttpGet("/chefs/new")]
        public IActionResult AddChef()
        {
            return View();
        }
        [HttpPost("newchef")]
        public IActionResult AddChefToDB(Chef chefToAdd)
        {
            // If validation errors, send back to form
            if (ModelState.IsValid == false)
            {
                return View("AddChef");
            }
            chefToAdd.CreatedAt = DateTime.Now;
            chefToAdd.UpdatedAt = DateTime.Now;
            db.Add(chefToAdd);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET & POST for Dish form page
        [HttpGet("/dishes/new")]
        public IActionResult AddDish()
        {
            // Used in form drop-down of chefs
            ViewBag.ChefOptions = db.Chefs
                .ToList();
            return View();
        }
        [HttpPost("newdish")]
        public IActionResult AddDishToDB(Dish dishToAdd)
        {
            // If validation errors, send back to form
            if (ModelState.IsValid == false)
            {
                return View("AddDish");
            }
            dishToAdd.CreatedAt = DateTime.Now;
            dishToAdd.UpdatedAt = DateTime.Now;
            db.Add(dishToAdd);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
