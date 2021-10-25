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

        public DateTime WhenToDateTime()
        {
            return DateTime.Parse(When);
        }

        public TimeSpan WhenToTimeSpan()
        {
            if (When.IsNullEmptyOrWhitespace())
            {
                return new TimeSpan();
            }

            var time = When.Split(' ')[1];
            return TimeSpan.Parse(time);
        }
    }
}
