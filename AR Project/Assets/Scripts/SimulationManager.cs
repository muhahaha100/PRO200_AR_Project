using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    [SerializeField] PlacementManager PM = null;

    private bool SimulationRunning = false;
    private float timer = 0.0f;
    void Update()
    {
        if (SimulationRunning)
        {
            timer += Time.deltaTime;
        }
    }

    public void SimulateClicked()
    {
        PM.DisablePlacement();
        SimulationRunning = true;
        print("Simulation started");
    }
}
