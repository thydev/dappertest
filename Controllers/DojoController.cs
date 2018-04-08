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
    public class DojoController : Controller
    {
        private readonly DojoFactory dojoFactory;
        private readonly NinjaFactory ninjaFactory;
        public DojoController()
        {
            dojoFactory = new DojoFactory();
            ninjaFactory = new NinjaFactory();
        }
        // GET: /Home/
        [HttpGet]
        [Route("dojos")]
        public IActionResult Index()
        {

            ViewBag.Dojos = dojoFactory.FindAll();
            return View();
        }

        [HttpPost]
        [Route("dojos/create")]
        public IActionResult Create(Dojo dojo)
        {
            dojoFactory.Add(dojo);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("dojos/{id}")]
        public IActionResult Show(int id)
        {
            ViewBag.Dojo = dojoFactory.FindByID(id);
            ViewBag.Members = ninjaFactory.NinjasForDojoById(id);
            ViewBag.Rogues =  ninjaFactory.RogueNinja();
            return View("Show");
        }

        [HttpGet]
        [Route("dojos/{id}/{activity}/{ninja_id}")]
        public IActionResult UpdateDojo(int id, string activity, int ninja_id)
        {
            if(activity == "bonish") {
                ninjaFactory.UpdateDojo(ninja_id, 0);
            } else if(activity == "recruit") {
                ninjaFactory.UpdateDojo(ninja_id, dojo_id:id);
            }
            
            return RedirectToAction("Show", new { id = id});
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
