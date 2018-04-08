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
    public class NinjaController : Controller
    {
        private readonly NinjaFactory ninjaFactory;
        private readonly DojoFactory dojoFactory;

        public NinjaController()
        {
            ninjaFactory = new NinjaFactory();
            dojoFactory = new DojoFactory();
        }
        // GET: /Home/
        [HttpGet]
        [Route("ninjas")]
        public IActionResult Index()
        {
            IEnumerable<Ninja> Ninjas = ninjaFactory.FindAll();
            foreach (var item in Ninjas)
            {   
                System.Console.WriteLine(item.dojo_id);
                //This is a slow way. How to map one to many???
                if(item.dojo_id > 0){
                    item.Dojo = dojoFactory.FindByID(item.dojo_id);
                }
            }
            ViewBag.Ninjas = Ninjas;
            ViewBag.Dojos = dojoFactory.FindAll();

            return View();
        }

        [HttpPost]
        [Route("ninjas/create")]
        public IActionResult Create(Ninja ninja, int dojo_id)
        {
            System.Console.WriteLine();
            System.Console.WriteLine(dojo_id);
            System.Console.WriteLine();
            if(dojo_id > 0) {
                Dojo dojo = dojoFactory.FindByID(dojo_id);
                ninja.Dojo = dojo;
            }
            ninjaFactory.Add(ninja);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("ninjas/{id}")]
        public IActionResult Show(int id)
        {   
            Ninja ninja = ninjaFactory.FindByID(id);
            if(ninja.dojo_id > 0) {
                ninja.Dojo = dojoFactory.FindByID(ninja.dojo_id);
            }
            ViewBag.Ninja = ninja;
            return View("Show");
        }

        

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
