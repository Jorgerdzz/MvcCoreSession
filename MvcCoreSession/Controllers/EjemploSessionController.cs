using Microsoft.AspNetCore.Mvc;
using MvcCoreSession.Helpers;
using MvcCoreSession.Models;
using System;

namespace MvcCoreSession.Controllers
{
    public class EjemploSessionController : Controller
    {
        public IActionResult Index()
        {
            byte[] data = HttpContext.Session.Get("MASCOTA");
            if (data != null)
            {
                Mascota mascota = (Mascota)HelperBinarySession.ByteToObject(data);
                return View(mascota);
            }
            else
            {
                return View();
            }
        }

        public IActionResult SessionSimple(string accion)
        {
            if(accion != null)
            {
                if(accion.ToLower() == "almacenar")
                {
                    //GUARDAMOS DATOS EN SESSION
                    HttpContext.Session.SetString("nombre", "Jorge");
                    HttpContext.Session.SetString("hora", DateTime.Now.ToLongTimeString());
                    ViewData["MENSAJE"] = "Datos almacenados en Session";
                }else if(accion.ToLower() == "mostrar")
                {
                    //RECUPERAMOS LOS DATOS DE SESSION
                    ViewData["NOMBRE"] = HttpContext.Session.GetString("nombre");
                    ViewData["HORA"] = HttpContext.Session.GetString("hora");

                }
            }
            return View();
        }

        public IActionResult SessionMascotaBytes(string accion)
        {
            if (accion != null)
            {
                if (accion.ToLower() == "almacenar")
                {
                    Mascota mascota = new Mascota();
                    mascota.Nombre = "Garfield";
                    mascota.Raza = "gato";
                    mascota.Edad = 7;
                    //Para almacenar la mascota en Session, debemos convertirlo a byte[]
                    byte[] data = HelperBinarySession.ObjectToByte(mascota);
                    HttpContext.Session.Set("MASCOTA",data);
                    ViewData["MENSAJE"] = "Mascota almacenada en Session";
                }
                else if (accion.ToLower() == "mostrar")
                {
                    byte[] data = HttpContext.Session.Get("MASCOTA");
                    Mascota mascota = (Mascota)HelperBinarySession.ByteToObject(data);
                    ViewData["MASCOTA"] = mascota;
                }
            }
            return View();
        }

        public IActionResult SessionMascotaCollectionBytes(string accion)
        {
            if (accion != null)
            {
                if (accion.ToLower() == "almacenar")
                {
                    List<Mascota> mascotasList = new List<Mascota>
                    {
                        new Mascota{Nombre= "Nala", Raza="Leona", Edad=12},
                        new Mascota{Nombre= "Sebastian", Raza="Cangrejo", Edad=17},
                        new Mascota{Nombre= "Rafiki", Raza="Brujo", Edad=33},
                        new Mascota{Nombre= "Olaf", Raza="Muñeco", Edad=22},
                    };
                    byte[] data = HelperBinarySession.ObjectToByte(mascotasList);
                    HttpContext.Session.Set("MASCOTAS", data);
                    ViewData["MENSAJE"] = "Coleccion guardada";
                }
                else if (accion.ToLower() == "mostrar")
                {
                    byte[] data = HttpContext.Session.Get("MASCOTAS");
                    List<Mascota> mascotas = (List<Mascota>)HelperBinarySession.ByteToObject(data);
                    return View(mascotas);
                }
            }
            return View();
        }


        public IActionResult SessionMascotaJson(string accion)
        {
            if (accion != null)
            {
                if (accion.ToLower() == "almacenar")
                {
                    Mascota mascota = new Mascota();
                    mascota.Nombre = "Dora";
                    mascota.Raza = "Exploradora";
                    mascota.Edad = 18;
                    //QUEREMOS GUARDAR EL OBJETO MASCOTA COMO STRING EN SESSION
                    string mascotaJson = HelperJsonSession.SerializeObject<Mascota>(mascota);
                    HttpContext.Session.SetString("MASCOTAJSON", mascotaJson);
                    ViewData["MENSAJE"] = "Mascota almacenada en Session";
                }
                else if (accion.ToLower() == "mostrar")
                {
                    string jsonMascota = HttpContext.Session.GetString("MASCOTAJSON");
                    Mascota mascota = HelperJsonSession.DeserializeObject<Mascota>(jsonMascota);
                    ViewData["MASCOTA"] = mascota;
                }
            }
            return View();
        }

    }
}
