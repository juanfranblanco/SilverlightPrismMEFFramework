using System;
using System.ComponentModel.Composition;
using System.Text;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Regions;

namespace Infrastructure.Navigator
{ 
    public class NavigationService
    {
        private readonly IRegionManager regionManager;
        private string id;
      
        public NavigationService(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
            this.id = Guid.NewGuid().ToString();
        }

        public void OpenView(string targetName, UriQuery uriQuery, string regionName)
        {
            regionManager.Regions[regionName].RequestNavigate(GetFullUri(targetName, uriQuery));
        }

        public void OpenView(string targetName, UriQuery uriQuery, string regionName, Action<NavigationResult> navigationResult)
        {
            regionManager.Regions[regionName].RequestNavigate(GetFullUri(targetName, uriQuery), navigationResult);
        }

        public string GetFullUri(string targetName, UriQuery uriQuery)
        {
            var builder = new StringBuilder();
            
            builder.Append(targetName);
            builder.Append(uriQuery);

            return builder.ToString();
        }

        public int? GetNavigationParameterIntValue(NavigationContext navigationContext, string parameterName)
        {
            var param = navigationContext.Parameters[parameterName];

            int value;
            if (param != null && Int32.TryParse(param, out value))
            {
                return value;
            }

            return null;
        }

        public TEnumType GetNavigationParameterEnumValue<TEnumType>(NavigationContext navigationContext, string parameterName) where TEnumType : struct
        {
            var param = navigationContext.Parameters[parameterName];

            TEnumType value;
            if (param != null && Enum.TryParse(param, out value))
            {
                return value;
            }
            throw new ArgumentException();
        }
    }
}
