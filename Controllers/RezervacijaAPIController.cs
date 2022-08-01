using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uzunova_Nadica_1002387434_DSR_2021.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Uzunova_Nadica_1002387434_DSR_2021.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RezervacijaAPIController : ControllerBase
    {
        private readonly dbContext context = new dbContext();

        // GET: api/<RezervacijaAPIController>
        [HttpGet]
        public IEnumerable<Rezervacija> GetAll()
        {
            try
            {
                List<Rezervacija> rezervacije = new List<Rezervacija>();
                rezervacije = context.Rezervacije.ToList();

                foreach(var rezervacija in rezervacije)
                {
                    int sporedID = rezervacija.IdSpored;

                    try
                    {
                        rezervacija.Film = context.Spored.First(x => x.Id == sporedID).NaslovFilma;
                        rezervacija.Dvorana = context.Spored.First(x => x.Id == sporedID).NazivDvorane;
                        rezervacija.DatumCas = context.Spored.First(x => x.Id == sporedID).DatumCas;
                    }
                    catch (Exception)
                    {
                        rezervacija.Film = "ODPOVEDANO";
                        rezervacija.Dvorana = "ODPOVEDANO";
                        rezervacija.DatumCas = new DateTime();
                    }
                    
                }

                return rezervacije;
            }

            catch
            {
                return new List<Rezervacija>();
            }
        }

        // GET api/<RezervacijaAPIController>/5
        [HttpGet("{id}")]
        public Rezervacija GetId(int id)
        {
            try
            {
                Rezervacija rezervacija = new Rezervacija();
                rezervacija = context.Rezervacije.FirstOrDefault(x => x.Id == id);
                int sporedID = rezervacija.IdSpored;

                try
                {
                    rezervacija.Film = context.Spored.First(x => x.Id == sporedID).NaslovFilma;
                    rezervacija.Dvorana = context.Spored.First(x => x.Id == sporedID).NazivDvorane;
                    rezervacija.DatumCas = context.Spored.First(x => x.Id == sporedID).DatumCas;
                }
                catch (Exception)
                {
                    rezervacija.Film = "ODPOVEDANO";
                    rezervacija.Dvorana = "ODPOVEDANO";
                    rezervacija.DatumCas = new DateTime();
                }


                return rezervacija;
            }

            catch
            {
                return new Rezervacija();
            }
        }

        // GET: api/<RezervacijaAPIController>
        [HttpGet("Uporabnik/{uporabnik}")]
        public IEnumerable<Rezervacija> GetByUser(string uporabnik)
        {
            try
            {
                List<Rezervacija> rezervacije = new List<Rezervacija>();
                rezervacije = context.Rezervacije.Where(x => x.Email == uporabnik).ToList();

                foreach (var rezervacija in rezervacije)
                {
                    int sporedID = rezervacija.IdSpored;

                    try
                    {
                        rezervacija.Film = context.Spored.First(x => x.Id == sporedID).NaslovFilma;
                        rezervacija.Dvorana = context.Spored.First(x => x.Id == sporedID).NazivDvorane;
                        rezervacija.DatumCas = context.Spored.First(x => x.Id == sporedID).DatumCas;
                    }
                    catch (Exception)
                    {
                        rezervacija.Film = "ODPOVEDANO";
                        rezervacija.Dvorana = "ODPOVEDANO";
                        rezervacija.DatumCas = new DateTime();
                    }
                }

                return rezervacije;
            }

            catch
            {
                return new List<Rezervacija>();
            }
        }

        // POST api/<RezervacijaAPIController>
        [HttpPost]
        public void Post([FromBody] Rezervacija rezervacija)
        {
            try
            {
                context.Rezervacije.Add(rezervacija);
                context.SaveChanges();
            }

            catch
            {
            }
        }


        // DELETE api/<RezervacijaAPIController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            try
            {
                context.Rezervacije.Remove(context.Rezervacije.Single(x => x.Id == id));
                context.SaveChanges();
            }
            catch
            {
            }
        }
    }
}
