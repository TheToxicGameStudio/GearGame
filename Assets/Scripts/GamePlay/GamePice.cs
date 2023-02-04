using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePice : MonoBehaviour
{
    [Header("Tile Pos")]
    private int x;
    private int y;

    [Header("GameBoard")]
    private GameBoard.PiceType type;
    private GameBoard board;

    //Public Geter Methods.
    public int X { get { return x; } }
    public int Y { get { return y; } }

    public GameBoard.PiceType Type { get { return type; } }
    public GameBoard Board { get { return board; } }


    void Start()
    {

    }


    void Update()
    {

    }

    public void Init(int _x, int _y, GameBoard _board, GameBoard.PiceType _type)
    {
        x = _x;
        y = _y;
        board = _board;
        type = _type;
    }

}
