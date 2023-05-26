using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimGreenHearts.EventsAPI.Data;
using SimGreenHearts.Shared.Entities;

namespace SimGreenHearts.EventsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EventTypesController : ControllerBase
    {
        private readonly DataContext dataContext;

        public EventTypesController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult> GetEventTypes()
        {
            return Ok(await dataContext.EventTypes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsyc(int id)
        {
            var eventType = await dataContext.EventTypes.FirstOrDefaultAsync(c => c.Id == id);
            if (eventType == null)
            {
                return NotFound();
            }
            return Ok(eventType);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] EventType eventType)
        {
            try
            {
                dataContext.Add(eventType);
                await dataContext.SaveChangesAsync();
                return Ok(eventType);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe un tipo de evento con el mismo nombre.");
                }
                else
                {
                    return BadRequest(dbUpdateException.InnerException.Message);
                }
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] EventType eventType)
        {
            try
            {
                dataContext.Update(eventType);
                await dataContext.SaveChangesAsync();
                return Ok(eventType);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe un tipo de evento con el mismo nombre.");
                }
                else
                {
                    return BadRequest(dbUpdateException.InnerException.Message);
                }
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsyc(int id)
        {
            var evenType = await dataContext.EventTypes.FirstOrDefaultAsync(c => c.Id == id);
            if (evenType == null)
            {
                return NotFound();
            }

            dataContext.Remove(evenType);
            await dataContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
