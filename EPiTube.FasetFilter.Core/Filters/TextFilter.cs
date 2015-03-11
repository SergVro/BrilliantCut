﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Core;
using EPiServer.Find;
using EPiServer.Find.Api.Querying;
using EPiServer.Find.Api.Querying.Queries;
using EPiServer.Find.Framework;
using EPiTube.FasetFilter.Core.DataAnnotation;

namespace EPiTube.FasetFilter.Core.Filters
{
    [TextboxFilter]
    public class TextFilter : IFilterContent
    {
        private const string ForMethodName = "For";
        public string Name
        {
            get { return "TextSearch"; }
        }

        public string Description
        {
            get { return "Free text search"; }
        }

        public IEnumerable<IFilterOptionModel> GetFilterOptions(SearchResults<EPiTubeModel> searchResults)
        {
            yield return new FilterOptionModel("FreeTextFilter", string.Empty, string.Empty, string.Empty);
        }

        public ISearch Filter(IContent content, ISearch query, IEnumerable<object> values)
        {
            var valueArray = values as string[] ?? values.ToArray();
            if (!valueArray.Any())
            {
                return query;
            }

            var value = valueArray.OfType<string>().First();
            if (String.IsNullOrEmpty(value))
            {
                return query;
            }

            var typeSearchInterface = query.GetType().GetInterface(typeof(ITypeSearch<>).Name);
            if (typeSearchInterface == null)
            {
                return query;
            }

            var genericArgument = typeSearchInterface.GetGenericArguments().First();
            var methodInfoFor = typeof(TypeSearchExtensions).GetMethods().First(x => x.Name == ForMethodName);
            methodInfoFor = methodInfoFor.MakeGenericMethod(genericArgument);

            var search = methodInfoFor.Invoke(null, new object[] { query, value }) as ITypeSearch<CatalogContentBase>; //Search<object, QueryStringQuery>;

            Expression<Func<CatalogContentBase, Filter>> filterExpression = (x) => x.Code().AnyWordBeginsWith(value);
            var delegateType = typeof (Func<,>).MakeGenericType(genericArgument, typeof (Filter));
            var something = Expression.Lambda(delegateType, filterExpression.Body, filterExpression.Parameters[0]);

            var methodInfoInclude = typeof(TypeSearchExtensions).GetMethods().First(x => x.Name == "Include");
            methodInfoInclude = methodInfoInclude.MakeGenericMethod(genericArgument);
            search = methodInfoInclude.Invoke(null, new object[] { search, something, null }) as ITypeSearch<CatalogContentBase>; //Search<object, QueryStringQuery>;

            return search;
        }

        public ISearch AddFasetToQuery(ISearch query)
        {
            return query;
        }

        public int SortOrder { get; set; }
    }
}