using System;
using System.Linq.Expressions;

namespace FacetBuilder.Models
{
    public class ExpressionSaver<TTypeSave, TProperty> : ExpressionBase
        where TTypeSave : class
    {
        private Expression<Func<TTypeSave, TProperty>> _expression;

        public override LambdaExpression Expression => _expression;

        public ExpressionSaver(Expression<Func<TTypeSave, TProperty>> expression)
        {
            _expression = expression;
        }

        public override dynamic GetValue(object obj)
        {
            return _expression.Compile().Invoke(obj as TTypeSave);
        }

        public override T GetValueV2<T>(object obj) => (T) GetValue(obj);
    }
}