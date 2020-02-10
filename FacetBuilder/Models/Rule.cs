using System;
using System.Collections.Generic;

namespace FacetBuilder.Models
{
    public class Rule<TFacet, TIn, TFilter>
        where TFacet: class
    {
        internal List<Rule<TFacet, TIn, TFilter>> Rules { get; set; }

        protected Rule()
        {
            
        }
        internal Rule(List<Rule<TFacet, TIn, TFilter>> rules)
        {
            Rules = rules;
        }

        public Func<TIn, object> FilterWhatFunc { get; set; }
        public Func<TFilter, object> FilterByFunc { get; set; }
    }

    public class ExtendedRule<TFacet, TIn, TFilter, TProperty> : Rule<TFacet, TIn, TFilter>, AAA<TFacet, TIn, TFilter>
        where TFacet: class
        where TProperty: class
    {
        public ExtendedRule(Rule<TFacet, TIn, TFilter> rule) : base()
        {
            FilterWhatFunc = rule.FilterWhatFunc;
            FilterByFunc = rule.FilterByFunc;
        }
        
        public ExpressionSaver<TFacet, IEnumerable<TProperty>> FacetExpression { get; set; }
        public Func<TIn, TProperty> AsFunc { get; set; }
        public Func<TProperty, bool> AsFilterFunc { get; set; }

        public Func<TIn, dynamic> GetAsFunc()
        {
            return AsFunc;
        }
        
        public Func<dynamic, bool> GetAsFilterFunc()
        {
            return AsFilterFunc as Func<dynamic, bool>;
        }
    }

    public interface AAA<TFacet, TIn, TFilter>
    {
        Func<TIn, dynamic> GetAsFunc();
        
        Func<dynamic, bool> GetAsFilterFunc();
    }
}