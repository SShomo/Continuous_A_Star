using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeWall : MonoBehaviour
{
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 vec;
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            vec = Camera.main.ScreenToWorldPoint(mousePos);
            TileManager.instance.ToggleWall(vec);

            Debug.Log(TileManager.instance.GetTile(vec).GetWeight());
        }
    }
}
