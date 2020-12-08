using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;



public class GPS : MonoBehaviour
{
    public static GPS Instance { set; get; }

    public float lat;
    public float lon;

    private string city;
    public string City {
        get
        {
            //if (city == null || city == "")
            //{
            //    city = "not found";
            //}
            return city;
        }
        set 
        {
            city = value;
        } 
    }

    private void Start()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            StartCoroutine(StartLocationService());
        }
    }

    private IEnumerator StartLocationService()
    {
        if(!Input.location.isEnabledByUser)
        {
            Debug.Log("user has not enabled GPS");
            yield break;
        }

        Input.location.Start();
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if(maxWait <= 0)
        {
            Debug.Log("timed out");
            yield break;
        }

        if(Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("unable to determin device location");
            yield break;
        }

        lat = Input.location.lastData.latitude;
        lon = Input.location.lastData.longitude;

        StartCoroutine(getCityAtLocation());
    }

    IEnumerator getCityAtLocation()
    {
        string URL = "https://us1.locationiq.com/v1/reverse.php?key=pk.45836da0c5c161e06c9a86817c56fe7f&lat=" + lat + "&lon=" + lon + "&statecode=1&format=json";

        UnityWebRequest locationInfoRequest = UnityWebRequest.Get(URL);

        yield return locationInfoRequest.SendWebRequest();

        if(locationInfoRequest.isNetworkError || locationInfoRequest.isHttpError)
        {
            Debug.LogError(locationInfoRequest.error);
            yield break;
        }

        JSONNode locationInfo = JSON.Parse(locationInfoRequest.downloadHandler.text);

        //pull the info by index/name
        //ie: string city = locationInfor["city"];
        City = locationInfo["address"]["city"];
        
    }

    
}
