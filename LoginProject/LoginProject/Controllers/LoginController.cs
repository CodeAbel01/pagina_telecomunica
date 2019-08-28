using LoginProject.Models;
using LoginProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace LoginProject.Controllers
{
    public class LoginController : Controller
    {
        private readonly string rolAdmin = "Administrador";
        private readonly string rolCliente = "Cliente";

        // GET: Usuarios
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        //Metodo Index
        [HttpPost]
        public ActionResult Index(LoginViewModel model)
        {
            if (model != null)
            {
                using (var context = new LoginContext())
                {
                    var user = context.Usuarios                                    
                                    .Where(x => x.UserName == model.UserName && x.Password == model.Password)
                                    .FirstOrDefault();
                    
                    if (user != null)
                    {
                        var rol = context.Roles.FirstOrDefault(x => x.RolID == user.RoldID);
                        if(rol != null)
                        {
                            Session["User"] = $"{user.Nombre} {user.Apellido}";
                            Session["Rol"] = rol.RolName;

                            if (rol.RolName.Equals("Administrador"))
                            {
                                return Redirect("/Login/Admin");
                            }

                            else
                            {
                                return Redirect("/Login/Cliente");
                            }
                        }
                    }

                    else
                        return View(model);
                }
            }
            return View(model);
        }

        //Metodo Admin
        public ActionResult Admin()
        {
            var session = Session["User"];
                        
            if (session == null)
            {
                return RedirectToAction("Index");
            }

            if (Session["Rol"].ToString().Equals(this.rolAdmin))
            {
                ViewBag.User = Session["User"];
                return View();
            }

            else
            {
                return RedirectToAction("Cliente");
            }
        }

        //Metodo Cliente
        public ActionResult Cliente()
        {

            var session = Session["User"];

            if (session == null)
            {
                return RedirectToAction("Index");
            }

            if (Session["Rol"].ToString().Equals(this.rolCliente))
            {
                ViewBag.User = Session["User"];
                return View();
            }

            else
            {
                return RedirectToAction("Admin");
            }
        }

        //Metodo Logout
        public ActionResult Logout()
        {
            Session["User"] = null;

            return RedirectToAction("Index");
        }

        //Metodo Nuevo
        [HttpGet]
        public ActionResult Nuevo()
        {
            var roles = new List<Rol>();

            using (var context = new LoginContext())
            {
                roles = context.Roles.ToList();
            }
            

            ViewBag.Roles = new SelectList(roles, "RolID", "RolName");
            return View();
        }

        //Metodo Nuevo  
        [HttpPost]
        public ActionResult Nuevo(Usuario model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (LoginContext db = new LoginContext())
                    {
                        var oUsu = new Usuario();
                        oUsu.Nombre = model.Nombre;
                        oUsu.Apellido = model.Apellido;
                        oUsu.Direccion = model.Direccion;
                        oUsu.RoldID = model.RoldID;
                        oUsu.UserName = model.UserName;
                        oUsu.Password = model.Password;

                        db.Usuarios.Add(oUsu);
                        db.SaveChanges();
                    }
                    return Redirect("/");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}