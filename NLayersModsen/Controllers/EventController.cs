using Microsoft.AspNetCore.Mvc;
using NLayerApp.BLL.Interfaces;
using NLayerApp.BLL.DTO;
using NLayerApp.WEB.Models;
using AutoMapper;
using NLayerApp.BLL.Infrastructure;
using NLayerApp.DAL.EF;
using NLayerApp.DAL.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using NLayerApp.DAL.Interfaces;
using NLayerApp.DAL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace NLayerApp.WEB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : Controller
    {
        IEventService eventService;
        public EventController(IEventService serv)
        {
            eventService = serv;
        }

        // get all events
        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            var _event = await eventService.GetAllAsync();
            return Ok(_event);
        }

        // get event by id
        [HttpGet]
        [Route("GetById/{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var _event = await eventService.GetAsync(id);
            return Ok(_event);
        }

        // create request
        [HttpPost]
        [Route("CreateEvent")]
        public async Task<IActionResult> CreateEvent(AddEventDTO addEventDto)
        {
            var newEvent = await Task.FromResult(eventService.Create(addEventDto));
            return Ok(newEvent);
        }

        // update request
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, AddEventDTO item)
        {
            var getIssueEvent = await eventService.UpdateAsync(id, item);
            return Ok(getIssueEvent);
        }

        // delete request
        [HttpDelete]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var _event = await eventService.GetAsync(id);
            if (_event != null)
            {
                eventService.Delete(id);
            }
            return Ok(_event);
        }
    }
}
