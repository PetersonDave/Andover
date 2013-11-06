using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using Andover.Controller;
using Andover.Domain.Core;
using Andover.Domain.Core.Results;
using Andover.Data.Configuration;

namespace Andover.UI
{
	public partial class Analysis : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			var analyzer = GetAnalyzer();

		    BindCategories(analyzer);
		    BindSummary(analyzer);
		}

	    private static Analyzer GetAnalyzer()
	    {
	        var analyzerSettings = new AnalyzerSettings()
	            {
                    ConnectionString = WebConfigurationManager.ConnectionStrings[ConfigurationHelper.GetValueForKey("Database")].ConnectionString,
	                DatabaseName = ConfigurationHelper.GetValueForKey("Database"),
                    LogsPath = ConfigurationHelper.GetValueForKey("LogsPath"),
                    MaxChildItemsPerParent = ConfigurationHelper.GetValueForKey("MaxChildItemsPerParent"),
                    MaxVersionsPerItem = ConfigurationHelper.GetValueForKey("MaxVersionsPerItem"),
                    ContentStartPath = ConfigurationHelper.GetValueForKey("ContentStartPath")
	            };

	        var settings = StartUp.GetSystemSettings(analyzerSettings);
	        var factory = new Factory(settings);

	        var analyzer = new Analyzer(factory);
	        analyzer.Analyze();
	        return analyzer;
	    }

	    private void BindSummary(Analyzer analyzer)
	    {
	        int validCategories = analyzer.Categories.Count(category => category.IsCompliant);
	        int totalCategories = analyzer.Categories.Count;
	        lblSummary.Text = string.Format("Compliance Score: {0} out of {1} Categories Compliant", validCategories, totalCategories);

	        var report = (from a in analyzer.Categories
                          from b in a.Components
	                      select new
	                          {
	                              Category = a.Name,
                                  Component = b.Name,
	                              Compliant = b.IsCompliant,
	                              CompliantComponents = a.Components.Count(component => component.IsCompliant),
	                              TotalComponents = a.Components.Count
	                          });

	        gvSummary.DataSource = report;
	        gvSummary.DataBind();
	    }

	    private void BindCategories(Analyzer analyzer)
	    {
	        rCategories.DataSource = analyzer.Categories;
	        rCategories.DataBind();
	    }

	    protected void rCategories_OnItemDataBound(object sender, RepeaterItemEventArgs e)
	    {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var category = (e.Item.DataItem as ICategory);
                if (category == null) return;

                var litCategory = e.Item.FindControl("litCategory") as Literal;
                litCategory.Text = category.Name;

                var litDescription = e.Item.FindControl("litDescription") as Literal;
                litDescription.Text = category.Description;

                var gComponents = e.Item.FindControl("rComponents") as Repeater;
                gComponents.DataSource = category.Components;
                gComponents.DataBind();
            }
	    }

        protected void rComponents_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var component = (e.Item.DataItem as IComponent);
                if (component == null) return;

                var litComponent = e.Item.FindControl("litComponent") as Literal;
                litComponent.Text = component.Name;

                var litDescription = e.Item.FindControl("litDescription") as Literal;
                litDescription.Text = component.Description;

                var rComponentResults = e.Item.FindControl("rComponentResults") as Repeater;
                rComponentResults.DataSource = component.ComplianceReport();
                rComponentResults.DataBind();
            }
        }

	    protected void rComponentsResults_OnItemDataBound(object sender, RepeaterItemEventArgs e)
	    {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var resultsNormalized = (e.Item.DataItem as ResultsNormalized);
                if (resultsNormalized == null) return;

                var gvResults = e.Item.FindControl("gvResults") as GridView;
                gvResults.DataSource = resultsNormalized.Results;
                gvResults.DataBind();
            }	        
	    }

	    protected void rResults_OnItemDataBound(object sender, RepeaterItemEventArgs e)
	    {
	        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
	        {
	            var result = (e.Item.DataItem as Result);
	            if (result == null) return;

	            var litName = e.Item.FindControl("litName") as Literal;
	            litName.Text = result.Name;

	            var litDescription = e.Item.FindControl("litDescription") as Literal;
	            litDescription.Text = result.Description;

	            var litValue = e.Item.FindControl("litValue") as Literal;
	            litValue.Text = result.Value;
	        }
	    }
	}
}