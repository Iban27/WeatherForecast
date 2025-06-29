using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AdditionalInformationPresenter
{
    private AdditionalInformationModel _additionalInformationModel;
    private AdditionalInformationView _additionalInformationView;
    private readonly ForecastList _forecastList;

    public AdditionalInformationPresenter(ForecastList forecastList, TextMeshProUGUI additionalInformationText)
    {
        _forecastList = forecastList;
        _additionalInformationView = new AdditionalInformationView(additionalInformationText);
        DisplayInfo();
    }

    public void UpdateDate(ForecastElement[] forecastElements)
    {
        float averageTemperature = GetAverageTemperature(forecastElements);
        float minTemp = GetMaxMinTemperature(forecastElements).Item1;
        float maxTemp = GetMaxMinTemperature(forecastElements).Item2;
        int positiveTemperatureCount = GetCountPNTemperature(forecastElements).Item1;
        int negativeTemperatureCount = GetCountPNTemperature(forecastElements).Item2;
        _additionalInformationModel = new AdditionalInformationModel(averageTemperature, maxTemp, minTemp, positiveTemperatureCount, negativeTemperatureCount);
        DisplayInfo();
    }

    private void DisplayInfo()
    {
        if (_additionalInformationModel == null) 
        {
            return;
        }
        string additionalInfo = "";
        additionalInfo = $"Средняя температура: {_additionalInformationModel.AverageTemperature}\n";
        additionalInfo += $"Максимальная / Минимальная температура: {_additionalInformationModel.MaxTemperature}/{_additionalInformationModel.MinTemperature}\n";
        additionalInfo += $"Количество дней с Положительной / Отрицательной температурой: {_additionalInformationModel.PositiveTemperatureCount}/{_additionalInformationModel.NegativeTemperatureCount}";
        _additionalInformationView.UpdateAdditionalInfoText(additionalInfo);
    }

    private float GetAverageTemperature(ForecastElement[] forecastElements)
    {

        float averageTemperature = 0;
        float tempCount = 0.0f;
        foreach (var forecastElement in forecastElements)
        {

            averageTemperature += forecastElement.temperature;
            tempCount += 1.0f;


        }
        return averageTemperature / tempCount;
    }

    private (float, float) GetMaxMinTemperature(ForecastElement[] forecastElements)
    {

        float maxTemp = float.MinValue;
        float minTemp = float.MaxValue;

        foreach (var forecastElement in forecastElements)
        {
            if (forecastElement.temperature > maxTemp)
            {
                maxTemp = forecastElement.temperature;
            }
            if (forecastElement.temperature < minTemp)
            {
                minTemp = forecastElement.temperature;
            }
        }
        var minMaxTemp = (minTemp, maxTemp);
        return minMaxTemp;
    }


    private (int, int) GetCountPNTemperature(ForecastElement[] forecastElements)
    {
        int positiveTemperatureCount = 0;
        int negativeTemperatureCount = 0;


        foreach (var forecastElement in forecastElements)
        {
            if (forecastElement.temperature > 0)
            {
                positiveTemperatureCount++;
            }
            if (forecastElement.temperature < 0)
            {
                negativeTemperatureCount++;
            }
        }

        var posNegTempCount = (positiveTemperatureCount, negativeTemperatureCount);
        return posNegTempCount;
    }
}
