using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTiles : MonoBehaviour
{
    public Sprite tileSprite;
    [SerializeField] private Vector2Int mapStartSize;
    [SerializeField] private Vector2Int mapStartOffset;
    public void ShowAllTiles()
    {
        ShowTileInit();
        Dictionary<(float,float), Tile> map = TileManager.instance.getMap();

        foreach(KeyValuePair<(float, float), Tile> tile in map)
        {
            ShowTile(tile.Value);
        }
    }

    public void ShowTile(Tile tile)
    {
        GameObject tileObj = new GameObject();
        tileObj.transform.parent = transform;
        tileObj.name = "Tile (" + tile.currentPos.x + ", " + tile.currentPos.y + ")";
        tileObj.transform.localScale = Vector3.one * 0.95f;
        tileObj.transform.position = tile.currentPos;
        SpriteRenderer tileRenderer = tileObj.AddComponent<SpriteRenderer>();
        tileRenderer.sprite = tileSprite;
    }

    private void ShowTileInit()
    {
        for(int y = mapStartOffset.y;  y < mapStartSize.y; y++)
        {
            for(int x = mapStartOffset.x; x < mapStartSize.x; x++)
            {
                Tile test = TileManager.instance.GetTile(x, y);
            }
        }
    }
}
