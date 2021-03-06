using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using TicketingSystem.Entities;
using TicketingSystem.Models;

namespace TicketingSystem.Controllers
{
    public class HomeController : Controller
    {
        UnitOfWork unitOfWork;
        public HomeController()
        {
            unitOfWork = new UnitOfWork();
        }
        
        [OutputCache(Duration=3600)]
        public ActionResult Index()
        {   
            var model = unitOfWork.Tickets.GetFirst6();
            return View(model);
        }

        

        [HttpPost]
        public ActionResult Filter(string categoryFilter)
        {
            return PartialView("SearchResultsPartial", unitOfWork.Tickets.GetTicketsList(categoryFilter);
        }

        public ActionResult ViewAllTickets()
        {
            var model = new AddTicketViewModel
            {
                Categories = unitOfWork.Categories.GetListCategories()
            };
            
            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult ViewAllTickets(AddTicketViewModel model)
        {
            return View(model);
        }
        
        public ActionResult ShowTicket(int ID)
        {
            return View(unitOfWork.Tickets.GetTicketListByID(ID));
        }

 
        [Authorize(Roles = "user")]
        public ActionResult AddComment(string ID, string comment)
        {
            int ID1 = int.Parse(ID);
            if ((!string.IsNullOrEmpty(ID)) && (!string.IsNullOrEmpty(comment)))
            {
                unitOfWork.Comments.Create(new Comments { User = User.Identity.GetUserId(), Content = comment, Ticket = ID1 });
                unitOfWork.Save();
            }
            return PartialView("AddCommentsPartial", unitOfWork.Comments.GetListByTicketsID(ID1));
        }


        [Authorize(Roles = "user")]
        public ActionResult AddTicket()
        {
            var model = new AddTicketViewModel
            {
                Categories = unitOfWork.Categories.GetListCategories()
        };
            return View(model);
        }
        
        [Authorize(Roles = "user")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddTicket(AddTicketViewModel model)
        {
            model.Categories = unitOfWork.Categories.GetListCategories();
            
            if (ModelState.IsValid)
            {
                var ticket = new Tickets {  Author = User.Identity.GetUserId(), Title = model.Title, Priority = model.Priority, Description = model.Description, Screenshot = model.Screenshot, Category = model.Category };

                unitOfWork.Tickets.Create(ticket);
                unitOfWork.Save();
                    return RedirectToAction("Index", "Home");

            }

            return View(model);
        }
    }
}
