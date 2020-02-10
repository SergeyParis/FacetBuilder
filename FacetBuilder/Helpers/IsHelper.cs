using System.Collections;
using System.Dynamic;

namespace FacetBuilder.Helpers
{
    public static class IsHelper
    {
        public static bool IsEnumerable(this object obj) => obj.GetType().GetInterface(nameof(IEnumerable)) != null;
        
        public static bool IsString(this object obj) => obj is string;
        
        public static bool IsExpandoObject(this object obj) => obj is ExpandoObject;
    }
}