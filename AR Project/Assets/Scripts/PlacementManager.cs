using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject redMinion = null;
    public GameObject RedMinion { get { return redMinion; } set { redMinion = value; } }
    [SerializeField] GameObject greenMinion = null;
    public GameObject GreenMinion { get { return greenMinion; } set { greenMinion = value; } }
    [SerializeField] GameObject blueMinion = null;
    public GameObject BlueMinion { get { return blueMinion; } set { blueMinion = value; } }
    public string ColorClicked { get; set; } = "";

    //Buttons
    [SerializeField] GameObject RB = null;
    [SerializeField] GameObject GB = null;
    [SerializeField] GameObject BB = null;

    public void ButtonClicked(string Color)
    {
        ColorClicked = Color;
    }

    public void DisablePlacement()
    {
        ColorClicked = "";
        RB.SetActive(false);
        GB.SetActive(false);
        BB.SetActive(false);
    }
}
