using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiControl : MonoBehaviour
{
    public enum MovementType
    {
        Knight,
        Diagonal,
        LongJump
    }

    public int rows = 7;
    public int columns = 10;
    public int[,] m_board;
    // Start is called before the first frame update
    void Start()
    {
        m_board = new int[7,10];
    }

    public void AddEntity(int row, int col, int value)
    {
        if ( m_board[row, col] == 0 )
        {
            m_board[row, col] = value;
        }
        else
        {
            Debug.LogError($"Row: {row.ToString()},\n Col: {col.ToString()} already has an entity!");
        }
    }

    public void SetBoardValue( int row, int col, int value )
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

        int valueHolder = m_board[rowFrom, colFrom];
        m_board[rowFrom, colFrom] = 0;
        m_board[newRow, newCol] = valueHolder;
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
