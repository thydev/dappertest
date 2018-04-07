using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dappertest.Models;
using dappertest.Factory;

namespace dappertest.Controllers
{
    public class HomeController : Controller
    {
        private readonly TrailFactory trailFactory;
        public HomeController()
        {
            //Instantiate a TrailFactory object that is immutable (READONLY)
            //This establishes the initial DB connection for us.
            trailFactory = new TrailFactory();
        }
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            //We can call upon the methods of the userFactory directly now.
            ViewBag.Trails = trailFactory.FindAll();
            return View();
        }

        [HttpGet]
        [Route("add")]
        public IActionResult Add()
        {
            return View("Add");
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create(Trail trail)
        {
            System.Console.WriteLine(trail.Name);
            System.Console.WriteLine(trail.Description);
            trailFactory.Add(trail);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("trail/{id}")]
        public IActionResult Show(int id)
        {
            ViewBag.Trail = trailFactory.FindByID(id);
            return View("DisplayTrail");
        }

        // public IActionResult About()
        // {
        //     ViewData["Message"] = "Your application description page.";

        //     return View();
        // }

        // public IActionResult Contact()
        // {
        //     ViewData["Message"] = "Your contact page.";

        //     return View();
        // }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
