using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.IdentityModel.Tokens;
using NLayerApp.BLL.DTO;
using NLayerApp.BLL.Infrastructure;
using NLayerApp.BLL.Interfaces;
using NLayerApp.DAL.Entities;
using NLayerApp.DAL.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.BLL.Services
{
    public class EventService : IEventService
    {
        IUnitOfWork Database { get; set; }
        
        // constructor
        public EventService(IUnitOfWork database)
        {
            this.Database = database;
        }
        // getting all events from a table
        public async Task<IEnumerable<EventDTO>> GetAllAsync()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<EventEntity, EventDTO>()).CreateMapper();
            var allEvents = await Database.Events.GetAll();
            return await Task.FromResult(mapper.Map<IEnumerable<EventEntity>, List<EventDTO>>(allEvents));
        }

        // reading an event by id from a table
        public async Task<EventEntity> GetAsync(int id)
        {
            var _event = await Database.Events.GetAsync(id);
            if (_event == null)
            {
                throw new ValidationException("the Event doesn't exist", "");
            }
            return _event;
        }

        // updating a row in a table
        public async Task<EventEntity> UpdateAsync(int id, AddEventDTO eventDto)
        {
                var _event = await Database.Events.GetAsync(id);
                if (eventDto == null)
                    throw new ArgumentNullException(nameof(_event));

                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<AddEventDTO, EventEntity>()).CreateMapper();
                _event = mapper.Map(eventDto, _event);

                Database.Save();
                return _event;
        }

        // creating an event in the table
        public EventEntity Create(AddEventDTO addEventDto)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<AddEventDTO, EventEntity>()).CreateMapper();
            var newEvent = mapper.Map<EventEntity>(addEventDto);
            if (newEvent == null)
            {
                throw new ArgumentNullException(nameof(newEvent));
            }
            Database.Events.Create(newEvent);
            Database.Save();
            return newEvent;
        }

        // deleting an event from a table
        public void Delete(int id)
        {

            Database.Events.Delete(id);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
