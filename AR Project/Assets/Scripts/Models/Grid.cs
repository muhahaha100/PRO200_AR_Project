using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] Camera Camera = null;
    [SerializeField] PlacementManager PM = null;

    [SerializeField] int GridRows = 0;
    [SerializeField] int GridCols = 0;
    [SerializeField] GameObject TilePrefab = null;
    [SerializeField] Material BlackMat = null;
    [SerializeField] Material WhiteMat = null;

    public GameObject[,] Tiles = null;

    // Start is called before the first frame update
    void Start()
    {
        Tiles = new GameObject[GridRows,GridCols];
        //FOR CREATING THE TILE GRID
        float xOffset = ((GridRows / 2.0f) - 0.5f) * -1;
        float zOffset = ((GridCols / 2.0f) - 0.5f) * -1;

        for(int x = 0; x < GridRows; x++)
        {
            for(int y = 0; y < GridCols; y++)
            {
                Vector3 Position = new Vector3(xOffset + (1 * x), TilePrefab.transform.position.y, zOffset + (1 * y));
                GameObject Tile = Instantiate(TilePrefab, Position, TilePrefab.transform.rotation);
                Tile.GetComponent<Tile>().Row = x;
                Tile.GetComponent<Tile>().Col = y;
                Tiles[x, y] = Tile;
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
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit Hit = new RaycastHit();
            Ray Ray = Camera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(Ray, out Hit))
            {
                if(Hit.collider.gameObject.GetComponent<Tile>() != null)
                {
                    Vector3 position = new Vector3(Hit.collider.transform.position.x, Hit.collider.transform.position.y + 1.5f, Hit.collider.transform.position.z);
                    Quaternion rotation = Hit.collider.transform.rotation;

                    Debug.Log("Tile Hit");
                    //PLACEMENT
                    if(Hit.collider.gameObject.GetComponent<Tile>().Col <= 2)
                    {
                        if (!Hit.collider.gameObject.GetComponent<Tile>().HasEntity)
                        {
                            GameObject minion = null;
                            if(PM.ColorClicked == "Red")
                            {
                                minion = Instantiate(PM.RedMinion, position, rotation);
                            }
                            else if (PM.ColorClicked == "Green")
                            {
                                minion = Instantiate(PM.GreenMinion, position, rotation);
                            }
                            else if (PM.ColorClicked == "Blue")
                            {
                                minion = Instantiate(PM.BlueMinion, position, rotation);
                            }
                            else
                            {
                                Debug.Log("No Color Selected");
                            }

                            if(minion != null)
                            {
                                Hit.collider.gameObject.GetComponent<Tile>().HasEntity = true;
                                Hit.collider.gameObject.GetComponent<Tile>().Entity = minion.GetComponent<Minion>();
                                Debug.Log("Minion Placed");
                            }
                        }
                    }
                }
            }
        }
    }
}
