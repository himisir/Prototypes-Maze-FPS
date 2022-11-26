using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LineRendererTest : MonoBehaviour
{
    [SerializeField] Line line;
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            if (Input.GetKey(KeyCode.Mouse1))
            {
                var pos = Input.mousePosition;
                pos = Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y, Camera.main.nearClipPlane));
                // if (line == null)
                //     return;
                line.SetUp(this.transform.position, pos); //Comment out this line to just check throwing mechanism since it is causing issues. 
            }
        }
    }
}