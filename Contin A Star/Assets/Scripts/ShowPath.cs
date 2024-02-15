using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.U2D;

public class ShowPath : MonoBehaviour
{
    public AStar aStar;
    public LineRenderer lineRenderer;
    public Spline spline;
    [SerializeField] private Sprite nodeSprite;
    private List<GameObject> nodes = new List<GameObject>();

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

        ResetPath();

        lineRenderer.positionCount = path.Count;
        lineRenderer.widthMultiplier = 0.2f;

        for (int i = 0; i < path.Count; i++)
        {
            GameObject NodeObject = new GameObject("node" + i);
            NodeObject.transform.parent = transform;
            NodeObject.transform.position = new Vector3(path[i].currentPos.x, path[i].currentPos.y, (-20f));
            NodeObject.transform.localScale *= 0.3f;
            SpriteRenderer sr = NodeObject.AddComponent<SpriteRenderer>();
            sr.sprite = nodeSprite;
            sr.color = Color.blue;
            nodes.Add(NodeObject);

            //spline.InsertPointAt(i, NodeObject.transform.position);

            lineRenderer.SetPosition(i, NodeObject.transform.position);
            lineRenderer.startColor = Color.blue;
            lineRenderer.endColor = Color.blue;
        }
    }

    public void ResetPath()
    {
        foreach (GameObject n in nodes)
        {
            Destroy(n);
        }
        nodes.Clear();
        lineRenderer.positionCount = 0;
    }
}
