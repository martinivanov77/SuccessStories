using SitefinityWebApp.MVC.Models;
using System;
using System.Linq;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Versioning;
using System.Web.Mvc;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Personalization;
using Telerik.Web.UI;

namespace SitefinityWebApp.DynamicModuleManagerMethods
{
    public class CreateSuccessStory
    {
        public static void CreateStory(SuccessStoryWidgetViewModel successStoryWidgetViewModel)
        {
            var transactionName = "Submit";

            var providerName = "OpenAccessDataProvider";
            var versionManager = VersionManager.GetManager(providerName, transactionName);

            var dynamicModuleProviderName = "OpenAccessProvider";
            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(dynamicModuleProviderName, transactionName);

            using (new ElevatedModeRegion(dynamicModuleManager))  //ElevatedModeRegion is set to override access restrictions for given users
            {
                Type successStoryType = TypeResolutionService.ResolveType("Telerik.Sitefinity.DynamicTypes.Model.SuccessStories.SuccessStory");
                DynamicContent successStoryItem = dynamicModuleManager.CreateDataItem(successStoryType);

                successStoryItem.SetValue("Title", successStoryWidgetViewModel.Title);
                successStoryItem.SetValue("Description", successStoryWidgetViewModel.Description);
                successStoryItem.SetValue("SummaryDescription", successStoryWidgetViewModel.SummaryDescription);
                successStoryItem.SetValue("ProductsUsed", successStoryWidgetViewModel.ProductsUsed);
                successStoryItem.SetValue("Company", successStoryWidgetViewModel.Company);
                successStoryItem.SetValue("CompanyWebsite", successStoryWidgetViewModel.CompanyWebsite);
                successStoryItem.SetValue("Industry", successStoryWidgetViewModel.Industry);

                // Get related item manager
                LibrariesManager thumbnailManager = LibrariesManager.GetManager();

                var thumbnailItem = thumbnailManager.GetImages().FirstOrDefault(i => i.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master);

                if (thumbnailItem != null)
                {
                    // This is how we relate an item
                    successStoryItem.CreateRelation(thumbnailItem, "Thumbnail");
                }

                successStoryItem.SetString("UrlName", "SomeUrlName");
                successStoryItem.SetValue("Owner", SecurityManager.GetCurrentUserId());
                successStoryItem.SetValue("PublicationDate", DateTime.UtcNow);

                successStoryItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Draft");

                versionManager.CreateVersion(successStoryItem, true);
                TransactionManager.CommitTransaction(transactionName);
            }
        }
    }
}