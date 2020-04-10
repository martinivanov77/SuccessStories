using SitefinityWebApp.MVC.Models;
using System.Web.Mvc;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Personalization;
using SitefinityWebApp.DynamicModuleManagerMethods;

namespace SitefinityWebApp.MVC.Controllers
{
    [ControllerToolboxItem(Name = "SuccessStoryWidget_MVC", Title = "SuccessStoryWidget", SectionName = "CustomWidgets")]
    public class SuccessStoryWidgetController : Controller, IPersonalizable
    {
        public ActionResult Index()
        {
            var model = new SuccessStoryWidgetViewModel();
            return View("Index", model);
        }

        [HttpPost]
        public ActionResult Submit(SuccessStoryWidgetViewModel successStoryWidgetViewModel)
        {
            if (ModelState.IsValid)
            {
                CreateSuccessStory.CreateStory(successStoryWidgetViewModel: successStoryWidgetViewModel);
            }

            return View("Index", successStoryWidgetViewModel);
        }
        protected override void HandleUnknownAction(string actionName)
        {
            this.ActionInvoker.InvokeAction(this.ControllerContext, "Index");
        }
    }
}
