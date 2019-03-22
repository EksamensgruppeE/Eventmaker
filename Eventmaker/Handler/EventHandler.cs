using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Eventmaker.Model;
using Eventmaker.ViewModel;
using System.Threading;
using Eventmaker.Converter;
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

            if (EventViewModel.Description != null && EventViewModel.Name != null && EventViewModel.Place != null)
            {
                //Event newEvent = new Event(
                //            Converter.DateTimeConverter.DateTimeOffsetAndTimeSetToDateTime(EventViewModel.Date,EventViewModel.Time), 
                //            EventViewModel.Description, 
                //            EventViewModel.Name, 
                //            EventViewModel.Place);

                //EventViewModel.EventCatalogSingleton.AddEvent(newEvent);

                EventViewModel.EventCatalogSingleton.AddEvent(
                        EventViewModel.Name, 
                        EventViewModel.Place,
                        DateTimeConverter.DateTimeOffsetAndTimeSetToDateTime(
                                                EventViewModel.Date,
                                                EventViewModel.Time),
                        EventViewModel.Description);
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

        public void UpdateEvent()
        {
            // I singletonen er der en metode der bruges her
            EventViewModel.EventCatalogSingleton.UpdateEvent(EventViewModel.SelectedEvent);
        }

        public async void DeleteEvent()
        {
            // Create the message dialog and set its content

            // Added check: if null, don't do anything
            if (EventViewModel.SelectedEvent != null)
            {
                var messageDialog = new MessageDialog("Are you sure you want to Delete the Event: " + EventViewModel.SelectedEvent.Name + " ?");

                // Add commands and set their callbacks; both buttons use the same callback function instead of inline event handlers
                messageDialog.Commands.Add(new UICommand("Yes", new UICommandInvokedHandler(this.CommandInvokedHandler)));
                messageDialog.Commands.Add(new UICommand("No", null));

                // Set the command that will be invoked by default
                messageDialog.DefaultCommandIndex = 0;

                // Set the command to be invoked when escape is pressed
                messageDialog.CancelCommandIndex = 1;

                // Show the message dialog
                await messageDialog.ShowAsync();

            }



        }

        private void CommandInvokedHandler(IUICommand command)
        {
            //denne metode kalder den metode der fjerner det event vi har selected i listview.
            EventViewModel.EventCatalogSingleton.RemoveEvent(EventViewModel.SelectedEvent);
        }

        //denne metode tager det event den har fået fra EventViewModel.SelectedEventCommand
        //og sætter EventViewModels property SelectedEvent til at være lig med det event vi har modtaget. 
        public void SetSelectedEvent(Event selectedEvent)
        {
            EventViewModel.SelectedEvent = selectedEvent;
        }


        // This is not needed anymore
        //public void SaveEvents()
        //{
        //    PersistencyService.SaveEventsAsJsonAsync(EventViewModel.EventCatalogSingleton.Events);
        //}


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
