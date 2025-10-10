using Assets;
using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WindowManagerSystem;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Unity.VisualScripting;
using System.Linq;

namespace WindowManagerSystem
{
    public class StartMenuWindow : Window
    {
        [SerializeField] private CityData[] _cityData;
        [SerializeField] private CityElement _cityElementPrefab;
        [SerializeField] private Transform _cityElementContainer;
        private ForecastLoader _forecastLoader = new ForecastLoader();
        private CitiesLoader _citiesLoader = new CitiesLoader();
        private List<NewCityData> _newCityData;
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private TextMeshProUGUI _noCitiesText;
        [SerializeField] private GameObject _loadingPanel;
        [SerializeField] private TextMeshProUGUI _progressText;
        [SerializeField] private Slider _progressSlider;
        private int _loadedCityCount = 0;
        private int _cityCount = 0;

        private void NewLoadCities(List<NewCityData> newCityData)
        {
            _cityCount = newCityData.Count;
            
            foreach (var city in newCityData)
            {
                CityElement cityElement = Instantiate(_cityElementPrefab, _cityElementContainer);
                cityElement.onButtonClicked += OpenWeatherWindow;
                cityElement.onImageLoaded += ChangeProgressSlider;
                cityElement.onImageLoaded += UpdateLoadingPanel;
                cityElement.Initizalize(city);
            }
        }

        private void UpdateLoadingPanel()
        {
            var percentage = (double)_loadedCityCount / (double)_cityCount * 100f;
            _progressText.text = percentage.ToString("f2") + "%";
            _progressSlider.value = (float)percentage / 100f;
            //Debug.Log(percentage);
            if (percentage >= 100)
            {
                _loadingPanel.SetActive(false);
            }
        }

        private void ChangeProgressSlider()
        {
            _loadedCityCount++;
        }

        public async void Start()
        {
            _newCityData = await _citiesLoader.Initialization();
            NewLoadCities(_newCityData);
        }

        private void OpenWeatherWindow(float latitude, float longitude)
        {
            LoadWeather(latitude, longitude);
        }

        private async void LoadWeather(float latitude, float longitude)
        {
            WindowManager.Instance.Open<LoadingWindow>();
            WindowManager.Instance.Close<StartMenuWindow>();
            var forecastData = await _forecastLoader.Initialization(latitude, longitude);
            WeatherWindow weatherWindow = WindowManager.Instance.Open<WeatherWindow>() as WeatherWindow;
            weatherWindow.Initialize(forecastData);
            WindowManager.Instance.Close<LoadingWindow>();
        }

        public Dictionary<NewCityData, int> GetDictionaryPriority(List<NewCityData> cities)
        {
            string inputText = Regex.Replace(_inputField.text, @"[^a-z0-9?-??\s]", string.Empty, RegexOptions.IgnoreCase).ToLower();
            Dictionary<NewCityData, int> citiesPriority = new Dictionary<NewCityData, int>();
            for (int i = 0; i < cities.Count; i++)
            {
                string cityText = Regex.Replace(cities[i].city, @"[^a-z0-9?-??\s]", string.Empty, RegexOptions.IgnoreCase).ToLower();
                int priority = LevenshteinDistance(inputText, cityText);
                citiesPriority.Add(cities[i], priority);
            }
            var sorted = citiesPriority.OrderBy(x => x.Value).Take(10).ToDictionary(x => x.Key, x => x.Value);
            return sorted;
        }

        public void OnInputFieldChanged()
        {
            if (_inputField.text.Length == 0)
            {
                DestroyCityElements();
                NewLoadCities(_newCityData);
                ChangeNoCitiesText(0);
                return;
            }
            
            List<NewCityData> searchedCityElements = new List<NewCityData>();
            var priority = GetDictionaryPriority(_newCityData);
            
            foreach (var city in priority)
            {
                int i = 0;
                if (i < 10)
                {
                    if (city.Value < 4)
                    {
                        searchedCityElements.Add(city.Key);
                        i++;
                    }
                }
            }
            
            ChangeNoCitiesText(searchedCityElements.Count);
            DestroyCityElements();
            NewLoadCities(searchedCityElements);
            
        }

        public void ChangeNoCitiesText(int cityCount)
        {
            if (cityCount == 0 && _inputField.text.Length > 0)
            {
                _noCitiesText.gameObject.SetActive(true);
            }
            else
            {
                _noCitiesText.gameObject.SetActive(false);
            }
        }

        public void DestroyCityElements()
        {
            for (int i = 0; i < _cityElementContainer.childCount; i++)
            {
                Destroy(_cityElementContainer.GetChild(i).gameObject);
            }
        }

        public static int LevenshteinDistance(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            for (int i = 0; i <= n; i++)
                d[i, 0] = i;
            for (int j = 0; j <= m; j++)
                d[0, j] = j;

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (s[i - 1] == t[j - 1]) ? 0 : 1;
                    int deletion = d[i - 1, j] + 1;
                    int insert = d[i, j - 1] + 1;
                    int sub = d[i - 1, j - 1] + cost;
                    d[i, j] = Math.Min(Math.Min(deletion, insert), sub);
                }
            }
            return d[n, m];
        }
    }
}