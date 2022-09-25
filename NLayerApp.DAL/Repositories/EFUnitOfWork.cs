using NLayerApp.DAL.EF;
using NLayerApp.DAL.Interfaces;
using NLayerApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NLayerApp.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        EventContext db;
        EventRepository eventRepository;
        public EFUnitOfWork(DbContextOptions options, IRepository<EventEntity> repository)
        {
            db = new EventContext(options);
            //eventRepository = repository;
        }
        public IRepository<EventEntity> Events
        {
            get
            {
                if(eventRepository == null)
                {
                    eventRepository = new EventRepository(db);
                }
                return eventRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        // gc collector
        public virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                if(disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
