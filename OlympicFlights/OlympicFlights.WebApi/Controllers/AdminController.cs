using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using OlympicFlights.Service.FlightManagement;
using OlympicFlights.Service.OlympicFlightsServiceExceptions;
using OlympicFlights.Service.TicketManagement;
using System;
using System.Threading.Tasks;

namespace OlympicFlights.WebApi.Controllers
{
    [Route("api/admin")]
    [ApiController]
    [EnableCors]
    //[Authorize(Roels = "")
    public class AdminController : ControllerBase
    {
        private readonly ITicketAdministrate _tAdministrate;
        private readonly IFlightService _fService;

        public AdminController(ITicketAdministrate tAdministrate, IFlightService fService)
        {
            _tAdministrate = tAdministrate ?? throw new ArgumentNullException(nameof(tAdministrate));
            _fService = fService ?? throw new ArgumentNullException(nameof(fService));
        }

        [HttpPut("tickets/approve/all")]
        public async Task<object> ApproveTickets(int[] ticketIds)
        {
            try
            {
                var areTicketsApproved = await _tAdministrate.ApproveTickets(ticketIds);

                return Ok(new { successMessage = "Tickets where apporved" });
            }
            catch (IdNotFoundException ex)
            {
                return NotFound(new { errorMessage = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { errorMessage = ex.Message });
            }
        }

        [HttpPut("tickets/approve/{ticketId}")]
        public async Task<object> ApproveTicket([FromRoute] int ticketId)
        {
            try
            {
                var areTicketApproved = await _tAdministrate.ApproveTicket(ticketId);

                if (areTicketApproved)
                {
                    return Ok(new { successMessage = "Ticket was apporved" });
                }
                else
                {
                    throw new Exception("Some error occured while approving tickets");
                }
            }
            catch (IdNotFoundException ex)
            {
                return NotFound(new { errorMessage = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { errorMessage = ex.Message });
            }
        }
    }
}
