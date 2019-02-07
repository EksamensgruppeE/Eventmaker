using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Eventmaker.Model;
using Eventmaker.ViewModel;
using System.Threading;
using NoteMVVM;


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

            if (EventViewModel.Description !=null && EventViewModel.Name != null && EventViewModel.Place != null)
            {
                Event newEvent = new Event(
                    Converter.DateTimeConverter.DateTimeOffsetAndTimeSetToDateTime(EventViewModel.Date,
                        EventViewModel.Time), EventViewModel.Description, EventViewModel.Name, EventViewModel.Place);


                Model.EventCatalogSingleton.Instance.AddEvent(newEvent);
                PersistencyService.SaveEventsAsJsonAsync(EventCatalogSingleton.Instance.Events);
            }

            if (EventViewModel.Name == null)
            {
                MessageDialogHelper.Show("Name of event is empty", "Invalid event name");
            }

            if (EventViewModel.Place == null)
            {
                MessageDialogHelper.Show("Location for event is empty", "Invalid location");
            }

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

        private class MessageDialogHelper
        {
            public static async void Show(string content, string title)
            {
                MessageDialog messageDialog = new MessageDialog(content, title);
                await messageDialog.ShowAsync();
            }
        }

    }
}
