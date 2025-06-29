using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using Assets;

public class ForecastLoader
{
    private NetworkLoader _networkLoader = new NetworkLoader();
    private const string URL = "https://api.open-meteo.com/v1/forecast?latitude=52.52&longitude=13.41&hourly=temperature_2m";
    public ForecastData forecastData = new ForecastData();

    public async Task<ForecastData> Initialization()
    {
        string jsonData = await _networkLoader.GetData(URL);
        forecastData = JsonConvert.DeserializeObject<ForecastData>(jsonData);
        return forecastData;
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
