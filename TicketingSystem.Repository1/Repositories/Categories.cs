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



   

    public class CategoriesRepository : IRepository<Categories>
    {
        private TicketingSystemDBEntities db;

        public CategoriesRepository(TicketingSystemDBEntities context)
        {
            this.db = context;
        }

        public IEnumerable<Categories> GetAll()
        {
            return db.Categories;
        }

        public class CategoriesList
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }
        public List<CategoriesList> GetCategories()
        {
            return db.Categories.Select(x => new CategoriesList { ID = x.ID, Name = x.Name }).ToList();
        }
            
            
        public IEnumerable<SelectListItem> GetListCategories()
        {
            var categories = db.Categories.Select(x => new SelectListItem
            {
                Value = x.ID.ToString(),
                Text = x.Name
            });
            return new SelectList(categories, "Value", "Text");
        }

        public Categories Get(int id)
        {
            return db.Categories.Find(id);
        }

        public void Create(Categories category)
        {
            db.Categories.Add(category);

        }

        public void Update(Categories order)
        {
            db.Entry(order).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Categories order = db.Categories.Find(id);
            if (order != null)
                db.Categories.Remove(order);
        }
    }

 
}
