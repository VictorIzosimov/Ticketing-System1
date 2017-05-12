using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Security.Principal;
using TicketingSystem.Entities;
namespace TicketingSystem.Models
{
    public class TicketsRepository : IRepository<Tickets>
    {
        private TicketingSystemDBEntities db;

        public TicketsRepository(TicketingSystemDBEntities context)
        {
            this.db = context;
        }

        public IEnumerable<Tickets> GetAll()
        {
            return db.Tickets.Include(o => o.ID);
        }

        public Tickets Get(int id)
        {
            return db.Tickets.Find(id);
        }
        public class TicketsList
        {
            public int ID { get; set; }
            public string Title { get; set; }
            public string Category { get; set; }
            public string Author { get; set; }
            public int CommentsCount { get; set; }
        }

        public class TicketsList2: TicketsList
        {
            public string Priority { get; set; }
        }
        public class TicketsList3 : TicketsList2
        {
            public string Description { get; set; }
            public string Screenshot { get; set; }

            public IEnumerable<Comments> Comments { get; set; }
        }
        public List<TicketsList> GetFirst6()
        {
           
            return db.Tickets.Select(x => new TicketsList { ID = x.ID, Title = x.Title, Category = x.Categories.Name, Author = x.AspNetUsers.UserName, CommentsCount = x.Comments.Count() }).OrderBy(x => x.CommentsCount).Take(6).ToList();
             
        }

        public List<TicketsList2> GetTicketsList(string categoryName)
        {
            var query = db.Tickets.Select(x => new TicketsList2 { ID = x.ID, Title = x.Title, Category = x.Categories.Name, Author = x.AspNetUsers.UserName, Priority = x.Priority });
            if (!string.IsNullOrEmpty(categoryName)) return query.Where(x => x.Category == categoryName).ToList(); else
            return query.ToList();

        }

        public List<TicketsList3> GetTicketListByID(int ID)
        {
            return db.Tickets.Select(x => new TicketsList3 { Comments = x.Comments, ID = x.ID, Title = x.Title, Category = x.Categories.Name, Author = x.AspNetUsers.UserName, Priority = x.Priority, Description = x.Description, Screenshot = x.Screenshot }).Where(x => x.ID == ID).ToList();

        }

        public void Create(Tickets ticket)
        {
            var user = db.AspNetUsers.Find(ticket.Author);
            user.Points++;
            db.Tickets.Add(ticket);
           
        }


        public void Update(Tickets ticket)
        {
            db.Entry(ticket).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Tickets ticket = db.Tickets.Find(id);
            if (ticket != null)
                db.Tickets.Remove(ticket);
        }
    }
}
