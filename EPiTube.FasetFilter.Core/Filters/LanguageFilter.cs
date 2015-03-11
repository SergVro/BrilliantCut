﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Core;
using EPiServer.Find;
using EPiServer.Find.Framework;
using EPiTube.FasetFilter.Core.DataAnnotation;

namespace EPiTube.FasetFilter.Core.Filters
{
    [RadiobuttonFilter]
    public class LanguageFilter : FilterContentBase<CatalogContentBase, string>
    {
        public override string Name
        {
            get { return "Language"; }
        }

        public override ITypeSearch<CatalogContentBase> Filter(IContent currentCntent, ITypeSearch<CatalogContentBase> query, IEnumerable<string> values)
        {
            var localizableContent = currentCntent as ILocale;

            var valuesArray = values.ToArray();
            if ((!valuesArray.Any() || (valuesArray.Length == 1 && valuesArray[0] == "current")) && localizableContent != null)
            {
                return query.Filter(x => x.LanguageName().Match(localizableContent.Language.Name));
            }

            var marketFilter = SearchClient.Instance.BuildFilter<EntryContentBase>();
            marketFilter = valuesArray.Aggregate(marketFilter, (current, value) => current.Or(x => x.LanguageName().Match(value)));

            return query.Filter(marketFilter);
        }

        public override IEnumerable<IFilterOptionModel> GetFilterOptions(SearchResults<EPiTubeModel> searchResults)
        {
            var authorCounts = searchResults
                .TermsFacetFor<EntryContentBase>(x => x.LanguageName()).Terms;

            var filterOptionModels = new List<FilterOptionModel>();
            filterOptionModels.Add(new FilterOptionModel("languageCurrent", "Current", "current", true));
            filterOptionModels.AddRange(authorCounts.Select(authorCount => new FilterOptionModel("language" + authorCount.Term, String.Format(CultureInfo.InvariantCulture, "{0} ({1})", authorCount.Term, authorCount.Count), authorCount.Term, false)));

            return filterOptionModels;
        }

        public override ITypeSearch<CatalogContentBase> AddFasetToQuery(ITypeSearch<CatalogContentBase> query)
        {
            return query.TermsFacetFor(x => x.LanguageName());
        }
    }
}