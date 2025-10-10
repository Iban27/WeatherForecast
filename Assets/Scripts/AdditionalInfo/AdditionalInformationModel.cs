using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalInformationModel
{
    public AdditionalInformationModel(float averageTemperature, float maxTemperature, float minTemperature, int positiveTemperatureCount, int negativeTemperatureCount)
    {
        this.AverageTemperature = averageTemperature;
        this.MaxTemperature = maxTemperature;
        this.MinTemperature = minTemperature;
        this.PositiveTemperatureCount = positiveTemperatureCount;
        this.NegativeTemperatureCount = negativeTemperatureCount;
    }

    public float AverageTemperature
    {
        private set;
        get;
    }

    public float MaxTemperature
    {
        private set;
        get;
    }

    public float MinTemperature
    {
        private set;
        get;
    }

    public int PositiveTemperatureCount
    {
        private set;
        get;
    }

    public int NegativeTemperatureCount
    {
        private set;
        get;
    }
}
