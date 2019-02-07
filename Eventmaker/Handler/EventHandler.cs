using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eventmaker.Model;
using Eventmaker.ViewModel;
using System.Threading;



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
            //denne metode kalder den metode der fjerner det event vi har selected i listview.
            EventCatalogSingleton.Instance.RemoveEvent();
        }

        //denne metode tager det event den har fået fra EventViewModel.SelectedEventCommand
        //og sætter EventViewModels property SelectedEvent til at være lig med det event vi har modtaget. 
        public void SetSelectedEvent(Event selectedEvent)
        {
            EventViewModel.SelectedEvent = selectedEvent;
        }

        public  void ExpireCheck()
        {
            
                for (int i = 0; i < EventViewModel.EventCatalogSingleton.Events.Count; i++)
                {
                    if (EventViewModel.EventCatalogSingleton.Events[i].DateTime < DateTime.Now)
                    {
                        EventViewModel.EventCatalogSingleton.Events[i].IsExpired = true;
                    }


                }

                

            
        }

    }
}
