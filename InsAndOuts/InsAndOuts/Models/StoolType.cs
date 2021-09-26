using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace InsAndOuts.Models
{
    [Table("StoolTypes")]
    public class StoolType : BaseModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}
