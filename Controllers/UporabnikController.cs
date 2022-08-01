using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uzunova_Nadica_1002387434_DSR_2021.Areas.Identity.Data;
using Uzunova_Nadica_1002387434_DSR_2021.Data;
using Uzunova_Nadica_1002387434_DSR_2021.Models;

namespace Uzunova_Nadica_1002387434_DSR_2021.Controllers
{
    //[Authorize(Roles = "Zaposleni")]
    public class UporabnikController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<Uporabnik> userManager;
        public UporabnikController(RoleManager<IdentityRole> roleManager, UserManager<Uporabnik> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Inicializiraj()
        {

            IdentityRole identityRoleGost = new IdentityRole
            {
                Name = "Gledalec"
            };
            IdentityResult gost = await roleManager.CreateAsync(identityRoleGost);

            IdentityRole identityRoleZaposleni = new IdentityRole
            {
                Name = "Zaposleni"
            };
            IdentityResult zaposleni = await roleManager.CreateAsync(identityRoleZaposleni);

            var gledalec1 = new Uporabnik { UserName = "gledalec@cinemovie.si", Email = "gledalec@cinemovie.si", Ime = "Janez", Priimek = "Novak" };
            await userManager.CreateAsync(gledalec1, "Janez-Novak123");

            var zaposleni1 = new Uporabnik { UserName = "zaposleni@cinemovie.si", Email = "zaposleni@cinemovie.si", Ime = "Nadica", Priimek = "Uzunova" };
            await userManager.CreateAsync(zaposleni1, "Nadica-Uzunova123");

            await userManager.AddToRoleAsync(await userManager.FindByEmailAsync("gledalec@cinemovie.si"), "Gledalec");
            await userManager.AddToRoleAsync(await userManager.FindByEmailAsync("zaposleni@cinemovie.si"), "Zaposleni");

            return RedirectToAction("Index");
        }


        public IActionResult Index()
        {

                List<UporabnikZGeslom> seznam = new List<UporabnikZGeslom>();

                var seznamUporabnikov = userManager.Users.ToList();

                foreach (var x in seznamUporabnikov)
                {
                    UporabnikZGeslom y = new UporabnikZGeslom();
                    y.Id = x.Id;
                    y.Ime = x.Ime;
                    y.Priimek = x.Priimek;
                    y.DatumRojstva = x.DatumRojstva;
                    y.Naslov = x.Naslov;
                    y.Posta = x.Posta;
                    y.PostnaStevilka = x.PostnaStevilka;
                    y.Drzava = x.Drzava;
                    y.Email = x.Email;

                    seznam.Add(y);
                }

                return View(seznam);
        }



        public async Task<IActionResult> Odstrani(UporabnikZGeslom u)
        {
            try
            {
                var user = await userManager.FindByIdAsync(u.Id);
                if (user == null)
                {
                    ViewBag.ErrorMessage = $"User with ID:{u.Id} does not exist!";
                    return NotFound();
                }
                else
                {
                    await userManager.DeleteAsync(user);
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }

        [Authorize(Roles = "Zaposleni")]
        public async Task<IActionResult> Uredi(string id)
        {
            var x = await userManager.FindByIdAsync(id);
            UporabnikZGeslom y = new UporabnikZGeslom();
            y.Id = x.Id;
            y.Ime = x.Ime;
            y.Priimek = x.Priimek;
            y.DatumRojstva = x.DatumRojstva;
            y.Naslov = x.Naslov;
            y.Posta = x.Posta;
            y.PostnaStevilka = x.PostnaStevilka;
            y.Drzava = x.Drzava;
            y.Email = x.Email;

            return View(y);
        }

        [Authorize(Roles = "Zaposleni")]
        [HttpPost]
        public async Task<IActionResult> Uredi(UporabnikZGeslom u)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(u.Id);
                if (user == null)
                {
                    ViewBag.ErrorMessage = $"User with ID:{u.Id} does not exist!";
                    return NotFound();
                }
                else
                {

                    Uporabnik uporabnik = user;
                    uporabnik.Ime = u.Ime;
                    uporabnik.Priimek = u.Priimek;
                    uporabnik.DatumRojstva = u.DatumRojstva;
                    uporabnik.Naslov = u.Naslov;
                    uporabnik.Posta = u.Posta;
                    uporabnik.PostnaStevilka = u.PostnaStevilka;
                    uporabnik.Drzava = u.Drzava;
                    uporabnik.Email = u.Email;
                    uporabnik.UserName = u.Email;

                    await userManager.UpdateAsync(uporabnik);
                    //var resultEmail = await userManager.SetEmailAsync(uporabnik, model.Email);
                    //var resultUserName = await userManager.SetUserNameAsync(uporabnik, model.Email);

                    var token = await userManager.GenerateEmailConfirmationTokenAsync(uporabnik);
                    await userManager.ConfirmEmailAsync(uporabnik, token);

                    if (u.Password == null || u.Password == "")
                    {
                        // DO NOT CHANGE PASSWORD
                    }
                    else
                    {
                        var passwordValidator = new PasswordValidator<Uporabnik>();
                        var passValidation = await passwordValidator.ValidateAsync(userManager, null, u.Password);
                        if (passValidation.Succeeded)
                        {
                            await userManager.RemovePasswordAsync(uporabnik);
                            var result = await userManager.AddPasswordAsync(uporabnik, u.Password);

                            if (!result.Succeeded)
                            {
                                foreach (var error in result.Errors)
                                {
                                    ModelState.AddModelError("Password", error.Description);
                                }
                                return View(u);
                            }
                        }
                        else
                        {
                            foreach (var error in passValidation.Errors)
                            {
                                ModelState.AddModelError("Password", error.Description);
                            }
                            return View(u);
                        }

                    }



                    return RedirectToAction("Index");
                }
            }

            return View(u);
        }
    }
}

