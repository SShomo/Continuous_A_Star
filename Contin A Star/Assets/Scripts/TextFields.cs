using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;

public class TextFields : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI xSource;
    [SerializeField] private TextMeshProUGUI ySource;
    [SerializeField] private TextMeshProUGUI xDes;
    [SerializeField] private TextMeshProUGUI yDes;


    public Node source = null;
    public Node des = null;

    private void Start()
    {
        GameObject sourceGameObject = new GameObject("source");
        sourceGameObject.transform.parent = transform;
        source = sourceGameObject.AddComponent<Node>();
        source.currentTile = TileManager.instance.GetTile(0, 0);
        sourceGameObject.transform.position = new Vector2(0,0);

        GameObject desGameObject = new GameObject("des");
        desGameObject.transform.parent = transform;
        des = desGameObject.AddComponent<Node>();
        des.currentTile = TileManager.instance.GetTile(5, 5);
        desGameObject.transform.position = new Vector2(5, 5);
    }
    private void Update()
    {
        try
        {
            source.currentTile = TileManager.instance.GetTile(float.Parse(xSource.text), float.Parse(ySource.text));

            des.currentTile = TileManager.instance.GetTile(float.Parse(xDes.text), float.Parse(yDes.text));

        }
        catch
        {
            source.currentTile = TileManager.instance.GetTile(0, 0);
            des.currentTile = TileManager.instance.GetTile(5, 5);
        }

        TileManager.instance.SetGoals(source.currentTile, des.currentTile);
    }
}
