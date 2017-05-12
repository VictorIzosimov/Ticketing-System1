using System.Web.Mvc;
using TicketingSystem.Entities;
using TicketingSystem.Models;

namespace TicketingSystem.Controllers
{

    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        UnitOfWork unitOfWork;
        public AdminController()
        {
            unitOfWork = new UnitOfWork();

        }

        public ViewResult CreateCategories()
        {
            return View("EditCategories", new Categories());
        }

        public ViewResult EditCategories(int id)
        {
           return View(unitOfWork.Categories.Get(id));
        }

        [HttpPost]
        public ActionResult DeleteCategories(int id)
        {
            unitOfWork.Categories.Delete(id);
            unitOfWork.Save();
            return RedirectToAction("Index","Home");
        }

        [HttpPost]
        public ActionResult EditCategories(Categories category)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Save();
                return RedirectToAction("Index","Home");
            }
            else
            {
                return View(category);
            }
        }


        public ActionResult AdminCategories()
        {
            return View(unitOfWork.Categories.GetAll());
        }

        public ActionResult AdminComments()
        {
            return View(unitOfWork.Comments.GetAll());
        }

        public ViewResult EditComments(int id)
        {
            return View(unitOfWork.Comments.Get(id));
        }


        [HttpPost]
        public ActionResult EditComments(Comments comment)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Save();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(comment);
            }
        }

        [HttpPost]
        public ActionResult DeleteComments(int id)
        {
            unitOfWork.Comments.Delete(id);
            unitOfWork.Save();
            return RedirectToAction("Index", "Home");
        }
    }
}