using NLayerApp.DAL.EF;
using NLayerApp.DAL.Entities;
using NLayerApp.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.DAL.Repositories
{
    public class EventRepository : IRepository<EventEntity>
    {
        private EventContext db;

        public EventRepository(EventContext context)
        {
            this.db = context ?? throw new ArgumentNullException(nameof(context));
        }

        // get all events
        public IEnumerable<EventEntity> GetAll()
        {
            return db.Events;
        }

        // Get event by Id
        public EventEntity Get(int id)
        {
            return db.Events.Find(id) ?? throw new ArgumentNullException(nameof(id));
        }

        // create event 
        public void Create(EventEntity _event)
        {
            db.Events.Add(_event);
        }

        // update
        public void Update(EventEntity _event)
        {
            db.Entry(_event).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        // find
        public IEnumerable<EventEntity> Find(Func<EventEntity, Boolean> predicate)
        {
            return db.Events.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            EventEntity _event = db.Events.Find(id) ?? throw new ArgumentNullException(nameof(id));
            if (_event != null)
            {
                db.Events.Remove(_event);
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

    }
}
