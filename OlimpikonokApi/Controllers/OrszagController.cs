using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OlimpikonokApi.Models;
using OlimpikonokApi.Services;

namespace OlimpikonokApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrszagController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllOrszagok()
        {
            try
            {
                List<Orszag> orszagok = new OrszagService().GetAllOrszagok();
                return Ok(orszagok);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Hiba történt az országok lekérése során: {ex.Message}");
            }
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id) 
        {
            try
            {
                Orszag? eredmeny = new OrszagService().GetOrszagById(id);
                if (eredmeny != null)
                {
                    return Ok(eredmeny);
                }
                else 
                {
                    return NotFound($"Nincs ország a megadott azonosítóval: {id}");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Hiba a kérés teljesítése közben" + ex.Message);
            }
        }
    }
}
