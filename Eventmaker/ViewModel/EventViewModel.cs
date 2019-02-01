using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eventmaker.Model;

namespace Eventmaker.ViewModel
{
    class EventViewModel
    {
        public EventCatalogSingleton EventCatalogSingleton { get; set; }

        public EventViewModel()
        {
            EventCatalogSingleton = EventCatalogSingleton.Instance;
        }
    }

    
}
