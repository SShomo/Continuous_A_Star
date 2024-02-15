using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;

public class TextFields : MonoBehaviour
{
    public ShowTiles show;
    private static GameObject sourceGameObject; 
    private static GameObject desGameObject;

    public static Node source = null;
    public static Node des = null;

    private void Start()
    {
        sourceGameObject = new GameObject("source");
        desGameObject = new GameObject("des");

        sourceGameObject.transform.parent = transform;
        source = sourceGameObject.AddComponent<Node>();
        source.currentTile = TileManager.instance.GetTile(0, 0);
        sourceGameObject.transform.position = new Vector2(0,0);

        
        desGameObject.transform.parent = transform;
        des = desGameObject.AddComponent<Node>();
        des.currentTile = TileManager.instance.GetTile(5, 5);
        desGameObject.transform.position = new Vector2(5, 5);
    }
    private void Update()
    {
        TileManager.instance.SetGoals(source.currentTile, des.currentTile);
    }

    public static void ChangeSource(Tile tile)
    {
        sourceGameObject.transform.position = tile.currentPos;
        source.currentTile = tile;
    }
    public static void ChangeDes(Tile tile)
    {
        desGameObject.transform.position = tile.currentPos;
        des.currentTile = tile;
    }
}
