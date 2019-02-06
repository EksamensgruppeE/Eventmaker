using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Eventmaker.Common;
using Eventmaker.Handler;
using Eventmaker.Model;

namespace Eventmaker.ViewModel
{
    class EventViewModel
    {
        private ICommand _selectedEventCommand;
        private ICommand _deleteEventCommand;


        public EventCatalogSingleton EventCatalogSingleton { get; set; }

        public static Event SelectedEvent { get; set; }

        public ICommand SelectedEventCommand
        {
            get
            {
                return _selectedEventCommand ?? (_selectedEventCommand =
                           new RelayArgCommand<Event>(ev => EventHandler.SetSelectedEvent(ev))); }
            set { _selectedEventCommand = value; }
        }
        
        public ICommand CreateEventCommand { get; set; }

        public ICommand DeleteEventCommand
        {
            get { return _deleteEventCommand ?? (_deleteEventCommand = new RelayCommand(EventHandler.DeleteEvent)); }

            set { _deleteEventCommand = value; }

        }

        public Handler.EventHandler EventHandler { get; set; }

        public int Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Place { get; set; }

        public DateTimeOffset Date { get; set; }

        public TimeSpan Time { get; set; }


        public EventViewModel()
        {

            DateTime dt = System.DateTime.Now;

            Date = new DateTimeOffset(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute , 0, 0, new TimeSpan());
            Time = new TimeSpan(dt.Hour, dt.Minute, dt.Second);

            EventCatalogSingleton = EventCatalogSingleton.Instance;

            EventHandler = new Handler.EventHandler(this);

            CreateEventCommand = new RelayCommand(EventHandler.CreateEvent);
        }


    }

    
}
