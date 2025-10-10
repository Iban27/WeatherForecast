using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HourlyWeatherElement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _hour;
    [SerializeField] private TextMeshProUGUI _temperature;
    [SerializeField] private LoadImageToRaw _loadImageToRaw;
    private DateTime _time;

    public void Initialize(string time, float temperature, Condition condition)
    {
        _hour.text = time;
        _temperature.text = temperature.ToString();
        condition.text = "https:" + condition.text;
        _loadImageToRaw.Initialize(condition.icon);
    }
}
