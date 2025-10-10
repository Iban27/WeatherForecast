using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Assets;

public class CityElement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private Button _button;
    [SerializeField] private LoadImageToRaw LoadImageToRaw;
    [SerializeField] private TextMeshProUGUI _longitudeLatitudeText;
    public event Action<float, float> onButtonClicked = (lon, lan) => { };
    public event Action onImageLoaded = () => { };
    private NewCityData _cityData;

    private void Awake()
    {
        _button.onClick.AddListener(OnButtonClicked);
    }

    public void Initizalize(NewCityData cityData)
    {
        _cityData = cityData;
        _title.text = $"{cityData.city}, {cityData.country}";
        _longitudeLatitudeText.text = $"Долгота: {cityData.longitude} Широта: {cityData.latitude}";
        LoadImageToRaw.onImageLoaded += OnImageLoaded;
        LoadImageToRaw.InitializeIcon(cityData.icon_id);
    }

    private void OnButtonClicked()
    {
        onButtonClicked?.Invoke(_cityData.latitude, _cityData.longitude);
    }

    public void OnImageLoaded()
    {
        Debug.Log("OnImageLoaded");
        onImageLoaded?.Invoke();
    }
}