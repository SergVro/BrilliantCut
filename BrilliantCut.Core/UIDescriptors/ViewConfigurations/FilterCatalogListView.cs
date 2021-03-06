﻿using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.ServiceLocation;
using EPiServer.Shell;

namespace BrilliantCut.Core.UIDescriptors.ViewConfigurations
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
            ControllerType = "brilliantcut/widget/FilterCataloglist";
            IconClass = "epi-iconList";
            SortOrder = 10;
            Category = "catalog";
        }
    }
}
