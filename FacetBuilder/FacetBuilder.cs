using System.Collections.Generic;
using System.Linq;
using FacetBuilder.Helpers;
using FacetBuilder.Models;

namespace FacetBuilder
{
    /// <typeparam name="TFacet">Fields of this class must be generic collections</typeparam>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TFilter"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    public class FacetBuilder<TFacet, TIn, TFilter, TProperty>
        where TFacet : class, new()
        where TIn : class
        where TFilter : class
        where TProperty : class
    {
        private readonly List<Rule<TFacet, TIn, TFilter, TProperty>> _rules;

        public FacetBuilder()
        {
            _rules = new List<Rule<TFacet, TIn, TFilter, TProperty>>();
        }

        public Rule<TFacet, TIn, TFilter, TProperty> AddRule()
        {
            var rule = new Rule<TFacet, TIn, TFilter, TProperty>();
            _rules.Add(rule);
            return rule;
        }

        public TFacet Compile(IEnumerable<TIn> data, TFilter filter)
        {
            var listData = data.ToList();

            var facet = new TFacet();

            var i = 0;
            foreach (var rule in _rules)
            {
                var facetProperty = rule.FacetExpression;

                var filteredData = CompileOneFacetProperty(rule, listData, filter);
                var mappedData = filteredData.Select(rule.AsFunc.Invoke).Where(rule.AsFilterFunc.Invoke).Distinct().ToList();

                var propertyName = facetProperty.Expression.ToString().Split('.').Skip(1).First();
                var targetProperty = facet.GetType().GetProperty(propertyName);

                targetProperty.SetValue(facet, mappedData, null);
            }
            
            return facet;
        }
        
        private List<TIn> CompileOneFacetProperty(Rule<TFacet, TIn, TFilter, TProperty> currentRule, List<TIn> data,
            TFilter filter)
        {
            var resultCollection = new List<TIn>();

            foreach (var oneData in data)
            {
                var filteringResult = true;
                foreach (var oneRule in _rules)
                {
                    var filterByValue = oneRule.FilterByFunc.Invoke(filter);
                    var filterWhatValue = oneRule.FilterWhatFunc.Invoke(oneData);
                    
                    if (filterWhatValue == null)
                    {
                        continue;
                    }
                    if (filterByValue == null)
                        continue;
                    if (oneRule == currentRule)
                        continue;

                    if (filterByValue.IsEnumerable() && !filterByValue.IsString() && !filterByValue.IsExpandoObject())
                    {
                        if (!((IEnumerable<object>) filterByValue).Contains(filterWhatValue))
                        {
                            filteringResult = false;
                        }
                    }
                    else
                    {
                        if (!filterByValue.ToString().Equals(filterWhatValue.ToString()))
                        {
                            filteringResult = false;
                        }
                    }
                }

                if (filteringResult)
                    resultCollection.Add(oneData);
            }

            return resultCollection;
        }
    }
}