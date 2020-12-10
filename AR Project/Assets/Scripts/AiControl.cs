using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AiControl : MonoBehaviour
{

    public SimulationManager simulationManager;
    public PlacementManager placementManager; // i dont think this is needed but still nice to have
    public Grid grid;
    
    public GameObject RedMinionPrefab;
    public GameObject GreenMinionPrefab;
    public GameObject BlueMinionPrefab;
    public enum MovementType
    {
        Knight,
        Diagonal,
        LongJump
    }

    public int rows = 7;
    public int columns = 10;
    public GameObject[,] m_board;
    
    // Start is called before the first frame update
    void Start()
    {
        //m_board = new int[7,10];
    }

    public void AddEntity(int row, int col, GameObject value)
    {
        if ( m_board[row, col] == null )
        {
            m_board[row, col] = value;
        }
        else
        {
            Debug.LogError($"Row: {row.ToString()},\n Col: {col.ToString()} already has an entity!");
        }
    }

    public void SetBoardValue( int row, int col, GameObject value )
    {
        m_board[row, col] = value;
    }

    public void MoveEntityCloserTo( int rowFrom, int colFrom, int rowTo, int colTo,int distance , MovementType movType = MovementType.LongJump )
    {
        int newRow = 0;
        int newCol = 0;
        if ( Mathf.Abs(rowFrom - rowTo) <= 1 )
        {
            newRow = rowFrom;
            if ( Mathf.Abs(colFrom - colTo) <= 1 )
            {
                // add element proficinecy here
                
                Debug.Log("Entity is already next to where it wants to be!");
            }
            else
            {
                int difference = colTo - colFrom;
                newCol = colFrom + difference;
            }
        }
        else
        {
            int rowDifference = rowTo - rowFrom;
            newRow = rowFrom + rowDifference;

            int colDifference = colTo - colFrom;
            newCol = colFrom + colDifference;
            // OMG REUSED CODE, THE HUMANITY
        }

        Entity valueHolder = grid.Tiles[rowFrom, colFrom].GetComponent<Tile>().Entity;
        grid.Tiles[rowFrom, colFrom].GetComponent<Tile>().Entity = null;
        grid.Tiles[newRow, newCol].GetComponent<Tile>().Entity = valueHolder;

        valueHolder.tile = grid.Tiles[newRow, newCol].GetComponent<Tile>();
        valueHolder.gameObject.transform.position = valueHolder.tile.gameObject.transform.position + Vector3.up;
        // lerp or something
        

    }

    public void BetterMoveEntityCloser(int rowFrom, int colFrom, int rowTo, int colTo, int maxDistance)
    {
        int[][] possibleMoves = new int[8][];
        int index = 0;

        for ( int r = rowTo - 1; r < rowTo + 2; r++ )
        {
            for ( int c = colTo - 1; c < colTo + 2; c++ )
            {
                try
                {
                    if ( grid.Tiles[r, c].GetComponent<Tile>().Entity == null )
                    {
                        int[] tempArr = new int[2];
                        tempArr[0] = r;
                        tempArr[1] = c;
                        possibleMoves[index] = tempArr;
                        index++;
                    }
                }
                catch ( IndexOutOfRangeException e )
                {
                    Debug.LogWarning(e);
                }
            }
        }

        bool anything = false;
        for ( int i = 0; i < possibleMoves.Length; i++ )
        {
            if ( possibleMoves[i] != null )
            {
                anything = true;
                index = i;
                break;
            }
        }

        if ( anything )
        {
            MoveEntityCloserTo(rowFrom, colFrom, possibleMoves[index][0],possibleMoves[index][1], 3);
        }
        
        
    }
    

    // Update is called once per frame
    void Update()
    {
        simulation_time += Time.deltaTime;

        if ( simulation_time >= simulation_time_max )
        {
            simulation_time = 0;
            GoThroughEachEntityToMoveAndAttack();
        }
    }

    public void GoThroughEachEntityToMoveAndAttack() // call this every 3 seconds or something
    {
        foreach ( Entity entity in entities )
        {
            int closestCol = 100;
            int closestRow = 100;

            foreach ( Entity otherEntity in entities )
            {
                if ( entity != otherEntity )
                {
                    if ( entity.enemy != otherEntity.enemy )
                    {
                        BetterMoveEntityCloser(entity.tile.Row, entity.tile.Col, otherEntity.tile.Row, otherEntity.tile.Col, 3);
                    }
                }
            }
        }
    }

    public void CallMeToMakeThePiecesMove()
    {
        
    }

    public void EnemyAddMinions()
    {
        
    }

    public void StartSimulating()
    {
        foreach (GameObject tile in grid.Tiles)
        {
            Tile tile_tile = tile.GetComponent<Tile>();
            if ( tile_tile.Entity != null )
            {
                entities.Add(tile_tile.Entity);
                Debug.Log(tile.GetComponent<Tile>().Entity.name);
            }
        }
        
        spawnEnemies();

        simulating = true;
    }

    private float simulation_time = 0;
    private float simulation_time_max = 3;
    private bool simulating = false;
    // properties down here? HERESY
    private List<Entity> entities = new List<Entity>();

    public void spawnEnemies()
    {
        for ( int i = 0; i < 3; i++ )
        {
            int row = (int) Mathf.Floor( Random.Range(0, 10) );
            int type = (int) Mathf.Floor(Random.Range(1, 4));
            GameObject prefab = RedMinionPrefab;

            switch ( type )
            {
                case 0:
                    prefab = RedMinionPrefab;
                    break;
                case 1:
                    prefab = BlueMinionPrefab;
                    break;
                case 2:
                    prefab = GreenMinionPrefab;
                    break;
                default:
                    prefab = RedMinionPrefab;
                    break;
            }

            GameObject minion = Instantiate(prefab, grid.Tiles[row, 9 - i].transform.position + Vector3.up * 2, Quaternion.identity);
            grid.Tiles[row, 9 - i].GetComponent<Tile>().HasEntity = true;
            minion.GetComponent<Entity>().enemy = true;
            grid.Tiles[row, 9 - i].GetComponent<Tile>().Entity = minion.GetComponent<Entity>();
            minion.GetComponent<Entity>().tile = grid.Tiles[row, 9 - i].GetComponent<Tile>();
            entities.Add(minion.GetComponent<Entity>());
        }
    }
}
