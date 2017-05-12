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

    public class CommentsRepository : IRepository<Comments>
    {
        private TicketingSystemDBEntities db;

        public class CommentsList
        {
            public string Content { get; set; }
            public string User { get; set; }

        }
        public CommentsRepository(TicketingSystemDBEntities context)
        {
            this.db = context;
        }

        public List<CommentsList> GetListByTicketsID(int ID)
        {
            return db.Comments.Where(x => x.Ticket == ID).Select(x => new CommentsList { Content = x.Content, User = x.AspNetUsers.UserName }).ToList();
        }
        public IEnumerable<Comments> GetAll()
        {
            return db.Comments;
        }

        public Comments Get(int id)
        {
            return db.Comments.Find(id);
        }

        public void Create(Comments comment)
        {
            db.Comments.Add(comment);

        }

        public void Update(Comments comment)
        {
            db.Entry(comment).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Comments comment = db.Comments.Find(id);
            if (comment != null)
                db.Comments.Remove(comment);
        }
    }
}

 

  