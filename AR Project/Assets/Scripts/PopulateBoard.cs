using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateBoard : MonoBehaviour
{
    [SerializeField] public GameObject prefab;
    public int numToCreate;

    private void Start()
    {



        Populate();
    }

    private void Populate()
    {
        GameObject newObj;

        for (int i = 0; i < numToCreate; i++)
        {
            newObj = (GameObject)Instantiate(prefab, transform);
        }
    }
}
