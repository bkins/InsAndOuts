using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace InsAndOuts.Models
{
    [Table("Stools")]
    public class Stool : BaseModel
    {
        [PrimaryKey, AutoIncrement]
        public int    Id            { get; set; }
        public byte[] Image         { get; set; }
        public string ImageFileName { get; set; }
        public string StoolType     { get; set; }

        public Stool()
        {
            StoolType     = string.Empty;
            Image         = Array.Empty<byte>();
            ImageFileName = string.Empty;
        }

        //[ForeignKey(typeof(StoolType))]
        //public int StoolTypeId { get; set; }

        //[OneToOne]
        //public StoolType StoolType { get; set; }

        public override string ToString()
        {
            if (StoolType.Contains(":"))
            {
                return $"{When} - {StoolType.Split(':')[0]}";    
            }

            return When;
        }
    }
}
