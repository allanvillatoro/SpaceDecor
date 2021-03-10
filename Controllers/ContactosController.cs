using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProyectoFinal.Models;

namespace ProyectoFinal.Controllers
{
    public class ContactosController : Controller
    {
        ConexionContacto conexion = new ConexionContacto();

        public IActionResult Create()
        {
            return View();
        }
    
            [HttpPost]
            public IActionResult Create(Contacto pac)
            {
                int filasAfectadas = conexion.addContacto(pac);
                if (filasAfectadas > 0)
                    Console.WriteLine("Se agregó a la bd");
                else
                    Console.WriteLine("Hubo un error");

                return RedirectToAction("Index","Home");
            }
        }

        






    }

