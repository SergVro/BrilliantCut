﻿using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.ServiceLocation;
using EPiServer.Shell;

namespace EPiTube.FasetFilter.Core.UIDescriptors.ViewConfigurations
{
    [ServiceConfiguration(typeof(ViewConfiguration))]
    public class FilterCatalogContentListView : ViewConfiguration<NodeContentBase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilterCatalogContentListView"/> class.
        /// </summary>
        public FilterCatalogContentListView()
        {
            Key = "catalogcontentlist";
            LanguagePath = "/commerce/contentediting/views/cataloglist";
            ControllerType = "epitubefasetfilter/widget/filtercatalogcontentlist";
            IconClass = "epi-iconList";
            SortOrder = 10;
            Category = "catalog";
        }
    }
}
