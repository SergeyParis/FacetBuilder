﻿using System.Linq.Expressions;

 namespace FacetBuilder.Models
{
    public abstract class ExpressionBase
    {
        public abstract LambdaExpression Expression { get; }
        public abstract dynamic GetValue(object obj);
        public abstract T GetValueV2<T>(object obj);
    }
}