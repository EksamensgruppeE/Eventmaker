using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eventmaker.ViewModel;

namespace Eventmaker.Handler
{
    class EventHandler
    {
        public EventViewModel EventViewModel { get; set; }

        public EventHandler(EventViewModel eventViewModel)
        {
            EventViewModel= eventViewModel;
        }

        public void CreatEvent()
        {

              //
        }
       
    }
}
