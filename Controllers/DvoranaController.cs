using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uzunova_Nadica_1002387434_DSR_2021.Models;

namespace Uzunova_Nadica_1002387434_DSR_2021.Controllers
{
    [Authorize]
    public class DvoranaController : Controller
    {
        public IActionResult Index()
        {
            using (var context = new dbContext())
            {
                return View(context.Dvorane.ToList());
            }
        }

        [Authorize(Roles = "Zaposleni")]
        public IActionResult Dodaj()
        {
            return View();
        }

        [Authorize(Roles = "Zaposleni")]
        [HttpPost]
        public IActionResult Dodaj(Dvorana dvorana)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var context = new dbContext())
                    {
                        context.Dvorane.Add(dvorana);
                        context.SaveChanges();
                    }
                    return RedirectToAction("Pregled", dvorana);
                }
                return View();
            }
            catch (Exception)
            {
                return View();
            }
        }

        public IActionResult Pregled(Dvorana dvorana)
        {
            return View(dvorana);
        }

        public IActionResult PregledByID(int id)
        {
            using (var context = new dbContext())
            {
                return RedirectToAction("Pregled", context.Dvorane.SingleOrDefault(x => x.Id == id));
            }
        }

        [Authorize(Roles = "Zaposleni")]
        public IActionResult Odstrani(int id)
        {
            try
            {
                using (var context = new dbContext())
                {
                    context.Dvorane.Remove(context.Dvorane.SingleOrDefault(x => x.Id == id));
                    context.SaveChanges();
                }

            }
            catch (Exception)
            {

            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Zaposleni")]
        public IActionResult Uredi(int id)
        {
            using (var context = new dbContext())
            {
                return View(context.Dvorane.SingleOrDefault(x => x.Id == id));
            }
        }

        [Authorize(Roles = "Zaposleni")]
        [HttpPost]
        public IActionResult Uredi(Dvorana dvorana)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var context = new dbContext())
                    {
                        context.Entry(context.Dvorane.SingleOrDefault(x => x.Id == dvorana.Id)).CurrentValues.SetValues(dvorana);
                        context.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch (Exception)
            {
                return View();
            }
        }
    }
}
