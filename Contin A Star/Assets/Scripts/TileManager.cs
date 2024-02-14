using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileManager : MonoBehaviour
{
    public static TileManager instance;
    [SerializeField] private ShowTiles showTiles;
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

        if(showTiles == null)
        {
            showTiles = GetComponent<ShowTiles>();
        }
        showTiles.ShowAllTiles();

    }

    public Tile GetTile(Vector2 vec)
    {
        Vector2 tilePos = VecToTilePos(vec);

        if (!map.ContainsKey((tilePos.x, tilePos.y)))
        {
            map.Add((tilePos.x, tilePos.y), CreateTile(tilePos));
            showTiles.ShowTile(map[(tilePos.x, tilePos.y)]);
        }

        return map[(tilePos.x, tilePos.y)];
    }
    public Tile GetTile(float x, float y)
    {
        return GetTile(new Vector2(x,y));
    }

    public void ToggleWall(Vector2 vec)
    {
        Tile tile = GetTile(vec);

        if(tile.GetWeight() == 0)
        {
            tile.SetWeight(1);
        }
        else
        {
            tile.SetWeight(0);
        }
    }

    private Tile CreateTile(Vector2 tilePos)
    {
        Tile temp = new Tile(tilePos);
        
        return temp;
    }
    private Tile CreateTile(Vector2 tilePos, int weight)
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

    public Dictionary<(float, float), Tile> getMap() { return map; }
}
