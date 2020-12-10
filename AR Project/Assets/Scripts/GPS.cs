using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.Android;

public class GPS : MonoBehaviour
{
    public static GPS Instance { set; get; }

    public float lat;
    public float lon;
    GameObject dialog = null;

    private string city;
    public string City {
        get
        {
            if (city == null || city == "")
            {
                city = "not found";
            }
            return city;
        }
        set 
        {
            city = value;
        } 
    }

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {

        if (!Permission.HasUserAuthorizedPermission(Permission.CoarseLocation))
        {
            Permission.RequestUserPermission(Permission.CoarseLocation);
            dialog = new GameObject();
        }

        StartCoroutine(StartLocationService());

    }

    private void OnGUI()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.CoarseLocation))
        {
            
            dialog.AddComponent<PermissionsRationaleDialog>();

            return;
        }
        else if (dialog != null)
        {
            Destroy(dialog);
            StartCoroutine(StartLocationService());

        }
    }

    private IEnumerator StartLocationService()
    {
        yield return new WaitUntil(() => dialog == null);

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

public class PermissionsRationaleDialog : MonoBehaviour
{
    const int kDialogWidth = 300;
    const int kDialogHeight = 100;
    private bool windowOpen = true;

    void DoMyWindow(int windowID)
    {
        GUI.Label(new Rect(10, 20, kDialogWidth - 20, kDialogHeight - 50), "the location service is used to track your city for the leaderboards.");
        GUI.Button(new Rect(10, kDialogHeight - 30, 100, 20), "No");
        if (GUI.Button(new Rect(kDialogWidth - 110, kDialogHeight - 30, 100, 20), "Yes"))
        {
#if PLATFORM_ANDROID
            Permission.RequestUserPermission(Permission.CoarseLocation);
#endif
            windowOpen = false;
        }
    }

    void OnGUI()
    {
        if (windowOpen)
        {
            Rect rect = new Rect((Screen.width / 2) - (kDialogWidth / 2), (Screen.height / 2) - (kDialogHeight / 2), kDialogWidth, kDialogHeight);
            GUI.ModalWindow(0, rect, DoMyWindow, "Permissions Request Dialog");
        }
    }
}