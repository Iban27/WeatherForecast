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
    public NewForecastData newForecastData = new NewForecastData();

    public async Task<NewForecastData> Initialization(float latitude, float longitude)
    {
        int daysCount = 4;
        
        string strLatitude = string.Format("{0}", latitude).Replace(",", ".");
        string strLongitude = string.Format("{0}", longitude).Replace(",", ".");
        string URL = string.Format("https://api.weatherapi.com/v1/forecast.json?key=eb57875e3e6843218f4112159250207&q={0},{1}&days={2}&lang=ru&aqi=yes", strLatitude, strLongitude, daysCount);
        
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
        return newForecastData;
    }
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
