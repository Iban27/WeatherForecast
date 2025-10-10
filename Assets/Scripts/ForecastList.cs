using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using WindowManagerSystem;
using UnityEngine;

namespace Assets.Scripts
{
    public class ForecastList
    {
        private readonly List<ForecastElement> _forecastElements = new List<ForecastElement>();
        public IReadOnlyList<ForecastElement> ForecastElements => _forecastElements;
        public void AddForecastElement(DateTime time, float temperature, Condition condition, Hour[] hours)
        {
            ForecastElement forecastElement = new ForecastElement(time, temperature, condition, hours);
            _forecastElements.Add(forecastElement);
        }

        public ForecastElement[] GetForecastListByDay(DateTime day)
        {
            List<ForecastElement> forecastElements = new List<ForecastElement>();
            foreach (var element in _forecastElements)
            {
                if (element.time.Day==day.Day)
                {
                    forecastElements.Add(element);
                }
            }
            return forecastElements.ToArray();
        }
    }

    public class ForecastElement
    {
        public readonly DateTime time;
        public readonly float temperature;
        public readonly Condition condition;
        public readonly Hour[] hours;
        public ForecastElement(DateTime time, float temperature, Condition condition, Hour[] hours)
        {
            this.time = time;
            this.temperature = temperature;
            this.condition = condition;
            this.hours = hours;
        }
    }
}
