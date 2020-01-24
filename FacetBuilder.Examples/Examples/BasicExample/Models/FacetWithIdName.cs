using System;

namespace FacetBuilder.Examples.Examples.BasicExample.Models
{
    public class FacetWithIdName : IEquatable<FacetWithIdName>
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public bool Equals(FacetWithIdName other)
        {
            if (other == null)
                return false;

            return Id == other.Id &&
                   Name == other.Name;
        }

        public override bool Equals(object obj)
        {
            if (obj is FacetWithIdName objCasted)
                return Id == objCasted.Id &&
                       Name == objCasted.Name;

            return false;
        }

        public override int GetHashCode()
        {
            return (Id == null ? 0 : Id.GetHashCode()) ^ 
                   (Name == null ? 0 : Name.GetHashCode());
        }
    }
}