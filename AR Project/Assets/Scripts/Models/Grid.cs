using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] int GridRows = 0;
    [SerializeField] int GridCols = 0;
    [SerializeField] GameObject TilePrefab = null;
    [SerializeField] Material BlackMat = null;
    [SerializeField] Material WhiteMat = null;

    private List<GameObject> Tiles = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //FOR CREATING THE TILE GRID
        float xOffset = ((GridRows / 2.0f) - 0.5f) * -1;
        float zOffset = ((GridCols / 2.0f) - 0.5f) * -1;

        for(int x = 0; x < GridRows; x++)
        {
            for(int z = 0; z < GridCols; z++)
            {
                Vector3 Position = new Vector3(xOffset + (1 * x), TilePrefab.transform.position.y, zOffset + (1 * z));
                GameObject Tile = Instantiate(TilePrefab, Position, TilePrefab.transform.rotation);
                Tiles.Add(Tile);
            }
        }

        //FOR SETTING THE TILES MATERIAL COLOR

        bool isEven = (GridRows % 2 == 0 && GridCols % 2 == 0);
        if (!isEven)
        {
            int counter = 0;
            foreach(GameObject Tile in Tiles)
            {
                if(counter == 0)
                {
                    Tile.GetComponent<MeshRenderer>().material = WhiteMat;
                    counter++;
                }
                else if(counter == 1)
                {
                    Tile.GetComponent<MeshRenderer>().material = BlackMat;
                    counter--;
                }
            }
        }
        else
        {
            bool swapped = false;
            int counter = 0;
            int tilesCounted = 0;
            foreach(GameObject Tile in Tiles)
            {
                if (swapped)
                {
                    if (counter == 0)
                    {
                        Tile.GetComponent<MeshRenderer>().material = WhiteMat;
                        counter++;
                    }
                    else if (counter == 1)
                    {
                        Tile.GetComponent<MeshRenderer>().material = BlackMat;
                        counter--;
                    }
                }
                else
                {
                    if (counter == 1)
                    {
                        Tile.GetComponent<MeshRenderer>().material = WhiteMat;
                        counter--;
                    }
                    else if (counter == 0)
                    {
                        Tile.GetComponent<MeshRenderer>().material = BlackMat;
                        counter++;
                    }
                }
                tilesCounted++;
                if (tilesCounted >= GridRows)
                {
                    swapped = !swapped;
                    tilesCounted = 0;
                }

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
