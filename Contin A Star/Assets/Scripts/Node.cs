using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Tile currentTile;

    void Start()
    {
        currentTile = TileManager.instance.getTile(transform.position);
    }

    private void Update()
    {
        currentTile = TileManager.instance.getTile(transform.position);
    }
}
