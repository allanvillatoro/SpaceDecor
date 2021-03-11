using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceDecor.Controllers
{
    public class CatalogoController : Controller
    {
        public IActionResult Index()
        {

            ViewBag.name = HttpContext.Session.GetString("name");

            return View();
        }

       

    }
}
