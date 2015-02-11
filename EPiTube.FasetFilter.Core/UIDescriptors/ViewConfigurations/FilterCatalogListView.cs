﻿using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.ServiceLocation;
using EPiServer.Shell;

namespace EPiTube.FasetFilter.Core.UIDescriptors.ViewConfigurations
{
    [ServiceConfiguration(typeof(ViewConfiguration))]
    public class FilterCatalogListView : ViewConfiguration<RootContent>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilterCatalogListView"/> class.
        /// </summary>
        public FilterCatalogListView()
        {
            Key = "cataloglist";
            LanguagePath = "/commerce/contentediting/views/cataloglist";
            ControllerType = "epitubefasetfilter/widget/filtercataloglist";
            IconClass = "epi-iconList";
            SortOrder = 10;
            Category = "catalog";
        }
    }
}
