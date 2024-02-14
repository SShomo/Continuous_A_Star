using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ShowPath : MonoBehaviour
{
    public AStar aStar;
    public LineRenderer lineRenderer;
    [SerializeField] private Sprite nodeSprite;

    public bool ran = false;

    private void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
    }

    private void Update()
    {
        if (aStar != null && !ran)
        {
            List<Node> path = aStar.generatePath();

            lineRenderer.positionCount = path.Count;
            lineRenderer.widthMultiplier = 0.2f;

            Debug.Log(path.Count);

            for(int i = 0; i < path.Count; i++)
            {
                GameObject NodeObject = new GameObject("node" + i);
                NodeObject.transform.parent = transform;
                NodeObject.transform.position = path[i].currentTile.currentPos;
                NodeObject.AddComponent<SpriteRenderer>().sprite = nodeSprite;

                lineRenderer.SetPosition(i, NodeObject.transform.position);
            }

            ran = true;
        }
    }
}
