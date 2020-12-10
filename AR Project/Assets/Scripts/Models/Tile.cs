using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool HasEntity { get; set; } = false;
    public Entity Entity { get; set; } = null;
    public int Row { get; set; } = 0;
    public int Col { get; set; } = 0;

    public Material OriginalMaterial;

    public void MakeMeOriginal()
    {
        gameObject.GetComponent<MeshRenderer>().material = OriginalMaterial;
    }
}
