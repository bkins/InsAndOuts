using System;
using Avails.D_Flat;
using InsAndOuts.Utilities;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace InsAndOuts.Models
{
    [Table("Pains")]
    public class Pain : BaseModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get;     set; }
        public int Level  { get; set; }
        
        [ForeignKey(typeof(SymptomType))]
        public int TypeId { get; set; }

        [OneToOne(CascadeOperations = CascadeOperation.All)]
        public SymptomType  Type  { get; set; }

        /// <summary>
        /// Pain is synonymous with symptom.  I did not want to change the name of this model in fear of losing data on update
        /// </summary>
        public Pain()
        {
            Level = 0;
        }

        public override string ToString()
        {
            var typeText = Type == null 
                        || Type.Name.IsNullEmptyOrWhitespace() ?
                                   "" :
                                   $"- {Type?.Name}";

            return $"{When} {typeText} - Level: {Level}";
        }

    }
}
