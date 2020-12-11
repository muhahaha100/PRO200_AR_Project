using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    [SerializeField] PlacementManager PM = null;

    public AiControl aiControl;
    
    private bool SimulationRunning = false;
    public float timer = 0.0f;
    void Update()
    {
        if (SimulationRunning)
        {
            timer += Time.deltaTime;
        }
    }

    public void SimulateClicked()
    {
        if ( !SimulationRunning )
        {
            PM.DisablePlacement();

            SimulationRunning = true;
            print("Simulation started");
        
            aiControl.StartSimulating();
        }
    }
}
