using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInteraction : MonoBehaviour
{
    [SerializeField] private ShowTiles show;
    [SerializeField] private float moveSpeed;
    private float origSpeed = 0.01f;
    private float runSpeed = 0.05f;
    public float mapGrowUp;
    public float mapGrowRight;
    private Vector3 startPos;

    private void Start()
    {
        origSpeed = moveSpeed;
        runSpeed = origSpeed * 3;
        startPos = transform.position;
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            moveSpeed = runSpeed;
        }
        else
        {
            moveSpeed = origSpeed;
        }

        Vector3 pos = Camera.main.transform.position;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y + moveSpeed, Camera.main.transform.position.z);

            mapGrowUp += Vector3.Distance(Camera.main.transform.position, pos);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x + moveSpeed, Camera.main.transform.position.y, Camera.main.transform.position.z);

            mapGrowRight += Vector3.Distance(Camera.main.transform.position, pos);
        }
        if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > startPos.x)
        {
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x - moveSpeed, Camera.main.transform.position.y, Camera.main.transform.position.z);

            mapGrowRight -= Vector3.Distance(Camera.main.transform.position, pos);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y - moveSpeed, Camera.main.transform.position.z);

            mapGrowUp -= Vector3.Distance(Camera.main.transform.position, pos);
        }

        if (Mathf.Abs(mapGrowUp) >= 1)
        {
            show.ShowTileY();
            mapGrowUp = 0;
        }
        if (Mathf.Abs(mapGrowRight) >= 1)
        {
            show.ShowTileX();
            mapGrowRight = 0;
        }
    }
}