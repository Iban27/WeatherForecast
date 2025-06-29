using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DayWeatherElement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private Button _button;
    public event Action<DateTime> onButtonClicked = (date) => { };
    private DateTime _dateTime;

    private void Awake()
    {
        _button.onClick.AddListener(OnButtonClicked);
    }

    public void Initialize(DateTime time)
    {
        _dateTime = time;
        _title.text = time.ToString("dd.MM, ddd");
    }

    private void OnButtonClicked()
    {
        onButtonClicked?.Invoke(_dateTime);
    }
}
