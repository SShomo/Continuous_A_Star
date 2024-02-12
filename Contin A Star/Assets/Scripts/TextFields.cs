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

    Node source = new Node();
    Node des = new Node();

    private void Update()
    {
        try
        {
            source.currentTile.currentPos.x = float.Parse(xSource.text);
            source.currentTile.currentPos.y = float.Parse(ySource.text);

            des.currentTile.currentPos.x = float.Parse(xDes.text);
            des.currentTile.currentPos.y = float.Parse(yDes.text);

        }
        catch
        {
            source.currentTile.currentPos.x = 0;
            source.currentTile.currentPos.y = 0;

            des.currentTile.currentPos.x = 5;
            des.currentTile.currentPos.y = 5;
        }
    }
}
