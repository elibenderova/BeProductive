using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeProductive.Models
{
    public class Task
    {
        public Task()
        {
            LastModified18118003 = DateTime.Now;
            NotificationAllowed = false;
        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [NotNull]
        public string Name { get; set; }
        public bool NotificationAllowed { get; set; }
        [NotNull]
        public DateTime DateAndTime { get; set; }
        public DateTime LastModified18118003 { get; set; }
        [ForeignKey(typeof(Priority))]
        public int PriorityId { get; set; }
        [ForeignKey(typeof(Category))]
        public int CategoryId { get; set; }
        [ManyToOne]
        public Priority Priority { get; set; }
        [ManyToOne]
        public Category Category { get; set; }
    }
}
