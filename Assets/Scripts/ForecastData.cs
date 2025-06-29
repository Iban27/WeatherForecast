using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class ForecastData
    {
        public float latitude;
        public float longitude;
        public float generationtime_ms;
        public int utc_offset_seconds;
        public string timezone;
        public string timezone_abbreviation;
        public float elevation;
        public HourlyUnits hourly_units;
        public Hourly hourly;
    }

    public class HourlyUnits
    {
        public string time;
        public string temperature_2m;
    }

    public class Hourly
    {
        public string[] time;
        public float[] temperature_2m;
    }
}
