using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeProductive.Models
{
    public class Priority
    {
        public Priority()
        {
            LastModified18118003 = DateTime.Now;
        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [NotNull, Unique]
        public string Level { get; set; }
        public DateTime LastModified18118003 { get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Task> Tasks { get; set; }
    }
}
