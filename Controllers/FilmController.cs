using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uzunova_Nadica_1002387434_DSR_2021.Models;

namespace Uzunova_Nadica_1002387434_DSR_2021.Controllers
{
    public class FilmController : Controller
    {
        public IActionResult Index()
        {
            using (var context = new dbContext())
            {
                return View(context.Filmi.ToList());
            }
        }

        [Authorize(Roles = "Zaposleni")]
        public IActionResult Dodaj()
        {
            return View();
        }

        [Authorize(Roles = "Zaposleni")]
        [HttpPost]
        public IActionResult Dodaj(Film film)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var context = new dbContext())
                    {
                        context.Filmi.Add(film);
                        context.SaveChanges();
                    }
                    return RedirectToAction("Pregled", film);
                }
                return View();
            }
            catch (Exception)
            {
                return View();
            }
        }


        public IActionResult Pregled(Film film)
        {
            return View(film);
        }

        public IActionResult PregledByID(int id)
        {
            using (var context = new dbContext())
            {
                return RedirectToAction("Pregled", context.Filmi.SingleOrDefault(x => x.Id == id));
            }
        }

        [Authorize(Roles = "Zaposleni")]
        public IActionResult Odstrani(int id)
        {
            try
            {
                using (var context = new dbContext())
                {
                    context.Filmi.Remove(context.Filmi.SingleOrDefault(x => x.Id == id));
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
                return View(context.Filmi.SingleOrDefault(x => x.Id == id));
            }
        }

        [Authorize(Roles = "Zaposleni")]
        [HttpPost]
        public IActionResult Uredi(Film film)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var context = new dbContext())
                    {
                        context.Entry(context.Filmi.SingleOrDefault(x => x.Id == film.Id)).CurrentValues.SetValues(film);
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
