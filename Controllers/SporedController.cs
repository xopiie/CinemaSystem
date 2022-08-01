using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Uzunova_Nadica_1002387434_DSR_2021.Models;

namespace Uzunova_Nadica_1002387434_DSR_2021.Controllers
{
    public class SporedController : Controller
    {
        private readonly string linkRezervacijaAPI = "https://localhost:44315/api/RezervacijaAPI/";
        private static readonly HttpClient _client = new HttpClient();
        public IActionResult Index()
        {
            using (var context = new dbContext())
            {
                List<Spored> spored = new List<Spored>();
                spored = context.Spored.ToList();

                foreach(var vnos in spored)
                {
                    List<Rezervacija> seznamRezervacij = new List<Rezervacija>();
                    seznamRezervacij = context.Rezervacije.Where(x => x.IdSpored == vnos.Id).ToList();

                    Dvorana dvorana = new Dvorana();
                    dvorana = context.Dvorane.FirstOrDefault(x => x.Naziv == vnos.NazivDvorane);

                    int x = 0;
                    foreach(var r in seznamRezervacij)
                    {
                        x += r.SteviloSedezev;
                    }

                    if (dvorana != null) 
                        vnos.ProsteSedeze = dvorana.Stevilo_sedezev - x;
                    else 
                        vnos.ProsteSedeze = -1;

                }
                return View(spored);
            }
        }

        public async Task<IActionResult> SeznamRezervacij()
        {
            string uporabnik = User.Identity.Name;
            List<Rezervacija> seznamRezervacij = new List<Rezervacija>();


            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (User.IsInRole("Zaposleni"))
            {
                var stringRezervacije = await _client.GetStreamAsync(linkRezervacijaAPI);
                seznamRezervacij = await JsonSerializer.DeserializeAsync<List<Rezervacija>>(stringRezervacije);
            }
            else
            {
                var stringRezervacije = await _client.GetStreamAsync(linkRezervacijaAPI + $"Uporabnik/{uporabnik}");
                seznamRezervacij = await JsonSerializer.DeserializeAsync<List<Rezervacija>>(stringRezervacije);
            }

            seznamRezervacij.Reverse();

            return View(seznamRezervacij);
        }

        [Authorize(Roles = "Zaposleni")]
        public IActionResult Dodaj()
        {
            SporedDodaj dodajanje = new SporedDodaj();
            try
            {
                using (var context = new dbContext())
                {

                    context.Filmi.ToList().ForEach(x => dodajanje.Filmi.Add(x.Naslov));
                    context.Dvorane.ToList().ForEach(x => dodajanje.Dvorane.Add(x.Naziv));

                }
            }

            catch
            {

            
            }
            return View(dodajanje);
        }

        [Authorize(Roles = "Zaposleni")]
        [HttpPost]
        public IActionResult Dodaj(SporedDodaj spored)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var context = new dbContext())
                    {
                        Spored s = new Spored();
                        s.NaslovFilma = spored.NaslovFilma;
                        s.NazivDvorane = spored.NazivDvorane;
                        s.DatumCas = spored.DatumCas;
                        
                        context.Spored.Add(s);
                        context.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }

                return View(spored);
            }
            catch (Exception)
            {
                return View(spored);
            }
        }

        public IActionResult Rezervacija(int id)
        {
            Rezervacija r = new Rezervacija();
            r.IdSpored = id;
            return View(r);
        }

        public IActionResult RezervacijaById(int id)
        {
            using (var context = new dbContext())
            {
                return RedirectToAction("Rezervacija", new Rezervacija() { IdSpored = id }) ;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Rezerviraj(Rezervacija rezervacija)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    rezervacija.Email = User.Identity.Name;

                    _client.DefaultRequestHeaders.Accept.Clear();
                    _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    StringContent vsebina = new StringContent(JsonSerializer.Serialize(rezervacija));
                    vsebina.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var result = await _client.PostAsync(linkRezervacijaAPI, vsebina);

                    return RedirectToAction("SeznamRezervacij");
                }
                return View(rezervacija);
            }
            catch (Exception)
            {
                return View(rezervacija);
            }
        }

        public async Task<IActionResult> OdstraniRezervacijoAsync(int id)
        {
            try
            {
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage httpResponse = await _client.DeleteAsync(linkRezervacijaAPI + id);
            }
            catch (Exception)
            {

            }

            return RedirectToAction("SeznamRezervacij");
        }

        [Authorize(Roles = "Zaposleni")]
        public IActionResult Odstrani(int id)
        {
            try
            {
                using (var context = new dbContext())
                {
                    context.Spored.Remove(context.Spored.SingleOrDefault(x => x.Id == id));
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
                return View(context.Spored.SingleOrDefault(x => x.Id == id));
            }
        }

        [Authorize(Roles = "Zaposleni")]
        [HttpPost]
        public IActionResult Uredi(Spored spored)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var context = new dbContext())
                    {
                        context.Entry(context.Spored.SingleOrDefault(x => x.Id == spored.Id)).CurrentValues.SetValues(spored);
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

