using System;
using InsAndOuts.Utilities;
using SQLite;

namespace InsAndOuts.Models
{
    [Table("SymptomType")]
    public class SymptomType : BaseModel,  IEquatable<SymptomType>
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public override string ToString()
        {
            return Name;
        }

        
        public bool Equals(SymptomType other)
        {
            return other != null 
                && Name == other.Name;
        }

        public override int GetHashCode()
        {
            var hashFirstName = Name == null ? 0 : Name.GetHashCode();

            return hashFirstName;
        }
    }
}
