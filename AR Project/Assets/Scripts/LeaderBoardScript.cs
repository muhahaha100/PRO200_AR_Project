using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderBoardScript : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI CurrentCity;

    private void Update()
    {
        CurrentCity.text = GPS.Instance.City;
    }
}
