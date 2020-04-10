using SitefinityWebApp.MVC.Models;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Telerik.Sitefinity.Mvc;

namespace SitefinityWebApp.MVC.Models
{
	public class SuccessStoryWidgetViewModel
	{
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string SummaryDescription { get; set; }
        [Required]
        public string Company { get; set; }
        [Required]
        public string CompanyWebsite { get; set; }
        [Required]
        public string Industry { get; set; }
        [Required]
        public string ProductsUsed { get; set; }
        public string Thumbnail { get; set; }
    }
}