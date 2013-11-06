using System.Collections.Generic;
using System.Xml;
using Andover.Domain.Configuration;
using Sitecore.Configuration;

namespace Andover.Data.Configuration
{
    public class ConfigurationHelper
    {
        public static string GetValueForKey(string key)
        {
            var settings = Factory.GetConfigNode("andover/settings");
            var node = settings.SelectSingleNode(string.Format("add[@key='{0}']", key));
            bool isInValid = node == null || node.Attributes == null || node.Attributes["value"] == null;
            return isInValid ? string.Empty : node.Attributes["value"].Value;
        }

        public static Dictionary<ICategoryDto, List<IComponentDto>> GetConfigurationSettings()
        {
            var settings = new Dictionary<ICategoryDto, List<IComponentDto>>();

            var categories = Factory.GetConfigNode("andover/categories");
            var categoryList = categories.SelectNodes("category");
            foreach (XmlNode category in categoryList)
            {
                var categorySetting = new CategoryDto()
                    {
                        Description = category.Attributes["description"].InnerText,
                        Name = category.Attributes["name"].InnerText
                    };

                var componentSettings = new List<IComponentDto>();

                var components = category.SelectNodes("components/component");
                foreach (XmlNode component in components)
                {
                    var componentSetting = new ComponentDto()
                        {
                            Description = component.Attributes["description"].InnerText,
                            Name = component.Attributes["name"].InnerText,
                            Type = component.Attributes["type"].InnerText
                        };

                    componentSettings.Add(componentSetting);
                }

                settings.Add(categorySetting, componentSettings);
            }

            return settings;
        }
    }
}