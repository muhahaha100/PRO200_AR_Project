using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GPSTextTest : MonoBehaviour
{
    public Text Coordinates;

    private void Update()
    {
        Coordinates.text = "lat: " + GPS.Instance.lat.ToString() + "  long: " + GPS.Instance.lon.ToString() + " end";
    }
}
