using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventmaker.Model
{
    class EventCatalogSingleton
    {
        
        private static EventCatalogSingleton _instance = null;

        public static EventCatalogSingleton Instance
        {
            get { return _instance ?? (_instance = new EventCatalogSingleton()); }

        }

        public ObservableCollection<Event> Events { get; set; }

        private EventCatalogSingleton()
        {
            Events = new ObservableCollection<Event>();

            // For testing
            TestEvents();
        }

        private void TestEvents()
        {
            Events.Add(new Event(new DateTime(2000, 12, 12), 1, "Description1", "Name1", "Place1"));
            Events.Add(new Event(new DateTime(2012, 12, 10), 1, "Description3", "Name3", "Plac3"));
            Events.Add(new Event(new DateTime(2024, 12, 11), 1, "Description2", "Name2", "Place2"));
        }

        public void AddEvent(Event newEvent)
        {
            Events.Add(newEvent);
        }

    }
}
