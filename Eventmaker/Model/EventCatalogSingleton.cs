using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eventmaker.ViewModel;
using NoteMVVM;

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

            LoadEventsAsync();
            // For testing
            
        }

        private void TestEvents()
        {
            Events.Add(new Event(new DateTime(2000, 12, 12), "Description1", "Name1", "Place1"));
            Events.Add(new Event(new DateTime(2012, 12, 10), "Description3", "Name3", "Plac3"));
            Events.Add(new Event(new DateTime(2024, 12, 11), "Description2", "Name2", "Place2"));
        }

        public void AddEvent(Event newEvent)
        {
            Events.Add(newEvent);
           PersistencyService.SaveEventsAsJsonAsync(Events);

        }

        //denne metode tager det event vi har gemt i SelectedEvent og fjerner det fra listen. 
        public void RemoveEvent(Event removeEvent)
        {
            Events.Remove(removeEvent);
            PersistencyService.SaveEventsAsJsonAsync(Events);
        }

        public async void LoadEventsAsync()
        {
            var loadedEvents = await PersistencyService.LoadEventsFromJsonAsync();

            if (loadedEvents != null)
            {
                foreach (var events in loadedEvents)
                {
                    Events.Add(events);
                }
            }
            else
            {
                TestEvents();
            }

        }

        
    }
}
