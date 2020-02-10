using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FacetBuilder.Models;

namespace FacetBuilder.Extensions
{
    public static class FacetBuilderExtensions
    {
        public static Rule<TFacet, TIn, TFilter> FilterBy<TFacet, TIn, TFilter>(this Rule<TFacet, TIn, TFilter> rule, Func<TFilter, object> func)
            where TFacet : class
            where TIn : class
            where TFilter : class
        {
            rule.FilterByFunc = func;
            return rule;
        }
        
        public static Rule<TFacet, TIn, TFilter> FilterWhat<TFacet, TIn, TFilter>(this Rule<TFacet, TIn, TFilter> rule, Func<TIn, object> func)
            where TFacet : class
            where TIn : class
            where TFilter : class
        {
            rule.FilterWhatFunc = func;
            return rule;
        }

        public static ExtendedRule<TFacet, TIn, TFilter, TProperty> To<TFacet, TIn, TFilter, TProperty>(this Rule<TFacet, TIn, TFilter> rule, Expression<Func<TFacet, IEnumerable<TProperty>>> expression)
            where TFacet : class
            where TIn : class
            where TFilter : class
            where TProperty : class
        {
            var extendedRule = new ExtendedRule<TFacet, TIn, TFilter, TProperty>(rule)
            {
                FacetExpression = new ExpressionSaver<TFacet, IEnumerable<TProperty>>(expression)
            };
            rule.Rules.Remove(rule);
            rule.Rules.Add(extendedRule);
            return extendedRule;
        }

        public static ExtendedRule<TFacet, TIn, TFilter, TProperty> As<TFacet, TIn, TFilter, TProperty>(this ExtendedRule<TFacet, TIn, TFilter, TProperty> rule, Func<TIn, TProperty> func)
            where TFacet : class
            where TIn : class
            where TFilter : class
            where TProperty : class
        {
            rule.AsFunc = func;
            return rule;
        }
        
        public static ExtendedRule<TFacet, TIn, TFilter, TProperty> AsFilter<TFacet, TIn, TFilter, TProperty>(this ExtendedRule<TFacet, TIn, TFilter, TProperty> rule, Func<TProperty, bool> func)
            where TFacet : class
            where TIn : class
            where TFilter : class
            where TProperty : class
        {
            rule.AsFilterFunc = func;
            return rule;
        }
    }
}