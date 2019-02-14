using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Media;
using Eventmaker.Annotations;
using Eventmaker.Common;
using Eventmaker.Handler;
using Eventmaker.Model;

namespace Eventmaker.ViewModel
{
    class EventViewModel : INotifyPropertyChanged
    {
        private ICommand _selectedEventCommand;
        private ICommand _deleteEventCommand;


        public EventCatalogSingleton EventCatalogSingleton { get; set; }

        //her gemmes det Event vi har valgt i ListViewet - Rember OnPropertyChanged if you want to use a selected event to show more data
        private Event _selectedEvent;
        public Event SelectedEvent
        {
            get { return _selectedEvent;}
            set
            {
                _selectedEvent = value;
                OnPropertyChanged();
            }
        }



        // Using savecommand this to save the main list when you're done editing an event.
        private ICommand _saveEventCommand;

        public ICommand SaveEventCommand
        {
            get { return _saveEventCommand ?? (_saveEventCommand = new RelayCommand(EventHandler.SaveEvents)); }
            set { _saveEventCommand = value; }
        }

        public ICommand SelectedEventCommand
        {
            get
            {    //vi sætter _selectedEventCommand til at være lig en ny RelayCommand der kan tage argumenter af typen Event
                // dernæst kalder den metoden SetSelectedEvent i EventHandler. 
                return _selectedEventCommand ?? (_selectedEventCommand = new RelayArgCommand<Event>(ev => EventHandler.SetSelectedEvent(ev))); }

            set { _selectedEventCommand = value; }
        }

        public ICommand CheckExpireCommand { get; set; }
        
        public ICommand CreateEventCommand { get; set; }

        public ICommand DeleteEventCommand
        {
            //denne metode kalder DeleteEvent metoden i EventHandler via RelayCommand.
            get { return _deleteEventCommand ?? (_deleteEventCommand = new RelayCommand(EventHandler.DeleteEvent)); }

            set { _deleteEventCommand = value; }

        }

        private string _filterText;
        public string FilterText
        {
            get { return _filterText;}
            set
            {
                if (_filterText != null)
                {
                    _filterText = value;
                    OnPropertyChanged();
                }
                
                
                FilterEvents();
            }
        }

        private ObservableCollection<Event> _filteredList;

        public ObservableCollection<Event> FilteredList
        {
            get { return _filteredList;}
            set
            {
                _filteredList = value;
                OnPropertyChanged();
            }
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
            
            CheckExpireCommand = new RelayCommand(EventHandler.ExpireCheck);

            FilteredList = EventCatalogSingleton.Events;

        }

        public void FilterEvents()
        {
            if (_filterText == null) _filterText = "";
            FilteredList = new ObservableCollection<Event>(EventCatalogSingleton.Instance.Events
                .Where(e => e.Name.ToLower().Contains(FilterText.ToLower())));
        }


        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } 
        #endregion
    }

    
}
