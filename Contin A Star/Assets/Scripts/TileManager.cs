using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public static TileManager instance;
    private Dictionary<(float, float), Tile> map;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(this.gameObject);
        if(map == null)
            map = new Dictionary<(float, float), Tile>();
    }

    public Tile getTile(Vector2 vec)
    {
        Vector2 tilePos = VecToTilePos(vec);

        if (!map.ContainsKey((tilePos.x, tilePos.y)))
        {
            map.Add((tilePos.x, tilePos.y), CreateTile(tilePos));
        }

        return map[(tilePos.x, tilePos.y)];
    }

    public Tile CreateTile(Vector2 tilePos)
    {
        Tile temp = new Tile(tilePos);
        
        return temp;
    }
    public Tile CreateTile(Vector2 tilePos, int weight)
    {
        Tile temp = new Tile(tilePos, weight);

        return temp;
    }

    private Vector2 VecToTilePos(Vector2 vec)
    {
        //cast?
        Vector2Int floor = Vector2Int.FloorToInt(vec);
        Vector2 originOffset = vec - floor;

        if(originOffset.x > 0.5)
            floor.x++;
        if(originOffset.y > 0.5) 
            floor.y++;

        return floor;
    }
}
