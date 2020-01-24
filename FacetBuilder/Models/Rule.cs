using System;
using System.Collections.Generic;
using FacetBuilder.Enums;

namespace FacetBuilder.Models
{
    public class Rule<TFacet, TIn, TFilter, TProperty>
        where TFacet: class
    {
        public FilterType Type { get; }
        public ExpressionSaver<TFacet, IEnumerable<TProperty>> FacetExpression { get; set; }
        public Func<TIn, object> FilterWhatFunc { get; set; }
        public Func<TFilter, object> FilterByFunc { get; set; }
        public Func<TIn, TProperty> AsFunc { get; set; }
        public Func<TProperty, bool> AsFilterFunc { get; set; }

        public Rule(FilterType type)
        {
            Type = type;
        }
    }
}