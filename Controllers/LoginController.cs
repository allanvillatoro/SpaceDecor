using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProyectoFinal.Models;

namespace ProyectoFinal.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User userLog)
        {
            User us = new User();
            List<User> lista = new List<User>();
            using (DatabaseContext db = new DatabaseContext())
            {
                var consulta = from s in db.users
                    where s.Mail == userLog.mail && s.Password == userLog.password
                    select new User 
                    {
                        id = s.id,
                        mail = s.Mail,
                        password = s.Password,
                        name = s.Name
                    };
           
              lista = consulta.ToList();
            }

            int count = lista.Count();

            if(count == 1)
            {
                foreach (var item in lista)
                {
                    if (string.IsNullOrEmpty(HttpContext.Session.GetString("id")))
                    {
                        HttpContext.Session.SetInt32("id", item.id);
                        HttpContext.Session.SetString("name", item.name);
                    }
                }

              return RedirectToAction("Index", "Catalogo");


            }
            else
            {
                ViewBag.f = true;
                return View();
            }
            
        }


        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("id");
            HttpContext.Session.Remove("name");
            return RedirectToAction("Index", "Home");
        }

    }
}
