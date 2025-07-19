using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Assets.Scripts;
using UnityEngine.Rendering;
using System;
using UnityEngine.UI;
using UnityEditorInternal;

namespace WindowManagerSystem
{
    public class WeatherWindow : Window
    {
        private AdditionalInformationPresenter _additionalInformationPresenter;
        [SerializeField] private TextMeshProUGUI _additionalInformationText;
        [SerializeField] private DayWeatherElement _dayWeatherElementPrefab;
        [SerializeField] private Transform _dayWeatherElementContainer;
        [SerializeField] private HourlyWeatherElement _hourlyWeatherElementPrefab;
        [SerializeField] private Transform _hourlyWeatherElementContainer;
        [SerializeField] private Toggle _allWeatherToggle;
        private ForecastList _forecastList;
        private DateTime _currentDateTime;

        public void Initialize(NewForecastData forecastData)
        {
            CreateForecastList(forecastData);
            _additionalInformationPresenter = new AdditionalInformationPresenter(_forecastList, _additionalInformationText);
            LoadDays();
        }

        private void CreateForecastList(NewForecastData forecastData)
        {
            _forecastList = new ForecastList();
            /*
            for (int i = 0; i < forecastData.hourly.time.Length; i++)
            {
                if (DateTime.TryParse(forecastData.hourly.time[i], out DateTime result))
                {
                    _forecastList.AddForecastElement(result, forecastData.hourly.temperature_2m[i]);
                }
            }
            */
            Debug.Log(forecastData.forecast.forecastday[0].hour.Length);
            for (int j = 0; j < forecastData.forecast.forecastday.Length; j++)
            {
                for (int i = 0; i < forecastData.forecast.forecastday[j].hour.Length; i++)
                {
                    if (DateTime.TryParse(forecastData.forecast.forecastday[j].hour[i].time, out DateTime result))
                    {
                        _forecastList.AddForecastElement(result, forecastData.forecast.forecastday[j].hour[i].temp_c, forecastData.forecast.forecastday[j].day.condition);

                    }
                }
            }
            
        }

        private void LoadDays()
        {
            foreach (var day in _forecastList.ForecastElements)
            {
                if (day.time.Hour == 0)
                {
                    DayWeatherElement dayWeatherElement = Instantiate(_dayWeatherElementPrefab, _dayWeatherElementContainer);
                    dayWeatherElement.Initialize(day);
                    dayWeatherElement.onButtonClicked += UpdateCurrentDate;
                }
            }
            UpdateAdditionalInfo(_forecastList.ForecastElements[0].time);
            LoadHourlyWeather(_forecastList.ForecastElements[0].time);
            UpdateCurrentDate(_forecastList.ForecastElements[0].time);
        }

        private void UpdateAdditionalInfo(DateTime dateTime)
        {
            ForecastElement[] forecastElements = _forecastList.GetForecastListByDay(dateTime);
            _additionalInformationPresenter.UpdateDate(forecastElements);
        }

        private void LoadHourlyWeather(DateTime dateTime)
        {
            DestroyWeatherElements();
            ForecastElement[] forecastElements = _forecastList.GetForecastListByDay(dateTime);
            foreach (var hourly in forecastElements)
            {
                HourlyWeatherElement hourlyWeatherElement = Instantiate(_hourlyWeatherElementPrefab, _hourlyWeatherElementContainer);
                hourlyWeatherElement.Initialize(hourly.time.ToString("HH"), hourly.temperature);
            }
        }

        private void LoadDayWeather(DateTime dateTime)
        {
            DestroyWeatherElements();
            ForecastElement[] forecastElements = _forecastList.GetForecastListByDay(dateTime);

            foreach (var hourly in forecastElements)
            {
                if (hourly.time.Hour == 9)
                {
                    HourlyWeatherElement hourlyWeatherElement = Instantiate(_hourlyWeatherElementPrefab, _hourlyWeatherElementContainer);
                    hourlyWeatherElement.Initialize("Morning", hourly.temperature);
                }
                if (hourly.time.Hour == 12)
                {
                    HourlyWeatherElement hourlyWeatherElement = Instantiate(_hourlyWeatherElementPrefab, _hourlyWeatherElementContainer);
                    hourlyWeatherElement.Initialize("Afternoon", hourly.temperature);
                }
                if (hourly.time.Hour == 18)
                {
                    HourlyWeatherElement hourlyWeatherElement = Instantiate(_hourlyWeatherElementPrefab, _hourlyWeatherElementContainer);
                    hourlyWeatherElement.Initialize("Evening", hourly.temperature);
                }
                if (hourly.time.Hour == 0)
                {
                    HourlyWeatherElement hourlyWeatherElement = Instantiate(_hourlyWeatherElementPrefab, _hourlyWeatherElementContainer);
                    hourlyWeatherElement.Initialize("Night", hourly.temperature);
                }
            }
        }

        private void DestroyWeatherElements()
        {
            for (int i = 0; i < _hourlyWeatherElementContainer.childCount; i++)
            {
                Destroy(_hourlyWeatherElementContainer.GetChild(i).gameObject);
            }
        }

        public void UpdateCurrentDate(DateTime dateTime)
        {
            _currentDateTime = dateTime;
            UpdateWeatherTable();
        }

        public void OnToggleStateChanged()
        {
            UpdateWeatherTable();
        }

        private void UpdateWeatherTable()
        {
            if (_allWeatherToggle.isOn)
            {
                LoadHourlyWeather(_currentDateTime);
            }
            else
            {
                LoadDayWeather(_currentDateTime);
            }
            UpdateAdditionalInfo(_currentDateTime);
        }
    }
}
