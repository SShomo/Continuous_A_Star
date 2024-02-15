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
        GameObject lo = new GameObject();
        lo.name = "gay";
        lo.transform.parent = transform;
        lo.transform.position -= Vector3.forward * 10f;
        lineRenderer = lo.AddComponent<LineRenderer>();
    }
    public void RunAStar() {
        List<Tile> path = aStar.generatePath();

        lineRenderer.positionCount = path.Count;
        lineRenderer.widthMultiplier = 0.2f;

        Debug.Log(path.Count);

        for (int i = 0; i < path.Count; i++)
        {
            GameObject NodeObject = new GameObject("node" + i);
            NodeObject.transform.parent = transform;
            NodeObject.transform.position = new Vector3(path[i].currentPos.x, path[i].currentPos.y, (-20f));
            NodeObject.transform.localScale *= 0.3f;
            SpriteRenderer sr = NodeObject.AddComponent<SpriteRenderer>();
            sr.sprite = nodeSprite;
            sr.color = Color.blue;


            lineRenderer.SetPosition(i, NodeObject.transform.position);
            lineRenderer.startColor = Color.blue;
            lineRenderer.endColor = Color.blue;
        }
    }
}
