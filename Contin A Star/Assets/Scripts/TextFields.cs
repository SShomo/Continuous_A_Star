using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;

public class TextFields : MonoBehaviour
{
    [SerializeField] private Text xSource;
    [SerializeField] private Text ySource;
    [SerializeField] private Text xDes;
    [SerializeField] private Text yDes;

    Node source = new Node();
    Node des = new Node();

    private void Update()
    {
       // source.currentTile.currentPos.x = (float)xSource.text;

    }
}
