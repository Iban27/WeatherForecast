using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using Assets.Scripts;

public class DayWeatherElement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _buttonDateText;
    [SerializeField] private TextMeshProUGUI _dayNameText;
    [SerializeField] private TextMeshProUGUI _dateText;
    [SerializeField] private TextMeshProUGUI _dayTemperatureText;
    [SerializeField] private TextMeshProUGUI _nightTemperatureText;
    [SerializeField] private Button _button;
    [SerializeField] private LoadImageToRaw _loadImageToRaw;

    public event Action<DateTime> onButtonClicked = (date) => { };
    public event Action onImageLoaded = () => { };
    private DateTime _dateTime;
    
    private void Awake()
    {
        _button.onClick.AddListener(OnButtonClicked);
    }

    public void Initialize(ForecastElement forecastElement)
    {
        _dateTime = forecastElement.time;
        _buttonDateText.text = forecastElement.time.ToString("dd.MM, ddd");
        forecastElement.condition.icon = "https:"+forecastElement.condition.icon;
        _loadImageToRaw.Initialize(forecastElement.condition.icon);
        _loadImageToRaw.onImageLoaded += OnImageLoaded;
        
        if (forecastElement.time.Day == DateTime.Now.Day)
        {
            _dayNameText.text = "Сегодня";
            _dayNameText.color = Color.red;
        }
        else
        {
            _dayNameText.text = forecastElement.time.ToString("ddd");
        }
        
        _dateText.text = forecastElement.time.Day.ToString("d") + " " + forecastElement.time.ToString("MMMM");
        _dayTemperatureText.text = forecastElement.temperature + "°";
        _nightTemperatureText.text = forecastElement.hours[3].temp_c.ToString();
    }

    private void OnButtonClicked()
    {
        onButtonClicked?.Invoke(_dateTime);
    }
    
    public void OnImageLoaded()
    {
        Debug.Log("OnImageLoaded");
        onImageLoaded?.Invoke();
    }
}
