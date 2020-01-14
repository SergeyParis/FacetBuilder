using FacetBuilder.Enums;

namespace FacetBuilder.Models
{
    public class Rule<TFacet, TIn, TOut>
    {
        public FilterType Type { get; }
        public ExpressionBase FacetExpression { get; set; }
        public ExpressionBase FilterWhatExpression { get; set; }
        public ExpressionBase FilterByExpression { get; set; }
        public ExpressionBase AsExpression { get; set; }

        public Rule(FilterType type)
        {
            Type = type;
        }
    }
}