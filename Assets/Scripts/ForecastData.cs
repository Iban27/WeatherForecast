using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    #region OldForecast
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
    #endregion

    public class NewForecastData
    {
        public Location location;
        public Current current;
        public Forecast forecast;
    }

    public class Location
    {
        public string name;
        public string region;
        public string country;
        public float lat;
        public float lon;
        public string tz_id;
        public int localtime_epoch;
        public string localtime;
    }

    public class Current
    {
        public int last_updated_epoch;
        public string last_updated;
        public float temp_c;
        public float temp_f;
        public int is_day;
        public Condition condition;
        public float wind_mph;
        public float wind_kph;
        public int wind_degree;
        public string wind_dir;
        public float pressure_mb;
        public float pressure_in;
        public float precip_mm;
        public float precip_in;
        public int humidity;
        public int cloud;
        public float feelslike_c;
        public float feelslike_f;
        public float windchill_c;
        public float windchill_f;
        public float heatindex_c;
        public float heatindex_f;
        public float dewpoint_c;
        public float dewpoint_f;
        public float vis_km;
        public float vis_miles;
        public float uv;
        public float gust_mph;
        public float gust_kph;
        public AirQuality air_quality;
    }

    public class Forecast
    {
        public ForecastDay[] forecastday;
    }

    public class Condition
    {
        public string text;
        public string icon;
        public int code;
    }

    public class AirQuality
    {
        public float co;
        public float no2;
        public float o3;
        public float so2;
        public float pm2_5;
        public float pm10;
        public int us_epa_index;
        public int gb_defra_index;
    }

    #region forecastDay
    public class ForecastDay
    {
        public string date;
        public int date_epoch;
        public Day day;
        public Astro astro;
        public Hour[] hour;
    }

    public class Day
    {
        public float maxtemp_c;
        public float maxtemp_f;
        public float mintemp_c;
        public float mintemp_f;
        public float avgtemp_c;
        public float avgtemp_f;
        public float maxwind_mph;
        public float maxwind_kph;
        public float totalprecip_mm;
        public float totalprecip_in;
        public float totalsnow_cm; 
        public float avgvis_km;
        public float avgvis_miles;
        public float avghumidity;
        public int daily_will_it_rain;
        public int daily_chance_of_rain;
        public int daily_will_it_snow;
        public int daily_chance_of_snow;
        public Condition condition;
        public float uv;
        public AirQuality airQuality;
    }

    public class Astro
    {
        public string sunrise;
        public string sunset;
        public string moonrise;
        public string moonset;
        public string moon_phase;
        public int moon_illumination;
        public int is_moon_up;
        public int is_sun_up;
    }

    public class Hour
    {
        public int time_epoch;
        public string time;
        public float temp_c;
        public float temp_f;
        public int is_day;
        public Condition condition;
        public float wind_mph;
        public float wind_kph;
        public int wind_degree;
        public string wind_dir;
        public float pressure_mb;
        public float pressure_in;
        public float precip_mm;
        public float precip_in;
        public float snow_cm;
        public int humidity;
        public int cloud;
        public float feelslike_c;
        public float feelslike_f;
        public float windchill_c;
        public float windchill_f;
        public float heatindex_c;
        public float heatindex_f;
        public float dewpoint_c;
        public float dewpoint_f;
        public int will_it_rain;
        public int chance_of_rain;
        public int will_it_snow;
        public int chance_of_snow;
        public float vis_km;
        public float vis_miles;
        public float gust_mph;
        public float gust_kph;
        public float uv;
        public AirQuality airQuality;
    }

    
    #endregion
}