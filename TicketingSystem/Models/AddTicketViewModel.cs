using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Web.Mvc;
namespace TicketingSystem.Models
{

    public class AddTicketViewModel
    {
        [Required]
        [Display(Name = "Category")]
        public int Category { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }

        [Required]
        [Display(Name = "Title")]
        [CheckBug]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Priority")]
        public string Priority { get; set; }
        public IEnumerable<SelectListItem> Priorities = new List<SelectListItem>
        {
            new SelectListItem { Text = "High", Value="High" },
            new SelectListItem { Text = "Medium", Value="Medium", Selected = true },
            new SelectListItem { Text = "Low", Value="Low" },
        };

        [Display(Name = "Screenshot")]
        public string Screenshot { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

    }

  
}