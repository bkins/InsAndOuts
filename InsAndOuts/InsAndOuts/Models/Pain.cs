using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace InsAndOuts.Models
{
    [Table("Pains")]
    public class Pain : BaseModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id    { get; set; }
        public int Level { get; set; }

        public Pain()
        {
            Level = 0;
        }

        public override string ToString()
        {
            return $"{When} - Level: {Level}";
        }
    }
}
