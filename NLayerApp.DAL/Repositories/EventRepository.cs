using Microsoft.EntityFrameworkCore;
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
        public async Task<IEnumerable<EventEntity>> GetAll()
        {
            return await db.Events.ToListAsync();
        }

        // Get event by Id
        public async Task<EventEntity> GetAsync(int id)
        {
            var _event = await db.Events.FindAsync(id);
            if (_event == null)
            {
                throw new ArgumentNullException(nameof(_event));
            }
            return _event;
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
