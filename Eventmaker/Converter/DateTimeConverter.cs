using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventmaker.Converter
{
    class DateTimeConverter
    {
        //denne metode tager 2 argumenter af typen DateTimeOffset, som er den lokale dato og tid med tidszonen med i beregningen, 
        // og TimeSpan, som er forskellen mellem 2 datoer. Disse kombineres så til en ny DateTime
        public static DateTime DateTimeOffsetAndTimeSetToDateTime(DateTimeOffset date, TimeSpan time)
        {
            return new DateTime(date.Year, date.Month, date.Day, time.Hours, time.Minutes, 0);
        }

    }
}
