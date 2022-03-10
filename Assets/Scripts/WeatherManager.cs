using System;
using System.Xml;
using UnityEngine;

public class WeatherManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status {get; private set;}

    private NetworkService network;

    public float CloudValue {get; private set;}

    public void Startup(NetworkService service)
    {
        Debug.Log("Weather manager starting...");

        network = service;
        StartCoroutine(network.GetWeatherXML(OnXMLDataLoaded));

        status = ManagerStatus.Initializing;
    }

    public void OnXMLDataLoaded(string data)
    {
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(data);
        XmlNode root = doc.DocumentElement;

        XmlNode node = root.SelectSingleNode("clouds");
        string value = node.Attributes["value"].Value;
        CloudValue = Convert.ToInt32(value) / 100f;

        Messenger.Broadcast(GameEvent.WEATHER_UPDATED);

        status = ManagerStatus.Started;
    }

    public void LogWeather(string name)
    {
        StartCoroutine(network.LogWeather(name, CloudValue, OnLogged));
    }

    private void OnLogged(string response){
        Debug.Log(response);
    }
}
