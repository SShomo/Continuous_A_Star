using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    public Vector2 currentPos;
    private int weight;

    public Tile(Vector2 currentPos)
    {
        this.currentPos = currentPos;
        this.weight = 0;
    }
    public Tile(Vector2 currentPos, int weight)
    {
        this.currentPos = currentPos;
        this.weight = weight;
    }

    public bool IsOnTile(Vector2 inPos)
    {
        Vector2 offset = inPos - currentPos;
        if(offset.magnitude <= (1/Mathf.Sqrt(2)))
        {
            return true;
        }
        return false;
    }

    public int GetWeight() { return weight; }
    public void SetWeight(int v) { weight = v; }
}
