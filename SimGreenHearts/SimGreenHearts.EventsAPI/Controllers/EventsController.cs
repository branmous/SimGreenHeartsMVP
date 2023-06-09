﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimGreenHearts.EventsAPI.Data;
using SimGreenHearts.EventsAPI.Helpers;
using SimGreenHearts.Shared.DTO;
using SimGreenHearts.Shared.Entities;
using SimGreenHearts.Shared.Enums;

namespace SimGreenHearts.EventsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EventsController : ControllerBase
    {
        private readonly DataContext dataContext;
        private readonly IUserHelper userHelper;

        public EventsController(DataContext dataContext, IUserHelper userHelper)
        {
            this.dataContext = dataContext;
            this.userHelper = userHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            var user = await userHelper.GetUserAsync(User.Identity!.Name!);
            if (user == null)
            {
                return NotFound();
            }

            List<Event> events = new List<Event>();
            if (user.UserType != UserType.Admin && user.UserType != UserType.Provider)
            {
                events = await dataContext.Events.Include(e => e.User).Include(e => e.EventType).Where(e => e.UserId == user.Id).ToListAsync();
            }
            else
            {
                events = await dataContext.Events.Include(e => e.User).Include(e => e.EventType).ToListAsync();

            }

            var eventList = new List<EventListDTO>();
            foreach (var item in events)
            {
                eventList.Add(new EventListDTO
                {
                    CreatedDate = item.CreatedDate,
                    EventType = new EventTypeDTO
                    {
                        Description = item.EventType!.Description,
                        Id = item.EventType!.Id,
                    },
                    Id = item.Id,
                    Observation = item.Observation,
                    UserFullName = item.User!.FullName

                });
            }

            return Ok(eventList.OrderByDescending(e => e.CreatedDate));
        }

        [HttpPost]
        public async Task<IActionResult> PostEvent([FromBody] EventDTO eventDTO)
        {
            var user = await userHelper.GetUserAsync(User.Identity!.Name!);
            if (user == null)
            {
                return NotFound();
            }

            var eventEntity = new Event();
            eventEntity.Observation = eventDTO.Observation;
            eventEntity.CreatedDate = DateTime.Now;
            eventEntity.EventTypeId = eventDTO.EventId;
            eventEntity.UserId = user.Id;
            dataContext.Add(eventEntity);
            try
            {
                await dataContext.SaveChangesAsync();
                return Ok(eventEntity);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
