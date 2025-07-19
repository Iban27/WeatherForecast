using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using Assets;
using System.Net;

public class ForecastLoader
{
    private NetworkLoader _networkLoader = new NetworkLoader();
    
    public ForecastData forecastData = new ForecastData();
    public NewForecastData newForecastData = new NewForecastData();

    public async Task<NewForecastData> Initialization(float latitude, float longitude)
    {
        int daysCount = 5;
        string URL = string.Format("https://api.open-meteo.com/v1/forecast?latitude={0}&longitude={1}&hourly=temperature_2m", latitude, longitude).Replace(",", ".");
        string strLatitude = string.Format("{0}", latitude).Replace(",", ".");
        string strLongitude = string.Format("{0}", longitude).Replace(",", ".");
        URL = string.Format("https://api.weatherapi.com/v1/forecast.json?key=eb57875e3e6843218f4112159250207&q={0},{1}&days={2}&lang=ru&aqi=yes", strLatitude, strLongitude, daysCount);
        
        Debug.Log(URL);
        using (UnityWebRequest webRequest = UnityWebRequest.Get(URL))
        {
            var operation = webRequest.SendWebRequest();
            while (operation.isDone == false)
            {
                Debug.Log(operation.progress);
                await Task.Yield();
            }
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error: {webRequest.error}");
                return null;
            }
            string jsonData = webRequest.downloadHandler.text;
            newForecastData = JsonConvert.DeserializeObject<NewForecastData>(jsonData);
        }

        Debug.Log("json data loaded");
        //forecastData = JsonConvert.DeserializeObject<ForecastData>(jsonData);
        
        Debug.Log("forecast data deserialized");
        return newForecastData;
    }

    //forecstData => List<CityData>
    //https://acinusproject.turgaliev.kz/city_for_forcast.json
}

public class CitiesLoader
{
    private NetworkLoader _networkLoader = new NetworkLoader();
    private const string URL = "https://acinusproject.turgaliev.kz/city_for_forcast.json";
    public List<NewCityData> cityData = new List<NewCityData>();

    public async Task<List<NewCityData>> Initialization()
    {
        string jsonData = await _networkLoader.GetData(URL);
        cityData = JsonConvert.DeserializeObject<List<NewCityData>>(jsonData);
        return cityData;
    }
}
