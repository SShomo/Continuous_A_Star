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
        currentTile = new Tile();
    }

    private void Update()
    {
        floorX = Mathf.FloorToInt(transform.position.x);
        floorY = Mathf.FloorToInt(transform.position.y);
        if (transform.position.x - floorX <= 0.5f)
        {
            currentTile.currentPos.x = floorX;
        }
        else
        {
            currentTile.currentPos.x = floorX + 1;
        }
        if (transform.position.y - floorY <= 0.5f)
        {
            currentTile.currentPos.y = floorY;
        }
        else
        {
            currentTile.currentPos.y = floorY + 1;
        }

        Debug.Log(currentTile.currentPos);
    }
}
