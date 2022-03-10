using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour
{
    [SerializeField] Material sky;
    [SerializeField] Light sun;
    private float fullIntensity;
    private float cloudValue = 0f;

    void OnEnable(){
        Messenger.AddListener(GameEvent.WEATHER_UPDATED, OnWeatherUpdated);
    }

    void OnDisable(){
        Messenger.RemoveListener(GameEvent.WEATHER_UPDATED, OnWeatherUpdated);
    }

    void Start(){
        fullIntensity = sun.intensity;
    }

    private void OnWeatherUpdated(){
        SetOvercast(Managers.Weather.CloudValue);
    }

    void Update()
    {
        SetOvercast(cloudValue);
        cloudValue += 0.005f;
    }

    private void SetOvercast(float value)
    {
        sky.SetFloat("_Blend", value);
        sun.intensity = fullIntensity - (fullIntensity * value);
    }
}
