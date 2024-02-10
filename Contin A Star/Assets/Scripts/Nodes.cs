using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodes : MonoBehaviour
{
    public Tile currentTile;
    private int floorX;
    private int floorY;
    // Start is called before the first frame update
    void Start()
    {
        currentTile = TileManager.instance.getTile(transform.position);
    }

    private void Update()
    {
        currentTile = TileManager.instance.getTile(transform.position);

        Debug.Log(currentTile.currentPos);
    }
}
