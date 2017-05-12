using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TicketingSystem.Entities;

namespace TicketingSystem.Models
{

    public class UnitOfWork : IDisposable
    {
        private TicketingSystemDBEntities db = new TicketingSystemDBEntities();
        private CommentsRepository commentsRepository;
        private CategoriesRepository categoriesRepository;
        private TicketsRepository ticketsRepository;
        public CommentsRepository Comments
        {
            get
            {
                if (commentsRepository == null)
                    commentsRepository = new CommentsRepository(db);
                return commentsRepository;
            }
        }

        public CategoriesRepository Categories
        {
            get
            {
                if (categoriesRepository == null)
                    categoriesRepository = new CategoriesRepository(db);
                return categoriesRepository;
            }
        }
        public TicketsRepository Tickets
        {
            get
            {
                if (ticketsRepository == null)
                    ticketsRepository = new TicketsRepository(db);
                return ticketsRepository;
            }
        }
        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
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
