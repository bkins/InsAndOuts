using System;
using System.Collections.Generic;
using System.Text;
using InsAndOuts.Utilities;
using SQLite;

namespace InsAndOuts.Models
{
    [Table("Meals")]
    public class Meal : BaseModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public override string ToString()
        {
            return $"{When} - {Name}";
        }
        
    }
}
