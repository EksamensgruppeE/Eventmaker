using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eventmaker.Model;
using Eventmaker.ViewModel;

namespace Eventmaker.Handler
{
    class EventHandler
    {
        public EventViewModel EventViewModel { get; set; }

        public EventHandler(EventViewModel eventViewModel)
        {
            EventViewModel = eventViewModel;
        }

        public void CreateEvent()
        {
            // denne sindssyge oprettelse af et event, benytter sig af alle de properties fra ViewModellen 
            // + den konverterede DateTime fra DateTimeConverteren
            Event newEvent = new Event(Converter.DateTimeConverter.DateTimeOffsetAndTimeSetToDateTime(EventViewModel.Date, EventViewModel.Time),
            EventViewModel.Id, EventViewModel.Description, EventViewModel.Name, EventViewModel.Place);

            Model.EventCatalogSingleton.Instance.AddEvent(newEvent);

        }

        public void DeleteEvent()
        {
            EventCatalogSingleton.Instance.RemoveEvent();
        }

        public void SetSelectedEvent(Event selectedEvent)
        {
            EventViewModel.SelectedEvent = selectedEvent;
        }
       
    }
}
