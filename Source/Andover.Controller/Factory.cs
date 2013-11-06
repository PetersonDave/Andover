using System;
using System.Collections.Generic;
using System.Linq;
using Andover.Data.Configuration;
using Andover.Domain.Core;
using Andover.Domain.Exceptions;
using Andover.Domain.Utils;
using Ninject;
using Andover.Domain.Configuration;

namespace Andover.Controller
{
	public class Factory : IFactory
	{
		private ISystemSettings _systemSettings;
        public Dictionary<ICategoryDto, List<IComponentDto>> ConfigurationSettings { get; set; }

		public Factory(ISystemSettings systemSettings)
		{
			_systemSettings = systemSettings;
            ConfigurationSettings = ConfigurationHelper.GetConfigurationSettings();
		}

		public List<ICategory> GetRelatedCategories<TCategory>(IFactory factory)
		{
			var categories = new List<ICategory>();
			var categoryTypes = FactoryHelper.GetClasses(typeof(TCategory));

			foreach (var categoryType in categoryTypes)
			{
				ICategory category = null;

				var constructorInfo = categoryType.GetConstructors();
				var parameters = constructorInfo[0].GetParameters();
				if (parameters.Any())
				{
					category = Activator.CreateInstance(categoryType, factory) as ICategory;
				}

				if (category != null)
				{
					categories.Add(category);
				}
			}

			return categories;
		}

	    public void PreloadProviders(Type T)
		{
			var componentTypes = FactoryHelper.GetClasses(T);

			foreach (var componentType in componentTypes)
			{
				var constructorInfo = componentType.GetConstructors();
				var parameters = constructorInfo[0].GetParameters();
				if (parameters.Any())
				{
					var interfaceType = parameters[0].ParameterType;
					var provider = _systemSettings.Kernel.Get(interfaceType);

					bool isProviderRegistered = provider != null;
					if (!isProviderRegistered)
					{
						throw new ProviderNotMappedException(interfaceType.FullName);
					}
				}
			}
		}

		public IComponent GetComponent(Type componentType)
		{
			IComponent component;

			var constructorInfo = componentType.GetConstructors();
			var parameters = constructorInfo[0].GetParameters();
			if (parameters.Any())
			{
				var interfaceType = parameters[0].ParameterType;

				var provider = GetProvider(_systemSettings, interfaceType);

				bool isProviderRegistered = provider != null;
				if (!isProviderRegistered)
				{
					throw new ProviderNotMappedException(interfaceType.FullName);
				}

				component = Activator.CreateInstance(componentType, new object[] { provider }) as IComponent;
			}
			else
			{
				component = Activator.CreateInstance(componentType) as IComponent;
			}

			return component;
		}

		public static object GetProvider(ISystemSettings systemSettings, Type interfaceType)
		{
			Object provider;
			if (systemSettings.Providers.ContainsKey(interfaceType))
			{
				provider = systemSettings.Providers[interfaceType];
			}
			else
			{
				provider = systemSettings.Kernel.Get(interfaceType);
				systemSettings.Providers.Add(interfaceType, provider);
			}
			return provider;
		}
	}
}
