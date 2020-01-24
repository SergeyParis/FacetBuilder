using System;
using System.Linq.Expressions;

namespace FacetBuilder.Models
{
    public class ExpressionSaver<TTypeSave, TProperty>
        where TTypeSave : class
    {
        private Expression<Func<TTypeSave, TProperty>> _expression;

        public LambdaExpression Expression => _expression;

        public ExpressionSaver(Expression<Func<TTypeSave, TProperty>> expression)
        {
            _expression = expression;
        }

        public TProperty GetValue(TTypeSave obj)
        {
            return _expression.Compile().Invoke(obj);
        }
    }
}