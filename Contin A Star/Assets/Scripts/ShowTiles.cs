using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTiles : MonoBehaviour
{
    public Dictionary<(float, float), SpriteRenderer> tileRenderers;
    [SerializeField] private Sprite tileSprite;
    [SerializeField] private Vector2Int mapStartSize;
    [SerializeField] private Vector2Int mapStartOffset;

    public void ShowAllTiles()
    {
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
        for(int y = mapStartOffset.y;  y < mapStartSize.y; y++)
        {
            for(int x = mapStartOffset.x; x < mapStartSize.x; x++)
            {
                Tile test = TileManager.instance.GetTile(x, y);
                if (x == 1 && y == 1)
                    test.SetWeight(1);
            }
        }
    }
}
