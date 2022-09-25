using NLayerApp.BLL.DTO;
using NLayerApp.DAL.EF;
using NLayerApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.BLL.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<EventDTO>> GetAllAsync(); // getting all events
        EventEntity Create(AddEventDTO addEventDto); // creating an event
        Task<EventEntity> GetAsync(int id); // getting/reading 
        Task<EventEntity> UpdateAsync(int id, AddEventDTO addEventDTO); // updating an event
        void Delete(int id);    // deleting an event
        void Dispose();
    }
}
