using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Threading;

namespace Eventmaker.Model
{
    class Event
    {
        private static int numberOfEvents = 0;

        // Properties
        public DateTime DateTime { get; set; }

        public string DateTimeFormat { get; set; }

        public int Id { get; set; }

        public string Description { get; set; }
        public string Name { get; set; }
        public string Place { get; set; }


        // If IsExpired property is true, set Color property to this color, if false, set to another color. -> Color is binded to in the view
        public string Color { get; set; }

        private bool _isExpired;
        public bool IsExpired
        {
            get { return _isExpired; }
            set
            {
                _isExpired = value;
                if (_isExpired)
                {
                    Color = "#e74c3c";
                }
                else if (_isExpired == false)
                {
                    Color = "#27ae60";
                }
                
            }

        }

        // Cultureinfo ...........
        //CultureInfo culture = CultureInfo.CurrentUICulture;
        //CultureInfo culture = new CultureInfo("da-DK");
        //CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
        // Constructor
        public Event(DateTime dateTime, string description, string name, string place)
        {
 
            DateTime = dateTime;
            Description = description;
            Id = 1 + numberOfEvents;
            Name = name;
            Place = place;
            numberOfEvents++;

            //DateTimeFormat = DateTime.ToString("dd MMMM | yyyy");
            DateTimeFormat = DateTime.ToString("dd MMMM | yyyy", new CultureInfo("da-DK"));
            // TODO Culture info ..
            //DateTimeFormat = DateTime.ToString("D");
            //DateTime = DateTime.Parse(DateTimeFormat, CultureInfo.CurrentCulture);
            //DateTime.ParseExact() <- Takes it from string to datetime

            if (DateTime < DateTime.Now)
                IsExpired = true;
            else
                IsExpired = false;

        }

        public override string ToString()
        {
            return $"{nameof(DateTime)}: {DateTime}, {nameof(Id)}: {Id}, {nameof(Description)}: {Description}, {nameof(Name)}: {Name}, {nameof(Place)}: {Place}";
        }



        


    }
}
