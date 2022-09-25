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
        IEnumerable<EventDTO> GetAll(); // getting all events
        EventEntity Create(AddEventDTO addEventDto); // creating an event
        EventEntity Get(int id); // getting/reading 
        EventEntity Update(int id, AddEventDTO addEventDTO); // updating an event
        void Delete(int id);    // deleting an event
        void Dispose();
    }
}
