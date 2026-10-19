using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OlimpikonokApi.DTOs;
using OlimpikonokApi.Models;
using OlimpikonokApi.Services;

namespace OlimpikonokApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SportoloController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllSportolok()
        {
            try
            {
                List<Sportolo> sportolok = new SportoloService().GetAllSportolok();
                return Ok(sportolok);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Hiba történt a sportolók lekérése során: {ex.Message}");
            }
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                Sportolo? eredmeny = new SportoloService().GetSportoloById(id);
                if (eredmeny != null)
                {
                    return Ok(eredmeny);
                }
                else
                {
                    return NotFound($"Nincs sportoló a megadott azonosítóval: {id}");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Hiba a kérés teljesítése közben" + ex.Message);
            }
        }

        // egy példa arra ha nem a service rétegben készítjük el a DTO-t hanem a controller rétegben
        [HttpGet("GetAllSportoloDTO")]
        public IActionResult GetAllSportoloDTO()
        {
            try
            {
                using (var context = new OlimpikonokContext()) 
                {
                    List<SportoloDTO> dtok = context.Sportolos.Include(s => s.Sportag).Include(s => s.Orszag).Select(s => new SportoloDTO() 
                    {
                        Id = s.Id,
                        Nev = s.Nev,
                        SportagNev = s.Sportag.Megnevezes,
                        Ermek = s.Ermek,
                        OrszagNev = s.Orszag.Nev
                    }).ToList();
                    return Ok(dtok);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Hiba történt a sportolók lekérése során: {ex.Message}");
            }
        }

        [HttpGet("GetSportoloDTOById/{id}")]
        public IActionResult GetSportoloDTOById(int id) 
        {
            try
            {
                using (var context = new OlimpikonokContext()) 
                {
                    SportoloDTO? dto = context.Sportolos.Include(s => s.Sportag).Include(s => s.Orszag).Where(s => s.Id == id).Select(s => new SportoloDTO()
                    {
                        Id = s.Id,
                        Nev = s.Nev,
                        SportagNev = s.Sportag.Megnevezes,
                        Ermek = s.Ermek,
                        OrszagNev = s.Orszag.Nev
                    }).FirstOrDefault();
                    return Ok(dto);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Hiba történt a sportoló lekérése során: {id}" + ex.Message);
            }
        }

    }
}
