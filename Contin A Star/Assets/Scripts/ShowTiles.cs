using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTiles : MonoBehaviour
{
    public Dictionary<(float, float), SpriteRenderer> tileRenderers;
    [SerializeField] private Sprite tileSprite;
    [SerializeField] private Vector2Int mapSize;
    [SerializeField] private Vector2Int mapStartOffset;

    public void ShowAllTiles()
    {
        mapStartOffset = -mapSize/ 2;
        if(tileRenderers == null)
            tileRenderers = new Dictionary<(float, float), SpriteRenderer>();
        ShowTileInit();
        Dictionary<(float,float), Tile> map = TileManager.instance.getMap();

        foreach(KeyValuePair<(float, float), Tile> tile in map)
        {
            ShowTile(tile.Value);
        }
    }

    public void ChangeColor(Tile tile, Color color)
    {
        tileRenderers[(tile.currentPos.x, tile.currentPos.y)].color = color;

        Debug.Log(tile.currentPos + "\t " + tileRenderers[(tile.currentPos.x, tile.currentPos.y)].color);
    }

    public void ShowTile(Tile tile)
    {
        GameObject tileObj = new GameObject();
        tileObj.transform.parent = transform;
        tileObj.name = "Tile (" + tile.currentPos.x + ", " + tile.currentPos.y + ")";
        tileObj.transform.localScale = Vector3.one * 0.925f;
        tileObj.transform.position = tile.currentPos;
        SpriteRenderer tileRenderer = tileObj.AddComponent<SpriteRenderer>();
        tileRenderers.Add((tile.currentPos.x, tile.currentPos.y), tileRenderer);
        tileRenderer.sprite = tileSprite;

        if(tile.GetWeight() > 0)
        {
            tileRenderer.color = Color.black;
        }
    }

    private void ShowTileInit()
    {
        for(int y = mapStartOffset.y;  y < mapSize.y + mapStartOffset.y; y++)
        {
            for(int x = mapStartOffset.x; x < mapSize.x + mapStartOffset.x; x++)
            {
                Tile test = TileManager.instance.GetTile(x, y);
            }
        }
    }
    
    public void ShowTileY()
    {
        mapSize.y += 2;
        for(int x = -mapSize.x/2;  x < mapSize.x - mapSize.x / 2; x++)
        {
            Tile test = TileManager.instance.GetTile(x, -((mapSize.y/2)));
            ShowTile(test);
        }
        for (int x = -mapSize.x / 2; x < mapSize.x - mapSize.x / 2; x++)
        {
            Tile test = TileManager.instance.GetTile(x, (mapSize.y / 2)-1);
            ShowTile(test);
        }
    }
    public void ShowTileX()
    {
        mapSize.x += 2;
        for (int y = -mapSize.y / 2; y < mapSize.y - mapSize.y/2; y++)
        {
            Tile test = TileManager.instance.GetTile(-((mapSize.x / 2)), y);
            ShowTile(test);
        }
        for (int y = -mapSize.y / 2; y < mapSize.y - mapSize.y/2; y++)
        {
            Tile test = TileManager.instance.GetTile((mapSize.x / 2)- 1, y);
            ShowTile(test);
        }
    }
}
