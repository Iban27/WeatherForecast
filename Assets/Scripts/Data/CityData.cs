using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "City", menuName = "CityManager/CreateCity")]
public class CityData : ScriptableObject
{
    [SerializeField] private string _cityName;
    public string GetName => _cityName;
    [SerializeField] private string _countryName;
    public string GetCountryName => _countryName;
    [SerializeField] private string _latitude;
    public string GetLatitude => _latitude;
    [SerializeField] private string _longitude;
    public string GetLongitude => _longitude;
}
