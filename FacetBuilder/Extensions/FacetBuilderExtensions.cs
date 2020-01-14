using System;
using System.Linq.Expressions;
using FacetBuilder.Models;

namespace FacetBuilder.Extensions
{
    public static class FacetBuilderExtensions
    {
        public static Rule<TFacet, TIn, TFilter> FilterBy<TFacet, TIn, TFilter, TProperty>(
            this Rule<TFacet, TIn, TFilter> rule, Expression<Func<TFilter, TProperty>> expression)
            where TFacet : class
            where TIn : class
            where TFilter : class
        {
            rule.FilterByExpression = new ExpressionSaver<TFilter, TProperty>(expression);
            return rule;
        }

        public static Rule<TFacet, TIn, TFilter> FilterWhat<TFacet, TIn, TFilter, TProperty>(
            this Rule<TFacet, TIn, TFilter> rule, Expression<Func<TIn, TProperty>> expression)
            where TFacet : class
            where TIn : class
            where TFilter : class
        {
            rule.FilterWhatExpression = new ExpressionSaver<TIn, TProperty>(expression);
            return rule;
        }

        public static Rule<TFacet, TIn, TFilter> To<TFacet, TIn, TFilter, TProperty>(
            this Rule<TFacet, TIn, TFilter> rule, Expression<Func<TFacet, TProperty>> expression)
            where TFacet : class
            where TIn : class
            where TFilter : class
        {
            rule.FacetExpression = new ExpressionSaver<TFacet, TProperty>(expression);
            return rule;
        }

        public static Rule<TFacet, TIn, TFilter> As<TFacet, TIn, TFilter>(this Rule<TFacet, TIn, TFilter> rule,
            Expression<Func<TIn, dynamic>> expression)
            where TFacet : class
            where TIn : class
            where TFilter : class
        {
            rule.AsExpression = new ExpressionSaver<TIn, dynamic>(expression);
            return rule;
        }
    }
}