using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using OlympicFlights.Service.CityManagement;
using OlympicFlights.Service.OlympicFlightsServiceExceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OlympicFlights.WebApi.Controllers
{
    [Route("api/cities")]
    [ApiController]
    [EnableCors]
    //[Authorize]
    public class CitiesController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CitiesController(ICityService cityService)
        {
            _cityService = cityService ?? throw new ArgumentNullException(nameof(cityService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> GetAllCities()
        {
            try
            {
                var countries = await _cityService.GetCitiesAsync();

                return Ok(countries);
            }
            catch (ItemsNotFoundException ex)
            {
                return this.NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<City>> GetCityById(int id)
        {
            try
            {
                var city = await _cityService.GetCityByIdAsync(id);

                return Ok(city);
            }
            catch (IdNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("/country/{countryId}")]
        public async Task<ActionResult<IEnumerable<City>>> GetCitiesByCountryId(int countryId)
        {
            try
            {
                var cities = await _cityService.GetCitiesByCountryIdAsync(countryId);

                return Ok(cities);
            }
            catch (IdNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        } 

        [HttpPost]
        //[Authorize(Roles = "applicationAdmin")]
        public async Task<ActionResult<City>> CreateCity([FromBody] UpdateCityRequest city)
        {
            try
            {
                var created = await _cityService.CreateCityAsync(city);

                return Ok(created);
            }
            catch (DuplicateFoundException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500,new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "applicationAdmin")]
        public async Task<ActionResult<int>> DeleteCityById(int id)
        {
            try
            {
                var removed = await _cityService.RemoveCityByIdAsync(id);

                return Ok(removed);
            }
            catch (IdNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost("{id}")]
        //[Authorize(Roles = "applicationAdmin")]
        public async Task<ActionResult<City>> UpdateCityById([FromBody]UpdateCityRequest city, int id)
        {
            try
            {
                var updated = await _cityService.UpdateCityByIdAsync(id, city);

                return Ok(updated);
            }
            catch(IdNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch(DuplicateFoundException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost("{id}/status/{cityStatus}")]
        //[Authorize(Roles = "applicationAdmin")]
        public async Task<ActionResult<City>> UpdateCityById([FromRoute]int id, bool cityStatus)
        {
            try
            {
                var isStatusChanged = await _cityService.SetCityStatusById(id, cityStatus);

                if (!isStatusChanged)
                {
                    throw new Exception("Some error occured while trying to set status of current city");
                }
                
                return Ok(new { successMessage = "City status was changed" });
            }
            catch (IdNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
