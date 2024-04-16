using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 toPos = new Vector3(transform.position.x, transform.position.y + 2.093862f, transform.position.z);
        transform.position = Vector3.Slerp(transform.position, toPos, Time.deltaTime);
    }
}
