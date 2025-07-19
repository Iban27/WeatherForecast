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
        Debug.Log(forecastElement.condition.icon);
        _loadImageToRaw.Initialize(forecastElement.condition.icon);

        //dayNameText formating
        if (forecastElement.time.Day == DateTime.Now.Day)
        {
            _dayNameText.text = "Сегодня";
            _dayNameText.color = Color.red;
        }
        else
        {
            //_dayNameText.text = forecastElement.time.DayOfWeek.ToString();
            _dayNameText.text = forecastElement.time.ToString("ddd");
            //Debug.Log(forecastElement.time.DayOfWeek);
            // "G", "g", "X", "x", "F", "f", "D" or "d"
        }

        //dayNameText formating
        _dateText.text = forecastElement.time.Day.ToString("d") + " " + forecastElement.time.ToString("MMMM");

        //temperature formating
        _dayTemperatureText.text = forecastElement.temperature.ToString() + "°";
    }

    private void OnButtonClicked()
    {
        onButtonClicked?.Invoke(_dateTime);
    }
}
