using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventmaker.Model
{
    class Event 
    {
        // Properties
        public DateTime DateTime { get; set; }
        public int Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Place { get; set; }

        // Constructor
        public Event(DateTime dateTime, int id, string description, string name, string place)
        {
            DateTime = dateTime;
            Id = id;
            Description = description;
            Name = name;
            Place = place;
        }

        public override string ToString()
        {
            return $"{nameof(DateTime)}: {DateTime}, {nameof(Id)}: {Id}, {nameof(Description)}: {Description}, {nameof(Name)}: {Name}, {nameof(Place)}: {Place}";
        }



      

    }
}
