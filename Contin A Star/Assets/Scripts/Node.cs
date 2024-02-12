using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Tile currentTile;
    public int costSoFar = 0;

    void Start()
    {
        currentTile = TileManager.instance.GetTile(transform.position);
    }

    private void Update()
    {
        currentTile = TileManager.instance.GetTile(transform.position);
    }

    public void InitNode(Tile currentTile, int costSoFar)
    {
        this.currentTile = currentTile;
        this.costSoFar = costSoFar;
    }
}
