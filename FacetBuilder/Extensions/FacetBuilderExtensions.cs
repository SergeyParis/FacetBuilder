using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FacetBuilder.Models;

namespace FacetBuilder.Extensions
{
    public static class FacetBuilderExtensions
    {
        public static Rule<TFacet, TIn, TFilter, TProperty> FilterBy<TFacet, TIn, TFilter, TProperty>(this Rule<TFacet, TIn, TFilter, TProperty> rule, Func<TFilter, object> func)
            where TFacet : class
            where TIn : class
            where TFilter : class
        {
            rule.FilterByFunc = func;
            return rule;
        }
        
        public static Rule<TFacet, TIn, TFilter, TProperty> FilterWhat<TFacet, TIn, TFilter, TProperty>(this Rule<TFacet, TIn, TFilter, TProperty> rule, Func<TIn, object> func)
            where TFacet : class
            where TIn : class
            where TFilter : class
        {
            rule.FilterWhatFunc = func;
            return rule;
        }

        public static Rule<TFacet, TIn, TFilter, TProperty> To<TFacet, TIn, TFilter, TProperty>(
            this Rule<TFacet, TIn, TFilter,TProperty> rule, Expression<Func<TFacet, IEnumerable<TProperty>>> expression)
            where TFacet : class
            where TIn : class
            where TFilter : class
        {
            rule.FacetExpression = new ExpressionSaver<TFacet, IEnumerable<TProperty>>(expression);
            return rule;
        }

        public static Rule<TFacet, TIn, TFilter, TProperty> As<TFacet, TIn, TFilter, TProperty>(this Rule<TFacet, TIn, TFilter, TProperty> rule, Func<TIn, TProperty> func)
            where TFacet : class
            where TIn : class
            where TFilter : class
        {
            rule.AsFunc = func;
            return rule;
        }
        
        public static Rule<TFacet, TIn, TFilter, TProperty> AsFilter<TFacet, TIn, TFilter, TProperty>(this Rule<TFacet, TIn, TFilter, TProperty> rule, Func<TProperty, bool> func)
            where TFacet : class
            where TIn : class
            where TFilter : class
        {
            rule.AsFilterFunc = func;
            return rule;
        }
    }
}