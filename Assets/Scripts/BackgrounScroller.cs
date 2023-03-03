using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgrounScroller : MonoBehaviour
{
    [SerializeField] float bgSpeed = 4f;
    [SerializeField] double offset = - 84.7;
    [SerializeField] double nextPos = 85;

    void Update()
    {
        transform.position += new Vector3(-bgSpeed * Time.deltaTime, 0, 0);

        if(transform.position.x < offset)
        {
            transform.position = new Vector3((float)nextPos, 0, 0);
        }
    }
}
