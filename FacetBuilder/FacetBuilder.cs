using System;
using System.Collections.Generic;
using System.Linq;
using FacetBuilder.Enums;
using FacetBuilder.Models;
using Newtonsoft.Json;

namespace FacetBuilder
{
    /// <typeparam name="TFacet">Fields of this class must be generic collections</typeparam>
    class FacetBuilder<TFacet, TIn, TFilter>
        where TFacet : class, new()
        where TIn : class
        where TFilter : class
    {
        private readonly List<Rule<TFacet, TIn, TFilter>> _rules;

        public FacetBuilder()
        {
            _rules = new List<Rule<TFacet, TIn, TFilter>>();
        }

        public Rule<TFacet, TIn, TFilter> AddRule(FilterType type)
        {
            var rule = new Rule<TFacet, TIn, TFilter>(type);
            _rules.Add(rule);
            return rule;
        }

        public TFacet Compile(IEnumerable<TIn> data, TFilter filter)
        {
            var listData = data.ToList();

            var facet = new TFacet();


            foreach (var rule in _rules)
            {
                var facetPropertyExpression = rule.FacetExpression;

                var filteredData = CompileOneFacetProperty(rule, listData, filter);
                var mappedData = filteredData.Select(rule.AsExpression.GetValue).ToList();

                var propertyName = facetPropertyExpression.Expression.ToString().Split('.').Skip(1).First();
                var targetProperty = facet.GetType().GetProperty(propertyName);
                var valueOfTargetProperty = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(mappedData), targetProperty.PropertyType);

                try
                {
                    targetProperty.SetValue(facet, valueOfTargetProperty, null);
                }
                catch (NullReferenceException e)
                {
                    // Log error: function 'To' must be use only for choose property
                }
            }

            return facet;
        }

        private List<TIn> CompileOneFacetProperty(Rule<TFacet, TIn, TFilter> currentRule, List<TIn> data,
            TFilter filter)
        {
            var resultCollection = new List<TIn>();

            foreach (var oneData in data)
            {
                bool filteringResult = true;
                foreach (var oneRule in _rules)
                {
                    var filterByValue = oneRule.FilterByExpression.GetValue(filter);
                    var filterWhatValue = oneRule.FilterWhatExpression.GetValue(oneData);

                    if (filterByValue == null)
                        continue;
                    if (oneRule == currentRule)
                        continue;

                    switch (oneRule.Type)
                    {
                        case FilterType.Contains:
                            if (!Enumerable.Contains(filterByValue, filterWhatValue))
                            {
                                filteringResult = false;
                            }

                            break;

                        default: throw new NotImplementedException();
                    }
                }

                if (filteringResult)
                    resultCollection.Add(oneData);
            }

            return resultCollection;
        }
    }
}