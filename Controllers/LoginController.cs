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
                var consulta = from s in db.Users
                    where s.Email == userLog.Email && s.Userpass == userLog.Password
                    select new User 
                    {
                        id = s.idUsers,
                        Email = s.Email,
                        Password = s.Userpass,
                        FirstName = s.FirstName,
                        LastName = s.LastName,
                        Admin = s.UserAdmin                  
                    };
           
              lista = consulta.ToList();
            }

            int count = lista.Count();

            if(count == 1)
            {
                foreach (var item in lista)
                {
                    if (string.IsNullOrEmpty(HttpContext.Session.GetString("Id")))
                    {
                        HttpContext.Session.SetInt32("Id", item.id);
                        HttpContext.Session.SetString("Name", item.FirstName);
                        HttpContext.Session.SetString("LastName", item.LastName);
                        HttpContext.Session.SetInt32("Admin", item.Admin);
                    }
                }

              return RedirectToAction("Index", "Catalogo");


            }
            else
            {
                ViewBag.f = 1;
                return View();
            }
            
        }


        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Id");
            HttpContext.Session.Remove("Name");
            HttpContext.Session.Remove("LastName");
            HttpContext.Session.Remove("Admin");
            return RedirectToAction("Index", "Home");
        }

    }
}
